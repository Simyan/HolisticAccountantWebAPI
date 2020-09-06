using AutoMapper;
using HolisticAccountant.Models.DTO;
using HolisticAccountant.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HolisticAccountant.Models.Mappings
{
    public class AutomapperProfiles : Profile
    {
        public AutomapperProfiles()
        {
            CreateMap<Transaction, TransactionDTO>().ReverseMap();
            //CreateMap<List<Transaction>, List<TransactionDTO>>().ReverseMap();
        }
    }
}
