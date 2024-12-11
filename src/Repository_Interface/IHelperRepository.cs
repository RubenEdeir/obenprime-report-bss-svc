using Model;

namespace Repository_Interface;

public interface IHelperRepository
{
    Task<Ent_Param_Ns_Token> ObtenerToken(string proceso);
    Task<bool> Actualizar_Token(string proceso, string token);
    Task<Ent_Param_Ns_Param_Rpta> ObtenerParametrosNetSuitePlus(Ent_Param_Ns_Param_Filtro oClass);
    Task<Ent_Respuesta> Insertar_Log(Ent_Logger oClass);
    Task<List<Ent_Parametros_Produccion>> ObtenerParametrosProduccion();
}
