using System;
using System.Collections.Generic;
using System.Text;

namespace LH.Business.Models
{
    public class RegistroPonto : Entity
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public decimal ValorHora { get; set; }
        public DateTime DataEntrada { get; set; }
        public DateTime DataSaida { get; set; }
        public TimeSpan Almoco { get; set; }
    }   

}
