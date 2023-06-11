using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tesztpizza
{
    public interface IDatabaseConnectionFactory
    {
        IDbConnection CreateConnection(string connectionString);
    }
}
