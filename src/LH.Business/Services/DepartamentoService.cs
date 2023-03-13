using LH.Business.Interfaces;
using LH.Business.Interfaces.Repository;
using LH.Business.Interfaces.Services;
using LH.Business.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace LH.Business.Services
{
    public class DepartamentoService : BaseService, IDepartamentoService
    {

        private readonly IDepartamentoRepository _departamentoRepository;
        private readonly IRegistroPontoRepository _registroPontoRepository;
        private readonly IFuncionarioService _funcionarioService;
        private readonly IFuncionarioRepository _funcionarioRepository;

        public DepartamentoService(IDepartamentoRepository departamentoRepository,
                                  INotificador notificador,
                                  IRegistroPontoRepository registroPontoRepository,
                                  IFuncionarioRepository funcionarioRepository,
                                  IFuncionarioService funcionarioService) : base(notificador)
        {
            _departamentoRepository = departamentoRepository;
            _registroPontoRepository = registroPontoRepository;
            _funcionarioService = funcionarioService;
            _funcionarioRepository = funcionarioRepository;
        }

        public async Task Adicionar(Departamento departamento)
        {
            await _departamentoRepository.Adicionar(departamento);
        }

        public void Dispose()
        {
            _departamentoRepository?.Dispose();
        }

        public async Task CalcularSalario(string nomeDepartamento)
         {

            var registroPontos = await _registroPontoRepository.ObterTodos();

            var departamento = await _departamentoRepository.ObterDepartamentoNome(nomeDepartamento);
            var funcionariosPorCodigo = new Dictionary<int, Funcionario>();

            var totalDias = await CalcularDiasTrabalhados(registroPontos);
            var diasTrabalhados = totalDias.diasTrabalhados;
            var diasFaltantes = totalDias.diasFaltantes;

            var jornadaTrabalho = 9;
            decimal TotalPagar = 0;
            double TotalHorasTrabalhadas = 0.0;
            var totalHorasExtras = 0.0;
            var totalHoasDebito = 0.0;

            foreach (var registro in registroPontos)
            {
                var codigo = registro.Codigo;
                if (!funcionariosPorCodigo.ContainsKey(registro.Codigo))
                {
                    // Cria um novo objeto Funcionario se o código ainda não existe no dicionário
                    var novoFuncionario = new Funcionario
                    {
                        Codigo = codigo,
                        Nome = registro.Nome,
                        DepartamentoId = departamento.Id
                    };
                    funcionariosPorCodigo.Add(codigo, novoFuncionario);
                }

                var horasTrabalhadasDia = (registro.Saida - registro.Entrada - registro.Almoco).TotalHours;
                var valorHora = registro.ValorHora;
                var valorReceber = (decimal)horasTrabalhadasDia * valorHora;
                TotalPagar += valorReceber;
                TotalHorasTrabalhadas += horasTrabalhadasDia;

                if (horasTrabalhadasDia > jornadaTrabalho)
                {
                    var horasExtras = horasTrabalhadasDia - jornadaTrabalho;
                    totalHorasExtras += horasExtras;
                }

                if (horasTrabalhadasDia < jornadaTrabalho)
                {
                    var horasDebito = jornadaTrabalho - horasTrabalhadasDia;
                    totalHoasDebito += horasDebito;
                }

                // Atualiza as propriedades do objeto Funcionario com base nos valores calculados
                var funcionario = funcionariosPorCodigo[codigo];
                funcionario.TotalReceber += valorReceber;
                funcionario.HorasExtras += totalHorasExtras;
                funcionario.HorasDebito += totalHoasDebito;
                funcionario.DiasFalta = diasFaltantes;
                funcionario.DiasTrabalhados = diasTrabalhados;

                var func = await _funcionarioRepository.ObterPorId(funcionario.Id);

                if (func != null)
                {
                    await _funcionarioRepository.Atualizar(funcionario);
                }
                await _funcionarioService.Adicionar(new List<Funcionario> { funcionario });
            }

           
        }

        public async Task AdicionarDepartamento(string nomeDepartamento, string mesVigencia, string anoVigencia)
        {
            var dataAno = DateTimeOffset.ParseExact(anoVigencia, "yyyy", CultureInfo.InvariantCulture);

            var departamento = new Departamento()
            {
                NomeDepartamento = nomeDepartamento,
                MesVigencia = mesVigencia,
                AnoVigencia = dataAno.Year,
                TotalPagar = 0,
                TotalDescontos = 0,
                TotalExtras = 0
            };

            await _departamentoRepository.Adicionar(departamento);
        }

        private async Task<(int diasTrabalhados, int diasFaltantes)> CalcularDiasTrabalhados(List<RegistroPonto> registrosPonto)
        {
            DateTime dataInicial = registrosPonto.Min(rp => rp.Data);
            DateTime dataFinal = registrosPonto.Max(rp => rp.Data);

            var todosDias = Enumerable.Range(0, (dataFinal - dataInicial).Days + 1)
                                                   .Select(d => dataInicial.AddDays(d))
                                                   .ToList();

            var diasTrabalhados = new List<DateTime>();
            var diasFaltantes = new List<DateTime>();

            foreach (DateTime dia in todosDias)
            {
                bool registroEncontrado = registrosPonto.Any(rp => rp.Data.Date == dia.Date);

                if (registroEncontrado)
                {
                    diasTrabalhados.Add(dia);
                }
                else
                {
                    diasFaltantes.Add(dia);
                }
            }

            return (diasTrabalhados.Count(), diasFaltantes.Count());
        }

       
    }
}