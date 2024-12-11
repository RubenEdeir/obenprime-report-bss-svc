namespace Model
{
    public class Ent_Netsuite
    {
        public Int64 iditem { get; set; }
        public decimal can { get; set; }
        public int idalm { get; set; }
        public string lot { get; set; }
        public string item { get; set; }
        public string alm { get; set; }
    }

    public class Ent_Generico
    {
        public Int64 id { get; set; }
        public string desc { get; set; }
    }

    public class Ent_Header_Response_Netsuite
    {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
    }

    public class Ent_Netsuite_Api_Response : Ent_Header_Response_Netsuite
    {
        public List<Ent_Netsuite> Data { get; set; }
    }

    public class Ent_Generico_Api_Response : Ent_Header_Response_Netsuite
    {
        public List<Ent_Generico> Data { get; set; }
    }

    public class Ent_Netsuite_Filtro
    {
        public DateOnly fecha_inicio { get; set; }
        public DateOnly fecha_fin { get; set; }
        public int id_ubicacion { get; set; }
        public int id_empresa { get; set; }
        public int id_usuario { get; set; }
        public Int64 id_producto { get; set; }
        public string lote { get; set; }
        public int pagina { get; set; }
        public int filas { get; set; }
    }

    public class Ent_Auditoria
    {
        public int id_empresa { get; set; }
        public int id_usuario { get; set; }
    }
}
