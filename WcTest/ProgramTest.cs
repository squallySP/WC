using System;
using System.IO;
using WC;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WcTest
{
    [TestClass]
    public class ProgramTest
    {

        #region CharCount Tests
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CharCountTest1()
        {
            Program.CharCount(" ");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CharCountTest1_1()
        {
            Program.CharCount(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CharCountTest1_2()
        {
            Program.CharCount("__");
        }

        [TestMethod]
        public void CharCountTest2()
        {
            Assert.AreEqual(0, Program.CharCount("/WcTestFiles/0_empty.txt"));
        }

        [TestMethod]
        public void CharCountTest3()
        {
            Assert.AreEqual(0, Program.CharCount("/WcTestFiles/0_space.txt"));
        }

        [TestMethod]
        public void CharCountTest4()
        {
            Assert.AreEqual(0, Program.CharCount("/WcTestFiles/0_tab.txt"));
        }

        [TestMethod]
        public void CharCountTest5()
        {
            Assert.AreEqual(1, Program.CharCount("/WcTestFiles/1_char1.txt"));
        }

        [TestMethod]
        public void CharCountTest6()
        {
            Assert.AreEqual(1, Program.CharCount("/WcTestFiles/1_char2.txt"));
        }

        [TestMethod]
        public void CharCountTest7()
        {
            Assert.AreEqual(5, Program.CharCount("/WcTestFiles/5_char.txt"));
        } 
        #endregion

        #region WordCount Tests
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void WordCountTest1()
        {
            Program.WordCount(" ");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void WordCountTest1_1()
        {
            Program.WordCount(null);
        }

        [TestMethod]
        public void WordCountTest2()
        {
            Assert.AreEqual(0, Program.WordCount("/WcTestFiles/0_empty.txt"));
        }

        [TestMethod]
        public void WordCountTest3()
        {
            Assert.AreEqual(0, Program.WordCount("/WcTestFiles/0_space.txt"));
        }

        [TestMethod]
        public void WordCountTest4()
        {
            Assert.AreEqual(0, Program.WordCount("/WcTestFiles/0_tab.txt"));
        }

        [TestMethod]
        public void WordCountTest5()
        {
            Assert.AreEqual(1, Program.WordCount("/WcTestFiles/1_char1.txt"));
        }

        [TestMethod]
        public void WordCountTest6()
        {
            Assert.AreEqual(1, Program.WordCount("/WcTestFiles/1_char2.txt"));
        }

        [TestMethod]
        public void WordCountTest7()
        {
            Assert.AreEqual(4, Program.WordCount("/WcTestFiles/5_char.txt"));
        }

        [TestMethod]
        public void WordCountTest8()
        {
            Assert.AreEqual(5, Program.WordCount("/WcTestFiles/5_word_5.txt"));
        } 
        #endregion

        #region LineCount Tests
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void LineCountTest1()
        {
            Program.LineCount(" ");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void LineCountTest1_1()
        {
            Program.LineCount(null);
        }

        [TestMethod]
        public void LineCountTest2()
        {
            Assert.AreEqual(0, Program.LineCount("/WcTestFiles/0_empty.txt"));
        }

        [TestMethod]
        public void LineCountTest3()
        {
            Assert.AreEqual(1, Program.LineCount("/WcTestFiles/0_space.txt"));
        }

        [TestMethod]
        public void LineCountTest4()
        {
            Assert.AreEqual(1, Program.LineCount("/WcTestFiles/0_tab.txt"));
        }

        [TestMethod]
        public void LineCountTest5()
        {
            Assert.AreEqual(1, Program.LineCount("/WcTestFiles/1_char1.txt"));
        }

        [TestMethod]
        public void LineCountTest6()
        {
            Assert.AreEqual(1, Program.LineCount("/WcTestFiles/1_char2.txt"));
        }

        [TestMethod]
        public void LineCountTest7()
        {
            Assert.AreEqual(5, Program.LineCount("/WcTestFiles/5_char.txt"));
        }

        [TestMethod]
        public void LineCountTest8()
        {
            Assert.AreEqual(6, Program.LineCount("/WcTestFiles/5_word_5.txt"));
        }

        [TestMethod]
        public void LineCountTest9()
        {
            Assert.AreEqual(6, Program.LineCount("/WcTestFiles/6_line_6.txt"));
        }
        #endregion

        #region GetSubFolders Tests
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetSubFoldersTest1()
        {
            Program.GetSubFolders(" ");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetSubFoldersTest1_1()
        {
            Program.GetSubFolders(null);
        }

        [TestMethod]
        public void GetSubFoldersTest2()
        {
            Assert.AreEqual(3, Program.GetSubFolders("/WcTestFiles/").Count);
        }

        [TestMethod]
        public void GetSubFoldersTest3()
        {
            Assert.AreEqual(2, Program.GetSubFolders("/WcTestFiles/subFolder/").Count);
        }

        [TestMethod]
        public void GetSubFoldersTest4()
        {
            Assert.AreEqual(1, Program.GetSubFolders("/WcTestFiles/subFolder/ssFolder").Count);
        }
        #endregion

        #region GetFiles Tests

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetFilesTest1_1()
        {
            Program.GetFiles(null, "*");
        }

        [TestMethod]
        public void GetFilesTest2()
        {
            List<DirectoryInfo> dirInfos = new List<DirectoryInfo>();
            dirInfos.Add(new DirectoryInfo("/WcTestFiles/"));
            dirInfos.Add(new DirectoryInfo("/WcTestFiles/subFolder/"));
            dirInfos.Add(new DirectoryInfo("/WcTestFiles/subFolder/ssFolder"));
            Assert.AreEqual(17, Program.GetFiles(dirInfos, "*").Count);
        }

        [TestMethod]
        public void GetFilesTest3()
        {
            List<DirectoryInfo> dirInfos = new List<DirectoryInfo>();
            dirInfos.Add(new DirectoryInfo("/WcTestFiles/subFolder/"));
            dirInfos.Add(new DirectoryInfo("/WcTestFiles/subFolder/ssFolder"));
            Assert.AreEqual(6, Program.GetFiles(dirInfos, "*").Count);
        }

        [TestMethod]
        public void GetFilesTest4()
        {
            List<DirectoryInfo> dirInfos = new List<DirectoryInfo>();
            dirInfos.Add(new DirectoryInfo("/WcTestFiles/subFolder/ssFolder"));
            Assert.AreEqual(3, Program.GetFiles(dirInfos, "*").Count);
        }

        [TestMethod]
        public void GetFilesTest5()
        {
            List<DirectoryInfo> dirInfos = new List<DirectoryInfo>();
            dirInfos.Add(new DirectoryInfo("/WcTestFiles/subFolder/"));
            dirInfos.Add(new DirectoryInfo("/WcTestFiles/subFolder/ssFolder"));
            Assert.AreEqual(3, Program.GetFiles(dirInfos, "sub?.txt").Count);
        }

        [TestMethod]
        public void GetFilesTest6()
        {
            List<DirectoryInfo> dirInfos = new List<DirectoryInfo>();
            dirInfos.Add(new DirectoryInfo("/WcTestFiles/subFolder/"));
            dirInfos.Add(new DirectoryInfo("/WcTestFiles/subFolder/ssFolder"));
            Assert.AreEqual(3, Program.GetFiles(dirInfos, "ss?.txt").Count);
        }
        #endregion

        #region LineCountAdvance Tests
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void LineCountAdvanceTest1()
        {
            Program.LineCountAdvance(" ");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void LineCountAdvanceTest1_1()
        {
            Program.LineCountAdvance(null);
        }

        [TestMethod]
        public void LineCountAdvanceTest2()
        {
            var result = Program.LineCountAdvance("/WcTestFiles/6_line_adv1_2_1_3.txt");
            Assert.AreEqual(2, result[0]);
            Assert.AreEqual(1, result[1]);
            Assert.AreEqual(3, result[2]);
        }

        [TestMethod]
        public void LineCountAdvanceTest3()
        {

            var result = Program.LineCountAdvance("/WcTestFiles/6_line_adv2_3_3_3.txt");
            Assert.AreEqual(3, result[0]);
            Assert.AreEqual(3, result[1]);
            Assert.AreEqual(3, result[2]);
        }

        [TestMethod]
        public void LineCountAdvanceTest4()
        {

            var result = Program.LineCountAdvance("/WcTestFiles/6_line_adv3_5_8_4.txt");
            Assert.AreEqual(5, result[0]);
            Assert.AreEqual(8, result[1]);
            Assert.AreEqual(4, result[2]);
        }
        #endregion
    }
}
