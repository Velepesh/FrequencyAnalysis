using System;
using System.IO;

namespace FrequencyAnalysis
{
    class TextReader
    {
        readonly private string _inputPath = @"..\..\..\Input.txt";
        readonly private string _encodePath = @"..\..\..\Encode.txt";
        readonly private string _monogramPath = @"..\..\..\MonogramsDecode.txt";
        readonly private string _bigramPath = @"..\..\..\BigramsDecode.txt";

        public string ReadText()
        {
            string text = "";

            try
            {
                using (StreamReader streamReader = new StreamReader(_inputPath))
                {
                    text = streamReader.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return text;
        }

        public void WriteEncodeText(string text)
        {
            WriteText(_encodePath, text);
        }

        public void WriteMonogramsDecode(string text)
        {
            WriteText(_monogramPath, text);
        }

        public void WriteBigrams(string text)
        {
            WriteText(_bigramPath, text);
        }

        private void WriteText(string path, string text)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.Default))
                {
                    sw.WriteLine(text);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}