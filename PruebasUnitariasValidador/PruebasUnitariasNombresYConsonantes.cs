using Validador.Implementaciones;
using Validador.Mensajes;
using Validador.Modelos;

namespace PruebasUnitariasValidador;

public class PruebasUnitariasNombresYConsonantes
{
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
        // Reiniciar datos de entrada antes de cada prueba
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

    #region Validación de Nombres Vacíos

    [Test]
    public void CuandoNombresEstaVacio_ValidarNombres_DebeRetornarErrorDeNombresInvalidos()
    {
        // Arrange
        var validador = new ValidadorNombres();
        var errorEsperado = GeneradorDeErrores.ObtenerMensajeDeError(TipoDeError.NombresInvalidos);
        _datosEntrada = _datosEntrada with { nombres = "" };

        // Act
        var errores = validador.Validar(_datosEntrada).ToArray();

        // Assert
        Assert.That(errores, Does.Contain(errorEsperado));
    }

    [Test]
    public void CuandoNombresNoEstaVacio_ValidarNombres_NoDebeRetornarErrorDeNombresVacios()
    {
        // Arrange
        var validador = new ValidadorNombres();
        var errorNoEsperado = GeneradorDeErrores.ObtenerMensajeDeError(TipoDeError.NombresInvalidos);

        // Act
        var errores = validador.Validar(_datosEntrada).ToArray();

        // Assert
        Assert.That(errores, Does.Not.Contain(errorNoEsperado));
    }

    #endregion

    #region Validación de Nombres Compuestos (María/José)

    [Test]
    public void CuandoNombreEsMariaConAcento_ValidarNombres_DebeUsarSegundoNombre()
    {
        // Arrange
        var validador = new ValidadorNombres();
        // CURP debe coincidir con segundo nombre "Fernanda" → F, consonante interna R
        _datosEntrada = _datosEntrada with
        {
            curp = "GALF850101HDFRPR09",
            nombres = "María Fernanda"
        };

        // Act
        var errores = validador.Validar(_datosEntrada).ToArray();

        // Assert
        Assert.That(errores, Does.Not.Contain(GeneradorDeErrores.ObtenerMensajeDeError(TipoDeError.NombresInvalidos)));
    }

    [Test]
    public void CuandoNombreEsJoseSinAcento_ValidarNombres_DebeUsarSegundoNombre()
    {
        // Arrange
        var validador = new ValidadorNombres();
        // CURP debe coincidir con segundo nombre "Carlos" → C, consonante interna R
        _datosEntrada = _datosEntrada with
        {
            curp = "GALC850101HDFRPR09",
            nombres = "Jose Carlos"
        };

        // Act
        var errores = validador.Validar(_datosEntrada).ToArray();

        // Assert
        Assert.That(errores, Does.Not.Contain(GeneradorDeErrores.ObtenerMensajeDeError(TipoDeError.NombresInvalidos)));
    }

    [Test]
    public void CuandoNombreEsJoseConAcento_ValidarNombres_DebeUsarSegundoNombre()
    {
        // Arrange
        var validador = new ValidadorNombres();
        _datosEntrada = _datosEntrada with
        {
            curp = "GALC850101HDFRPR09",
            nombres = "José Carlos"
        };

        // Act
        var errores = validador.Validar(_datosEntrada).ToArray();

        // Assert
        Assert.That(errores, Does.Not.Contain(GeneradorDeErrores.ObtenerMensajeDeError(TipoDeError.NombresInvalidos)));
    }

    #endregion

    #region Validación de Apellidos Vacíos

    [Test]
    public void CuandoApellidoPaternoEstaVacio_ValidarNombres_DebeRetornarErrorDeApellidoPaterno()
    {
        // Arrange
        var validador = new ValidadorNombres();
        var errorEsperado = GeneradorDeErrores.ObtenerMensajeDeError(TipoDeError.ApellidoPaternoInvalido);
        _datosEntrada = _datosEntrada with { apellidoPaterno = "" };

        // Act
        var errores = validador.Validar(_datosEntrada).ToArray();

        // Assert
        Assert.That(errores, Does.Contain(errorEsperado));
    }

    [Test]
    public void CuandoApellidoMaternoEstaVacio_ValidarNombres_DebeRetornarErrorDeApellidoMaterno()
    {
        // Arrange
        var validador = new ValidadorNombres();
        var errorEsperado = GeneradorDeErrores.ObtenerMensajeDeError(TipoDeError.ApellidoMaternoInvalido);
        _datosEntrada = _datosEntrada with { apellidoMaterno = "" };

        // Act
        var errores = validador.Validar(_datosEntrada).ToArray();

        // Assert
        Assert.That(errores, Does.Contain(errorEsperado));
    }

    [Test]
    public void CuandoAmbosApellidosEstanVacios_ValidarNombres_DebeRetornarAmbosErrores()
    {
        // Arrange
        var validador = new ValidadorNombres();
        var errorApellidoPaterno = GeneradorDeErrores.ObtenerMensajeDeError(TipoDeError.ApellidoPaternoInvalido);
        var errorApellidoMaterno = GeneradorDeErrores.ObtenerMensajeDeError(TipoDeError.ApellidoMaternoInvalido);
        _datosEntrada = _datosEntrada with { apellidoPaterno = "", apellidoMaterno = "" };

        // Act
        var errores = validador.Validar(_datosEntrada).ToArray();

        // Assert
        Assert.That(errores, Does.Contain(errorApellidoPaterno));
        Assert.That(errores, Does.Contain(errorApellidoMaterno));
    }

    #endregion

    #region Validación de Primeras 4 Letras de CURP

    [Test]
    public void CuandoPrimeras4LetrasDeCurpNoCoinciden_ValidarNombres_DebeRetornarErrorDeNombres()
    {
        // Arrange
        var validador = new ValidadorNombres();
        var errorEsperado = GeneradorDeErrores.ObtenerMensajeDeError(TipoDeError.NombresInvalidos);
        // CURP con primeras 4 letras incorrectas (XXXX en lugar de GALJ)
        _datosEntrada = _datosEntrada with { curp = "XXXX850101HDFRPN09" };

        // Act
        var errores = validador.Validar(_datosEntrada).ToArray();

        // Assert
        Assert.That(errores, Does.Contain(errorEsperado));
    }

    [Test]
    public void CuandoPrimeras4LetrasDeCurpCoinciden_ValidarNombres_NoDebeRetornarErrorDeNombres()
    {
        // Arrange
        var validador = new ValidadorNombres();
        var errorNoEsperado = GeneradorDeErrores.ObtenerMensajeDeError(TipoDeError.NombresInvalidos);

        // Act
        var errores = validador.Validar(_datosEntrada).ToArray();

        // Assert
        Assert.That(errores, Does.Not.Contain(errorNoEsperado));
    }

    #endregion

    #region Validación de Consonantes Internas

    [Test]
    public void CuandoConsonantesInternasNoCoinciden_ValidarNombres_DebeRetornarErrorDeConsonantes()
    {
        // Arrange
        var validador = new ValidadorNombres();
        var errorEsperado = GeneradorDeErrores.ObtenerMensajeDeError(TipoDeError.ConsonantesInternasInvalidas);
        // CURP con consonantes internas incorrectas (XXX en lugar de RPN)
        _datosEntrada = _datosEntrada with { curp = "GALJ850101HDFXXX09" };

        // Act
        var errores = validador.Validar(_datosEntrada).ToArray();

        // Assert
        Assert.That(errores, Does.Contain(errorEsperado));
    }

    [Test]
    public void CuandoConsonantesInternasCoinciden_ValidarNombres_NoDebeRetornarErrorDeConsonantes()
    {
        // Arrange
        var validador = new ValidadorNombres();
        var errorNoEsperado = GeneradorDeErrores.ObtenerMensajeDeError(TipoDeError.ConsonantesInternasInvalidas);

        // Act
        var errores = validador.Validar(_datosEntrada).ToArray();

        // Assert
        Assert.That(errores, Does.Not.Contain(errorNoEsperado));
    }

    [Test]
    public void CuandoNombreNoTieneConsonantesInternas_ValidarNombres_DebeRetornarErrorDeConsonantes()
    {
        // Arrange
        var validador = new ValidadorNombres();
        var errorEsperado = GeneradorDeErrores.ObtenerMensajeDeError(TipoDeError.ConsonantesInternasInvalidas);
        // Nombre sin consonantes internas (solo vocales después de la primera letra)
        _datosEntrada = _datosEntrada with { nombres = "Ai" };

        // Act
        var errores = validador.Validar(_datosEntrada).ToArray();

        // Assert
        Assert.That(errores, Does.Contain(errorEsperado));
    }

    #endregion

    #region Validación Completa (Datos Válidos)

    [Test]
    public void CuandoTodosLosDatosSonValidos_ValidarNombres_NoDebeRetornarErrores()
    {
        // Arrange
        var validador = new ValidadorNombres();

        // Act
        var errores = validador.Validar(_datosEntrada).ToArray();

        // Assert
        Assert.That(errores, Is.Empty);
    }
    #endregion

    #region Múltiples Errores
    
    [Test]
    public void Cuando3DatosDeEntradaSonInvalidos_ValidarNombres_DebeRetornar3Errores()
    {
        // Arrange
        var validador = new ValidadorNombres();
        _datosEntrada = _datosEntrada with
        {
            curp = "GALC850101HDFRPR09",
            nombres = "José A",
            apellidoMaterno = "U",
            apellidoPaterno = "E"
        };
        // Errores esperados:
        // 1. NombresInvalidos - Porque el segundo nombre "A" tiene menos de 2 caracteres
        // 2. ApellidoPaternoInvalido - Porque "E" tiene menos de 2 caracteres
        // 3. ApellidoMaternoInvalido - Porque "U" tiene menos de 2 caracteres
        // 4. NombresInvalidos - Porque primerNombre es inválido (menos de 2 caracteres)
        // Nota: No se validan consonantes porque el validador retorna temprano cuando hay campos inválidos
        var erroresEsperados = new[]
        {
            GeneradorDeErrores.ObtenerMensajeDeError(TipoDeError.ApellidoMaternoInvalido),
            GeneradorDeErrores.ObtenerMensajeDeError(TipoDeError.ApellidoPaternoInvalido),
            GeneradorDeErrores.ObtenerMensajeDeError(TipoDeError.NombresInvalidos),
        };
        // Act
        var errores = validador.Validar(_datosEntrada).ToArray();

        // Assert
        Assert.That(errores, Is.EquivalentTo(erroresEsperados));
    }
    
    [Test]
    public void CuandoNoHayConsonantesInternas_ValidarNombres_DebeRetornar2Errores()
    {
        // Arrange
        var validador = new ValidadorNombres();
        _datosEntrada = _datosEntrada with
        {
            curp = "GALC850101HDFRPR09",
            nombres = "José Aaa",
            apellidoMaterno = "Ui",
            apellidoPaterno = "Ei"
        };
        // Errores esperados:
        // 1. NombresInvalidos - Porque el segundo nombre "Aaa" no tiene consonantes
        // 4. ConsonantesInvalidas - Porque RPR no coincide con las consonantes internas esperadas ya que no hay consonantes internas
        var erroresEsperados = new[]
        {
            GeneradorDeErrores.ObtenerMensajeDeError(TipoDeError.NombresInvalidos),
            GeneradorDeErrores.ObtenerMensajeDeError(TipoDeError.ConsonantesInternasInvalidas),
        };
        // Act
        var errores = validador.Validar(_datosEntrada).ToArray();

        // Assert
        Assert.That(errores, Is.EquivalentTo(erroresEsperados));
    }
    #endregion
}