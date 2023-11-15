using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Common;
using core.Data;
using core.DTOs;
using Core.Data;
using Core.DTOs;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Entities;

namespace Core.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BatchController : ControllerBase
    {
        private readonly CoreDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly MongoDBContext _mongoDBContext;

        public BatchController(IMapper mapper, MongoDBContext mongoDBContext)
        {
            _mapper = mapper;
            _mongoDBContext = mongoDBContext;
        }


        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(typeof(ApiResponse<string>), 201)]
        public async Task<IActionResult> Create(BatchDto model)
        {
            var batch = _mapper.Map<Batch>(model);

            await _mongoDBContext.Batches.InsertOneAsync(batch);

            return Created("", ApiResponse<string>.Success());
        }

        [HttpPut]
        [Route("Update")]
        [ProducesResponseType(typeof(ApiResponse<string>), 200)]
        public async Task<IActionResult> Update(string id, BatchDto model)
        {
            var result = await _mongoDBContext.Batches.Find(b => b.Id == ObjectId.Parse(id)).FirstOrDefaultAsync();

            if (result == null)
            {
                return NotFound(ApiResponse<string>.NotFound());
            }

            var batch = _mapper.Map<Batch>(model);
            batch.Id = ObjectId.Parse(id);

            await _mongoDBContext.Batches.ReplaceOneAsync(p => p.Id == ObjectId.Parse(id), batch);

            return Ok(ApiResponse<GetBatchDto>.Success());
        }

        [HttpGet]
        [Route("GetAll")]
        [ProducesResponseType(typeof(ApiResponse<List<GetBatchDto>>), 200)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mongoDBContext.Batches.Find(_ => true).ToListAsync();

            if (result == null)
                return BadRequest(ApiResponse<string>.Error("Error Occurred"));

            if (result.Count() == 0)
                return NotFound(ApiResponse<string>.NotFound());

            var dto = _mapper.Map<List<GetBatchDto>>(result);

            return Ok(ApiResponse<List<GetBatchDto>>.Success(dto));
        }

        [HttpGet]
        [Route("GetById")]
        [ProducesResponseType(typeof(ApiResponse<GetBatchDto>), 200)]
        public async Task<IActionResult> GetAll(string id)
        {
            var result = await _mongoDBContext.Batches.Find(b => b.Id == ObjectId.Parse(id)).FirstOrDefaultAsync();

            if (result == null)
                return NotFound(ApiResponse<string>.NotFound());

            var dto = _mapper.Map<GetBatchDto>(result);

            return Ok(ApiResponse<GetBatchDto>.Success(dto));
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _mongoDBContext.Batches.Find(b => b.Id == ObjectId.Parse(id)).FirstOrDefaultAsync();

            if (result == null)
                return NotFound(ApiResponse<string>.NotFound());

            await _mongoDBContext.Batches.DeleteOneAsync(b => b.Id == ObjectId.Parse(id));

            return Ok(ApiResponse<GetBatchDto>.Success());
        }
    }
}