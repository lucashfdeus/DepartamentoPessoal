using CsvHelper.Configuration;
using LH.Business.Models;

namespace LH.App.Extensions
{
    public class RegistroPontoMap : ClassMap<RegistroPonto>
    {
        public RegistroPontoMap() {
            Map(m => m.Codigo).Name("Código");
            Map(m => m.Nome).Name("Nome");
            Map(m => m.ValorHora).Name("Valor hora").TypeConverter<ValorConverter>();
            Map(m => m.Data).Name("Data");
            Map(m => m.Entrada).Name("Entrada");
            Map(m => m.Saida).Name("Saída");
            Map(m => m.Almoco).Name("Almoço");
        }
    }
}
