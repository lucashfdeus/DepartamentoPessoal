using LH.Business.Interfaces;
using LH.Business.Interfaces.Repository;
using LH.Business.Interfaces.Services;
using LH.Business.Models;
using LH.Business.Validations;
using System;
using System.Threading.Tasks;

namespace LH.Business.Services
{
    public class RegistroPontoService : BaseService, IRegistroPontoService
    {

        private readonly IRegistroPontoRepository _registroPontoRepository;

        public RegistroPontoService(IRegistroPontoRepository registroPontoRepository, 
                                   INotificador notificador) : base(notificador)
        {
            _registroPontoRepository = registroPontoRepository;
        }

        public async Task Adicionar(RegistroPonto registroPonto)
        {
            if (!ExecutarValidacao(new RegistroPontoValidation(), registroPonto)) return;

            await _registroPontoRepository.Adicionar(registroPonto);
        }

        public async Task Atualizar(RegistroPonto registroPonto)
        {
            if (!ExecutarValidacao(new RegistroPontoValidation(), registroPonto)) return;

            await _registroPontoRepository.Atualizar(registroPonto);
        }        

        public async Task Remover(Guid id)
        {
            await _registroPontoRepository.Remover(id);
        }        

        public void Dispose()
        {
            _registroPontoRepository?.Dispose();
        }
    }
}
