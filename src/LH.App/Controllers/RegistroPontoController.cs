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
using Microsoft.Extensions.Logging;
using LH.App.Extensions;
using System.Security.Cryptography;

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
        //[HttpPost]
        //public async Task<IActionResult> ImportarRegistrosPonto(IFormFile arquivo)
        //{
        //    if (arquivo == null || arquivo.Length == 0)
        //    {
        //        ModelState.AddModelError("arquivo", "Por favor, selecione um arquivo");
        //        return View();
        //    }

        //    var registrosPontoEntrada = new List<RegistroPonto>();
        //    using (var streamReader = new StreamReader(arquivo.OpenReadStream()))
        //    {
        //        using (var csvReader = new CsvReader(streamReader))
        //        {
        //            csvReader.Configuration.Delimiter = ";";
        //            csvReader.Configuration.HeaderValidated = null;
        //            csvReader.Configuration.MissingFieldFound = null;
        //            registrosPontoEntrada = csvReader.GetRecords<RegistroPonto>().ToList();
        //        }
        //    }

        //    // Mapeia os objetos para a ViewModel correspondente
        //    var registrosPontoSaida = _mapper.Map<List<RegistroPontoViewModel>>(registrosPontoEntrada);

        //    var jsonSerializerSettings = new JsonSerializerSettings
        //    {
        //        ContractResolver = new CamelCasePropertyNamesContractResolver(),
        //        Formatting = Formatting.Indented
        //    };

        //    return await DownloadRegistrosPontoJson(registrosPontoSaida, jsonSerializerSettings);
        //}


        //[Route("registar-ponto-colaboradores")]
        //public async Task<IActionResult> ImportarRegistrosPonto(string folderPath)
        //{
        //    try
        //    {
        //        var files = Directory.GetFiles(folderPath, "*.csv");

        //        var allRegistroPontos = new List<RegistroPonto>();

        //        foreach (var file in files)
        //        {
        //            using (var reader = new StreamReader(file))
        //            using (var csvReader = new CsvReader(reader))
        //            {
        //                csvReader.Configuration.Delimiter = ";";
        //                csvReader.Configuration.HeaderValidated = null;
        //                csvReader.Configuration.MissingFieldFound = null;
        //                var registroPontos = csvReader.GetRecords<RegistroPonto>().ToList();
        //                allRegistroPontos.AddRange(registroPontos);
        //            }
        //        }

        //        var resultado = new Resultado(allRegistroPontos);

        //        var json = JsonConvert.SerializeObject(resultado);

        //        return Content(json, "application/json");
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e.Message);
        //    }
        //}







        public async Task<IActionResult> DownloadRegistrosPontoJson(List<RegistroPontoViewModel> registrosPonto, JsonSerializerSettings jsonSerializerSettings)
        {
            // Converte os registros para um array de bytes no formato JSON
            var jsonBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(registrosPonto, jsonSerializerSettings));

            var nomeArquivo = $"registros_ponto_saida-{DateTime.Now.ToString("dd/MM/yyyy")}.json";
            var fileContentResult = File(jsonBytes, "application/json", nomeArquivo);
            return await Task.FromResult(fileContentResult);
        }




        [HttpPost]
        public IActionResult ProcessarArquivos(string pasta)
        {
            var arquivos = LerArquivos(pasta);
            // processar arquivos aqui
            return View();
        }

        public static List<string> LerArquivos(string pasta)
        {
            var arquivos = new List<string>();
            try
            {
                DirectoryInfo dinfo = new DirectoryInfo(pasta);
                FileInfo[] Files = dinfo.GetFiles("*.csv");

                foreach (FileInfo file in Files)
                {
                    arquivos.Add(file.FullName);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocorreu um erro: " + ex.Message);
            }
            return arquivos;
        }

        [Route("registar-ponto-colaboradores")]
        [HttpPost]
        public async Task<IActionResult> ImportarRegistrosPonto(string caminhoPasta)
        {
            var caminhoCompleto = Path.Combine(caminhoPasta, "*.csv");
            var arquivos = Directory.GetFiles(caminhoPasta, "*.csv");
            var listaDados = new List<RegistroPonto>();

            foreach (var arquivo in arquivos)
            {
                using (var reader = new StreamReader(arquivo))
                using (var csv = new CsvReader(reader))
                {
                    csv.Configuration.RegisterClassMap<RegistroPontoMap>();
                    listaDados.AddRange(csv.GetRecords<RegistroPonto>());
                }
            }

            return Json(listaDados);
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
