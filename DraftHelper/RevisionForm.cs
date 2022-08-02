using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DraftHelper
{
    public partial class RevisionForm : Form
    {
        public RevisionForm()
        {
            InitializeComponent();
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);

            AddRevision.Init();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddRevision.Add(txtRevNumber.Text, txtFecha.Text, txtAutor.Text, txtDescripcion.Text);
        }
    }
}
