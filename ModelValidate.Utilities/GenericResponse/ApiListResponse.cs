using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelValidate.Utilities.GenericResponse
{
    //Return Custom response from API to UI/Swagger : List of model Data
    public class ApiListResponse<T>
    {
        public bool Success { get; set; }
        public  IEnumerable<T> Data { get; set; }
        public string AlertMessage { get; set; }
        public ApiListResponse()
        {
            Success = true;
        }
    }
}
