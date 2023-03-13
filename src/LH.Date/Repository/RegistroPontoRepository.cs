using LH.Business.Models;
using LH.Date.Context;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
using LH.Business.Interfaces.Repository;

namespace LH.Date.Repository
{
    public class RegistroPontoRepository : Repository<RegistroPonto>, IRegistroPontoRepository
    {
        public RegistroPontoRepository(AppRhContext context) : base(context) { }

        public async Task<RegistroPonto> ObterRegistroPonto(Guid id)
        {
            return await Db.RegistroPontos
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
        }        
    }
}
