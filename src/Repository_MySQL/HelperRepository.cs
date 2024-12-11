using Model;
using MySqlConnector;
using Repository_Interface;
using System.Data;

namespace Repository_MySQL;

public class HelperRepository : Repository, IHelperRepository
{
    public HelperRepository(MySqlConnection context, MySqlTransaction transaction)
    {
        _context = context;
        _transaction = transaction;
    }

    public async Task<Ent_Param_Ns_Token> ObtenerToken(string proceso)
    {
        using var command = CreateCommand("USP_ObtenerTokens");
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.AddWithValue("p_proceso", proceso);

        using var reader = await command.ExecuteReaderAsync(CommandBehavior.SingleRow);

        if (reader.HasRows)
        {
            await reader.ReadAsync();

            return new Ent_Param_Ns_Token
            {
                Token = reader.IsDBNull(reader.GetOrdinal("f_token")) ? null : reader.GetString(reader.GetOrdinal("f_token")),
            };
        }

        return null;
    }

    public async Task<bool> Actualizar_Token(string proceso, string token)
    {
        using var command = CreateCommand("USP_ActualizarTokens");
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.AddWithValue("p_proceso", proceso);
        command.Parameters.AddWithValue("p_token", token);
        return await command.ExecuteNonQueryAsync() > 0;
    }

    public async Task<Ent_Param_Ns_Param_Rpta> ObtenerParametrosNetSuitePlus(Ent_Param_Ns_Param_Filtro oClass)
    {
        using var command = CreateCommand("USP_ObtenerParametrosNetSuitePlus");
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.AddWithValue("p_origen", oClass.origen);
        command.Parameters.AddWithValue("p_param_1", oClass.param_1);
        command.Parameters.AddWithValue("p_param_2", oClass.param_2);
        command.Parameters.AddWithValue("p_param_3", oClass.param_3);
        command.Parameters.AddWithValue("p_param_4", oClass.param_4);
        command.Parameters.AddWithValue("p_param_5", oClass.param_5);
        command.Parameters.AddWithValue("p_param_6", oClass.param_6);
        command.Parameters.AddWithValue("p_param_7", oClass.param_7);
        command.Parameters.AddWithValue("p_param_8", oClass.param_8);
        command.Parameters.AddWithValue("p_param_9", oClass.param_9);
        command.Parameters.AddWithValue("p_param_10", oClass.param_10);

        using var reader = await command.ExecuteReaderAsync(CommandBehavior.SingleRow);

        if (reader.HasRows)
        {
            await reader.ReadAsync();

            return new Ent_Param_Ns_Param_Rpta
            {
                origen = reader.IsDBNull(reader.GetOrdinal("parnet_origen")) ? "" : reader.GetString(reader.GetOrdinal("parnet_origen")),
                query = reader.IsDBNull(reader.GetOrdinal("parnet_query")) ? "" : reader.GetString(reader.GetOrdinal("parnet_query")),
            };
        }

        return null;
    }

    public async Task<Ent_Respuesta> Insertar_Log(Ent_Logger oClass)
    {
        using var oCmd = CreateCommand("USP_Insertar_Log");
        oCmd.CommandType = CommandType.StoredProcedure;
        oCmd.Parameters.AddWithValue("p_logPriId", oClass.id_log);
        oCmd.Parameters.AddWithValue("p_controller", oClass.controller);
        oCmd.Parameters.AddWithValue("p_metodo", oClass.metodo);
        oCmd.Parameters.AddWithValue("p_store", oClass.store);
        oCmd.Parameters.AddWithValue("p_parametro", oClass.parametro);
        oCmd.Parameters.AddWithValue("p_parametroNet", oClass.parametro_ns);
        oCmd.Parameters.AddWithValue("p_mensajes", oClass.mensaje);
        oCmd.Parameters.AddWithValue("p_mensajeNet", oClass.mensaje_ns);
        oCmd.Parameters.AddWithValue("p_mensaje", string.Empty).Direction = ParameterDirection.Output;

        await oCmd.ExecuteNonQueryAsync();

        return new Ent_Respuesta
        {
            descripcion = oCmd.Parameters["p_mensaje"].Value.ToString()
        };
    }

    public async Task<List<Ent_Parametros_Produccion>> ObtenerParametrosProduccion()
    {
        var lo_lista = new List<Ent_Parametros_Produccion>();

        using (var command = CreateCommand("USP_ObtenerParametrosProduccion"))
        {
            command.CommandType = CommandType.StoredProcedure;

            using (var reader = await command.ExecuteReaderAsync(CommandBehavior.SingleResult))
            {
                while (await reader.ReadAsync())
                {
                    lo_lista.Add(new Ent_Parametros_Produccion
                    {
                        id_par_prod = reader.IsDBNull(reader.GetOrdinal("par_pro_id")) ? 0 : reader.GetInt32(reader.GetOrdinal("par_pro_id")),
                        origen = reader.IsDBNull(reader.GetOrdinal("Origen")) ? "" : reader.GetString(reader.GetOrdinal("Origen")),
                        nombre = reader.IsDBNull(reader.GetOrdinal("Nombre")) ? "" : reader.GetString(reader.GetOrdinal("Nombre")),
                        valor_numerico = reader.IsDBNull(reader.GetOrdinal("Valor_Numerico")) ? 0 : reader.GetDecimal(reader.GetOrdinal("Valor_Numerico")),
                        valor_alfanumerico = reader.IsDBNull(reader.GetOrdinal("Valor_Alfanumerico")) ? "" : reader.GetString(reader.GetOrdinal("Valor_Alfanumerico"))
                    });
                }
            }
        }
        return lo_lista;
    }
}