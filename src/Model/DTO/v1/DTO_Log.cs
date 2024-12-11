namespace Model.DTO.v1
{
    public class LogPrimeDto
    {
        public int LogPriId { get; set; }
        public string Controller { get; set; }
        public string Metodo { get; set; }
        public string Store { get; set; }
        public string Parametro { get; set; }
        public string ParametroNet { get; set; }
        public string Mensaje { get; set; }
        public string MensajeNet { get; set; }
        public DateTime Fecha { get; set; }
    }


}
