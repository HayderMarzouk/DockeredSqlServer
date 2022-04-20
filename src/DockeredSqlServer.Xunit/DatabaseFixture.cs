using System;
using System.Collections.Generic;
using System.Text;

namespace DockeredSqlServer.Xunit
{
    public class DatabaseFixture: IDisposable
    {
        public DatabaseFixture()
        {

        }


        protected virtual void StartSqlServer()
        {

        }
        protected virtual SqlServerConfiguration ReadConfiguration()
        {
            string mode = Configuration
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
