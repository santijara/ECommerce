namespace ECommerce.Application.Common
{
    /// <summary>
    /// Representa una respuesta estándar de la API.
    /// 
    /// Encapsula el estado de la operación, un mensaje descriptivo
    /// y los datos resultantes.
    /// 
    /// Se utiliza como contrato uniforme hacia el cliente
    /// para mantener consistencia en las respuestas HTTP.
    /// </summary>


    public class ApiResponse<T>
    {
        public bool State { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public ApiResponse(bool state, string message, T data)
        {
            State = state;
            Message = message;
            Data = data;
        }

        public static ApiResponse<T>Ok(T data) => new(true, string.Empty, data);
        public static ApiResponse<object> Fail(string message, object? data = null) => new(false, message, data);
    }

    
}
