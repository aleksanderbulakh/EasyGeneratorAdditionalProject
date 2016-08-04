using AutoMapper;
using EasyGeneratorAdditionalProject.Models.Entities;
using EasyGeneratorAdditionalProject.Web.Convertors;
using EasyGeneratorAdditionalProject.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EasyGeneratorAdditionalProject.Web.Configuration
{
    public class MappingConfiguration
    {
        public static void Configure()
        {
            var _convertor = DependencyResolver.Current.GetService<IDateConvertor>();

            Mapper.Initialize(config =>
            {
                config.CreateMap<User, UserViewModel>();
                config.CreateMap<SimpleSelectAnswers, AnswerViewModel>();

                config.CreateMap<Course, CourseViewModel>()
                .ForMember(p => p.CreatedOn, sourse => sourse.MapFrom(p => _convertor.ConvertDateToMilliseconds(p.CreatedOn)))
                .ForMember(p => p.LastModifiedDate, sourse => sourse.MapFrom(p => _convertor.ConvertDateToMilliseconds(p.LastModifiedDate)));

                config.CreateMap<Section, SectionViewModel>()
                .ForMember(p => p.CreatedOn, sourse => sourse.MapFrom(p => _convertor.ConvertDateToMilliseconds(p.CreatedOn)))
                .ForMember(p => p.LastModifiedDate, sourse => sourse.MapFrom(p => _convertor.ConvertDateToMilliseconds(p.LastModifiedDate)));

                config.CreateMap<Question, QuestionViewModel>()
                .ForMember(p => p.CreatedOn, sourse => sourse.MapFrom(p => _convertor.ConvertDateToMilliseconds(p.CreatedOn)))
                .ForMember(p => p.LastModifiedDate, sourse => sourse.MapFrom(p => _convertor.ConvertDateToMilliseconds(p.LastModifiedDate)));
            });
        }
    }
}