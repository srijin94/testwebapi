using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWebApi.Application.DTOs;
using TestWebApi.Domain.Entities;

namespace TestWebApi.Application.Interfaces
{
    public interface IRestoRepository
    {
        Task InsertResto(RestoUser restouser);
    }
}
