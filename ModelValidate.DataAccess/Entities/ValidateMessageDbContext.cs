using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelValidate.DataAccess.Entities
{
    /// <summary>
    /// DB Context
    /// </summary>
    public class ValidateMessageDbContext : DbContext
    {
        public ValidateMessageDbContext(DbContextOptions<ValidateMessageDbContext> options)
            : base(options)
        {
        }

        public DbSet<ValidatedMessage> ValidatedMessages { get; set; }
    }
}
