using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System;
using LH.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Globalization;
using CsvHelper;
using LH.App.ViewModels;
using System.Linq;
using AutoMapper;
using LH.Business.Interfaces;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Text;

namespace LH.App.Controllers
{
    public class RegistroPontoController : BaseController
    {
        private readonly IMapper _mapper;

        public RegistroPontoController(IMapper mapper, INotificador notificador) : base(notificador)
        {
            _mapper = mapper;
        }

        //[Route("registar-ponto-colaboradores")]
        //public IActionResult ImportarRegistrosPonto()
        //{
        //    return View();
        //}

        [Route("registar-ponto-colaboradores")]
        [HttpPost]
        public async Task<IActionResult> ImportarRegistrosPonto(IFormFile arquivo)
        {
            if (arquivo == null || arquivo.Length == 0)
            {
                ModelState.AddModelError("arquivo", "Por favor, selecione um arquivo");
                return View();
            }

            var registrosPontoEntrada = new List<RegistroPonto>();
            using (var streamReader = new StreamReader(arquivo.OpenReadStream()))
            {
                using (var csvReader = new CsvReader(streamReader))
                {
                    csvReader.Configuration.Delimiter = ";";
                    csvReader.Configuration.HeaderValidated = null;
                    csvReader.Configuration.MissingFieldFound = null;
                    registrosPontoEntrada = csvReader.GetRecords<RegistroPonto>().ToList();
                }
            }

            // Mapeia os objetos para a ViewModel correspondente
            var registrosPontoSaida = _mapper.Map<List<RegistroPontoViewModel>>(registrosPontoEntrada);

            // Retorna os registros como um objeto JSON
            var jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Formatting = Formatting.Indented
            };

            return await DownloadRegistrosPontoJson(registrosPontoSaida, jsonSerializerSettings);
        }

        public async Task<IActionResult> DownloadRegistrosPontoJson(List<RegistroPontoViewModel> registrosPonto, JsonSerializerSettings jsonSerializerSettings)
        {
            // Converte os registros para um array de bytes no formato JSON
            var jsonBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(registrosPonto, jsonSerializerSettings));

            var nomeArquivo = $"registros_ponto_saida-{DateTime.Now.ToString("dd/MM/yyyy")}.json";
            var fileContentResult = File(jsonBytes, "application/json", nomeArquivo);
            return await Task.FromResult(fileContentResult);
        }
    }
}
