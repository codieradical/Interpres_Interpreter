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
    public class CSVDataImporter : IDataImporter
    {
        public Dictionary<string, object> ImportData()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Comma Seperated Values (*.csv)|*.csv|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.ShowDialog();

            object[][] data = File.ReadAllLines(openFileDialog.FileName).Select(l => (l.Split(',') as object[]).ToArray()).ToArray();

            for (int x = 0; x < data.Length; x++)
            {
                for (int y = 0; y < data[x].Length; y++)
                {
                    double outD = 0;
                    int outI = 0;
                    if (double.TryParse(data[x][y].ToString(), out outD))
                        data[x][y] = outD;
                    else if (int.TryParse(data[x][y].ToString(), out outI))
                        data[x][y] = outI;
                }
            }


            Dictionary<string, object> vars = new Dictionary<string, object>();
            vars.Add(Path.GetFileName(openFileDialog.FileName).Replace(Path.GetExtension(openFileDialog.FileName), ""), data);

            return vars;
        }
    }
}
