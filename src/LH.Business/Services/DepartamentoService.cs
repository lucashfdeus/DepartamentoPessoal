using LH.Business.Interfaces;
using LH.Business.Interfaces.Repository;
using LH.Business.Interfaces.Services;
using LH.Business.Models;
using System;
using System.Threading.Tasks;

namespace LH.Business.Services
{
    public class DepartamentoService : BaseService, IDepartamentoService
    {

        private readonly IDepartamentoRepository _departamentoRepository;

        public DepartamentoService(IDepartamentoRepository departamentoRepository,
                                  INotificador notificador) : base(notificador)
        {
            _departamentoRepository = departamentoRepository;
        }

        public async Task<Departamento> Adicionar(Departamento departamento)
        {
            var novoDepartamento = new Departamento()
            {
                NomeDepartamento = "Teste T.I",
                MesVigencia = "Março"
                               
            };

            departamento.NomeDepartamento = novoDepartamento.NomeDepartamento;
            departamento.MesVigencia = novoDepartamento.MesVigencia;

            var funcionarios = await _fun

            return await departamento;
        }

        public Task Atualizar(Departamento departamento)
        {
            throw new NotImplementedException();
        }     

        public Task Remover(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _departamentoRepository?.Dispose();
        }
    }
}
