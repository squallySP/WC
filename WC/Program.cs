using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace WC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string[] fileDir = new string[2] {null, null};
            bool charCount = false;
            bool wordCount = false;
            bool lineCount = false;
            bool subFolder = false;
            bool advance = false;
            bool window = false;
            int fileDirNum = 0;

            #region 解析命令参数
            foreach (string word in args)
            {
                if (word.Substring(0, 1) == "-")
                {
                    if (word == "-c") charCount = true;
                    else if (word == "-w") wordCount = true;
                    else if (word == "-l") lineCount = true;
                    else if (word == "-s") subFolder = true;
                    else if (word == "-a") advance = true;
                    else if (word == "-x") window = true;
                    else
                    {
                        Console.WriteLine("参数错误： " + word);
                        return;
                    }
                }
                else
                {
                    if (fileDirNum < 2)
                    {
                        fileDir[fileDirNum] = word;
                        fileDirNum += 1;
                    }
                    else
                    {
                        Console.WriteLine("文件名或路径错误： " + fileDir + ", " + word);
                        return;
                    }

                }
            } 
            #endregion

            #region check the fileDir is valid
            //try
            //{
            //    StreamReader sr = new StreamReader(fileDir);
            //}
            //catch
            //{
            //    Console.WriteLine("filename or directory error! ");
            //    return;
            //} 
            #endregion

            #region 执行操作
            string fileName = fileDir[0] + fileDir[1];
            if (charCount) Console.WriteLine("charCount: " + CharCount(fileName));
            if (wordCount) Console.WriteLine("wordCount: " + WordCount(fileName));
            if (lineCount) Console.WriteLine("lineCount: " + LineCount(fileName)); 
            #endregion

            Console.Read();
        }

        public static int CharCount(string fileName)
        {
            //
            int result = 0;
            try
            {
                StreamReader sr_try = new StreamReader(fileName);
                sr_try.Close();
            }
            catch
            {
                throw new ArgumentException();
            }
            StreamReader sr = new StreamReader(fileName);
            string line = sr.ReadLine();
            while(line != null){
                foreach (char c in line)
                {
                    if (c == ' ' || c == '\t')
                        result += 0;
                    else
                        result += 1;
                }
                line = sr.ReadLine();
            }
            sr.Close();
            return result;
        }

        public static int WordCount(string fileName)
        {
            
            //
            int result = 0;
            try
            {
                StreamReader sr_try = new StreamReader(fileName);
                sr_try.Close();
            }
            catch
            {
                throw new ArgumentException();
            }
            StreamReader sr = new StreamReader(fileName);
            string line = sr.ReadLine();
            while (line != null)
            {
                bool inWord = false;
                foreach (char c in line)
                {
                    if (c == ' ' || c == '\t')
                        inWord = false;
                    else if (!inWord)
                    {
                        result += 1;
                        inWord = true;
                    }
                        
                }
                line = sr.ReadLine();
            }
            sr.Close();
            return result;
        }

        public static int LineCount(string fileName)
        {
            //
            int result = 0;
            try
            {
                StreamReader sr_try = new StreamReader(fileName);
                sr_try.Close();
            }
            catch
            {
                throw new ArgumentException();
            }
            StreamReader sr = new StreamReader(fileName);
            string line = sr.ReadLine();
            while (line != null)
            {
                result += 1;
                line = sr.ReadLine();
            }
            sr.Close();
            return result;
        }

        public static int[] LineCountAdvance(string fileName)
        {
            // int[0]空行数  int[1]注释行数  int[2]代码行数量
            int[] result = {0,0,0};
            try
            {
                StreamReader sr_try = new StreamReader(fileName);
                sr_try.Close();
            }
            catch
            {
                throw new ArgumentException();
            }
            StreamReader sr = new StreamReader(fileName);
            string line = sr.ReadLine();
            bool InCommentBlock = false;
            while (line != null)
            {
                bool isComment = false;
                bool isCode = false;
                if (!InCommentBlock)
                {
                    //int codeEnd = line.Length;
                    string code = line;

                    int indexOfFF = code.IndexOf("//");
                    if (indexOfFF > -1)
                    {
                        isComment = true;
                        code = code.Substring(0, indexOfFF);
                    }
                    int indexOfFS = code.IndexOf("/*");
                    if (indexOfFS > -1)
                    {
                        isComment = true;
                        InCommentBlock = true;
                        code = code.Substring(0, indexOfFS);

                        string comment = line.Substring(indexOfFS+2);
                        if (comment.IndexOf("*/") > -1)
                            InCommentBlock = false;
                    }
                    isCode = LineHasCode(code);
                }
                else
                {
                    isComment = true;
                    if (line.Length == 0)
                        isComment = false;
                    else if (line.IndexOf("*/")>-1)
                    {
                        InCommentBlock = false;
                    }
                }
                
                
                if (isCode)
                    result[2] += 1;
                else if (isComment)
                    result[1] += 1;
                else
                    result[0] += 1;
                
                line = sr.ReadLine();
            }
            sr.Close();
            return result;
        }

        public static bool LineHasCode(string code)
        {
            int codeCharNum = 0;
            foreach(Char c in code){
                if(c!=' '&&c!='\t')
                    codeCharNum += 1;
            }
            if (codeCharNum > 1)
                return true;
            else
                return false;
        }

        public static List<DirectoryInfo> GetSubFolders(string dir)
        {
            
            if (!Directory.Exists(dir))
                throw new ArgumentException();

            List<DirectoryInfo> result = new List<DirectoryInfo>();
            DirectoryInfo dirInfo = new DirectoryInfo(dir);
            result.Add(dirInfo);
            result = GetSubDirs(dirInfo, result);
            
            return result;
        }
        public static List<DirectoryInfo> GetSubDirs(DirectoryInfo dirInfo, List<DirectoryInfo> result)
        {
            foreach (DirectoryInfo subDirInfo in dirInfo.GetDirectories())
            {
                result.Add(subDirInfo);
                result = GetSubDirs(subDirInfo, result);
            }
            return result;
        }
        
        // 获取制定路径下的文件，支持文件通配符
        public static List<FileInfo> GetFiles(List<DirectoryInfo> dirInfos , string fileName)
        {
            List<FileInfo> result = new  List<FileInfo>();
            foreach(DirectoryInfo dirInfo in dirInfos){
                foreach (FileInfo fileInfo in dirInfo.GetFiles(fileName))
                {
                    result.Add(fileInfo);
                }
            }
            return result;
        }

    }
}
