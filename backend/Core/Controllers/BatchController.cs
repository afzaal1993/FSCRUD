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
using MongoDB.Driver;

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
            if (batch.Id != null)
                return Created("", ApiResponse<string>.Success());
            else
                return BadRequest(ApiResponse<string>.Error());
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
    }
}