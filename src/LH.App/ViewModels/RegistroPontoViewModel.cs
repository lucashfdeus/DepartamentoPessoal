using System;

namespace LH.App.ViewModels
{
    public class RegistroPontoViewModel
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public decimal ValorHora { get; set; }
        public DateTime Data { get; set; }
        public DateTime Entrada { get; set; }
        public DateTime Saida { get; set; }
        public TimeSpan Almoco { get; set; }
    }
}
