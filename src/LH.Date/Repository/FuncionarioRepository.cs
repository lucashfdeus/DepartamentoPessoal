using LH.Business.Interfaces;
using LH.Business.Interfaces.Repository;
using LH.Business.Models;
using LH.Date.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace LH.Date.Repository
{
    public class FuncionarioRepository : Repository<Funcionario>, IFuncionarioRepository
    {
        public FuncionarioRepository(AppRhContext context) : base(context) { }     

        public async Task<Funcionario> ObterFuncionario(Guid id)
        {
            return await Db.Funcionarios
                .AsNoTracking()
                .FirstOrDefaultAsync (c => c.Id == id);
        }
    }
}
