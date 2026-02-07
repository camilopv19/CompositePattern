// See https://aka.ms/new-console-template for more information

using Validador.Implementaciones;
using Validador.Modelos;

var datosEntrada = new DatosEntrada(
    curp: "GARC850101HDFRRL09",
    nombres: "Juan Carlos",
    apellidoPaterno: "Garcia",
    apellidoMaterno: "Lopez",
    fechaNacimiento: "1985-01-01",
    sexo: Sexo.Masculino,
    esMexicano: true
);

var validadores = new GrupoDeValidadores<DatosEntrada>()
    .AñadirValidador(new ValidadorCurp());
    // .AñadirValidador(new ValidadorNombres())
    // .AñadirValidador(new ValidadorApellidoPaterno())
    // .AñadirValidador(new ValidadorApellidoMaterno())
    // .AñadirValidador(new ValidadorFechaNacimiento())
    // .AñadirValidador(new ValidadorSexo())
    // .AñadirValidador(new ValidadorEsMexicano());

var errores = validadores.ValidarCurp(datosEntrada);
    Console.WriteLine($"Errores encontrados: {string.Join(", ", errores)}");

