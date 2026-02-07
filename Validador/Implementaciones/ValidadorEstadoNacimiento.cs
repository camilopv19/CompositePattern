using Validador.Abstracciones;
using Validador.Mensajes;
using Validador.Modelos;
using Validador.Utilidades;

namespace Validador.Implementaciones;

public sealed class ValidadorEstadoNacimiento : IValidador<DatosEntrada>
{
    private readonly string _errorEstadoNacimientoInvalido = GeneradorDeErrores.ObtenerMensajeDeError(TipoDeError.EstadoNacimientoInvalido);
    private const string CodigoExtranjero = "NE";

    public IEnumerable<string> Validar(DatosEntrada datosEntrada)
    {
        var errores = new List<string>();

        var estadoCurp = datosEntrada.curp.SepararCurpEnPartes(TipoDeSeccion.EstadoNacimiento);

        // Si no es mexicano, el estado de nacimiento en la CURP debe ser "NE" (Nacido en el Extranjero)
        // Si es mexicano, el estado de nacimiento NO debe ser "NE"
        var esExtranjeroEnCurp = estadoCurp == CodigoExtranjero;

        if (!datosEntrada.esMexicano && !esExtranjeroEnCurp)
        {
            errores.Add(_errorEstadoNacimientoInvalido);
        }

        if (datosEntrada.esMexicano && esExtranjeroEnCurp && !errores.Contains(_errorEstadoNacimientoInvalido))
        {
            errores.Add(_errorEstadoNacimientoInvalido);
        }

        foreach (var error in errores)
        {
            yield return error;
        }
    }
}