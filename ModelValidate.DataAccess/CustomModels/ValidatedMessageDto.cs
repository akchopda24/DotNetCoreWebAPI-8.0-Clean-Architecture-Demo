using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelValidate.DataAccess.CustomModels
{
    /// <summary>
    /// Custom DTO model class
    /// </summary>
    public class ValidatedMessageDto
    {
        public int Id { get; set; }
        public string Message { get; set; }
    }
}
