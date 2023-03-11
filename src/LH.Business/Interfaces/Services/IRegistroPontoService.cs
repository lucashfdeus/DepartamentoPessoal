using LH.Business.Models;
using System;
using System.Threading.Tasks;

namespace LH.Business.Interfaces.Services
{
    public interface IRegistroPontoService : IDisposable
    {
        Task Adicionar(RegistroPonto registroPonto);
        Task Atualizar(RegistroPonto registroPonto);
        Task Remover(Guid id);
    }
}
