using AutoMapper;
using Common;
using core.Data;
using core.DTOs;
using Core.Data;
using Core.DTOs;
using Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly CoreDbContext _dbContext;
        private readonly MongoDBContext _mongoDBContext;
        private readonly IMapper _mapper;
        public StudentController(MongoDBContext mongoDBContext, IMapper mapper)
        {
            _mongoDBContext = mongoDBContext;
            _mapper = mapper;
        }


        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(typeof(ApiResponse<string>), 201)]
        public async Task<IActionResult> Create([FromForm] StudentDto model)
        {
            string fileName = Guid.NewGuid().ToString();

            var student = _mapper.Map<Student>(model);

            student.CreatedDate = Helper.GetDateAndTime();
            student.FileName = fileName;

            await _mongoDBContext.Students.InsertOneAsync(student);

            return Created("", ApiResponse<string>.Success());
        }

        [HttpPut]
        [Route("Update")]
        [ProducesResponseType(typeof(ApiResponse<string>), 200)]
        public async Task<IActionResult> Update(string id, [FromForm] UpdateStudentDto model)
        {
            var documentId = new ObjectId(id);

            var filter = Builders<Student>.Filter.Eq("_id", documentId);

            var update = Builders<Student>.Update
                            .Set(x => x.ModifiedBy, model.ModifiedBy)
                            .Set(x => x.ModifiedDate, Helper.GetDateAndTime())
                            .Set(x => x.FirstName, model.FirstName)
                            .Set(x => x.MiddleName, model.MiddleName)
                            .Set(x => x.LastName, model.LastName)
                            .Set(x => x.Address, model.Address)
                            .Set(x => x.MobileNo, model.MobileNo)
                            .Set(x => x.IsActive, model.IsActive)
                            .Set(x => x.DateOfBirth, model.DateOfBirth);


            var updateResult = await _mongoDBContext.Students.UpdateOneAsync(filter, update);

            if (updateResult.ModifiedCount > 0)
                return Ok(ApiResponse<string>.Success());
            else
                return BadRequest(ApiResponse<string>.Error());
        }

        [HttpGet]
        [Route("GetAll")]
        [ProducesResponseType(typeof(ApiResponse<List<GetStudentDto>>), 200)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mongoDBContext.Students.Find(_ => true).ToListAsync();

            if (result == null)
                return BadRequest(ApiResponse<string>.Error("Error Occurred"));

            if (result.Count() == 0)
                return NotFound(ApiResponse<string>.NotFound());

            var dto = _mapper.Map<List<GetStudentDto>>(result);

            return Ok(ApiResponse<List<GetStudentDto>>.Success(dto));
        }

        [HttpGet]
        [Route("GetById")]
        [ProducesResponseType(typeof(ApiResponse<GetStudentDto>), 200)]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _mongoDBContext.Students.Find(s => s.Id == ObjectId.Parse(id)).FirstOrDefaultAsync();

            if (result == null)
                return NotFound(ApiResponse<string>.NotFound());

            var dto = _mapper.Map<GetStudentDto>(result);

            return Ok(ApiResponse<GetStudentDto>.Success(dto));
        }

        [HttpPost]
        [Route("AddCourseForStudent")]
        [ProducesResponseType(typeof(ApiResponse<string>), 200)]
        public async Task<IActionResult> AddCourseForStudent(AddCourseForStudentDto model)
        {
            var studentCourse = _mapper.Map<StudentCourse>(model);

            await _mongoDBContext.StudentCourses.InsertOneAsync(studentCourse);

            return Created("", ApiResponse<string>.Success());
        }

        [HttpGet]
        [Route("GetStudentCourseByStudentId")]
        [ProducesResponseType(typeof(ApiResponse<List<GetStudentCourseDto>>), 200)]
        public async Task<IActionResult> GetStudentCourseByStudentId(string studentId)
        {
            var result = await _mongoDBContext.StudentCourses.Aggregate()
                                .Match(s => s.StudentId == ObjectId.Parse(studentId))
                                .Lookup("Students", "StudentId", "_id", "Student")
                                .Unwind("Student")
                                .Lookup("Courses", "CourseId", "_id", "Course")
                                .Unwind("Course")
                                .FirstOrDefaultAsync();

            if (result == null)
                return NotFound(ApiResponse<string>.NotFound());

            StudentCourse studentCourse = BsonSerializer.Deserialize<StudentCourse>(result);

            var dto = _mapper.Map<GetStudentCourseDto>(studentCourse);

            return Ok(ApiResponse<GetStudentCourseDto>.Success(dto));
        }

    }
}
