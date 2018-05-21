using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer
{
    public interface IDBConnection
    {
        string connectionString { get; set; }
    }
}
