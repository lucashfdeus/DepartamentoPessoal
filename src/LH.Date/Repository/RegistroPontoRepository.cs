using LH.Business.Interfaces;
using LH.Business.Models;
using LH.Date.Context;
using System.Threading.Tasks;

namespace LH.Date.Repository
{
    public class RegistroPontoRepository : Repository<RegistroPonto>, IRegistoProntoRepository
    {
        public RegistroPontoRepository(AppRhContext context) : base(context) { }

        public Task<RegistroPonto> ObterTodosRegistroPonto()
        {
            throw new System.NotImplementedException();
        }       
    }
}
