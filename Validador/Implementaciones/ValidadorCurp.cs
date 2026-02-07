using Validador.Abstracciones;
using Validador.Mensajes;
using Validador.Modelos;

namespace Validador.Implementaciones;

public sealed class ValidadorCurp : IValidador<DatosEntrada>
{
    private readonly int MAXIMO_CARACTERES_CURP = 18;
    public IEnumerable<string> Validar(DatosEntrada datosEntrada)
    {
        // Lógica de validación de CURP: Debe tener 18 carácteres
        if (datosEntrada.curp.Length != MAXIMO_CARACTERES_CURP)
        {
            yield return GeneradorDeErrores.ObtenerMensajeDeError(TipoDeError.CurpInvalido);
        }
    }
}