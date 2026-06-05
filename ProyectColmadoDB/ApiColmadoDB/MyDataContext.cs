using Microsoft.EntityFrameworkCore;

namespace ApiColmadoDB
{
    public class MyDataContext:DbContext
    {
        public MyDataContext(DbContextOptions<MyDataContext> options) : base(options)
        {

        }
    }
}
