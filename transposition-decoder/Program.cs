using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace transposition_decoder
{
    class Program
    {
        static void Main(string[] args)
        {
            // ファイル名を受け取る
            Console.WriteLine("デコード対象ファイルパスを入力: ");
            string filePath = Console.ReadLine();
            StreamReader r = new StreamReader(filePath);
            string line;
            string str = "";
            while ((line = r.ReadLine()) != null) // 1行ずつ読み出し。
            {
                // ユニコード変換
                for (int i = 0; i*4 < line.Length; i++)
                {
                    string charCode = line.Substring(i*4, 4);
                    int charCode16 = Convert.ToInt32(charCode, 16);
                    char c = Convert.ToChar(charCode16);
                    str += c;
                }
            }
            Console.WriteLine(str);

            // 転置
            Console.ReadKey();
        }
    }
}
