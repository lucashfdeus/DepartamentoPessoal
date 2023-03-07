using LH.Business.Interfaces;
using LH.Business.Models;
using LH.Date.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LH.Date.Repository
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(AppDpContext context) : base(context){}

        public async Task<Produto> ObterProdutosFornecedor(Guid id)
        {
            return await Db.Produtos
                .AsNoTracking()
                .Include(f => f.Fornecedor)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Produto>> ObterProdutosFornecedores()
        {
            return await Db.Produtos
                .AsNoTracking()
                .Include(f => f.Fornecedor)
                .OrderBy(p => p.Nome).ToListAsync();
        }

        public async Task<IEnumerable<Produto>> ObterProdutosPorFornecedor(Guid fornecedorId)
        {
            return await Buscar(p => p.FornecedorId == fornecedorId);
        }
    }
}
