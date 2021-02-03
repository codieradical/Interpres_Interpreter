using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace Interpreter.IO
{
    [Serializable]
    public class LocalFileWorkspace : Workspace
    {
        [NonSerialized]
        private string path;

        public string Path { get { return path; } }

        public LocalFileWorkspace(string path, string[] script) : base(script)
        {
            this.path = path;
        }

        public LocalFileWorkspace(string path, string[] script, Dictionary<string, object> variables) : base(script, variables)
        {
            this.path = path;
        }

        public override void SaveWorkspace()
        {
            if (this.path == null)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Interpres workspace (*.ipw)|*.ipw|Interpres script (*.ips)|*.ips";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.ShowDialog();

                this.path = saveFileDialog.FileName;
            }

            Stream file = File.OpenWrite(this.path);
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            if (this.path.EndsWith(".ips"))
                File.WriteAllLines(this.path, script);
            else
                binaryFormatter.Serialize(file, this);

            file.Close();
        }
    }
}
