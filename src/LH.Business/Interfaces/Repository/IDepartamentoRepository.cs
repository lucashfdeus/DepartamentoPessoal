using LH.Business.Models;
using System;
using System.Threading.Tasks;

namespace LH.Business.Interfaces.Repository
{
    public interface IDepartamentoRepository : IRepository<Departamento>
    {
        Task<Departamento> ObterDepartamento(Guid id);
    }
}
