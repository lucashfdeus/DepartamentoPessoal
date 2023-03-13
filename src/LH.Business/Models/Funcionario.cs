using System;
using System.Collections.Generic;
using System.Text;

namespace LH.Business.Models
{
    public class Funcionario : Entity
    {
        public Guid DepartamentoId { get; set; }
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public decimal TotalReceber { get; set; }
        public double HorasExtras { get; set; }
        public double HorasDebito { get; set; }
        public int DiasFalta { get; set; }
        public int DiasExtras { get; set; }
        public int DiasTrabalhados { get; set; }
        public Departamento Departamento { get; set; }
    }   

}
