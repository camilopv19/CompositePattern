using System.Globalization;
using System.Text;

namespace Validador.Utilidades;

public static class StringExtensions
{
    public static string RemoverAcentos(this string texto)
    {
        if (string.IsNullOrEmpty(texto))
            return texto;

        var textoNormalizado = texto.Normalize(NormalizationForm.FormD);
        var resultado = new StringBuilder();

        foreach (var caracter in textoNormalizado)
        {
            var categoriaUnicode = CharUnicodeInfo.GetUnicodeCategory(caracter);
            if (categoriaUnicode != UnicodeCategory.NonSpacingMark)
            {
                resultado.Append(caracter);
            }
        }

        return resultado.ToString().Normalize(NormalizationForm.FormC);
    }
}