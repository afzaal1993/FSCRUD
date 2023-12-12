using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.Runtime.Internal;
using AutoMapper;
using Common;
using core.Data;
using Core.Data;
using Core.DTOs;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace Core.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly CoreDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly MongoDBContext _mongoDBContext;

        public CourseController(CoreDbContext coreDbContext, IMapper mapper, MongoDBContext mongoDBContext)
        {
            _mapper = mapper;
            _mongoDBContext = mongoDBContext;
            _dbContext = coreDbContext;
        }


        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(typeof(ApiResponse<string>), 201)]
        public async Task<IActionResult> Create(CourseDto model)
        {
            var course = _mapper.Map<Course>(model);

            course.CreatedDate = DateTime.Now.ToString();

            await _mongoDBContext.Courses.InsertOneAsync(course);

            return Created("", ApiResponse<string>.Success());
        }

        [HttpPut]
        [Route("Update")]
        [ProducesResponseType(typeof(ApiResponse<string>), 200)]
        public async Task<IActionResult> Update(string id, UpdateCourseDto model)
        {
            var documentId = new ObjectId(id);

            var filter = Builders<Course>.Filter.Eq("_id", documentId);

            var update = Builders<Course>.Update
                            .Set(x => x.ModifiedBy, model.ModifiedBy)
                            .Set(x => x.ModifiedDate, DateTime.Now.ToString())
                            .Set(x => x.BatchId, ObjectId.Parse(model.BatchId))
                            .Set(x => x.IsActive, model.IsActive)
                            .Set(x => x.CourseFee, model.CourseFee)
                            .Set(x => x.CourseName, model.CourseName);

            var updateResult = await _mongoDBContext.Courses.UpdateOneAsync(filter, update);

            if (updateResult.ModifiedCount > 0)
                return Ok(ApiResponse<string>.Success());
            else
                return BadRequest(ApiResponse<string>.Error());
        }

        [HttpGet]
        [Route("GetAll")]
        [ProducesResponseType(typeof(ApiResponse<List<GetCourseDto>>), 200)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mongoDBContext.Courses.Aggregate()
                               .Lookup("Batches", "BatchId", "_id", "Batch")
                               .Unwind("Batch")
                               .ToListAsync();

            if (result == null)
                return BadRequest(ApiResponse<string>.Error("Error Occurred"));

            if (result.Count() == 0)
                return NotFound(ApiResponse<string>.NotFound());

            var list = result.Select(bsonDoc => BsonSerializer.Deserialize<Course>(bsonDoc)).ToList();

            var dto = _mapper.Map<List<GetCourseDto>>(list);

            return Ok(ApiResponse<List<GetCourseDto>>.Success(dto));
        }

        [HttpGet]
        [Route("GetById")]
        [ProducesResponseType(typeof(ApiResponse<GetCourseDto>), 200)]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _mongoDBContext.Courses.Aggregate()
                                .Match(c => c.Id == ObjectId.Parse(id))
                                .Lookup("Batches", "BatchId", "_id", "Batch")
                                .Unwind("Batch")
                                .FirstOrDefaultAsync();

            if (result == null)
                return NotFound(ApiResponse<string>.NotFound());

            Course course = BsonSerializer.Deserialize<Course>(result);

            var dto = _mapper.Map<GetCourseDto>(course);

            return Ok(ApiResponse<GetCourseDto>.Success(dto));
        }
    }
}