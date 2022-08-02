﻿using SolidEdgeCommunity.AddIn;
using SolidEdgeCommunity.Extensions;
using SolidEdgeDraft;
using SolidEdgeFramework;
using SolidEdgeFrameworkSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace DraftHelper
{
    class AddRevision
    {
        public static SolidEdgeFramework.Application objSEApp;
        public static DraftDocument objDraftDoc;
        public static Sheet objSheet;
        public static Table tablarevisiones;
        public static string HojaPlanoAntiguo;
        public static bool activar;
        public static string AutorPlano;
        public static string FechaPlano;

        public static void Init()
        {
            try
            {
                var app = SolidEdgeAddIn.Instance.Application;
                var draftDocument = app.GetActiveDocument<DraftDocument>();

                if (draftDocument != null)
                {
                    AddRevision.objDraftDoc = draftDocument;
                    AddRevision.objSheet = draftDocument.ActiveSheet;
                }
            }
            catch (Exception ex)
            {

                Helpers.ShowException(ex);
            }

            try
            {
                AddRevision.ObtenerTablaRevisiones();
            }
            catch (Exception ex)
            {

                Helpers.ShowException(ex);
            }
        }
        public static void Add(string revisionNumber, string fecha, string autor, string description)
        {
            AñadirRevisionTabla(revisionNumber, fecha, autor, description);
            HojaPlanoAntiguo = string.Format("rev-{0:00}", (object)(Convert.ToDouble(revisionNumber) - 1.0));
            CrearHojaPlanoAntiguo();
            CopiarFondo();
            CopiarVistas();
            TacharPlanoAntiguo();
            AñadirAnuladoPlanoAntiguo(fecha);
            CambiarEscalaPlanoAntiguo();
            DesvincularTextos();
            //System.Windows.Forms.Application.DoEvents();
            DesvincularVistas();
            // ISSUE: reference to a compiler-generated method
            // ISSUE: reference to a compiler-generated method
            objDraftDoc.Sheets.Item((object)"DFT").Activate();
            AñadirRevisionFechaPlanoRevisado(revisionNumber, fecha);
            // ISSUE: reference to a compiler-generated method
            objDraftDoc.Sections.BackgroundSection.Deactivate();
            AñadirRevisionTituloPieza();
            ObtenerUltimaRevision();
        }
        public static void AñadirRevisionTituloPieza()
        {
            // ISSUE: reference to a compiler-generated method
            // ISSUE: variable of a compiler-generated type
            ModelLink modelLink = objDraftDoc.ModelLinks.Item((object)1);
            // ISSUE: variable of a compiler-generated type
            SolidEdgeDocument modelDocument = (SolidEdgeDocument)modelLink.ModelDocument;
            try
            {
                //// ISSUE: variable of a compiler-generated type
                //SolidEdgeFileProperties.PropertySets instance = (SolidEdgeFileProperties.PropertySets)Activator.CreateInstance(Type.GetTypeFromCLSID(new Guid("AED8FE60-3129-11D1-BC83-0800360E1E02")));
                //string fileName = modelLink.FileName;
                //// ISSUE: variable of a boxed type
                //__Boxed<bool> local1 = (ValueType)false;
                //// ISSUE: reference to a compiler-generated method
                //instance.Open(fileName, (object)local1);
                //// ISSUE: variable of a boxed type
                //__Boxed<int> local2 = (ValueType)0;
                //// ISSUE: variable of a compiler-generated type
                //SolidEdgeFileProperties.Property property = (SolidEdgeFileProperties.Property)((IJProperties)instance[(object)local2])[(object)0];
                //string str1 = property[].ToString();
                //if (str1.Contains(" rev-"))
                //    str1 = str1.Remove(str1.IndexOf(" rev-"));
                //string str2 = str1 + string.Format(" rev-{0:00} ", (object)txtRevNumber.Text) + DPFechaRevision.Text;
                //property[] = (object)str2;
                //// ISSUE: reference to a compiler-generated method
                //instance.Save();
                //// ISSUE: reference to a compiler-generated method
                //instance.Close();
            }
            catch (Exception ex1)
            {
                //ProjectData.SetProjectError(ex1);
                try
                {
                    //// ISSUE: variable of a compiler-generated type
                    //SolidEdgeFramework.PropertySets properties = (SolidEdgeFramework.PropertySets)modelDocument.Properties;
                    //// ISSUE: variable of a boxed type
                    //__Boxed<int> local1 = (ValueType)1;
                    //// ISSUE: reference to a compiler-generated method
                    //// ISSUE: reference to a compiler-generated method
                    //// ISSUE: variable of a compiler-generated type
                    //SolidEdgeFramework.Property property = properties.Item((object)local1).Item((object)1);
                    //string str = property.Value.ToString();
                    //if (str.Contains(" rev-"))
                    //    str = str.Remove(str.IndexOf(" rev-"));
                    //object obj = (object)(str + string.Format(" rev-{0:00} ", (object)txtRevNumber.Text) + DPFechaRevision.Text);
                    //// ISSUE: explicit reference operation
                    //// ISSUE: variable of a reference type
                    //object&local2 = @obj;
                    //property.Value = (object)local2;
                    //// ISSUE: reference to a compiler-generated method
                    //properties.Save();
                    //// ISSUE: reference to a compiler-generated method
                    //modelDocument.Save();
                }
                catch (Exception ex2)
                {
                    //ProjectData.SetProjectError(ex2);
                    //ProjectData.ClearProjectError();
                }
                //ProjectData.ClearProjectError();
            }
        }
        

        public static void ObtenerTablaRevisiones()
        {
            // ISSUE: variable of a compiler-generated type
            Tables tables = objDraftDoc.Tables;
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
                        tablarevisiones = tables.Item((object)num1);
                        flag = true;
                        break;
                    }
                    checked { ++num2; }
                }
                checked { ++num1; }
            }
            if (!flag)
            {
                activar = false;
                Helpers.ShowException("Abra un documento de Solid Edge Plano válido\r\r      (Edición 6 o superior)");
                //int num2 = (int)Interaction.MsgBox((object)"Abra un documento de Solid Edge Plano válido\r\r      (Edición 6 o superior)", MsgBoxStyle.Critical, (object)null);
            }
            else
                ObtenerUltimaRevision();
        }

        public static void ObtenerUltimaRevision()
        {
            //TODO RE
            //int count = tablarevisiones.Rows.Count;
            //int num1 = 1;
            //while (num1 <= count)
            //{
            //    // ISSUE: reference to a compiler-generated method
            //    if (string.Compare(tablarevisiones.get_Cell((object)num1, (object)1).value, "", false) == 0)
            //    {
            //        if (num1 == 1)
            //        {
            //            ObtenerAutorFecha();
            //            // ISSUE: reference to a compiler-generated method
            //            tablarevisiones.get_Cell((object)num1, (object)1).value = "00";
            //            // ISSUE: reference to a compiler-generated method
            //            tablarevisiones.get_Cell((object)num1, (object)2).value = FechaPlano;
            //            // ISSUE: reference to a compiler-generated method
            //            tablarevisiones.get_Cell((object)num1, (object)3).value = AutorPlano;
            //            // ISSUE: reference to a compiler-generated method
            //            tablarevisiones.get_Cell((object)num1, (object)4).value = "Creación del Plano";
            //            // ISSUE: reference to a compiler-generated method
            //            tablarevisiones.Update();

            //            txtRevNumber.Text = "01";

            //            AutorPlano = (string)null;
            //            FechaPlano = (string)null;
            //            break;
            //        }
            //        // ISSUE: reference to a compiler-generated method
            //        txtRevNumber.Text = string.Format("{0:00}", (object)(Convert.ToDouble(tablarevisiones.get_Cell((object)checked(num1 - 1), (object)1).value) + 1.0));
            //        break;
            //    }
            //    if (num1 == tablarevisiones.Rows.Count)
            //    {
            //        txtRevNumber.Text = "##";
            //        btnAddRevision.Enabled = false;
            //        DPFechaRevision.Enabled = false;
            //        cb_Autor.Enabled = false;
            //        txtDescripcion.Enabled = false;
            //        activar = false;
            //        Helpers.ShowException("Se ha alcanzado el número máximo de revisiones");
            //        //int num2 = (int)Interaction.MsgBox((object)"Se ha alcanzado el número máximo de revisiones", MsgBoxStyle.Critical, (object)"Número máximo de revisiones alcanzado");
            //    }
            //    checked { ++num1; }
            //}
            //if (string.Compare(txtRevNumber.Text, "01", false) == 0)
            //    btnDelRevision.Enabled = false;
            //else
            //    btnDelRevision.Enabled = true;
        }

        public static void ObtenerAutorFecha()
        {
            try
            {
                // ISSUE: reference to a compiler-generated method
                //TODO RE
                //foreach (object balloon in (IEnumerable)objDraftDoc.Sheets.Item((object)"DFT").Background.Balloons)
                //{
                //    object objectValue = RuntimeHelpers.GetObjectValue(balloon);
                //    if (Operators.ConditionalCompareObjectEqual(NewLateBinding.LateGet(objectValue, (Type)null, "BalloonText", new object[0], (string[])null, (Type[])null, (bool[])null), (object)"%{Autores|R1}", false))
                //        AutorPlano = Conversions.ToString(NewLateBinding.LateGet(objectValue, (Type)null, "BalloonDisplayedText", new object[0], (string[])null, (Type[])null, (bool[])null));
                //    if (Operators.ConditionalCompareObjectEqual(NewLateBinding.LateGet(objectValue, (Type)null, "BalloonText", new object[0], (string[])null, (Type[])null, (bool[])null), (object)"%{FechaPlano|R1}", false))
                //        FechaPlano = Conversions.ToString(NewLateBinding.LateGet(objectValue, (Type)null, "BalloonDisplayedText", new object[0], (string[])null, (Type[])null, (bool[])null));
                //}
            }
            finally
            {
                //IEnumerator enumerator;
                //if (enumerator is IDisposable)
                //    (enumerator as IDisposable).Dispose();
            }
        }

        public static void AñadirRevisionTabla(string revisionNumber, string fecha, string autor, string description)
        {
            int count = tablarevisiones.Rows.Count;
            int num = 1;
            while (num <= count)
            {
                // ISSUE: reference to a compiler-generated method
                if (string.Compare(tablarevisiones.get_Cell((object)num, (object)1).value, "", false) == 0)
                {
                    // ISSUE: reference to a compiler-generated method
                    tablarevisiones.get_Cell((object)num, (object)1).value = revisionNumber;
                    // ISSUE: reference to a compiler-generated method
                    tablarevisiones.get_Cell((object)num, (object)1).FontStyle = DimTextFontStyleConstants.igDimStyleFontNormal;
                    // ISSUE: reference to a compiler-generated method
                    tablarevisiones.get_Cell((object)num, (object)2).value = fecha;
                    // ISSUE: reference to a compiler-generated method
                    tablarevisiones.get_Cell((object)num, (object)2).FontStyle = DimTextFontStyleConstants.igDimStyleFontNormal;
                    // ISSUE: reference to a compiler-generated method
                    tablarevisiones.get_Cell((object)num, (object)3).value = autor;
                    // ISSUE: reference to a compiler-generated method
                    tablarevisiones.get_Cell((object)num, (object)3).FontStyle = DimTextFontStyleConstants.igDimStyleFontNormal;
                    // ISSUE: reference to a compiler-generated method
                    tablarevisiones.get_Cell((object)num, (object)4).value = description;
                    // ISSUE: reference to a compiler-generated method
                    tablarevisiones.get_Cell((object)num, (object)4).FontStyle = DimTextFontStyleConstants.igDimStyleFontNormal;
                    // ISSUE: reference to a compiler-generated method
                    tablarevisiones.Update();
                    break;
                }
                checked { ++num; }
            }
        }

        public static void CrearHojaPlanoAntiguo()
        {
            // ISSUE: reference to a compiler-generated method
            objDraftDoc.Sheets.AddSheet((object)HojaPlanoAntiguo, SheetSectionTypeConstants.igWorkingSection, RuntimeHelpers.GetObjectValue((object)Missing.Value), (object)"DFT");
            // ISSUE: reference to a compiler-generated method
            objDraftDoc.Sheets.Item((object)HojaPlanoAntiguo).BackgroundVisible = false;
            // ISSUE: reference to a compiler-generated method
            // ISSUE: reference to a compiler-generated method
            objDraftDoc.Sheets.Item((object)HojaPlanoAntiguo).SheetSetup.SheetSizeOption = objDraftDoc.Sheets.Item((object)"DFT").SheetSetup.SheetSizeOption;
        }
        public static void CopiarFondo()
        {
            // ISSUE: reference to a compiler-generated method
            // ISSUE: reference to a compiler-generated method
            objDraftDoc.Sheets.Item((object)"DFT").Background.Activate();
            CopiarPegarObjetos();
            try
            {
                // ISSUE: reference to a compiler-generated method
                foreach (BlockOccurrence blockOccurrence in objDraftDoc.Sheets.Item((object)HojaPlanoAntiguo).BlockOccurrences)
                {
                    if (blockOccurrence.Block.Name.ToUpper().Contains("CAJETIN"))
                    {
                        // ISSUE: reference to a compiler-generated method
                        blockOccurrence.Unblock();
                    }
                }
            }
            finally
            {
                //IEnumerator enumerator;
                //if (enumerator is IDisposable)
                //    (enumerator as IDisposable).Dispose();
            }
        }
        public static void CopiarVistas()
        {
            // ISSUE: reference to a compiler-generated method
            // ISSUE: reference to a compiler-generated method
            objDraftDoc.Sheets.Item((object)"DFT").Activate();
            CopiarPegarObjetos();
        }
        public static void CopiarPegarObjetos()
        {
            // ISSUE: reference to a compiler-generated method
            objDraftDoc.SelectSet.AddAll();
            // ISSUE: reference to a compiler-generated method
            objDraftDoc.SelectSet.Copy();
            if (objDraftDoc.SelectSet.Count == 0)
                return;
            // ISSUE: reference to a compiler-generated method
            // ISSUE: reference to a compiler-generated method
            objDraftDoc.Sheets.Item((object)HojaPlanoAntiguo).Activate();
            
            //TODO RE
            //NewLateBinding.LateCall(objSEApp.ActiveWindow, (Type)null, "Paste", new object[0], (string[])null, (Type[])null, (bool[])null, true);
        }

        public static void CambiarEscalaPlanoAntiguo()
        {
            try
            {
                var balloons = objDraftDoc.Sheets.Item((object)HojaPlanoAntiguo).Balloons as IEnumerable<object>;
                // ISSUE: reference to a compiler-generated method
                foreach (object balloon in balloons)
                {
                    //TODO RE
                    //object objectValue = RuntimeHelpers.GetObjectValue(balloon);

                    //if (Operators.ConditionalCompareObjectEqual(NewLateBinding.LateGet(objectValue, (Type)null, "BalloonText", new object[0], (string[])null, (Type[])null, (bool[])null), (object)"%{Escala de la hoja}", false))
                    //{
                    //    double PaperRatioComponent;
                    //    double ModelRatioComponent;
                    //    // ISSUE: reference to a compiler-generated method
                    //    // ISSUE: reference to a compiler-generated method
                    //    objDraftDoc.Sheets.Item((object)"DFT").SheetSetup.GetDefaultDrawingViewScale(out PaperRatioComponent, out ModelRatioComponent);
                    //    NewLateBinding.LateSet(objectValue, (Type)null, "BalloonText", new object[1]
                    //    {
                    //(object) (Convert.ToString(ModelRatioComponent) + ":" + Convert.ToString(1.0 / PaperRatioComponent))
                    //    }, (string[])null, (Type[])null);
                    //    break;
                    //}
                }
            }
            finally
            {
                //IEnumerator enumerator;
                //if (enumerator is IDisposable)
                //    (enumerator as IDisposable).Dispose();
            }
        }

        public static void DesvincularVistas()
        {
            // ISSUE: reference to a compiler-generated method
            int count = objDraftDoc.Sheets.Item((object)HojaPlanoAntiguo).DrawingViews.Count;
            while (count >= 1)
            {
                // ISSUE: reference to a compiler-generated method
                // ISSUE: reference to a compiler-generated method
                // ISSUE: reference to a compiler-generated method
                objDraftDoc.Sheets.Item((object)HojaPlanoAntiguo).DrawingViews.Item((object)count).Drop();
                checked { count += -1; }
            }
        }

        public static void DesvincularTextos()
        {
            try
            {
                //TODO RE
                // ISSUE: reference to a compiler-generated method
                //foreach (object balloon in (IEnumerable)objDraftDoc.Sheets.Item((object)HojaPlanoAntiguo).Balloons)
                //{
                //    object objectValue = RuntimeHelpers.GetObjectValue(balloon);
                //    NewLateBinding.LateSet(objectValue, (Type)null, "BalloonText", new object[1]
                //    {
                //        NewLateBinding.LateGet(objectValue, (Type) null, "balloonDisplayedText", new object[0], (string[]) null, (Type[]) null, (bool[]) null)
                //    }, (string[])null, (Type[])null);
                //}
            }
            finally
            {
                //IEnumerator enumerator;
                //if (enumerator is IDisposable)
                //    (enumerator as IDisposable).Dispose();
            }
        }

        public static void TacharPlanoAntiguo()
        {
            // ISSUE: reference to a compiler-generated method
            double sheetWidth = objDraftDoc.Sheets.Item((object)HojaPlanoAntiguo).SheetSetup.SheetWidth;
            // ISSUE: reference to a compiler-generated method
            double sheetHeight = objDraftDoc.Sheets.Item((object)HojaPlanoAntiguo).SheetSetup.SheetHeight;
            // ISSUE: reference to a compiler-generated method
            // ISSUE: variable of a compiler-generated type
            Lines2d lines2d = objDraftDoc.Sheets.Item((object)HojaPlanoAntiguo).Lines2d;
            double x1_1 = 0.002;
            double y1_1 = 0.002;
            double x2_1 = sheetWidth - 0.002;
            double y2_1 = sheetHeight - 0.002;
            // ISSUE: reference to a compiler-generated method
            // ISSUE: variable of a compiler-generated type
            GeometryStyle2d style1 = lines2d.AddBy2Points(x1_1, y1_1, x2_1, y2_1).Style;
            int maxValue1 = (int)byte.MaxValue;
            style1.LinearColor = maxValue1;
            double num1 = 0.002;
            style1.Width = num1;
            double x1_2 = 0.002;
            double y1_2 = sheetHeight - 0.002;
            double x2_2 = sheetWidth - 0.002;
            double y2_2 = 0.002;
            // ISSUE: reference to a compiler-generated method
            // ISSUE: variable of a compiler-generated type
            GeometryStyle2d style2 = lines2d.AddBy2Points(x1_2, y1_2, x2_2, y2_2).Style;
            int maxValue2 = (int)byte.MaxValue;
            style2.LinearColor = maxValue2;
            double num2 = 0.002;
            style2.Width = num2;
        }

        public static void AñadirAnuladoPlanoAntiguo(string fecha)
        {
            try
            {
                var textBoxes = objDraftDoc.Sheets.Item((object)HojaPlanoAntiguo).TextBoxes as IEnumerable<TextBox>;

                // ISSUE: reference to a compiler-generated method
                foreach (SolidEdgeFrameworkSupport.TextBox textBox in textBoxes)
                {
                    if (string.Compare(textBox.Text, "__/__/____", false) == 0)
                    {
                        textBox.Text = fecha;
                        textBox.Edit.TextSize = 0.003;
                        textBox.Justification = TextJustificationConstants.igTextJustifyVCenter;
                    }
                }
            }
            finally
            {
                //IEnumerator enumerator;
                //if (enumerator is IDisposable)
                //    (enumerator as IDisposable).Dispose();
            }
        }

        public static void AñadirRevisionFechaPlanoRevisado(string revNumber, string fecha)
        {
            List<SolidEdgeFrameworkSupport.TextBox> textBoxList = new List<SolidEdgeFrameworkSupport.TextBox>();
            // ISSUE: variable of a compiler-generated type
            Sheet activeSheet = objDraftDoc.ActiveSheet;
            // ISSUE: variable of a compiler-generated type
            TextBoxes textBoxes = (TextBoxes)activeSheet.Background.TextBoxes;
            try
            {
                foreach (BlockOccurrence blockOccurrence in activeSheet.Background.BlockOccurrences)
                {
                    if (blockOccurrence.Block.Name.ToUpper().Contains("CAJETIN"))
                        textBoxes = blockOccurrence.BlockView.TextBoxes;
                }
            }
            finally
            {
                //IEnumerator enumerator;
                //if (enumerator is IDisposable)
                //    (enumerator as IDisposable).Dispose();
            }
            try
            {
                foreach (SolidEdgeFrameworkSupport.TextBox textBox in textBoxes)
                {
                    if (textBox.Text == null)
                        textBoxList.Add(textBox);
                    else if (textBox.Text.Contains(" -- "))
                    {
                        textBox.Text = "";
                        textBox.Edit.TextSize = 1.0 / 400.0;
                        textBox.Text = revNumber + " -- " + fecha;
                        textBox.Edit.TextSize = 1.0 / 400.0;
                        textBox.Justification = TextJustificationConstants.igTextJustifyVCenter;
                        break;
                    }
                }
            }
            finally
            {
                //IEnumerator enumerator;
                //if (enumerator is IDisposable)
                //    (enumerator as IDisposable).Dispose();
            }
            List<SolidEdgeFrameworkSupport.TextBox>.Enumerator enumerator1;
            try
            {
                enumerator1 = textBoxList.GetEnumerator();
                while (enumerator1.MoveNext())
                {
                    // ISSUE: reference to a compiler-generated method
                    enumerator1.Current.Delete();
                }
            }
            finally
            {
                //enumerator1.Dispose();
            }
            try
            {
                // ISSUE: variable of a compiler-generated type
                SolidEdgeFramework.PropertySets properties = (SolidEdgeFramework.PropertySets)objDraftDoc.Properties;
                string str = "Custom";
                // ISSUE: reference to a compiler-generated method
                // ISSUE: reference to a compiler-generated method
                properties.Item((object)str).Add((object)"Revision", (object)revNumber);
                // ISSUE: reference to a compiler-generated method
                properties.Save();
            }
            catch (Exception ex)
            {
                //ProjectData.SetProjectError(ex);
                //ProjectData.ClearProjectError();
            }
        }
        public static void EliminarHojas()
        {
            int count = objDraftDoc.Sections.WorkingSection.Sheets.Count;
            while (count >= 1)
            {
                // ISSUE: reference to a compiler-generated method
                // ISSUE: variable of a compiler-generated type
                Sheet sheet = objDraftDoc.Sections.WorkingSection.Sheets.Item((object)count);
                if (sheet.Name.StartsWith("rev-"))
                {
                    // ISSUE: reference to a compiler-generated method
                    sheet.Delete();
                }
                checked { count += -1; }
            }
        }

        public static void EliminarRevisionTabla()
        {
            int count = tablarevisiones.Rows.Count;
            int num = 1;
            while (num <= count)
            {
                // ISSUE: reference to a compiler-generated method
                if (string.Compare(tablarevisiones.get_Cell((object)num, (object)1).value, "", false) != 0)
                {
                    // ISSUE: reference to a compiler-generated method
                    tablarevisiones.get_Cell((object)num, (object)1).value = "";
                    // ISSUE: reference to a compiler-generated method
                    tablarevisiones.get_Cell((object)num, (object)2).value = "";
                    // ISSUE: reference to a compiler-generated method
                    tablarevisiones.get_Cell((object)num, (object)3).value = "";
                    // ISSUE: reference to a compiler-generated method
                    tablarevisiones.get_Cell((object)num, (object)4).value = "";
                    // ISSUE: reference to a compiler-generated method
                    tablarevisiones.Update();
                }
                checked { ++num; }
            }
        }

        public static void QuitarRevisionHojaDFT()
        {
            List<SolidEdgeFrameworkSupport.TextBox> textBoxList = new List<SolidEdgeFrameworkSupport.TextBox>();
            // ISSUE: variable of a compiler-generated type
            Sheet activeSheet = objDraftDoc.ActiveSheet;
            // ISSUE: variable of a compiler-generated type
            TextBoxes textBoxes = (TextBoxes)activeSheet.Background.TextBoxes;
            try
            {
                foreach (BlockOccurrence blockOccurrence in activeSheet.Background.BlockOccurrences)
                {
                    if (blockOccurrence.Block.Name.ToUpper().Contains("CAJETIN"))
                        textBoxes = blockOccurrence.BlockView.TextBoxes;
                }
            }
            finally
            {
                //IEnumerator enumerator;
                //if (enumerator is IDisposable)
                //    (enumerator as IDisposable).Dispose();
            }
            try
            {
                foreach (SolidEdgeFrameworkSupport.TextBox textBox in textBoxes)
                {
                    if (textBox.Text == null)
                        textBoxList.Add(textBox);
                    else if (textBox.Text.Contains(" -- "))
                    {
                        textBox.Text = "";
                        textBox.Edit.TextSize = 1.0 / 400.0;
                        textBox.Text = "__ -- __/__/____";
                        textBox.Edit.TextSize = 1.0 / 400.0;
                        textBox.Justification = TextJustificationConstants.igTextJustifyVCenter;
                        break;
                    }
                }
            }
            finally
            {
                //IEnumerator enumerator;
                //if (enumerator is IDisposable)
                //    (enumerator as IDisposable).Dispose();
            }
            List<SolidEdgeFrameworkSupport.TextBox>.Enumerator enumerator1;
            try
            {
                enumerator1 = textBoxList.GetEnumerator();
                while (enumerator1.MoveNext())
                {
                    // ISSUE: reference to a compiler-generated method
                    enumerator1.Current.Delete();
                }
            }
            finally
            {
                //enumerator1.Dispose();
            }
            try
            {
                // ISSUE: variable of a compiler-generated type
                SolidEdgeFramework.PropertySets properties = (SolidEdgeFramework.PropertySets)objDraftDoc.Properties;
                string str = "Custom";
                // ISSUE: reference to a compiler-generated method
                // ISSUE: reference to a compiler-generated method
                properties.Item((object)str).Add((object)"Revision", (object)"00");
                // ISSUE: reference to a compiler-generated method
                properties.Save();
            }
            catch (Exception ex)
            {
                //ProjectData.SetProjectError(ex);
                //ProjectData.ClearProjectError();
            }
        }

    }
}