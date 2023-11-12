using AutoMapper;
using Common;
using core.DTOs;
using Core.Data;
using Core.DTOs;
using Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly CoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public StudentController(CoreDbContext coreDbContext, IMapper mapper)
        {
            _mapper = mapper;
            _dbContext = coreDbContext;
        }


        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(typeof(ApiResponse<string>), 201)]
        public async Task<IActionResult> Create(StudentDto model)
        {
            var student = _mapper.Map<Student>(model);

            await _dbContext.Students.AddAsync(student);

            var result = await _dbContext.SaveChangesAsync();

            if (result > 0)
                return Created("", ApiResponse<string>.Success());
            else
                return BadRequest(ApiResponse<string>.Error());
        }

        [HttpPut]
        [Route("Update")]
        [ProducesResponseType(typeof(ApiResponse<string>), 200)]
        public async Task<IActionResult> Update(int id, StudentDto model)
        {
            var existingStudent = await _dbContext.Students.FindAsync(id);

            if (existingStudent == null)
                return NotFound(ApiResponse<string>.NotFound());

            existingStudent.FirstName = model.FirstName;
            existingStudent.MiddleName = model.MiddleName;
            existingStudent.LastName = model.LastName;
            existingStudent.DateOfBirth = model.DateOfBirth;
            existingStudent.Address = model.Address;
            existingStudent.MobileNo = model.MobileNo;
            existingStudent.PhotoUrl = model.PhotoUrl;

            var result = await _dbContext.SaveChangesAsync();

            if (result > 0)
                return Ok(ApiResponse<string>.Success());
            else
                return BadRequest(ApiResponse<string>.Error());
        }

        [HttpGet]
        [Route("GetAll")]
        [ProducesResponseType(typeof(ApiResponse<List<Student>>), 200)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _dbContext.Students.ToListAsync();

            if (result == null)
                return BadRequest(ApiResponse<string>.Error("Error Occurred"));

            if (result.Count() == 0)
                return NotFound(ApiResponse<string>.NotFound());

            return Ok(ApiResponse<List<Student>>.Success(result));
        }

        [HttpGet]
        [Route("GetById")]
        [ProducesResponseType(typeof(ApiResponse<Student>), 200)]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _dbContext.Students.FirstOrDefaultAsync(y => y.Id == id);

            if (result == null)
                return NotFound(ApiResponse<string>.NotFound());

            return Ok(ApiResponse<Student>.Success(result));
        }

        [HttpPost]
        [Route("AddCourseForStudent")]
        [ProducesResponseType(typeof(ApiResponse<string>), 200)]
        public async Task<IActionResult> AddCourseForStudent(AddCourseForStudentDto model)
        {
            var studentCourse = _mapper.Map<StudentCourse>(model);

            await _dbContext.StudentCourses.AddAsync(studentCourse);

            var result = await _dbContext.SaveChangesAsync();

            if (result > 0)
                return Created("", ApiResponse<string>.Success());
            else
                return BadRequest(ApiResponse<string>.Error());
        }

        [HttpGet]
        [Route("GetAllStudentsCourses")]
        [ProducesResponseType(typeof(ApiResponse<List<GetStudentCourseDto>>), 200)]
        public async Task<IActionResult> GetAllStudentsCourses()
        {
            var result = await _dbContext.StudentCourses.Include(s => s.Student).Include(c => c.Course).ToListAsync();

            if (result == null)
                return BadRequest(ApiResponse<string>.Error("Error Occurred"));

            if (result.Count() == 0)
                return NotFound(ApiResponse<string>.NotFound());

            var dto = _mapper.Map<List<GetStudentCourseDto>>(result);

            return Ok(ApiResponse<List<GetStudentCourseDto>>.Success(dto));
        }

        [HttpGet]
        [Route("GetStudentCourseByStudentId")]
        [ProducesResponseType(typeof(ApiResponse<List<GetStudentCourseDto>>), 200)]
        public async Task<IActionResult> GetStudentCourseByStudentId(int studentId)
        {
            var result = await _dbContext.StudentCourses.Include(s => s.Student).Include(c => c.Course)
                                                    .FirstOrDefaultAsync(x => x.StudentId == studentId);

            if (result == null)
                return NotFound(ApiResponse<string>.NotFound());

            var dto = _mapper.Map<GetStudentCourseDto>(result);

            return Ok(ApiResponse<GetStudentCourseDto>.Success(dto));
        }

    }
}
