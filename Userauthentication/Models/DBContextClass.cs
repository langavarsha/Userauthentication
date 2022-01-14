using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Data.SqlClient;
namespace Userauthentication.Models
{
    public class DBContextClass : DbContext
    {
        public IConfiguration Configuration { get; }
        public DBContextClass()
        {

        }
        public DBContextClass(DbContextOptions<DBContextClass> options)
            : base(options)
        {

        }
        public virtual DbSet<User> User { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string s = null;

            if (!optionsBuilder.IsConfigured)
            {
                
                var builder = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory()) // <== compile failing here
              .AddJsonFile("appsettings.json")
                .Build();
                //  optionsBuilder.UseSqlServer("Server=DESKTOP-DDHP53U\\SQLEXPRESS;Database=PucDB;user id=varsha; password=sa;Trusted_Connection=True;");
                s = builder.GetSection("ConnectionStrings:DefaultConnection").Value;
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer(s);




            }
        }
    }
}
