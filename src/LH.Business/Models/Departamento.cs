using System;
using System.Collections;
using System.Collections.Generic;


namespace LH.Business.Models
{
    public class Departamento : Entity
    {
        public string NomeDepartamento { get; set; }
        public string MesVigencia { get; set; }
        public int AnoVigencia { get; set; }
        public decimal TotalPagar { get; set; }
        public decimal TotalDescontos { get; set; }
        public decimal TotalExtras { get; set; }
        public IEnumerable<Funcionario> Funcionarios { get; set; }
    }
}

//IEnumerable interface de leitura


//ICollection interface de gravação, que herda de IEnumerable.