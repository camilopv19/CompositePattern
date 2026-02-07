using Validador.Utilidades;

namespace PruebasUnitariasValidador;

public class PruebasUnitariasSeparadorCurp
{
    private const string CurpValida = "GARC850101HDFRRL09";

    [Test]
    public void CuandoSeElige_PrimerasLetras_DebeRetornarPrimeros4Caracteres()
    {
        // Arrange
        var curp = CurpValida;
        var seccionEsperada = "GARC";

        // Act
        var resultado = curp.SepararCurpEnPartes(TipoDeSeccion.PrimerasLetras);

        // Assert
        Assert.That(resultado, Is.EqualTo(seccionEsperada));
    }

    [Test]
    public void CuandoSeElige_FechaNacimiento_DebeRetornar6Digitos()
    {
        // Arrange
        var curp = CurpValida;
        var seccionEsperada = "850101";

        // Act
        var resultado = curp.SepararCurpEnPartes(TipoDeSeccion.FechaNacimiento);

        // Assert
        Assert.That(resultado, Is.EqualTo(seccionEsperada));
    }

    [Test]
    public void CuandoSeElige_Sexo_DebeRetornar1Caracter()
    {
        // Arrange
        var curp = CurpValida;
        var seccionEsperada = "H";

        // Act
        var resultado = curp.SepararCurpEnPartes(TipoDeSeccion.Sexo);

        // Assert
        Assert.That(resultado, Is.EqualTo(seccionEsperada));
    }

    [Test]
    public void CuandoSeElige_EstadoNacimiento_DebeRetornar2Caracteres()
    {
        // Arrange
        var curp = CurpValida;
        var seccionEsperada = "DF";

        // Act
        var resultado = curp.SepararCurpEnPartes(TipoDeSeccion.EstadoNacimiento);

        // Assert
        Assert.That(resultado, Is.EqualTo(seccionEsperada));
    }

    [Test]
    public void CuandoSeElige_ConsonantesInternas_DebeRetornar3Caracteres()
    {
        // Arrange
        var curp = CurpValida;
        var seccionEsperada = "RRL";

        // Act
        var resultado = curp.SepararCurpEnPartes(TipoDeSeccion.ConsonantesInternas);

        // Assert
        Assert.That(resultado, Is.EqualTo(seccionEsperada));
    }

    [Test]
    public void CuandoSeElige_DigitoVerificador_DebeRetornar2Caracteres()
    {
        // Arrange
        var curp = CurpValida;
        var seccionEsperada = "09";

        // Act
        var resultado = curp.SepararCurpEnPartes(TipoDeSeccion.DigitoVerificador);

        // Assert
        Assert.That(resultado, Is.EqualTo(seccionEsperada));
    }

    [Test]
    public void CuandoElTipoDeSeccionNoExiste_TipoDeSeccion_DebeLanzarExcepcion()
    {
        // Arrange
        var curp = CurpValida;
        var tipoInvalido = (TipoDeSeccion)999;

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => curp.SepararCurpEnPartes(tipoInvalido));
    }
}