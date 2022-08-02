using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            MySolidEdgeAddIn1.MyAddIn.OnRegister(typeof(MySolidEdgeAddIn1.MyAddIn));
        }
    }
}
