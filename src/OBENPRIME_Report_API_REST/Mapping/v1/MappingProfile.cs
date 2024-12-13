using AutoMapper;
using Model;
using Model.DTO.v1;

namespace OBENPRIME_Netsuite_API_REST.MappingProfile;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<DTO_Netsuite_Filtro, Ent_Netsuite_Filtro>();
        CreateMap<DTO_Auditoria, Ent_Auditoria>();

        CreateMap<Ent_Netsuite_Api_Response, DTO_Netsuite_Response>()
            .ForMember(destino => destino.Offset,
            opt => opt.MapFrom(origen => origen.Offset))
            .ForMember(destino => destino.HasMore,
            opt => opt.MapFrom(origen => origen.HasMore))
            .ForMember(destino => destino.Count,
            opt => opt.MapFrom(origen => origen.Count))
            .ForMember(destino => destino.TotalResults,
            opt => opt.MapFrom(origen => origen.TotalResults))
            .ForMember(destino => destino.Data,
            opt => opt.MapFrom(origen => origen.Data));

        CreateMap<Ent_Netsuite, DTO_Netsuite>()
            .ForMember(destino => destino.lote,
            opt => opt.MapFrom(origen => origen.lot))
            .ForMember(destino => destino.id_almacen,
            opt => opt.MapFrom(origen => origen.idalm))
            .ForMember(destino => destino.id_item,
            opt => opt.MapFrom(origen => origen.iditem))
            .ForMember(destino => destino.desc_item,
            opt => opt.MapFrom(origen => origen.item))
            .ForMember(destino => destino.desc_almacen,
            opt => opt.MapFrom(origen => origen.alm))
            .ForMember(destino => destino.stock,
            opt => opt.MapFrom(origen => origen.can));

        CreateMap<Ent_Generico, DTO_Generico>()
           .ForMember(destino => destino.id,
           opt => opt.MapFrom(origen => origen.id))
           .ForMember(destino => destino.descripcion,
           opt => opt.MapFrom(origen => origen.desc));
    }
}