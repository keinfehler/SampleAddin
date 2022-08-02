using System;
using System.Windows.Forms;
using System.Collections;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic.CompilerServices;
using SolidEdgeDraft;

namespace DraftHelper
{
    public partial class ARevision : Form
    {
        public ARevision()
        {
            InitializeComponent();
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);

            AddRevision.Init();


            try
            {
                txtFecha.Value = DateTime.Now.Date;
                txtAutor.Text = Environment.UserName;

                ObtenerTablaRevisiones();
            }
            catch (Exception ex)
            {

                DraftHelper.Helpers.ShowException(ex);
            }

            

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddRevision.Add(txtRevNumber.Text, txtFecha.Text, txtAutor.Text, txtDescripcion.Text);

            ObtenerTablaRevisiones();
        }

        public void ObtenerTablaRevisiones()
        {
            // ISSUE: variable of a compiler-generated type
            Tables tables = DraftHelper.AddRevision.objDraftDoc.Tables;
            bool flag = false;
            int count1 = tables.Count;
            int num1 = 1;
            while (num1 <= count1)
            {
                // ISSUE: reference to a compiler-generated method
                int count2 = tables.Item((object)num1).Titles.Count;
                int num2 = 1;
                while (num2 <= count2)
                {
                    // ISSUE: reference to a compiler-generated method
                    // ISSUE: reference to a compiler-generated method
                    if (string.Compare(tables.Item((object)num1).Titles.Item((object)num2).value, "Histórico de Revisiones", false) == 0)
                    {
                        // ISSUE: reference to a compiler-generated method
                        DraftHelper.AddRevision.tablarevisiones = tables.Item((object)num1);
                        flag = true;
                        break;
                    }
                    checked { ++num2; }
                }
                checked { ++num1; }
            }
            if (!flag)
            {
                DraftHelper.AddRevision.activar = false;
                DraftHelper.Helpers.ShowException("Abra un documento de Solid Edge Plano válido\r\r      (Edición 6 o superior)");
                //int num2 = (int)Interaction.MsgBox((object)"Abra un documento de Solid Edge Plano válido\r\r      (Edición 6 o superior)", MsgBoxStyle.Critical, (object)null);
            }
            else
                ObtenerUltimaRevision();
        }

        public void ObtenerUltimaRevision()
        {
            int count = DraftHelper.AddRevision.tablarevisiones.Rows.Count;
            int num1 = 1;
            while (num1 <= count)
            {
                // ISSUE: reference to a compiler-generated method
                if (string.Compare(DraftHelper.AddRevision.tablarevisiones.get_Cell((object)num1, (object)1).value, "", false) == 0)
                {
                    if (num1 == 1)
                    {
                        ObtenerAutorFecha();
                        // ISSUE: reference to a compiler-generated method
                        DraftHelper.AddRevision.tablarevisiones.get_Cell((object)num1, (object)1).value = "00";
                        // ISSUE: reference to a compiler-generated method
                        DraftHelper.AddRevision.tablarevisiones.get_Cell((object)num1, (object)2).value = DraftHelper.AddRevision.FechaPlano;
                        // ISSUE: reference to a compiler-generated method
                        DraftHelper.AddRevision.tablarevisiones.get_Cell((object)num1, (object)3).value = DraftHelper.AddRevision.AutorPlano;
                        // ISSUE: reference to a compiler-generated method
                        DraftHelper.AddRevision.tablarevisiones.get_Cell((object)num1, (object)4).value = "Creación del Plano";
                        // ISSUE: reference to a compiler-generated method
                        DraftHelper.AddRevision.tablarevisiones.Update();


                        txtRevNumber.Text = "01";

                        DraftHelper.AddRevision.AutorPlano = (string)null;
                        DraftHelper.AddRevision.FechaPlano = (string)null;
                        break;
                    }
                    // ISSUE: reference to a compiler-generated method


                    txtRevNumber.Text = string.Format("{0:00}", (object)(Convert.ToDouble(DraftHelper.AddRevision.tablarevisiones.get_Cell((object)checked(num1 - 1), (object)1).value) + 1.0));
                    break;
                }
                if (num1 == DraftHelper.AddRevision.tablarevisiones.Rows.Count)
                {
                    txtRevNumber.Text = "##";
                    add_button.Enabled = false;
                    txtFecha.Enabled = false;
                    txtAutor.Enabled = false;
                    txtDescripcion.Enabled = false;


                    DraftHelper.AddRevision.activar = false;
                    DraftHelper.Helpers.ShowException("Se ha alcanzado el número máximo de revisiones");
                    //int num2 = (int)Interaction.MsgBox((object)"Se ha alcanzado el número máximo de revisiones", MsgBoxStyle.Critical, (object)"Número máximo de revisiones alcanzado");
                }
                checked { ++num1; }
            }
            if (string.Compare(txtRevNumber.Text, "01", false) == 0)
                btnDelRevision.Enabled = false;
            else
                btnDelRevision.Enabled = true;
        }

        public static void ObtenerAutorFecha()
        {
            try
            {
                // ISSUE: reference to a compiler-generated method
                foreach (object balloon in (IEnumerable)DraftHelper.AddRevision.objDraftDoc.Sheets.Item((object)"DFT").Background.Balloons)
                {
                    object objectValue = RuntimeHelpers.GetObjectValue(balloon);
                    if (Operators.ConditionalCompareObjectEqual(NewLateBinding.LateGet(objectValue, (Type)null, "BalloonText", new object[0], (string[])null, (Type[])null, (bool[])null), (object)"%{Autores|R1}", false))
                        DraftHelper.AddRevision.AutorPlano = Conversions.ToString(NewLateBinding.LateGet(objectValue, (Type)null, "BalloonDisplayedText", new object[0], (string[])null, (Type[])null, (bool[])null));
                    if (Operators.ConditionalCompareObjectEqual(NewLateBinding.LateGet(objectValue, (Type)null, "BalloonText", new object[0], (string[])null, (Type[])null, (bool[])null), (object)"%{FechaPlano|R1}", false))
                        DraftHelper.AddRevision.FechaPlano = Conversions.ToString(NewLateBinding.LateGet(objectValue, (Type)null, "BalloonDisplayedText", new object[0], (string[])null, (Type[])null, (bool[])null));
                }
            }
            finally
            {
                //IEnumerator enumerator;
                //if (enumerator is IDisposable)
                //    (enumerator as IDisposable).Dispose();
            }
        }

        private void BtnDelRevision_Click(object sender, EventArgs e)
        {
            DraftHelper.AddRevision.EliminarHojas();
            DraftHelper.AddRevision.EliminarRevisionTabla();
            DraftHelper.AddRevision.QuitarRevisionHojaDFT();
            // ISSUE: reference to a compiler-generated method
            // ISSUE: reference to a compiler-generated method
            DraftHelper.AddRevision.objDraftDoc.Sheets.Item((object)"DFT").Activate();
            DraftHelper.AddRevision.QuitarRevisionTituloPieza();
            ObtenerUltimaRevision();
        }
      
    }
}
