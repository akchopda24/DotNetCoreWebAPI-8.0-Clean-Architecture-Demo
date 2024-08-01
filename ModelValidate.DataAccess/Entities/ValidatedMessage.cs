using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelValidate.DataAccess.Entities
{
    /// <summary>
    /// In-Memory DB entity model
    /// </summary>
    public class ValidatedMessage
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }
    }
}
