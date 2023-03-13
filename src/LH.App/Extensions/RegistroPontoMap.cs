using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using LH.Business.Models;
using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Text;

namespace LH.App.Extensions
{
    public class RegistroPontoMap : ClassMap<RegistroPonto>
    {
        public RegistroPontoMap()
        {
            Map(m => m.Codigo).Name("Código").ConvertUsing(row => int.Parse(row.GetField(0)));
            Map(m => m.Nome).Name("Nome").TypeConverterOption.CultureInfo(CultureInfo.CurrentCulture);
            Map(m => m.ValorHora).Name("Valor hora").TypeConverter<ValorConverter>();
            Map(m => m.Data).Name("Data");
            Map(m => m.Entrada).Name("Entrada");
            Map(m => m.Saida).Name("Saída").ConvertUsing(row => DateTime.ParseExact(row.GetField(5), "HH:mm:ss", CultureInfo.InvariantCulture));
            Map(m => m.Almoco).Name("Almoço").ConvertUsing(row =>
            {
                string intervaloAlmocoString = row.GetField(6);
                string[] partes = intervaloAlmocoString.Split('-');
                TimeSpan inicio = TimeSpan.Parse(partes[0].Trim());
                TimeSpan fim = TimeSpan.Parse(partes[1].Trim());
                TimeSpan intervaloAlmoco = fim - inicio;
                return intervaloAlmoco;
            });
        }       
    }
}
