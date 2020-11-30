using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace HttpListenerProject
{
    class HTMLLoader
    {
        public static string LoadHTML()
        {
            try
            {
                string str = "";
                using (FileStream FileOpenerReader = File.OpenRead("pages\\index.html"))
                {

                    byte[] textArray = new byte[FileOpenerReader.Length];
                    UTF8Encoding temp = new UTF8Encoding(true);
                    if (FileOpenerReader.Read(textArray, 0, textArray.Length) > 0)
                    {
                        str = temp.GetString(textArray);                      
                    }
                    else
                    {
                        FileOpenerReader.Close();
                    }
                }
                return str;
            }
            catch (Exception e)
            {
                Console.WriteLine("Не удалось открыть файл");
            }
            return "";
        }
    }
}
