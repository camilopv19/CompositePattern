// See https://aka.ms/new-console-template for more information

using Validador.Implementaciones;
using Validador.Modelos;

var datosEntrada = new DatosEntrada(
    curp: "GARC850101HDFRRL9",
    nombres: "",
    apellidoPaterno: "Garcia",
    apellidoMaterno: "Lopez",
    fechaNacimiento: "1985-01-01T00:00:00Z",
    sexo: Sexo.Masculino,
    esMexicano: true
);

var validadores = new GrupoDeValidadores<DatosEntrada>()
    .AñadirValidador(new ValidadorCurp())
    .AñadirValidador(new ValidadorNombres());
    // .AñadirValidador(new ValidadorApellidoPaterno())
    // .AñadirValidador(new ValidadorApellidoMaterno())
    // .AñadirValidador(new ValidadorFechaNacimiento())
    // .AñadirValidador(new ValidadorSexo())
    // .AñadirValidador(new ValidadorEsMexicano());

var errores = validadores.ValidarCurp(datosEntrada);
    Console.WriteLine($"Errores encontrados ({errores.Length}): {string.Join(", ", errores)}");

