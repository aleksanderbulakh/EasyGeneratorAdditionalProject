using AutoMapper;
using EasyGeneratorAdditionalProject.Models.Entities;
using EasyGeneratorAdditionalProject.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyGeneratorAdditionalProject.Web.Configuration
{
    public class MappingConfiguration
    {
        public static void Configure()
        {
            DateTime minDate = new DateTime(1969, 12, 31, 23, 59, 59);

            Mapper.Initialize(config =>
            {
                config.CreateMap<Course, CourseViewModel>()
                .ForMember(p => p.CreatedOn, sourse => sourse.MapFrom(p => ((p.CreatedOn - minDate).Ticks / TimeSpan.TicksPerMillisecond)))
                .ForMember(p => p.LastModifiedDate, sourse => sourse.MapFrom(p => ((p.LastModifiedDate - minDate).Ticks / TimeSpan.TicksPerMillisecond)));

                config.CreateMap<Section, SectionViewModel>()
                .ForMember(p => p.CreatedOn, sourse => sourse.MapFrom(p => ((p.CreatedOn - minDate).Ticks / TimeSpan.TicksPerMillisecond)))
                .ForMember(p => p.LastModifiedDate, sourse => sourse.MapFrom(p => ((p.LastModifiedDate - minDate).Ticks / TimeSpan.TicksPerMillisecond)));
            });
        }
    }
}