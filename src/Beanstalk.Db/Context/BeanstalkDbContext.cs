using Beanstalk.Db.Models;
using Microsoft.EntityFrameworkCore;

namespace Beanstalk.Db.Context
{
    public class BeanstalkDbContext: DbContext
    {
        public BeanstalkDbContext(DbContextOptions<BeanstalkDbContext> opts) : base(opts)
        {
        }

        public DbSet<Team> Teams { get; set; }
    }
}