using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWebApi.Application.DTOs;
using AutoMapper;
using TestWebApi.Domain.Entities;
namespace TestWebApi.Application.Mapping
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            // Map CreateCustomerDto → Customer entity
            CreateMap<CreateCustomerDto, Customer>()
                .ConstructUsing(dto => new Customer(
                    dto.Name, dto.Mob_No, dto.Flat_No, dto.Building_No,
                    dto.Road_No, dto.Block_No, dto.Area
                ));

            // Optional: Map Customer → CustomerResponseDto
            // CreateMap<Customer, CreateCustomerDto>();
        }
    }
}
