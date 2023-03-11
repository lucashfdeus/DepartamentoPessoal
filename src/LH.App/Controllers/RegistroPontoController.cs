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


        public RegistroPontoController(IMapper mapper, INotificador notificador, IRegistroPontoService registroPontoService) : base(notificador)
        {
            _mapper = mapper;
            _registroPontoService = registroPontoService;
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

            

            var registrosPontoSaida = _mapper.Map<List<RegistroPontoViewModel>>(registrosPonto);


            return Json(registrosPontoSaida);
            //return await DownloadRegistrosPontoJson(registrosPontoSaida, jsonSerializerSettings);

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
