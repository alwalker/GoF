using System;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading;
using System.Net;

namespace DoFactory.HeadFirst.Proxy.VirtualProxy
{
    /// <summary>
    /// Summary description for FormVirtualProxy.
    /// </summary>
    public class FormVirtualProxy : System.Windows.Forms.Form
    {
        private System.Windows.Forms.Button buttonTestImageProxy;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Label label1;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public FormVirtualProxy()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
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
            this.buttonTestImageProxy = new System.Windows.Forms.Button();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonTestImageProxy
            // 
            this.buttonTestImageProxy.Location = new System.Drawing.Point(197, 25);
            this.buttonTestImageProxy.Name = "buttonTestImageProxy";
            this.buttonTestImageProxy.Size = new System.Drawing.Size(105, 23);
            this.buttonTestImageProxy.TabIndex = 0;
            this.buttonTestImageProxy.Text = "Test Image Proxy";
            this.buttonTestImageProxy.Click += new System.EventHandler(this.buttonTestImageProxy_Click);
            // 
            // pictureBox
            // 
            this.pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox.Location = new System.Drawing.Point(31, 24);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(147, 159);
            this.pictureBox.TabIndex = 1;
            this.pictureBox.TabStop = false;
            this.pictureBox.Click += new System.EventHandler(this.pictureBox_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(194, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Click button twice";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // FormVirtualProxy
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(323, 217);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.buttonTestImageProxy);
            this.Name = "FormVirtualProxy";
            this.Text = "Virtual Proxy Test";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() 
        {
            Application.Run(new FormVirtualProxy());
        }

        private void buttonTestImageProxy_Click(object sender, System.EventArgs e)
        {
            this.pictureBox.Image = new ImageProxy().Image;
        }

        private void pictureBox_Click(object sender, System.EventArgs e)
        {

        }

        private void label1_Click(object sender, System.EventArgs e)
        {

        }

        private class ImageProxy
        {
            private static Image _image = null;
            private int _width = 133;
            private int _height = 154;
            private bool _retrieving = false;

            public int Width
            {
                get{ return _image == null ? _width : _image.Width; }
            }
            public int Height
            {
                get{ return _image == null ? _height : _image.Height; }
            }

            public Image Image
            {
                get
                {
                    if (_image != null)
                        return _image;
                    else
                    {
                        if (!_retrieving)
                        {
                            _retrieving = true;
                            Thread retrievalThread = new Thread(new ThreadStart(RetrieveImage));
                            retrievalThread.Start();
                        }
                        return PlaceHolderImage();
                    }
                }
            }

            public Image PlaceHolderImage()
            {
                return new Bitmap(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("DoFactory.HeadFirst.Proxy.VirtualProxy.PlaceHolder.jpg"));
            }

            public void RetrieveImage()
            {
                // Book image from amazon
                string url = "http://images.amazon.com/images/P/0596007124.01._PE34_SCMZZZZZZZ_.jpg";

                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                _image = Image.FromStream(response.GetResponseStream());
            }
        }
    }
}
