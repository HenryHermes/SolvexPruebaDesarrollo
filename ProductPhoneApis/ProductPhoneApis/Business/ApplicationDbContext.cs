using Microsoft.EntityFrameworkCore;

namespace ProductPhoneApis.Business
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

    }
}
