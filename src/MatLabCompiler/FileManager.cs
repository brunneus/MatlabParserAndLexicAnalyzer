using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Xml;

namespace MatLabCompiler
{
    public class FileManager
    {
        private FileManager()
        {
        }

        private static FileManager _instance;
        public static FileManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new FileManager();

                return _instance;
            }
        }

        public string Load(string filePath)
        {
            StreamReader reader = new StreamReader(filePath);
            return reader.ReadToEnd();
        }

        public void Save(string text, string filePath)
        {
            StreamWriter writer = new StreamWriter(filePath);
            writer.Write(text);
            writer.Close();
        }
    }
}
