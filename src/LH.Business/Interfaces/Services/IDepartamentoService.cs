using LH.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LH.Business.Interfaces.Services
{
    public interface IDepartamentoService : IDisposable
    {
        Task Adicionar(Departamento departamento);
        Task CalcularSalario(string nomeDepartamento);
        Task AdicionarDepartamento(string nomeDepartamento, string mesVigencia, string anoVigencia);    

    }
}
