using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using core.DTOs;
using Core.DTOs;
using Core.Entities;

namespace Core.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CourseDto, Course>();
            CreateMap<Course, GetCourseDto>()
                        .ForMember(dest => dest.BatchName,
                          opt => opt.MapFrom(src => src.Batch.BatchName));

            CreateMap<StudentDto, Student>();
            CreateMap<UpdateStudentDto, Student>();
            CreateMap<Student, GetStudentDto>();

            CreateMap<AddCourseForStudentDto, StudentCourse>();
            CreateMap<StudentCourse, GetStudentCourseDto>()
                        .ForMember(dest => dest.StudentName,
                        opt => opt.MapFrom(src => src.Student.FirstName + " " + src.Student.MiddleName + " " + src.Student.LastName))
                        .ForMember(dest => dest.CourseName,
                        opt => opt.MapFrom(src => src.Course.CourseName));

            CreateMap<BatchDto, Batch>();
            CreateMap<Batch, GetBatchDto>();
        }
    }
}