using LH.Business.Interfaces;
using LH.Business.Interfaces.Repository;
using LH.Business.Interfaces.Services;
using LH.Business.Models;
using LH.Business.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LH.Business.Services
{
    public class FuncionarioService : BaseService, IFuncionarioService
    {
        private readonly IFuncionarioRepository _funcionarioRepository;

        public FuncionarioService(IFuncionarioRepository funcionarioRepository,
                                INotificador notificador) : base(notificador)
        {
            _funcionarioRepository = funcionarioRepository;
        }

        public async Task Adicionar(List<Funcionario> funcionarios)
        {
            foreach (var funcionario in funcionarios)
            {
                await _funcionarioRepository.Adicionar(funcionario);
            }          
        }

        public async Task Atualizar(Funcionario funcionario)
        {
            await _funcionarioRepository.Atualizar(funcionario);
        }

        public async Task Remover(Guid id)
        {
            await _funcionarioRepository.Remover(id);
        }    

        public void Dispose()
        {
            _funcionarioRepository?.Dispose();
        }        
    }
}
