using LH.Business.Interfaces;
using LH.Business.Models;
using LH.Date.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LH.Date.Repository
{
    public class FornecedorRepository : Repository<Fornecedor>, IFornecedorRepository
    {
        public FornecedorRepository(AppFornecedorContext context) : base(context) { }

        public async Task<Fornecedor> ObterFornecedorEndereco(Guid id)
        {
            return await Db.Fornecedores
                .AsNoTracking()
                .Include(e => e.Endereco)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Fornecedor> ObterFornecedorProdutosEndereco(Guid id)
        {
            return await Db.Fornecedores
                .AsNoTracking()
                .Include(p => p.Produtos)
                .Include(e => e.Endereco)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
