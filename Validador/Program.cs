// See https://aka.ms/new-console-template for more information

using Validador.Implementaciones;
using Validador.Modelos;

var datosEntrada = new DatosEntrada(
    curp: "GALA850101HDFRRL19",
    nombres: "José Antonio",
    apellidoPaterno: "Garcia",
    apellidoMaterno: "Lopez",
    fechaNacimiento: "1985-01-01T00:00:00Z",
    sexo: Sexo.Masculino,
    esMexicano: true
);

var validadores = new GrupoDeValidadores<DatosEntrada>()
    .AñadirValidador(new ValidadorCurp())
    .AñadirValidador(new ValidadorNombres())
    .AñadirValidador(new ValidadorFechaNacimiento())
    .AñadirValidador(new ValidadorEstadoNacimiento())
    .AñadirValidador(new ValidadorSexo());

var errores = validadores.ValidarCurp(datosEntrada);
    Console.WriteLine($"Errores encontrados ({errores.Length}): {string.Join(", ", errores)}");

