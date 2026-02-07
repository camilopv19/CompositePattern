using Validador.Implementaciones;
using Validador.Mensajes;
using Validador.Modelos;

namespace PruebasUnitariasValidador;

public class PruebasUnitariasValidadorCurp
{
    private DatosEntrada DatosEntrada = new (
        curp: "GARC850101HDFRRL09",
        nombres: "Juan Carlos",
        apellidoPaterno: "Garcia",
        apellidoMaterno: "Lopez",
        fechaNacimiento: "1985-01-01T00:00:00Z",
        sexo: Sexo.Masculino,
        esMexicano: true
    );
    
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void CuandoCurpTiene19Caracteres_ValidarCurp_DebeRetornarErrorDeCurp()
    {
        // Arrange
        var grupoValidadores = new GrupoDeValidadores<DatosEntrada>().AñadirValidador(new ValidadorCurp());
        var expectedError = GeneradorDeErrores.ObtenerMensajeDeError(TipoDeError.CurpInvalido);
        DatosEntrada = DatosEntrada with { curp = "GARC850101HDFRRL090" }; // 19 caracteres
        
        // Act
        var errores = grupoValidadores.ValidarCurp(DatosEntrada);
        
        // Assert
        Assert.That(errores, Is.Not.Empty);
        Assert.That(errores, Has.Length.EqualTo(1));
        Assert.That(Equals(expectedError, errores[0]));
    }

    [Test]
    public void CuandoCurpTieneMenosde18Caracteres_ValidarCurp_DebeRetornarErrorDeCurp()
    {
        // Arrange
        var grupoValidadores = new GrupoDeValidadores<DatosEntrada>().AñadirValidador(new ValidadorCurp());
        var expectedError = GeneradorDeErrores.ObtenerMensajeDeError(TipoDeError.CurpInvalido);
        DatosEntrada = DatosEntrada with { curp = "GARC850101HL0" }; // Menos de 18 caracteres
        
        // Act
        var errores = grupoValidadores.ValidarCurp(DatosEntrada);
        
        // Assert
        Assert.That(errores, Is.Not.Empty);
        Assert.That(errores, Has.Length.EqualTo(1));
        Assert.That(Equals(expectedError, errores[0]));
    }

    [Test]
    public void CuandoCurpTiene18Caracteres_ValidarCurp_DebeRetornarStringVacio()
    {
        // Arrange
        var grupoValidadores = new GrupoDeValidadores<DatosEntrada>().AñadirValidador(new ValidadorCurp());
        DatosEntrada = DatosEntrada with { curp = "GARC850101HDFRRL09" };
        
        // Act
        var errores = grupoValidadores.ValidarCurp(DatosEntrada);
        
        // Assert
        Assert.That(errores, Is.Empty);
        Assert.That(errores, Has.Length.EqualTo(0));
    }

    [Test]
    public void CuandoNombresEsVacio_ValidarNombres_DebeRetornarErrorDeNombres()
    {
        // Arrange
        var grupoValidadores = new GrupoDeValidadores<DatosEntrada>().AñadirValidador(new ValidadorNombres());
        var expectedError = GeneradorDeErrores.ObtenerMensajeDeError(TipoDeError.NombresInvalidos);
        DatosEntrada = DatosEntrada with { nombres = "" };

        // Act
        var errores = grupoValidadores.ValidarCurp(DatosEntrada);

        // Assert
        Assert.That(errores, Is.Not.Empty);
        Assert.That(errores, Has.Length.EqualTo(1));
        Assert.That(Equals(expectedError, errores[0]));
    }
}