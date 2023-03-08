using LH.Business.Models;
using System;
using System.Threading.Tasks;

namespace LH.Business.Interfaces
{
    public interface IRegistoProntoRepository : IRepository<RegistroPonto>
    {
        Task<RegistroPonto> ObterTodosRegistroPonto();
    }
}
