using LH.Business.Models;
using System.Threading.Tasks;
using System;

namespace LH.Business.Interfaces.Repository
{
    public interface IRegistroPontoRepository : IRepository<RegistroPonto>
    {
        Task<RegistroPonto> ObterRegistroPonto(Guid id);
    }
}
