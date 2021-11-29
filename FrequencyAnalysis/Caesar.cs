using System;
using System.Text.RegularExpressions;

namespace FrequencyAnalysis
{
    class Caesar
    {
        readonly private string _alphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
        readonly string _pattern = @"[а-яёА-ЯЁ]";

        private char[] _newAlphabet = new char[33];

        public string Encrypt(string text)
        {
            string result = "";

            foreach (char symbol in text.ToLower())
            {
                if(Regex.IsMatch(symbol.ToString(), _pattern))
                {
                    for (int i = 0; i < _alphabet.Length; i++)
                    {
                        if (symbol == _alphabet[i])
                        {
                            result += _newAlphabet[i];
                            break;
                        }
                    }
                }
                else
                {
                    result += symbol;
                }
            }

            return result;
        }

        private bool IsContainsDuplicates(string keyWord)
        {
            for (int i = 0; i < keyWord.Length - 1; i++)
                for (int j = i + 1; j < keyWord.Length; j++)
                    if (keyWord[i] == keyWord[j])
                        return true;

            return false;
        }

        public void CreateNewAlphabet(string keyWord, int key)
        {
            if (IsContainsDuplicates(keyWord))
                throw new Exception();

            bool isSame = false;
            key--;
            int continuationIndex = -1; 
            int currentIndex = key;

            for (int i = key; i < keyWord.Length + key; i++)
            {
                _newAlphabet[currentIndex] = keyWord[i - key];
                currentIndex++;
            }

            for (int i = 0; i < _alphabet.Length; i++)
            {
                for (int j = 0; j < _newAlphabet.Length; j++)
                {
                    if (_alphabet[i] == _newAlphabet[j])
                    {
                        isSame = true;
                        break;
                    }
                }

                if (isSame == false)
                {
                    _newAlphabet[currentIndex] = _alphabet[i];
                    currentIndex++;
                }

                isSame = false;

                if (currentIndex == _newAlphabet.Length)
                {
                    continuationIndex = i;
                    break;
                }
            }

            currentIndex = 0;

            for (int i = continuationIndex; i < _alphabet.Length; i++)
            {
                for (int j = 0; j < _newAlphabet.Length; j++)
                {
                    if (_alphabet[i] == _newAlphabet[j])
                    {
                        isSame = true;
                        break;
                    }
                }

                if (isSame == false)
                {
                    _newAlphabet[currentIndex] = _alphabet[i];
                    currentIndex++;
                }

                isSame = false;
            }
        }

        public string GetNewAlphabet()
        {
            string newAlphabet = new string(_newAlphabet);

            return newAlphabet;
        }
    }
}