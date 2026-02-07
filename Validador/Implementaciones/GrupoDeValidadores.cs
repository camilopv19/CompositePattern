using Validador.Abstracciones;

namespace Validador.Implementaciones;

public sealed class GrupoDeValidadores<T> : IValidador<T>
{
    private readonly List<IValidador<T>> _validadores = new();
    private readonly IList<string> _errores = new List<string>();

    public GrupoDeValidadores<T> AñadirValidador(IValidador<T> validador)
    {
        _validadores.Add(validador);
        return this;
    }

    public IEnumerable<string> Validar(T entrada)
    {
        foreach (var validador in _validadores)
        {
            var errorDelValidador = validador.Validar(entrada);
            if (errorDelValidador.FirstOrDefault() != null) _errores.Add(errorDelValidador.FirstOrDefault());
        }
        return _errores;
    }
    
    public string[] ValidarCurp(T entrada) => Validar(entrada).ToArray();
}