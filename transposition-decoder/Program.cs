using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace transposition_decoder
{
    class Program
    {
        static void Main(string[] args)
        {
            // ファイル名を受け取る
            StreamReader r = GetReadFilePath();

            List<string> convertedLines = new List<string>();
            // 1行ずつ読み出し。
            string line;
            while ((line = r.ReadLine()) != null)
            {
                // ユニコード変換
                string str = ConvertUnicodeToStr(line);
                convertedLines.Add(str);
            }
            // 転置処理
            List<string> transedLines = Transposition(convertedLines);

            // 改行位置の修正
            string transedStr = "";
            foreach (var str in transedLines)
            {
                transedStr += str;
            }
            string[] splittedStr = transedStr.Split('・');

            // ファイル出力
            StreamWriter w = GetWriteFilePath();
            foreach (string str in splittedStr)
            {
                w.WriteLine(str);
            }
            w.Close();

            // 処理完了
            Console.WriteLine("変換処理が完了しました。任意のキー押下で終了します。");
            Console.ReadKey();
        }

        /// <summary>
        /// 読み込むファイルを得る
        /// </summary>
        /// <returns></returns>
        private static StreamReader GetReadFilePath()
        {
            Console.WriteLine("デコード対象ファイルパスを入力: ");
            StreamReader r;
            while (true)
            {
                string filePath = Console.ReadLine();
                try
                {
                    r = new StreamReader(filePath);
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("対象のファイルが見つかりません。");
                    Console.WriteLine("再入力: ");
                    continue;
                }
                return r;
            }

        }

        /// <summary>
        /// 変換処理後の文字列を書き込むファイルを得る。
        /// (既存のファイルであれば上書きする)
        /// </summary>
        /// <returns></returns>
        private static StreamWriter GetWriteFilePath()
        {
            Console.WriteLine("出力先ファイルパスを入力: ");
            StreamWriter w;
            while (true)
            {
                string filePath = Console.ReadLine();
                try
                {
                    w = new StreamWriter(filePath, false, Encoding.GetEncoding("shift_jis"));
                }
                catch (Exception)
                {
                    Console.WriteLine("入力されたファイルパスが不正です。");
                    Console.WriteLine("再入力: ");
                    continue;
                }

                return w;
            }
        }

        /// <summary>
        /// Unicode文字コードをあらわす文字列を文字に変換する
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private static string ConvertUnicodeToStr(string line)
        {
            string str = "";
            for (int i = 0; i * 4 < line.Length; i++)
            {
                int charCode = Convert.ToInt32(line.Substring(i * 4, 4), 16);
                char c = Convert.ToChar(charCode);
                str += c;
            }

            return str;
        }

        /// <summary>
        /// 転置処理
        /// </summary>
        /// <param name="convertedLines"></param>
        /// <returns></returns>
        private static List<string> Transposition(List<string> convertedLines)
        {
            // 行→列の変換処理
            List<string> transedLines = new List<string>();
            for (int i = 0; i < convertedLines[0].Length; i++)
            {
                string str = "";
                foreach (string convertedLine in convertedLines)
                {
                    str += convertedLine[i];
                }
                transedLines.Add(str);
            }

            return transedLines;
        }
    }
}
