using AutoMapper;
using Lab4_CodeFirst.Models;
using Lab4_CodeFirst.ViewModels;

namespace Lab4_CodeFirst.AutoMapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        //  Classes ==> ClassViewModel
        CreateMap<Classes, ClassViewModel>()
            .ForMember(dest => dest.CourseName, opt => opt.MapFrom(src => src.Course.CourseName))
            .ForMember(dest => dest.TeacherName, opt => opt.MapFrom(src => src.Teacher.FirstName + " " + src.Teacher.LastName))
            .ForMember(dest => dest.Students, opt => opt.MapFrom(src => src.Students.Select(s => new StudentNameViewModel
            {
                Name = s.FirstName + " " + s.LastName
            }).ToList()));
        
        //  Students ==> StudentViewModel
        CreateMap<Students, StudentViewModel>()
            .ForMember(dest => dest.Classes, opt => opt.MapFrom(src => 
                src.Classes.Select(c => new ClassNameViewModel
                {
                    CourseName = c.Course != null ? c.Course.CourseName : string.Empty,
                    TeacherName = c.Teacher != null ? c.Teacher.FirstName + " " + c.Teacher.LastName : string.Empty
                }).ToList()));
        
        // Mapping Course to CourseViewModel
        CreateMap<Courses, CourseViewModel>();

        // Mapping Teacher to TeacherViewModel
        CreateMap<Teachers, TeacherViewModel>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FirstName + " " + src.LastName));

        // Mapping StudentViewModel to Students
        CreateMap<StudentViewModel, Students>()
            .ForMember(dest => dest.Classes, opt => opt.Ignore());
        
       
    }
}