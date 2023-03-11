using LH.Business.Interfaces.Repository;
using LH.Business.Models;
using System;
using System.Threading.Tasks;

namespace LH.Business.Interfaces
{
    public interface IEnderecoRepository : IRepository<Endereco>
    {
        Task<Endereco> ObterEnderecoPorFornecedor(Guid fornecedorId);
    }
}
