namespace APICatalogo.DTOs
{
    public class Response<T>
    {
        public bool Valid {  get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }

        public Response()
        {
        }

        public Response(string message, T? data)
        {
            Valid = true;
            Message = message;
            Data = data;
        }

        public Response(string message, bool valid)
        {
            Valid = valid;
            Message = message;
        }

        public static Response<T> Success(string message, T? data)
        {
            if (data == null)
            {
                return new Response<T>(message, true);
            }
            return new Response<T>(message, data); 
        }

        public Response(bool valid, string message, T? data)
        {
            Valid = valid;
            Message = message;
            Data = data;
        }

        public static Response<T> Fail(string message)
        {
            return new Response<T>(message, false);
        }
        public static Response<T> Error(T data)
        {
            return new Response<T>(false, "Ocorreram erros de validação.", data);
        }
    }
}
