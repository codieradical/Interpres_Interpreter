using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.IO
{
    interface IDataImporter
    {
        public Dictionary<string, object> ImportData();
    }
}
