namespace Model.DTO.v1
{
    public class DTO_Response<T> where T : class
    {
        public bool IsSuccessful { get; set; }

        public string ErrorMessage { get; set; }

        public T Data { get; set; }
    }

    public class DTO_Response
    {
        public bool IsSuccessful { get; set; }

        public string Message { get; set; }

        public object Data { get; set; }
    }
}