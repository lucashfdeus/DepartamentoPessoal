

using LH.Business.Interfaces;
using LH.Business.Models;
using LH.Date.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace LH.Date.Repository
{
    public class EnderecoRepositoty : Repository<Endereco>, IEnderecoRepository
    {
        public EnderecoRepositoty(AppRhContext context) : base(context) {}

        public async Task<Endereco> ObterEnderecoPorFornecedor(Guid fornecedorId)
        {
            return await Db.Enderecos
                .AsNoTracking()
                .FirstOrDefaultAsync(f => f.FornecedorId == fornecedorId);
        }
    }
}
