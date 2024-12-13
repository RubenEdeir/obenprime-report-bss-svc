namespace Model.DTO.v1
{
    public class DTO_Netsuite
    {
        public Int64 id_item { get; set; }
        public decimal stock { get; set; }
        public int id_almacen { get; set; }
        public string lote { get; set; }
        public string desc_item { get; set; }
        public string desc_almacen { get; set; }
        public string id
        {
            get
            {
                return id_item.ToString() + id_almacen.ToString() + lote;
            }
        }
    }

    public class DTO_Paginacion_Ns
    {
        public bool HasMore { get; set; }
        public int Count { get; set; }
        public int Offset { get; set; }
        public int TotalResults { get; set; }
    }

    public class DTO_Netsuite_Response : DTO_Paginacion_Ns
    {
        public List<DTO_Netsuite> Data { get; set; }
    }

    public class DTO_Netsuite_Filtro
    {
        public DateOnly fecha_inicio { get; set; }
        public DateOnly fecha_fin { get; set; }
        public int id_ubicacion { get; set; }
        public Int64 id_producto { get; set; }
        public int id_empresa { get; set; }
        public int id_usuario { get; set; }
        public string lote { get; set; }
        public int pagina { get; set; }
        public int filas { get; set; }
    }

    public class DTO_Auditoria
    {
        public int id_empresa { get; set; }
        public int id_usuario { get; set; }
    }

    public class DTO_Generico
    {
        public Int64 id { get; set; }
        public string descripcion { get; set; }
    }
}
