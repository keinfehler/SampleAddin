
namespace DraftHelper
{
    partial class RevisionForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.add_button = new System.Windows.Forms.Button();
            this.txtRevNumber = new System.Windows.Forms.NumericUpDown();
            this.txtFecha = new System.Windows.Forms.DateTimePicker();
            this.txtAutor = new System.Windows.Forms.ComboBox();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.txtRevNumber)).BeginInit();
            this.SuspendLayout();
            // 
            // add_button
            // 
            this.add_button.Location = new System.Drawing.Point(446, 106);
            this.add_button.Name = "add_button";
            this.add_button.Size = new System.Drawing.Size(75, 23);
            this.add_button.TabIndex = 0;
            this.add_button.Text = "Add";
            this.add_button.UseVisualStyleBackColor = true;
            this.add_button.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtRevNumber
            // 
            this.txtRevNumber.Location = new System.Drawing.Point(28, 26);
            this.txtRevNumber.Name = "txtRevNumber";
            this.txtRevNumber.Size = new System.Drawing.Size(120, 20);
            this.txtRevNumber.TabIndex = 5;
            // 
            // txtFecha
            // 
            this.txtFecha.Location = new System.Drawing.Point(174, 26);
            this.txtFecha.Name = "txtFecha";
            this.txtFecha.Size = new System.Drawing.Size(200, 20);
            this.txtFecha.TabIndex = 6;
            // 
            // txtAutor
            // 
            this.txtAutor.FormattingEnabled = true;
            this.txtAutor.Location = new System.Drawing.Point(400, 26);
            this.txtAutor.Name = "txtAutor";
            this.txtAutor.Size = new System.Drawing.Size(121, 21);
            this.txtAutor.TabIndex = 7;
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(28, 67);
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(493, 20);
            this.txtDescripcion.TabIndex = 8;
            // 
            // RevisionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(553, 175);
            this.Controls.Add(this.txtDescripcion);
            this.Controls.Add(this.txtAutor);
            this.Controls.Add(this.txtFecha);
            this.Controls.Add(this.txtRevNumber);
            this.Controls.Add(this.add_button);
            this.Name = "RevisionForm";
            this.Text = "RevisionForm";
            ((System.ComponentModel.ISupportInitialize)(this.txtRevNumber)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button add_button;
        private System.Windows.Forms.NumericUpDown txtRevNumber;
        private System.Windows.Forms.DateTimePicker txtFecha;
        private System.Windows.Forms.ComboBox txtAutor;
        private System.Windows.Forms.TextBox txtDescripcion;
    }
}