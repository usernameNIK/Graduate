using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;
using WebApplication123.Models;
using WebApplicationGraduateWorkFinal.Model;

namespace WebApplication123.Profiles
{ 
    public class AutomapperProfiles : Profile
    {
        public AutomapperProfiles()
        {
            CreateMap<RegisterNewAccountModel, Account>();

            CreateMap<UpdateAccountModel, Account>();
            CreateMap<Account, GetAccountModel>();
            CreateMap<TransactionRequestDto, Transaction>();
        }
    }
}
