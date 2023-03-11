using Microsoft.AspNetCore.Mvc;
using System.IO;
using System;
using LH.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using CsvHelper;
using AutoMapper;
using LH.Business.Interfaces;
using LH.App.Extensions;
using System.Linq;
using LH.App.ViewModels;
using Newtonsoft.Json;
using System.Text;
using Newtonsoft.Json.Serialization;
using LH.Business.Interfaces.Services;
using LH.Business.Services;

namespace LH.App.Controllers
{
    public class RegistroPontoController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IRegistroPontoService _registroPontoService;
        private readonly IFuncionarioService _funcionarioService;
        private readonly IDepartamentoService _departamentoService;


        public RegistroPontoController(IMapper mapper, INotificador notificador, IRegistroPontoService registroPontoService, IFuncionarioService funcionarioService, IDepartamentoService departamentoService) : base(notificador)
        {
            _mapper = mapper;
            _registroPontoService = registroPontoService;
            _funcionarioService = funcionarioService;
            _departamentoService = departamentoService;
        }

        [HttpPost("processar-arquivos")]
        public async Task<IActionResult> ProcessarArquivos(string caminhoPasta)
        {
            if (string.IsNullOrEmpty(caminhoPasta))
            {
                return BadRequest("O caminho da pasta não pode ser nulo ou vazio.");
            }

            string caminhoFormatado = caminhoPasta.Replace(@"\", "/");
            string[] arquivos = Directory.GetFiles(caminhoFormatado, "*.csv");

            if (arquivos.Length == 0)
            {
                return BadRequest("Não há arquivos CSV na pasta especificada.");
            }

            var registrosPonto = new List<RegistroPonto>();
            var jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Formatting = Formatting.Indented
            };

            foreach (var arquivo in arquivos)
            {
                var registrosPontoArquivo = await ImportarRegistrosPonto(arquivo);
                registrosPonto.AddRange(registrosPontoArquivo);               

                foreach (var registro in registrosPontoArquivo)
                {
                    await _registroPontoService.Adicionar(registro);
                }
            }

            DateTimeOffset now = DateTimeOffset.Now;
            int year = now.Year;

            var departamento = new Departamento()
            {
                NomeDepartamento = "Teste T.I",
                MesVigencia = "Março",
                AnoVigencia = new DateTimeOffset(year, 1, 1, 0, 0, 0, now.Offset),
                TotalPagar = 20000,
                TotalDescontos = 0,
                TotalExtras = 0
            };

            await _departamentoService.Adicionar(departamento);

            var funcionario = new Funcionario()
            {
                Codigo = 1,
                Nome = "Taynara Amorim",
                TotalReceber = 10000,
                HorasExtras = 0,
                HorasDebito = 0,
                DiasFalta = 0,
                DiasExtras = 0,
                DiasTrabalhados = 25,
                DepartamentoId = departamento.Id,
            };

            await _funcionarioService.Adicionar(funcionario);

            await _departamentoService.CalcularSalario();

            var registrosPontoSaida = _mapper.Map<List<RegistroPontoViewModel>>(registrosPonto);

            return await DownloadRegistrosPontoJson(registrosPontoSaida, jsonSerializerSettings);

        }

        private async Task<List<RegistroPonto>> ImportarRegistrosPonto(string caminhoArquivo)
        {
            var registros = new List<RegistroPonto>();

            try
            {
                byte[] bytes = await System.IO.File.ReadAllBytesAsync(caminhoArquivo);
                using (var memoryStream = new MemoryStream(bytes))
                using (var reader = new StreamReader(memoryStream))
                using (var csv = new CsvReader(reader))
                {
                    csv.Configuration.Delimiter = ";";
                    csv.Configuration.RegisterClassMap<RegistroPontoMap>();
                    csv.Configuration.HeaderValidated = null; // Ignora validação de cabeçalho
                    csv.Configuration.MissingFieldFound = null; // Ignora campos ausentes
                    registros = csv.GetRecords<RegistroPonto>().ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocorreu um erro ao importar os registros de ponto: " + ex.Message);
                return registros;
            }

            return registros;
        }


        public async Task<IActionResult> DownloadRegistrosPontoJson(List<RegistroPontoViewModel> registrosPonto, JsonSerializerSettings jsonSerializerSettings)
        {
            // Converte os registros para um array de bytes no formato JSON
            var jsonBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(registrosPonto, jsonSerializerSettings));

            var nomeArquivo = $"registros_ponto_saida-{DateTime.Now.ToString("dd/MM/yyyy")}.json";
            var fileContentResult = File(jsonBytes, "application/json", nomeArquivo);
            return await Task.FromResult(fileContentResult);
        }
       
        private static string ObterDepartamento(List<string> arquivos, RegistroPonto dado)
        {
            foreach (var arquivo in arquivos)
            {
                if (arquivo.Contains(dado.Data.ToString("MMMM-yyyy")) && arquivo.Contains(dado.Nome))
                {
                    return dado.Nome;
                }
            }
            return null;
        }
    }
}
