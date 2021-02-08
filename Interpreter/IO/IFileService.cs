using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.IO
{
    interface IFileService
    {
        Workspace OpenWorkspace();
        void SaveWorkspace(Workspace workspace);
    }
}
