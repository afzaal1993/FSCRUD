using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Common;
using core.Data;
using Core.Data;
using Core.DTOs;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Core.Controllers
{
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

            //await _dbContext.Courses.AddAsync(course);

            //var result = await _dbContext.SaveChangesAsync();

            //if (result > 0)
            //    return Created("", ApiResponse<string>.Success());
            //else
            //    return BadRequest(ApiResponse<string>.Error());

            await _mongoDBContext.Courses.InsertOneAsync(course);
            if (course.Id != null)
                return Created("", ApiResponse<string>.Success());
            else
                return BadRequest(ApiResponse<string>.Error());
        }

        //[HttpPut]
        //[Route("Update")]
        //[ProducesResponseType(typeof(ApiResponse<string>), 200)]
        //public async Task<IActionResult> Update(int id, CourseDto model)
        //{
        //    var existingCourse = await _dbContext.Courses.FindAsync(id);

        //    if (existingCourse == null)
        //        return NotFound(ApiResponse<string>.NotFound());

        //    existingCourse.BatchId = model.BatchId;
        //    existingCourse.CourseFee = model.CourseFee;
        //    existingCourse.CourseName = model.CourseName;
        //    existingCourse.IsActive = model.IsActive;

        //    var result = await _dbContext.SaveChangesAsync();

        //    if (result > 0)
        //        return Ok(ApiResponse<string>.Success());
        //    else
        //        return BadRequest(ApiResponse<string>.Error());
        //}

        [HttpGet]
        [Route("GetAll")]
        [ProducesResponseType(typeof(ApiResponse<List<GetCourseDto>>), 200)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _dbContext.Courses.Include(x => x.Batch).ToListAsync();

            if (result == null)
                return BadRequest(ApiResponse<string>.Error("Error Occurred"));

            if (result.Count() == 0)
                return NotFound(ApiResponse<string>.NotFound());

            var dto = _mapper.Map<List<GetCourseDto>>(result);

            return Ok(ApiResponse<List<GetCourseDto>>.Success(dto));
        }

        //[HttpGet]
        //[Route("GetById")]
        //[ProducesResponseType(typeof(ApiResponse<GetCourseDto>), 200)]
        //public async Task<IActionResult> GetById(int id)
        //{
        //    var result = await _dbContext.Courses.Include(x => x.Batch).FirstOrDefaultAsync(y => y.Id == id);

        //    if (result == null)
        //        return NotFound(ApiResponse<string>.NotFound());

        //    var dto = _mapper.Map<GetCourseDto>(result);

        //    return Ok(ApiResponse<GetCourseDto>.Success(dto));
        //}
    }
}