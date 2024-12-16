using Model;
using UnitOfWork_Interface;
using obenprime_netsuite_bss_svc.Service;
using Newtonsoft.Json;
using obenprime_netsuite_bss_svc.Entitie;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace Service
{
    public interface INetsuiteService
    {
        Task<List<Ent_Netsuite>> GetReporteNetsuite(Ent_Netsuite_Filtro oClass);
        Task<List<Ent_Generico>> GetAlmacenNetsuite(Ent_Auditoria oClass);
    }

    public class NetsuiteService : INetsuiteService
    {
        private readonly IUnitOfWork _unitOfWork;
        public NetsuiteService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Ent_Netsuite>> GetReporteNetsuite(Ent_Netsuite_Filtro oClass)
        {
            using var context = _unitOfWork.Create();
            var lo_token = await context.Repositories.HelperRepository.ObtenerToken("NetSuite");
            Ent_Netsuite_Api_Response lo_entidad = new Ent_Netsuite_Api_Response();
            List<Ent_Netsuite> lo_return_lista = new List<Ent_Netsuite>();
            int li_vuelta = 1;
            bool lb_repetir = false;

            var lo_dataParametros = await context.Repositories.HelperRepository.ObtenerParametrosNetSuitePlus(new Ent_Param_Ns_Param_Filtro
            {
                origen = "GetStockGeneralNetsuite",
                id_empresa = oClass.id_empresa,
                id_usuario = oClass.id_usuario,
                param_1 = oClass.id_empresa,
                param_2 = oClass.id_producto,
                param_3 = oClass.id_ubicacion,
                param_6 = oClass.lote,
                param_9 = oClass.fecha_inicio,
                param_10 = oClass.fecha_fin
            });

            if (string.IsNullOrEmpty(lo_dataParametros.query))
            {
                return null;
            }

            var lo_modelo_query = new { q = lo_dataParametros.query };
            var lo_rpta_query = await Netsuite.Generate_QueryGeneralPlus(lo_modelo_query, lo_token.Token, oClass.pagina, oClass.filas);

            if (!lo_rpta_query.IsSuccessful)
            {
                return null;
            }

            var options = new JsonSerializerOptions()
            {
                NumberHandling = JsonNumberHandling.AllowReadingFromString |
                JsonNumberHandling.WriteAsString
            };

            var ls_json = Convert.ToString(lo_rpta_query.Data);
            var lo_rpta = System.Text.Json.JsonSerializer.Deserialize<Ent_Netsuite_Api_Response>(ls_json, options);

            lo_return_lista.AddRange(lo_rpta.Data);
            lb_repetir = lo_rpta.HasMore;

            while (lb_repetir)
            {
                lo_rpta_query = await Netsuite.Generate_QueryGeneralPlus(lo_modelo_query, lo_token.Token, li_vuelta * 1000, oClass.filas);
                li_vuelta++;

                if (!lo_rpta_query.IsSuccessful)
                {
                    break;
                }

                ls_json = Convert.ToString(lo_rpta_query.Data);
                lo_rpta = System.Text.Json.JsonSerializer.Deserialize<Ent_Netsuite_Api_Response>(ls_json, options);
                lo_return_lista.AddRange(lo_rpta.Data);
                lb_repetir = lo_rpta.HasMore;
            }
           
            return lo_return_lista;
        }

        public async Task<List<Ent_Generico>> GetAlmacenNetsuite(Ent_Auditoria oClass)
        {
            using var context = _unitOfWork.Create();
            var lo_token = await context.Repositories.HelperRepository.ObtenerToken("NetSuite");
            List<Ent_Generico> lo_lista = new List<Ent_Generico>();

            var lo_dataParametros = await context.Repositories.HelperRepository.ObtenerParametrosNetSuitePlus(new Ent_Param_Ns_Param_Filtro
            {
                origen = "GetAlmacenesNetsuite",
                id_empresa = oClass.id_empresa,
                id_usuario = oClass.id_usuario,
                param_1 = oClass.id_empresa,
            });

            if (string.IsNullOrEmpty(lo_dataParametros.query))
            {
                return lo_lista;
            }

            var lo_modelo_query = new { q = lo_dataParametros.query };
            var lo_rpta_query = await Netsuite.Generate_QueryGeneral(lo_modelo_query, lo_token.Token);

            if (!lo_rpta_query.IsSuccessful)
            {
                return lo_lista;
            }

            var options = new JsonSerializerOptions()
            {
                NumberHandling = JsonNumberHandling.AllowReadingFromString |
                JsonNumberHandling.WriteAsString
            };

            var ls_json = Convert.ToString(lo_rpta_query.Data);
            var lo_rpta = System.Text.Json.JsonSerializer.Deserialize<Ent_Generico_Api_Response>(ls_json, options);

            return lo_rpta.Data;
        }
    }
}
