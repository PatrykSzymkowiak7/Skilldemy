using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Skilldemy.Models;
using AutoMapper;

namespace Skilldemy.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<Course, CreateCourseViewModel>();
            Mapper.CreateMap<CreateCourseViewModel, Course>();
        }
    }
}