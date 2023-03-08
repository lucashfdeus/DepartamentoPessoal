using System;
using System.Collections.Generic;
using System.Text;

namespace LH.Business.Models
{
    public class Departamento : Entity
    {
        public string NomeDepartamento { get; set; }
        public string MesVigencia { get; set; }
        public DateTimeOffset AnoVigencia { get; set; }
        public decimal TotalPagar { get; set; }
        public decimal TotalDescontos { get; set; }
        public decimal TotalExtras { get; set; }
        public ICollection<Funcionario> Funcionarios { get; set; }
    }
}
