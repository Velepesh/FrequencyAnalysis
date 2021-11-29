using System;

namespace FrequencyAnalysis
{
    class Program
    {
        static void Main(string[] args)
        {
            TextReader textReader = new TextReader();
            Caesar caesar = new Caesar();
            FrequencyAnalysis frequencyAnalysis = new FrequencyAnalysis();

            string text = textReader.ReadText();
            string encode = "";

            Console.Write("Ключевое слово: ");
            string keyWord = Console.ReadLine().ToLower();

            Console.Write("Ключ: ");
            int key = Convert.ToInt32(Console.ReadLine());

            caesar.CreateNewAlphabet(keyWord, key);

            Console.WriteLine("Шифрованный алфавит: " + caesar.GetNewAlphabet());
            Console.WriteLine();

            encode = caesar.Encrypt(text);
            textReader.WriteEncodeText(encode);

            textReader.WriteMonogramsDecode(frequencyAnalysis.DecodeByMonograms(encode));
            textReader.WriteBigrams(frequencyAnalysis.DecodeByBigrams(text, encode));
        }
    }
}