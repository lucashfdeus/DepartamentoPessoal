using LH.Business.Interfaces.Repository;
using LH.Business.Models;
using LH.Date.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace LH.Date.Repository
{
    public class DepartamentoRepository : Repository<Departamento>, IDepartamentoRepository
    {
        public DepartamentoRepository(AppRhContext context) : base(context) { }       

        public async Task<Departamento> ObterDepartamento(Guid id)
        {
            return await Db.Departamentos
                .AsNoTracking()
                .Include(p => p.Funcionarios)
                .FirstOrDefaultAsync(p => p.Id == id);
        }        
    }
}
