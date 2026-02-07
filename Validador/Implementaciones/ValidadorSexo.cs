using Validador.Abstracciones;
using Validador.Mensajes;
using Validador.Modelos;
using Validador.Utilidades;

namespace Validador.Implementaciones;

public sealed class ValidadorSexo : IValidador<DatosEntrada>
{
    private readonly string _errorSexoInvalido = GeneradorDeErrores.ObtenerMensajeDeError(TipoDeError.SexoInvalido);
    private const string CodigoMasculino = "H";
    private const string CodigoFemenino = "M";

    public IEnumerable<string> Validar(DatosEntrada datosEntrada)
    {
        var errores = new List<string>();

        var sexoCurp = datosEntrada.curp.SepararCurpEnPartes(TipoDeSeccion.Sexo);

        // Validar que el sexo en la CURP sea H o M
        if (sexoCurp != CodigoMasculino && sexoCurp != CodigoFemenino)
        {
            errores.Add(_errorSexoInvalido);
        }

        // Validar que el sexo en la CURP coincida con el campo Sexo
        var sexoEsperado = datosEntrada.sexo switch
        {
            Sexo.Masculino => CodigoMasculino,
            Sexo.Femenino => CodigoFemenino,
            _ => string.Empty
        };

        if (sexoEsperado != sexoCurp && !errores.Contains(_errorSexoInvalido))
        {
            errores.Add(_errorSexoInvalido);
        }

        foreach (var error in errores)
        {
            yield return error;
        }
    }
}