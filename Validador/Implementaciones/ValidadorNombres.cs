using Validador.Abstracciones;
using Validador.Mensajes;
using Validador.Modelos;

namespace Validador.Implementaciones;

public sealed class ValidadorNombres : IValidador<DatosEntrada>
{
    public IEnumerable<string> Validar(DatosEntrada datosEntrada)
    {
        // Lógica de validación de Nombres:
        // No debe estar vacío
        if (datosEntrada.nombres.Length == 0)
        {
            yield return GeneradorDeErrores.ObtenerMensajeDeError(TipoDeError.NombresInvalidos);
        }
        
        var nombresSeparados = datosEntrada.nombres.Split(' ');
        if (nombresSeparados.Length > 0)
        {
            // No debe contenter nombres que comienzan con María o José
            var primerNombre = nombresSeparados[0];
            var primerosNombresInvalidos = new[] { "MARIA", "JOSE" };
            if (primerosNombresInvalidos.Contains(primerNombre)) 
            {
                yield return GeneradorDeErrores.ObtenerMensajeDeError(TipoDeError.NombresInvalidos);
            }
        }
    }
}