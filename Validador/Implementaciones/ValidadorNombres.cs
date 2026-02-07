using Validador.Abstracciones;
using Validador.Mensajes;
using Validador.Modelos;
using Validador.Utilidades;

namespace Validador.Implementaciones;

public sealed class ValidadorNombres : IValidador<DatosEntrada>
{
    public IEnumerable<string> Validar(DatosEntrada datosEntrada)
    {
        var errores = new List<string>();
        // Lógica de validación de Nombres:
        // No debe estar vacío
        if (datosEntrada.nombres.Length == 0)
        {
            yield return GeneradorDeErrores.ObtenerMensajeDeError(TipoDeError.NombresInvalidos);
        }
        
        var nombresSeparados = datosEntrada.nombres.Split(' ');
        if (nombresSeparados.Length > 0)
        {
            // Ignorar nombres compuestos que comienzan con María o José,
            // Primero se remueven acentos y se convierten a mayúsculas para la comparación
            var primerNombre = nombresSeparados[0].RemoverAcentos().ToUpper();
            var nomresAIgnorar = new[] { "MARIA", "JOSE" };
            if (nombresSeparados.Length > 1 && nomresAIgnorar.Contains(primerNombre))
            {
                primerNombre = nombresSeparados[1]; // El segundo nombre (del compuesto) es el que se usará para la validación
                if (primerNombre.Length == 0)
                {
                    errores.Add(GeneradorDeErrores.ObtenerMensajeDeError(TipoDeError.NombresInvalidos));
                }
            }
            
            // Validación appellidos: No deben estar vacíos
             if (datosEntrada.apellidoPaterno.Length == 0)
             {
                 errores.Add(GeneradorDeErrores.ObtenerMensajeDeError(TipoDeError.ApellidoPaternoInvalido));
             }
             
             if (datosEntrada.apellidoMaterno.Length == 0)
             {
                 errores.Add(GeneradorDeErrores.ObtenerMensajeDeError(TipoDeError.ApellidoMaternoInvalido));
             }
             
            // Validación de las primeras 4 letras de la CURP:
            var fusionarIniciales =  Obtener4CaracteresDelNombreCompleto(datosEntrada, primerNombre);
            if (fusionarIniciales != datosEntrada.curp.SepararCurpEnPartes(TipoDeSeccion.PrimerasLetras))
            {
                errores.Add(GeneradorDeErrores.ObtenerMensajeDeError(TipoDeError.NombresInvalidos));
            }
             
            // Validación de las primeras 3 letras de las consonantes internas de la CURP:
            var obtenerConsonantes =  Obtener3ConsonantesDelNombreCompleto(datosEntrada, primerNombre);
            
            // Remover la posible  existencia de '0', el cual se usa como convención de ausencia de vocales, indicará si
            // hay errores según la comparación de tamaño del arreglo resultante con el valor esperado de 3
            // consonantes internas
            var sinConsonantes = obtenerConsonantes.Replace("0", "").Length < 3;
            if (sinConsonantes || obtenerConsonantes != datosEntrada.curp.SepararCurpEnPartes(TipoDeSeccion.ConsonantesInternas))
            {
                errores.Add(GeneradorDeErrores.ObtenerMensajeDeError(TipoDeError.ConsonantesInternasInvalidas));
            }
            
            foreach (var error in errores)
            {
                yield return error;
            }
        }
    }
    
    // Las primeras 4 letras de la CURP se forman con:
    // - Las primeras 2 letras del apellido paterno
    // - La primera letra del apellido materno
    // - La primera letra del primer nombre en este contexto
    private string Obtener4CaracteresDelNombreCompleto(DatosEntrada datosEntrada, string primerNombre)
    {
        var primeras2LetasApellidoPaterno = datosEntrada.apellidoPaterno[..2]
            .ToUpper();
        var inicialApellidoMaterno = datosEntrada.apellidoMaterno.ToUpper()[0];
        var inicialPrimerNombre = primerNombre.ToUpper()[0];
        
        return string.Concat(primeras2LetasApellidoPaterno, inicialApellidoMaterno, inicialPrimerNombre);
    }
    
    // Las consonantes internas de la CURP se forman con:
    // - La primera consonante interna del apellido paterno (después de la primera letra)
    // - La primera consonante interna del apellido materno (después de la primera letra)
    // - La primera consonante interna del primer nombre (después de la primera letra)
    private string Obtener3ConsonantesDelNombreCompleto(DatosEntrada datosEntrada, string primerNombre)
    {
        var porcionApellidoPaterno = datosEntrada.apellidoPaterno.ToUpper()[1..];
        var porcionApellidoMaterno = datosEntrada.apellidoMaterno.ToUpper()[1..];
        var porcionPrimerNombre = primerNombre.ToUpper()[1..];
        var primeraConsonanteApellidoPaterno = ObtenerPrimeraConsonante(porcionApellidoPaterno);
        var primeraConsonanteApellidoMaterno = ObtenerPrimeraConsonante(porcionApellidoMaterno);
        var primeraConsonantePrimerNombre = ObtenerPrimeraConsonante(porcionPrimerNombre);
        
        return string.Concat(primeraConsonanteApellidoPaterno, primeraConsonanteApellidoMaterno, primeraConsonantePrimerNombre);
    }
    
    private char ObtenerPrimeraConsonante(string texto)
    {
        var vocales = new[] { 'A', 'E', 'I', 'O', 'U' };
        foreach (var caracter in texto.ToUpper())
        {
            if (!vocales.Contains(caracter) && char.IsLetter(caracter))
            {
                return caracter;
            }
        }
        return '0'; // Si no se encuentra consonante, se devuelve '0' por convención para marcar error
    }
    
}