using System.Globalization;
using Validador.Abstracciones;
using Validador.Mensajes;
using Validador.Modelos;
using Validador.Utilidades;

namespace Validador.Implementaciones;

public sealed class ValidadorFechaNacimiento : IValidador<DatosEntrada>
{
    private readonly string _errorFechaInvalida = GeneradorDeErrores.ObtenerMensajeDeError(TipoDeError.FechaNacimientoInvalida);

    public IEnumerable<string> Validar(DatosEntrada datosEntrada)
    {
        var errores = new List<string>();

        // Validación del formato ISO 8601 con UTC
        if (!DateTime.TryParse(datosEntrada.fechaNacimiento, CultureInfo.InvariantCulture,
                DateTimeStyles.RoundtripKind, out var fechaParseada))
        {
            errores.Add(_errorFechaInvalida);
            foreach (var error in errores)
            {
                yield return error;
            }
            yield break;
        }

        // Validación de que la fecha coincide con la sección de fecha de nacimiento de la CURP
        // La CURP usa formato YYMMDD (2 dígitos para año, 2 para mes, 2 para día)
        var fechaCurpEsperada = ObtenerFechaCurpDesdeIso(fechaParseada);
        var fechaCurpActual = datosEntrada.curp.SepararCurpEnPartes(TipoDeSeccion.FechaNacimiento);

        if (fechaCurpEsperada != fechaCurpActual && !errores.Contains(_errorFechaInvalida))
        {
            errores.Add(_errorFechaInvalida);
        }

        foreach (var error in errores)
        {
            yield return error;
        }
    }

    // Convierte una fecha DateTime a formato YYMMDD usado en la CURP
    private string ObtenerFechaCurpDesdeIso(DateTime fecha)
    {
        // La CURP solo utiliza los últimos 2 dígitos del año, por lo que se obtiene el año mod 100
        var año = fecha.Year % 100; // La opción de formato "yy" no funciona correctamente con años anteriores a 2000, por eso se hace el cálculo manual
        var mes = fecha.Month;
        var dia = fecha.Day;

        return $"{año:D2}{mes:D2}{dia:D2}";
    }
}