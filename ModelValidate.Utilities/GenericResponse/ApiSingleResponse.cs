using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelValidate.Utilities.GenericResponse
{
    //Return Custom response from API to UI/Swagger : Single model Data
    public class ApiSingleResponse<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public string AlertMessage { get; set; }
        public ApiSingleResponse()
        {
            Success = true;
        }
    }
}
