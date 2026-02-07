namespace Validador.Modelos;

public sealed record DatosEntrada(
    string curp,
    string nombres,
    string apellidoMaterno,
    string apellidoPaterno,
    string fechaNacimiento,
    Sexo sexo,
    bool esMexicano
);
