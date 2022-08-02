using System;
using System.Collections;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SolidEdgeDraft;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {

        public string RevNo { get; set; } = "5";
        public string Fecha { get; set; } = "05.08.2022";
        public string Autor { get; set; } = "Test Autor";
        public string Description { get; set; } = "Test Description";

        [TestMethod]
        public void TestMethod1()
        {
           //DraftHelper.AddRevision.Init();

           // try
           // {
           //     ObtenerTablaRevisiones();
           // }
           // catch (Exception ex)
           // {

           //     DraftHelper.Helpers.ShowException(ex);
           // }

           // DraftHelper.AddRevision.Add(RevNo, Fecha, Autor, Description);

           // ObtenerTablaRevisiones();
            //MySolidEdgeAddIn1.MyAddIn.OnRegister(typeof(MySolidEdgeAddIn1.MyAddIn));
        }
       

        
        
    }
}
