using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace DoFactory.HeadFirst.Template.Control
{
    /// <summary>
    /// Summary description for Form1.
    /// </summary>
    public class Form1 : System.Windows.Forms.Form
    {
        private System.Windows.Forms.Button buttonTemplate;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public Form1()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose( bool disposing )
        {
            if( disposing )
            {
                if (components != null) 
                {
                    components.Dispose();
                }
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonTemplate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonTemplate
            // 
            this.buttonTemplate.Location = new System.Drawing.Point(45, 30);
            this.buttonTemplate.Name = "buttonTemplate";
            this.buttonTemplate.Size = new System.Drawing.Size(126, 32);
            this.buttonTemplate.TabIndex = 0;
            this.buttonTemplate.Text = "Template";
            this.buttonTemplate.Click += new System.EventHandler(this.buttonTemplate_Click);
            this.buttonTemplate.MouseEnter += new System.EventHandler(this.buttonTemplate_MouseEnter);
            this.buttonTemplate.MouseLeave += new System.EventHandler(this.buttonTemplate_MouseLeave);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(206, 103);
            this.Controls.Add(this.buttonTemplate);
            this.Name = "Form1";
            this.Text = "Template Method";
            this.ResumeLayout(false);

        }
        #endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() 
        {
            Application.Run(new Form1());
        }

        private void buttonTemplate_MouseLeave(object sender, System.EventArgs e)
        {
            this.buttonTemplate.BackColor = System.Drawing.Color.Gold;
        }

        private void buttonTemplate_Click(object sender, System.EventArgs e)
        {
            Console.Beep(2000,15);
        }

        private void buttonTemplate_MouseEnter(object sender, System.EventArgs e)
        {
            this.buttonTemplate.BackColor = System.Drawing.Color.Red;
        }
    }
}
