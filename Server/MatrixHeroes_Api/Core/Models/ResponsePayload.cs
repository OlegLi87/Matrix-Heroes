using System.Collections.Generic;

namespace MatrixHeroes_Api.Core.Models
{
    public class ResponsePayload
    {
        public IEnumerable<string> Errors { get; set; }
        public object ResponseObj { get; set; }
    }
}