using LH.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LH.Business.Interfaces.Services
{
    public interface IFuncionarioService : IDisposable
    {
        Task Adicionar(List<Funcionario> funcionario);
        Task Atualizar(Funcionario funcionario);
        Task Remover(Guid id);
    }
}
