using AutoMapper;
using ModelValidate.DataAccess.CustomModels;
using ModelValidate.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelValidate.DataAccess.Profiler
{
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Mapping Automapper
        /// </summary>
        public MappingProfile()
        {
            CreateMap<ValidatedMessage, ValidatedMessageDto>();
            CreateMap<ValidatedMessageDto, ValidatedMessage>()
                .ForAllMembers(d => d.AllowNull());
        }
    }
}
