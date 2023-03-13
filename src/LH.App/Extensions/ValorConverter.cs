using CsvHelper.Configuration;
using CsvHelper;
using CsvHelper.TypeConversion;
using System.Text.RegularExpressions;
using System.Globalization;
using System;
using System.Text;
using System.Linq;

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

    public class StringConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            if (string.IsNullOrWhiteSpace(text)) return null;

            var cleanedText = new string(text
                .Normalize(NormalizationForm.FormD)
                .Where(ch => CharUnicodeInfo.GetUnicodeCategory(ch) != UnicodeCategory.NonSpacingMark)
                .ToArray());

            return cleanedText;
        }
    }

    public class IntervaloAlmocoTypeConverter : ITypeConverter
    {
        public object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            string[] partes = text.Split('-');
            TimeSpan inicio = TimeSpan.Parse(partes[0].Trim());
            TimeSpan fim = TimeSpan.Parse(partes[1].Trim());
            TimeSpan intervaloAlmoco = fim - inicio;
            return intervaloAlmoco;
        }

        public string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            return value.ToString();
        }
    }


}
