namespace Validador.Utilidades;

public enum  TipoDeSeccion
{
    PrimerasLetras = 0,
    FechaNacimiento = 1,
    Sexo = 2,
    EstadoNacimiento = 3,
    ConsonantesInternas = 4,
    DigitoVerificador = 5
}

public static class SepararCurp
{
    public static string SepararCurpEnPartes(this string curp, TipoDeSeccion tipoDeSeccion)
    {
        return tipoDeSeccion switch
        {
            TipoDeSeccion.PrimerasLetras => curp.Substring(0, 4), // Primeras 4 letras
            TipoDeSeccion.FechaNacimiento => curp.Substring(4, 6), // Fecha de nacimiento (6 dígitos)
            TipoDeSeccion.Sexo => curp.Substring(10, 1), // Sexo (1 letra)
            TipoDeSeccion.EstadoNacimiento => curp.Substring(11, 2), // Estado de nacimiento (2 letras)
            TipoDeSeccion.ConsonantesInternas => curp.Substring(13, 3), // Consonantes internas (3 letras)
            TipoDeSeccion.DigitoVerificador => curp.Substring(16, 2), // Dígito verificador (2 dígitos)
            _ => throw new ArgumentOutOfRangeException(nameof(tipoDeSeccion), "Tipo de sección no válido")
        };
    }
}