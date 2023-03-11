using CsvHelper.Configuration;
using CsvHelper;
using CsvHelper.TypeConversion;
using System.Text.RegularExpressions;
using System.Globalization;

namespace LH.App.Extensions
{
    public sealed class ValorConverter : DecimalConverter
    {
        public override object ConvertFromString(string texto, IReaderRow row, MemberMapData memberMapData)
        {
            string novoTexto = texto.Replace(",", ".");
            decimal valor = decimal.Parse(Regex.Replace(novoTexto, @"[^\d\.]", ""), CultureInfo.InvariantCulture);
            return base.ConvertFromString(valor.ToString(), row, memberMapData);
        }
    }
}
