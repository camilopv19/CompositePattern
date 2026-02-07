using Validador.Implementaciones;
using Validador.Mensajes;
using Validador.Modelos;

namespace PruebasUnitariasValidador;

public class PruebasUnitariasFechaSexoEstado
{
    // CURP base: GALJ850101HDFRPN09
    // - Fecha: 850101 (1985-01-01)
    // - Sexo: H (Masculino)
    // - Estado: DF (Distrito Federal - Mexicano)
    private DatosEntrada _datosEntrada = new(
        curp: "GALJ850101HDFRPN09",
        nombres: "Juan",
        apellidoPaterno: "Garcia",
        apellidoMaterno: "Lopez",
        fechaNacimiento: "1985-01-01T00:00:00Z",
        sexo: Sexo.Masculino,
        esMexicano: true
    );

    [SetUp]
    public void Setup()
    {
        _datosEntrada = new DatosEntrada(
            curp: "GALJ850101HDFRPN09",
            nombres: "Juan",
            apellidoPaterno: "Garcia",
            apellidoMaterno: "Lopez",
            fechaNacimiento: "1985-01-01T00:00:00Z",
            sexo: Sexo.Masculino,
            esMexicano: true
        );
    }

    #region ValidadorFechaNacimiento

    [Test]
    public void CuandoFechaIsoEsValidaYCoincideConCurp_ValidarFecha_NoDebeRetornarErrores()
    {
        // Arrange
        var validador = new ValidadorFechaNacimiento();

        // Act
        var errores = validador.Validar(_datosEntrada).ToArray();

        // Assert
        Assert.That(errores, Is.Empty);
    }

    [Test]
    public void CuandoFechaIsoTieneFormatoInvalido_ValidarFecha_DebeRetornarError()
    {
        // Arrange
        var validador = new ValidadorFechaNacimiento();
        var errorEsperado = GeneradorDeErrores.ObtenerMensajeDeError(TipoDeError.FechaNacimientoInvalida);
        _datosEntrada = _datosEntrada with { fechaNacimiento = "fecha-invalida" };

        // Act
        var errores = validador.Validar(_datosEntrada).ToArray();

        // Assert
        Assert.That(errores, Does.Contain(errorEsperado));
    }

    [Test]
    public void CuandoFechaIsoNoCoincideConCurp_ValidarFecha_DebeRetornarError()
    {
        // Arrange
        var validador = new ValidadorFechaNacimiento();
        var errorEsperado = GeneradorDeErrores.ObtenerMensajeDeError(TipoDeError.FechaNacimientoInvalida);
        // Fecha 1990-05-15 no coincide con CURP que tiene 850101
        _datosEntrada = _datosEntrada with { fechaNacimiento = "1990-05-15T00:00:00Z" };

        // Act
        var errores = validador.Validar(_datosEntrada).ToArray();

        // Assert
        Assert.That(errores, Does.Contain(errorEsperado));
    }

    [Test]
    public void CuandoFechaIsoEsVacia_ValidarFecha_DebeRetornarError()
    {
        // Arrange
        var validador = new ValidadorFechaNacimiento();
        var errorEsperado = GeneradorDeErrores.ObtenerMensajeDeError(TipoDeError.FechaNacimientoInvalida);
        _datosEntrada = _datosEntrada with { fechaNacimiento = "" };

        // Act
        var errores = validador.Validar(_datosEntrada).ToArray();

        // Assert
        Assert.That(errores, Does.Contain(errorEsperado));
    }

    #endregion

    #region ValidadorEstadoNacimiento

    [Test]
    public void CuandoEsMexicanoYEstadoNoEsNE_ValidarEstado_NoDebeRetornarErrores()
    {
        // Arrange
        var validador = new ValidadorEstadoNacimiento();
        // CURP tiene DF (mexicano) y esMexicano=true

        // Act
        var errores = validador.Validar(_datosEntrada).ToArray();

        // Assert
        Assert.That(errores, Is.Empty);
    }

    [Test]
    public void CuandoEsMexicanoYEstadoEsNE_ValidarEstado_DebeRetornarError()
    {
        // Arrange
        var validador = new ValidadorEstadoNacimiento();
        var errorEsperado = GeneradorDeErrores.ObtenerMensajeDeError(TipoDeError.EstadoNacimientoInvalido);
        // Cambiar CURP para tener NE en lugar de DF
        _datosEntrada = _datosEntrada with { curp = "GALJ850101HNERPN09" };

        // Act
        var errores = validador.Validar(_datosEntrada).ToArray();

        // Assert
        Assert.That(errores, Does.Contain(errorEsperado));
    }

    [Test]
    public void CuandoNoEsMexicanoYEstadoEsNE_ValidarEstado_NoDebeRetornarErrores()
    {
        // Arrange
        var validador = new ValidadorEstadoNacimiento();
        // CURP con NE y esMexicano=false
        _datosEntrada = _datosEntrada with
        {
            curp = "GALJ850101HNERPN09",
            esMexicano = false
        };

        // Act
        var errores = validador.Validar(_datosEntrada).ToArray();

        // Assert
        Assert.That(errores, Is.Empty);
    }

    [Test]
    public void CuandoNoEsMexicanoYEstadoNoEsNE_ValidarEstado_DebeRetornarError()
    {
        // Arrange
        var validador = new ValidadorEstadoNacimiento();
        var errorEsperado = GeneradorDeErrores.ObtenerMensajeDeError(TipoDeError.EstadoNacimientoInvalido);
        // CURP con DF (mexicano) pero esMexicano=false
        _datosEntrada = _datosEntrada with { esMexicano = false };

        // Act
        var errores = validador.Validar(_datosEntrada).ToArray();

        // Assert
        Assert.That(errores, Does.Contain(errorEsperado));
    }

    #endregion

    #region ValidadorSexo

    [Test]
    public void CuandoSexoMasculinoYCurpTieneH_ValidarSexo_NoDebeRetornarErrores()
    {
        // Arrange
        var validador = new ValidadorSexo();
        // CURP tiene H y sexo=Masculino

        // Act
        var errores = validador.Validar(_datosEntrada).ToArray();

        // Assert
        Assert.That(errores, Is.Empty);
    }

    [Test]
    public void CuandoSexoFemeninoYCurpTieneM_ValidarSexo_NoDebeRetornarErrores()
    {
        // Arrange
        var validador = new ValidadorSexo();
        // Cambiar CURP para tener M y sexo=Femenino
        _datosEntrada = _datosEntrada with
        {
            curp = "GALJ850101MDFRPN09",
            sexo = Sexo.Femenino
        };

        // Act
        var errores = validador.Validar(_datosEntrada).ToArray();

        // Assert
        Assert.That(errores, Is.Empty);
    }

    [Test]
    public void CuandoSexoMasculinoYCurpTieneM_ValidarSexo_DebeRetornarError()
    {
        // Arrange
        var validador = new ValidadorSexo();
        var errorEsperado = GeneradorDeErrores.ObtenerMensajeDeError(TipoDeError.SexoInvalido);
        // CURP con M pero sexo=Masculino
        _datosEntrada = _datosEntrada with { curp = "GALJ850101MDFRPN09" };

        // Act
        var errores = validador.Validar(_datosEntrada).ToArray();

        // Assert
        Assert.That(errores, Does.Contain(errorEsperado));
    }

    [Test]
    public void CuandoSexoFemeninoYCurpTieneH_ValidarSexo_DebeRetornarError()
    {
        // Arrange
        var validador = new ValidadorSexo();
        var errorEsperado = GeneradorDeErrores.ObtenerMensajeDeError(TipoDeError.SexoInvalido);
        // CURP con H pero sexo=Femenino
        _datosEntrada = _datosEntrada with { sexo = Sexo.Femenino };

        // Act
        var errores = validador.Validar(_datosEntrada).ToArray();

        // Assert
        Assert.That(errores, Does.Contain(errorEsperado));
    }

    [Test]
    public void CuandoCurpTieneSexoInvalido_ValidarSexo_DebeRetornarError()
    {
        // Arrange
        var validador = new ValidadorSexo();
        var errorEsperado = GeneradorDeErrores.ObtenerMensajeDeError(TipoDeError.SexoInvalido);
        // CURP con X (inválido) en posición de sexo
        _datosEntrada = _datosEntrada with { curp = "GALJ850101XDFRPN09" };

        // Act
        var errores = validador.Validar(_datosEntrada).ToArray();

        // Assert
        Assert.That(errores, Does.Contain(errorEsperado));
    }

    #endregion
}