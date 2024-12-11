namespace Model
{
    public class Ent_Param_Ns_Token
    {
        public string Token { get; set; }
    }

    public class Ent_Param_Ns_Param_Rpta
    {
        public int id_parametro { get; set; }
        public string origen { get; set; }
        public string query { get; set; }
    }

    public class Ent_Param_Ns_Param_Filtro
    {
        public string origen { get; set; }
        public int id_empresa { get; set; }
        public int id_usuario { get; set; }
        public Int64? param_1 { get; set; }
        public Int64? param_2 { get; set; }
        public Int64? param_3 { get; set; }
        public Int64? param_4 { get; set; }
        public string param_5 { get; set; }
        public string param_6 { get; set; }
        public string param_7 { get; set; }
        public string param_8 { get; set; }
        public DateOnly? param_9 { get; set; }
        public DateOnly? param_10 { get; set; }
    }

    public class Ent_Parametros_Produccion
    {
        public int id_par_prod { get; set; }
        public string origen { get; set; }
        public string nombre { get; set; }
        public decimal? valor_numerico { get; set; }
        public string valor_alfanumerico { get; set; }

    }
}
