 using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TokenServiceAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
       // IdentityDbContext has the idententity of the user. in this class we are giving the connectection string
       //to the Tokenserver from the sqlserver(which has all the user information).we are injecting where the database is going tobe
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    }
}
