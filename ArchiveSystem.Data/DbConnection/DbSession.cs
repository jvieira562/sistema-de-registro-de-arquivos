using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace ArchiveSystem.Data.DbConnection
{
    public sealed class DbSession : IDisposable
    {
        public IDbConnection Connection { get; }
        public IDbTransaction Transaction { get; set; }
        //private readonly IConfiguration configuration;

        public DbSession()
        {
            Connection = new SqlConnection("Data Source=localhost; Initial Catalog=Test_ArchiveSystem; Integrated Security=True;");
            //Connection = new SqlConnection("Data Source = VNSRVWNDB003\\INSDEV; Initial Catalog = DevTest_jvieira; Integrated Security = True;"); //SQLDEV\INSDEV
            Connection.Open();
        }
        public void Dispose()
        {
            Connection?.Dispose();
        }
    }
}
