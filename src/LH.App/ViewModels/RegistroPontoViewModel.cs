using System;

namespace LH.App.ViewModels
{
    public class RegistroPontoViewModel
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public decimal ValorHora { get; set; }
        public DateTime DataEntrada { get; set; }
        public DateTime DataSaida { get; set; }
        public TimeSpan Almoco { get; set; }
    }
}
