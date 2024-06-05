namespace bruno.Klir.WebApi.Common
{
    public class CustomResponse
    {
        public bool Success { get; set; }
        public dynamic? Data { get; set; }
        public IEnumerable<string> Errors { get; set; } = Enumerable.Empty<string>();

        public static CustomResponse SuccessResponse() => new CustomResponse
        {
            Success = true
        };

        public static CustomResponse SuccessResponse(dynamic data) => new CustomResponse
        {
            Data = data,
            Success = true,
        };

        public static CustomResponse ErrorResponse(IEnumerable<string> errors) => new CustomResponse
        {
            Errors = errors
        };
    }
}
