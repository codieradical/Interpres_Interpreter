using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace Interpreter.IO
{
    public class LocalFileService : IFileService
    {
        public Workspace OpenWorkspace()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Interpres workspace (*.ipw)|*.ipw|Interpres script (*.ips)|*.ips|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() != DialogResult.Cancel)
            {
                Stream file = openFileDialog.OpenFile();
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                try
                {
                    object deserialized = binaryFormatter.Deserialize(file);
                    if (deserialized is LocalFileWorkspace)
                    {
                        file.Close();
                        LocalFileWorkspace workspace = deserialized as LocalFileWorkspace;
                        return new LocalFileWorkspace(openFileDialog.FileName, workspace.script, workspace.variables);
                    }
                    else
                    {
                        StreamReader reader = new StreamReader(file);
                        Workspace workspace = new LocalFileWorkspace(openFileDialog.FileName, reader.ReadToEnd().Split(new string[] { "\r\n", "\r", "\n" }, System.StringSplitOptions.RemoveEmptyEntries));
                        file.Close();
                        return workspace;
                    }
                }
                catch
                {
                    StreamReader reader = new StreamReader(file);
                    Workspace workspace = new LocalFileWorkspace(openFileDialog.FileName, reader.ReadToEnd().Split(new string[] { "\r\n", "\r", "\n" }, System.StringSplitOptions.RemoveEmptyEntries));
                    file.Close();
                    return workspace;
                }
            }

            throw new IOException("File open cancelled.");
        }

        public void SaveWorkspace(Workspace workspace)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Interpres workspace (*.ipw)|*.ipw|Interpres script (*.ips)|*.ips";
            saveFileDialog.FilterIndex = 1;
            if (saveFileDialog.ShowDialog() != DialogResult.Cancel)
            {
                Stream file = saveFileDialog.OpenFile();
                BinaryFormatter binaryFormatter = new BinaryFormatter();

                if (saveFileDialog.FileName.EndsWith(".ips"))
                    File.WriteAllLines(saveFileDialog.FileName, workspace.script);
                else
                    binaryFormatter.Serialize(file, workspace);

                file.Close();
            }
        }
    }
}
