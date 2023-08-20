using System.Collections.Generic;
using System.IO;
using System;
using System.Windows.Forms;
using System.Drawing;

namespace Wordly
{
    public static class Word
    {
    
        public static Dictionary<int, int> WordsCount = new Dictionary<int, int>();
        public static string filename = Path.Combine(Application.StartupPath, "turkish_word_list.txt");
        public static string folderpath = Path.Combine(Application.StartupPath, "word_lists");
        public static void CreateWordFiles()
        {
            if (!Directory.Exists(folderpath))
            {
                Directory.CreateDirectory(folderpath);
              
 

            
                using (StreamReader sr = new StreamReader(filename))
                {
             

                
                    Console.WriteLine("merhaba dünya");
                    string data = sr.ReadToEnd();
                    string[] lines = data.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (string line in lines)
                    {
                        int wordCount = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length;

                        if (wordCount == 1)
                        {
                            int letterCount = line.Length;
                            string fileNameWithLetterCount = $"{letterCount}LetterWords.txt";
                            string filePathWithLetterCount = Path.Combine(folderpath, fileNameWithLetterCount);

                            using (StreamWriter sw = new StreamWriter(filePathWithLetterCount, true))
                            {
                                sw.WriteLine(line);
                            }

                            // Kelime sayılarını güncelle
                            if (WordsCount.ContainsKey(letterCount))
                            {
                                WordsCount[letterCount]++;
                            }
                            else
                            {
                                WordsCount.Add(letterCount, 1);
                            }
                        }
                    }

                    foreach (var wordcount in WordsCount)
                    {
                        if (wordcount.Value < 100)
                        {
                            string fileNameWithLetterCount = $"{wordcount.Key}LetterWords.txt";
                            string filePathWithLetterCount = Path.Combine(folderpath, fileNameWithLetterCount);
                            File.Delete(filePathWithLetterCount);
                            Console.WriteLine($"{filePathWithLetterCount} dosyası silindi.");
                        }
                    }





                    
                }
            }
        }
    }
}
