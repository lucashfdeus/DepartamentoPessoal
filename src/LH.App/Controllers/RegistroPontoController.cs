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

namespace LH.App.Controllers
{
    public class RegistroPontoController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IRegistroPontoService _registroPontoService;
        private readonly IFuncionarioService _funcionarioService;
        private readonly IDepartamentoService _departamentoService;


        public RegistroPontoController(IMapper mapper, 
            INotificador notificador,
            IRegistroPontoService registroPontoService,
            IFuncionarioService funcionarioService
            , IDepartamentoService departamentoService) : base(notificador)
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

            foreach (var arquivo in arquivos)
            {
                var registrosPontoArquivo = await ImportarRegistrosPonto(arquivo);
                registrosPonto.AddRange(registrosPontoArquivo);

                var nomeArquivo = Path.GetFileNameWithoutExtension(arquivo);
                var partesNome = nomeArquivo.Split('-');
                var nomeDepartamento = partesNome[0];
                var nomeMes = partesNome[1];
                var ano = partesNome[2];

                await _departamentoService.AdicionarDepartamento(nomeDepartamento, nomeMes, ano);

                foreach (var registro in registrosPontoArquivo)
                {
                    await _registroPontoService.Adicionar(registro);


                   await _departamentoService.CalcularSalario(nomeDepartamento);
                }                            
            }

            var registrosPontoSaida = _mapper.Map<List<RegistroPontoViewModel>>(registrosPonto);

            var jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Formatting = Formatting.Indented
            };

            return await DownloadRegistrosPontoJson(registrosPontoSaida, jsonSerializerSettings);

        }

        private async Task<List<RegistroPonto>> ImportarRegistrosPonto(string caminhoArquivo)
        {
            using (var reader = new StreamReader(caminhoArquivo))
            {
                using (var csv = new CsvReader(reader))
                {
                    csv.Configuration.RegisterClassMap<RegistroPontoMap>();
                    csv.Configuration.Delimiter = ";";
                    csv.Configuration.Encoding = Encoding.UTF8;
                    csv.Configuration.HeaderValidated = null;
                    csv.Configuration.MissingFieldFound = null;
                    csv.Configuration.HasHeaderRecord = true;

                    var registros = csv.GetRecords<RegistroPonto>().ToList();
                    return registros;
                }
            }
        }


        public async Task<IActionResult> DownloadRegistrosPontoJson(List<RegistroPontoViewModel> registrosPonto, JsonSerializerSettings jsonSerializerSettings)
        {
            var jsonBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(registrosPonto, jsonSerializerSettings));

            var nomeArquivo = $"registros_ponto_saida-{DateTime.Now.ToString("dd/MM/yyyy")}.json";
            var fileContentResult = File(jsonBytes, "application/json", nomeArquivo);
            return await Task.FromResult(fileContentResult);
        }
    }
}
