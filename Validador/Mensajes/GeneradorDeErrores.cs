using Validador.Modelos;

namespace Validador.Mensajes;

public static class GeneradorDeErrores
{
    public static string ObtenerMensajeDeError(TipoDeError codigoDeError) => codigoDeError switch
    {
        TipoDeError.CurpInvalido => "Curp Invalido",
        TipoDeError.ApellidoPaternoInvalido => "Apellido Paterno Invalido",
        TipoDeError.ApellidoMaternoInvalido => "Apellido Materno Invalido",
        TipoDeError.NombresInvalidos => "Nombres Invalidos",
        TipoDeError.ConsonantesInternasInvalidas => "Consonantes internas Inválidas",
        TipoDeError.FechaNacimientoInvalida => "Fecha de Nacimiento Invalida",
        TipoDeError.SexoInvalido => "Sexo Invalido",
        TipoDeError.EsMexicanoInvalido => "Es Mexicano Invalido",
        _ => "Error Desconocido"
    };
}