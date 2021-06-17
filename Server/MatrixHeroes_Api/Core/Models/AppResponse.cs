namespace MatrixHeroes_Api.Core.Models
{
    public class AppResponse
    {
        public int StatusCode { get; set; }
        public ResponsePayload ResponsePayload { get; set; }
        public bool IsSuccess => StatusCode < 400;
    }
}