using Validador.Modelos;

namespace Validador.Abstracciones;

// Patrón de diseño: Composite
public interface IValidador<T>
{
    IEnumerable<string> Validar(T datoEntrada);
}