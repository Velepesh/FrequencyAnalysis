using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace FrequencyAnalysis
{
    class FrequencyAnalysis
    {
        readonly private string _alphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
        readonly private string _monograms = "оеаинтсрвлкмдпуяыьгзбчйхжшюцщэфъё";
        readonly string _pattern = @"[а-яёА-ЯЁ]";
        readonly int _numberOfTopBigrams = 200;

        private Dictionary<char, int> _frequencies = new Dictionary<char, int>();
        private Dictionary<string, int> _bigrams = new Dictionary<string, int>();
        private Dictionary<string, int> _encryptedBigrams  = new Dictionary<string, int>();
        private List<string> _bigramsTop = new List<string>();
        private List<string> _encryptedBigramsTop  = new List<string>();
        private string _descendingMonograms = "";

        public string DecodeByMonograms(string text)
        {
            CountMonogramsFrequencies(text);

            return GetDecodingByMonograms(text);
        }

        public string DecodeByBigrams(string inputText, string encodeText)
        {
            CountBigramsFrequencies(inputText);
            CountEncryptBigramsFrequencies(encodeText);
            CreateBigramsTop();
            CreateCryptoBigrams();
            string bigramsAlphabet = GetBigramsEncodeAlphabet();

            return GetBigramsDecode(encodeText, bigramsAlphabet);
        }

        private void CountMonogramsFrequencies(string text)
        {
            foreach (var symbol in _alphabet)
            {
                if (Regex.IsMatch(symbol.ToString(), _pattern))
                {
                    _frequencies[symbol] = 0;
                }
            }

            foreach (var symbol in text)
            {
                if (Regex.IsMatch(symbol.ToString(), _pattern))
                {
                    _frequencies[symbol]++;
                }
            }
        }

        private string GetDecodingByMonograms(string text) 
        {
            string monogramsEncrypt = "";

            _frequencies = _frequencies.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

            foreach (var item in _frequencies)
            {
                _descendingMonograms += item.Key;
            }

            foreach (var symbol in text)
            { 
                if (Regex.IsMatch(symbol.ToString(), _pattern))
                {
                    int termIndex = _descendingMonograms.IndexOf(symbol);
                    monogramsEncrypt += _monograms[termIndex];
                }
                else
                {
                    monogramsEncrypt += symbol;
                }
            }

            return monogramsEncrypt;
        }


        private void CountBigramsFrequencies(string text)
        {
            CountFrequenciesForBigrams(text, _bigrams);
        }


        private void CountEncryptBigramsFrequencies(string text)
        {
            CountFrequenciesForBigrams(text, _encryptedBigrams);
        }

        private void CountFrequenciesForBigrams(string text, Dictionary<string, int> bigrams)
        {
            for (int i = 0; i < text.Length - 1; i++)
            {
                if (Regex.IsMatch(text[i].ToString(), _pattern)
                    && Regex.IsMatch(text[i + 1].ToString(), _pattern))
                {
                    string bigram = text.Substring(i, 2).ToLower();

                    if (bigrams.ContainsKey(bigram))
                        bigrams[bigram] += 1;
                    else
                        bigrams.Add(bigram, 1);
                }
            }
        }


        private void CreateBigramsTop()
        {
            CreateTop(_bigrams, _bigramsTop);
        }       

        private void CreateCryptoBigrams()
        {
            CreateTop(_encryptedBigrams , _encryptedBigramsTop);
        }

        private void CreateTop(Dictionary<string, int> bigrams, List<string> topBigrams)
        {
            for (int i = 0; i < _numberOfTopBigrams; i++)
            {
                int max = 0;
                string bigram = "";

                foreach (var item in bigrams)
                {
                    int value = item.Value;
                    if (value > max)
                    {
                        bigram = item.Key;
                        max = value;
                    }
                }

                topBigrams.Add(bigram);
                bigrams.Remove(bigram);
            }
        }

        private string GetBigramsEncodeAlphabet()
        {
            string bigramsAlphabet = _descendingMonograms;
            List<string> swapedLetters = new List<string>();

            for (int i = 0; i < _bigramsTop.Count; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    int a = bigramsAlphabet.IndexOf(_encryptedBigramsTop[i][j]);
                    int b = _monograms.IndexOf(_bigramsTop[i][j]);

                    if (a != b && swapedLetters.Contains(_encryptedBigramsTop[i][j].ToString()) == false)
                    {
                        bigramsAlphabet = SwapLetters(bigramsAlphabet, a, b);

                        swapedLetters.Add(bigramsAlphabet[a].ToString());
                        swapedLetters.Add(bigramsAlphabet[b].ToString());
                    }
                }
            }

            return bigramsAlphabet;
        }

        private string SwapLetters(string str, int a, int b)
        {
            if (a > b)
                (a, b) = (b, a);

            char[] alphabet = str.Select(x => x == str[a] ? str[b] : (x == str[b] ? str[a] : x)).ToArray();

            string result = new string(alphabet);

            return result;
        }

        private string GetBigramsDecode(string text, string bigramsAlphabet)
        {
            string decodingText = "";

            foreach (var symbol in text)
            {
                if (Regex.IsMatch(symbol.ToString(), _pattern))
                {
                    var index = bigramsAlphabet.IndexOf(symbol);

                    decodingText += _monograms[index];
                }
                else
                {
                    decodingText += symbol;
                }
            }

            return decodingText;
        }
    }
}