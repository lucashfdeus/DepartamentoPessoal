using LH.Business.Models;
using System;
using System.Threading.Tasks;

namespace LH.Business.Interfaces.Services
{
    public interface IDepartamentoService : IDisposable
    {
        Task<Departamento> Adicionar(Departamento departamento);
        Task Atualizar(Departamento departamento);
        Task Remover(Guid id);
    }
}
