using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Interpreter.IO
{
    public class JSONDataImporter : IDataImporter
    {
        public Dictionary<string, object> ImportData()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON (*.json)|*.json|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.ShowDialog();
            Stream file = openFileDialog.OpenFile();

            StreamReader reader = new StreamReader(file);

            Dictionary<string, object> data = JsonConvert.DeserializeObject<Dictionary<string, object>>(reader.ReadToEnd());
            file.Close();
            return data;
        }
    }
}
