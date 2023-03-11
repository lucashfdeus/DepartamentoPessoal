using System;

namespace LH.Business.Models
{
    public class RegistroPonto : Entity
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
