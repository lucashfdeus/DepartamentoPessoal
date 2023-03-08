using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LH.Business.Models
{
    public class Resultado
    {
        public decimal TotalHorasTrabalhadas { get; set; }
        public decimal TotalHorasExtras { get; set; }
        public decimal TotalValorExtras { get; set; }
        public List<Departamento> Departamentos { get; set; }

        public Resultado()
        {
            
        }
    }
}