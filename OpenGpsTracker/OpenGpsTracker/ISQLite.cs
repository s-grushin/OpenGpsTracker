using System;
using System.Collections.Generic;
using System.Text;

namespace OpenGpsTracker
{
    public interface ISQLite
    {
        string GetDatabasePath(string filename);
    }
}
