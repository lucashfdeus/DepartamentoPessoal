using LH.Business.Interfaces;
using LH.Business.Interfaces.Repository;
using LH.Business.Interfaces.Services;
using LH.Business.Models;
using System;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;

namespace LH.Business.Services
{
    public class DepartamentoService : BaseService, IDepartamentoService
    {

        private readonly IDepartamentoRepository _departamentoRepository;
        private readonly IRegistroPontoRepository _registroPontoRepository;

        public DepartamentoService(IDepartamentoRepository departamentoRepository,
                                  INotificador notificador,
                                  IRegistroPontoRepository registroPontoRepository) : base(notificador)
        {
            _departamentoRepository = departamentoRepository;
            _registroPontoRepository = registroPontoRepository;
        }

        public async Task Adicionar(Departamento departamento)
        {
            await _departamentoRepository.Adicionar(departamento);
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

        public async Task CalcularSalario()
        {
            var id = Guid.Parse("D42B2F38-43EC-4760-B10A-E91CFFD918C9");
            var registroPonto = await _registroPontoRepository.ObterRegistroPonto(id);

            //DateTime dataInicio = registroPonto.Entrada;
            //DateTime dataFinal = registroPonto.Saida;
            //var result = dataInicio - dataFinal;
        }
    }
}
