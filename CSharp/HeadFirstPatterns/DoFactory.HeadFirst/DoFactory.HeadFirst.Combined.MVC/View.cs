using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace DoFactory.HeadFirst.Combined.MVC
{
    /// <summary>
    /// The View class in the MVC pattern
    /// </summary>
    public class FormMain : System.Windows.Forms.Form
    {
        private System.Windows.Forms.TextBox textBoxBPM;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        
        private System.Windows.Forms.TrackBar trackBarBPM;
        private System.Windows.Forms.Button buttonSet;
        private System.Windows.Forms.Label labelMVC;
        private System.Windows.Forms.Button buttonStop;

        private System.ComponentModel.Container components = null;

        private System.Windows.Forms.GroupBox groupBoxControl;
        private System.Windows.Forms.GroupBox groupBoxView;

        // Two 'beatable' observer controls
        private BeatTextBox textBoxCurrentBPM;
        private BeatPanel panelColor;

        private Controller _controller;

        public FormMain()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        // Make stop button accessible to controller
        public Button ButtonStop
        {
            get{ return buttonStop; }
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
            this.textBoxBPM = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonSet = new System.Windows.Forms.Button();
            this.groupBoxControl = new System.Windows.Forms.GroupBox();
            this.trackBarBPM = new System.Windows.Forms.TrackBar();
            this.groupBoxView = new System.Windows.Forms.GroupBox();
            this.panelColor = new DoFactory.HeadFirst.Combined.MVC.BeatPanel();
            this.textBoxCurrentBPM = new DoFactory.HeadFirst.Combined.MVC.BeatTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.labelMVC = new System.Windows.Forms.Label();
            this.buttonStop = new System.Windows.Forms.Button();
            this.groupBoxControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBPM)).BeginInit();
            this.groupBoxView.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxBPM
            // 
            this.textBoxBPM.Location = new System.Drawing.Point(90, 24);
            this.textBoxBPM.Name = "textBoxBPM";
            this.textBoxBPM.Size = new System.Drawing.Size(68, 20);
            this.textBoxBPM.TabIndex = 0;
            this.textBoxBPM.Text = "120";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(23, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 19);
            this.label1.TabIndex = 1;
            this.label1.Text = "Enter BPM:";
            // 
            // buttonSet
            // 
            this.buttonSet.Location = new System.Drawing.Point(173, 24);
            this.buttonSet.Name = "buttonSet";
            this.buttonSet.TabIndex = 2;
            this.buttonSet.Text = "Set";
            this.buttonSet.Click += new System.EventHandler(this.buttonSet_Click);
            // 
            // groupBoxControl
            // 
            this.groupBoxControl.Controls.Add(this.trackBarBPM);
            this.groupBoxControl.Controls.Add(this.label1);
            this.groupBoxControl.Controls.Add(this.textBoxBPM);
            this.groupBoxControl.Controls.Add(this.buttonSet);
            this.groupBoxControl.Location = new System.Drawing.Point(19, 48);
            this.groupBoxControl.Name = "groupBoxControl";
            this.groupBoxControl.Size = new System.Drawing.Size(281, 116);
            this.groupBoxControl.TabIndex = 4;
            this.groupBoxControl.TabStop = false;
            this.groupBoxControl.Text = "DJ Control";
            // 
            // trackBarBPM
            // 
            this.trackBarBPM.LargeChange = 10;
            this.trackBarBPM.Location = new System.Drawing.Point(26, 61);
            this.trackBarBPM.Maximum = 200;
            this.trackBarBPM.Minimum = 1;
            this.trackBarBPM.Name = "trackBarBPM";
            this.trackBarBPM.Size = new System.Drawing.Size(233, 42);
            this.trackBarBPM.TabIndex = 3;
            this.trackBarBPM.TickFrequency = 10;
            this.trackBarBPM.Value = 120;
            this.trackBarBPM.Scroll += new System.EventHandler(this.trackBarBPM_Scroll);
            // 
            // groupBoxView
            // 
            this.groupBoxView.Controls.Add(this.panelColor);
            this.groupBoxView.Controls.Add(this.textBoxCurrentBPM);
            this.groupBoxView.Controls.Add(this.label2);
            this.groupBoxView.Location = new System.Drawing.Point(19, 183);
            this.groupBoxView.Name = "groupBoxView";
            this.groupBoxView.Size = new System.Drawing.Size(280, 116);
            this.groupBoxView.TabIndex = 5;
            this.groupBoxView.TabStop = false;
            this.groupBoxView.Text = "DJ View";
            // 
            // panelColor
            // 
            this.panelColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelColor.Location = new System.Drawing.Point(26, 64);
            this.panelColor.Name = "panelColor";
            this.panelColor.Size = new System.Drawing.Size(222, 30);
            this.panelColor.TabIndex = 6;
            // 
            // textBoxCurrentBPM
            // 
            this.textBoxCurrentBPM.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxCurrentBPM.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.textBoxCurrentBPM.ForeColor = System.Drawing.Color.Red;
            this.textBoxCurrentBPM.Location = new System.Drawing.Point(102, 27);
            this.textBoxCurrentBPM.Name = "textBoxCurrentBPM";
            this.textBoxCurrentBPM.Size = new System.Drawing.Size(49, 20);
            this.textBoxCurrentBPM.TabIndex = 5;
            this.textBoxCurrentBPM.Text = "120";
            this.textBoxCurrentBPM.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(23, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 19);
            this.label2.TabIndex = 4;
            this.label2.Text = "Current BPM:";
            // 
            // labelMVC
            // 
            this.labelMVC.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.labelMVC.ForeColor = System.Drawing.Color.Red;
            this.labelMVC.Location = new System.Drawing.Point(19, 13);
            this.labelMVC.Name = "labelMVC";
            this.labelMVC.Size = new System.Drawing.Size(173, 23);
            this.labelMVC.TabIndex = 6;
            this.labelMVC.Text = "Model  View  Controller";
            // 
            // buttonStop
            // 
            this.buttonStop.Enabled = false;
            this.buttonStop.Location = new System.Drawing.Point(225, 13);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.TabIndex = 7;
            this.buttonStop.Text = "Stop";
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // FormMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(319, 319);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.labelMVC);
            this.Controls.Add(this.groupBoxControl);
            this.Controls.Add(this.groupBoxView);
            this.Name = "FormMain";
            this.Text = "DJ View";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.groupBoxControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBPM)).EndInit();
            this.groupBoxView.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() 
        {
            Application.Run(new FormMain());
        }

        private void FormMain_Load(object sender, System.EventArgs e)
        {
            _controller = new Controller(this);

            _controller.Attach(this.textBoxCurrentBPM);
            _controller.Attach(this.panelColor);
        }

        // Set and Start the beat
        private void buttonSet_Click(object sender, System.EventArgs e)
        {
            _controller.BeatsPerMinute = int.Parse(textBoxBPM.Text);
            _controller.Start();
        }

        // Stop the beat
        private void buttonStop_Click(object sender, System.EventArgs e)
        {
            _controller.Stop();
        }

        // Update the pulse
        private void trackBarBPM_Scroll(object sender, System.EventArgs e)
        {
            _controller.BeatsPerMinute = this.trackBarBPM.Value;
            this.textBoxBPM.Text = this.trackBarBPM.Value.ToString();
        }
    }
}
