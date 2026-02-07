using Validador.Implementaciones;
using Validador.Mensajes;
using Validador.Modelos;

namespace PruebasUnitariasValidador;

public class PruebasUnitariasValidadorCurp
{
    private DatosEntrada _datosEntrada = new DatosEntrada(
        curp: "GARC850101HDFRRL09",
        nombres: "Juan Carlos",
        apellidoPaterno: "Garcia",
        apellidoMaterno: "Lopez",
        fechaNacimiento: "1985-01-01",
        sexo: Sexo.Masculino,
        esMexicano: true
    );
    
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void CuandoCurpTiene19Caracteres_ValidarCurp_DebeRetornarError()
    {
        // Arrange
        var grupoValidadores = new GrupoDeValidadores<DatosEntrada>().AñadirValidador(new ValidadorCurp());
        var expectedError = GeneradorDeErrores.ObtenerMensajeDeError(TipoDeError.CurpInvalido);
        _datosEntrada = _datosEntrada with { curp = "GARC850101HDFRRL090" }; // 19 caracteres
        
        // Act
        var errores = grupoValidadores.ValidarCurp(_datosEntrada);
        
        // Assert
        Assert.That(errores, Is.Not.Empty);
        Assert.That(errores, Has.Length.EqualTo(1));
        Assert.That(Equals(expectedError, errores[0]));
    }

    [Test]
    public void CuandoCurpTieneMenosde18Caracteres_ValidarCurp_DebeRetornarError()
    {
        // Arrange
        var grupoValidadores = new GrupoDeValidadores<DatosEntrada>().AñadirValidador(new ValidadorCurp());
        var expectedError = GeneradorDeErrores.ObtenerMensajeDeError(TipoDeError.CurpInvalido);
        _datosEntrada = _datosEntrada with { curp = "GARC850101HL0" }; // Menos de 18 caracteres
        
        // Act
        var errores = grupoValidadores.ValidarCurp(_datosEntrada);
        
        // Assert
        Assert.That(errores, Is.Not.Empty);
        Assert.That(errores, Has.Length.EqualTo(1));
        Assert.That(Equals(expectedError, errores[0]));
    }
}