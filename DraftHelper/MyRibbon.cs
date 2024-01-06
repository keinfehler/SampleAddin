using SolidEdgeCommunity.AddIn;
using SolidEdgeCommunity.Extensions; // https://github.com/SolidEdgeCommunity/SolidEdge.Community/wiki/Using-Extension-Methods
using SolidEdgeDraft;
using SolidEdgeFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DraftHelper
{
    public class Helpers
    {
        public static void ShowException(string ex)
        {
            Except(ex);
        }
        public static void ShowException(Exception ex)
        {
            Except(ex.Message);
        }
        public static void Except(string ex)
        {
            MessageBox.Show(ex);
        }

    }
    class MyRibbon : SolidEdgeCommunity.AddIn.Ribbon
    {
        private RibbonButton _button1;

        public MyRibbon()
        {
            // Get a reference to the current assembly. This is where the ribbon XML is embedded.
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            // Note: In this example, the ribbon XML file has a build action of "Embedded Resource".
            var _embeddedResourceName = String.Format("{0}.xml", this.GetType().FullName);
            this.LoadXml(assembly, _embeddedResourceName);

            // Example of how to bind a local variable to a particular ribbon control.
            _button1 = GetButton(0);

            // Example of how to bind a particular ribbon control click event.
            //_button1.Click += _button1_Click;
        }

        public override void OnControlClick(RibbonControl control)
        {
            var application = MyAddIn.Instance.Application;

            // Demonstrate how to handle commands without binding to a local variable.
            switch (control.CommandId)
            {
                case 0:
                    {
                        var frm = new ARevision();
                        frm.ShowDialog();
                        
                    }
                    break;

                case 1:
                    {
                        System.Diagnostics.Process.Start("X:/CAD/CONFIGURACIONES/SOLID/macros/08-SyN-CambioFondos/08 - FormatoPieza/PlugIns/CambioCajetines.exe"); //ubicacion donde esta el ejecutable)

                    }
                    break;

                case 2:
                    {
                        System.Diagnostics.Process.Start("X:/CAD/CONFIGURACIONES/SOLID/macros/08-SyN-CambioFondos/08 - FormatoConjunto/PlugIns/CambioCajetines.exe"); //ubicacion donde esta el ejecutable)

                    }
                    break;

                case 3:
                    {
                        System.Diagnostics.Process.Start("X:/CAD/CONFIGURACIONES/SOLID/macros/08-SyN-CambioFondos/08 - FormatoComercial/PlugIns/CambioCajetines.exe"); //ubicacion donde esta el ejecutable)

                    }
                    break;

                case 4:
                    {
                        System.Diagnostics.Process.Start("X:/CAD/CONFIGURACIONES/SOLID/macros/08-SyN-CambioFondos/08 - FormatoReplanteo/PlugIns/CambioCajetines.exe"); //ubicacion donde esta el ejecutable)

                    }
                    break;

                case 5:
                    {
                        System.Diagnostics.Process.Start("X:/CAD/CONFIGURACIONES/SOLID/macros/08-SyN-CambioFondos/08 - FormatoPieza/PlugIns/Conversion.exe"); //ubicacion donde esta el ejecutable)

                    }
                    break;

                case 6:
                    {
 
                        var activeDocument = SolidEdgeAddIn.Instance.Application.ActiveDocument as SolidEdgeDocument;

                        var activeDocumentFullName = activeDocument.FullName;
                        var activeDocumentDirectoryName = System.IO.Path.GetDirectoryName(activeDocumentFullName);

                        System.Diagnostics.Process.Start(activeDocumentDirectoryName);

                    }
                    break;
            }
        }

        //void _button1_Click(RibbonControl control)
        //{
        //    var nativeWindow = MyAddIn.Instance.Application.GetNativeWindow();
        //    System.Windows.Forms.MessageBox.Show(nativeWindow, control.Label, "RibbonControl", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //}
    }

    
}
