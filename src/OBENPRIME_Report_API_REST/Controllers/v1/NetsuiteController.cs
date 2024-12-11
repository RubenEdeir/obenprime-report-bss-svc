using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Model;
using Model.DTO.v1;
using OBENPRIME_Netsuite_API_REST.Utils;
using Service;

namespace OBENPRIME_Netsuite_API_REST.Controllers.v1;
[Route("api/v{version:apiVersion}/[Controller]")]
[ApiVersion("1")]
[ApiController]
public class NetsuiteController : ControllerBase
{
	private readonly INetsuiteService _service;
	private readonly IMapper _mapper;
    private readonly MetodosExcel _metodoExcel;
    public NetsuiteController(INetsuiteService NetsuiteService, IMapper mapper, MetodosExcel metodosExcel)
	{
		_service = NetsuiteService;
		_mapper = mapper;
        _metodoExcel = metodosExcel;
    }

    [HttpPost("ObtenerInventarioNetsuite")]
    public async Task<DTO_Response<object>> GetReporteNetsuite([FromBody] DTO_Netsuite_Filtro oClass)
    {
        try
        {
            if (oClass is null) return new DTO_Response<object> { Data = { }, ErrorMessage = "Datos nulos", IsSuccessful = false };

            var lo_filtro = _mapper.Map<Ent_Netsuite_Filtro>(oClass);
            var lo_lista = await _service.GetReporteNetsuite(lo_filtro);
            var lo_lista_dto = _mapper.Map<List<DTO_Netsuite>>(lo_lista);

            return new DTO_Response<object> { Data = lo_lista_dto, IsSuccessful = true };

        }
        catch (Exception err)
        {
            return new DTO_Response<object> { ErrorMessage = err.Message, IsSuccessful = false };
        }
    }

    [HttpPost("ObtenerAlmacenNetsuite")]
    public async Task<DTO_Response<object>> GetAlmacenNetsuite([FromBody] DTO_Auditoria oClass)
    {
        try
        {
            if (oClass is null) return new DTO_Response<object> { Data = { }, ErrorMessage = "Datos nulos", IsSuccessful = false };

            var lo_filtro = _mapper.Map<Ent_Auditoria>(oClass);
            var lo_lista = await _service.GetAlmacenNetsuite(lo_filtro);
            var lo_lista_dto = _mapper.Map<List<DTO_Generico>>(lo_lista);

            return new DTO_Response<object> { Data = lo_lista_dto, IsSuccessful = true };

        }
        catch (Exception err)
        {
            return new DTO_Response<object> { ErrorMessage = err.Message, IsSuccessful = false };
        }
    }
}