using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using WindowsFormsPresenter;
using WindowsFormsView;

namespace WindowsFormsApplication
{
    /// <summary>
    /// This is where users enter login credentials.
    /// </summary>
    /// <remarks>
    /// Valid demo values are: 
    ///   userName: debbie
    ///   password: secret123
    /// </remarks>
    public partial class FormLogin : Form, ILoginView
    {
        // The Presenter
        private LoginPresenter _loginPresenter;
        private bool _cancelClose;

        /// <summary>
        /// Default contructor of FormLogin.
        /// </summary>
        public FormLogin()
        {
            InitializeComponent();
            this.Closing += FormLogin_Closing;

            _loginPresenter = new LoginPresenter(this);
        }

        /// <summary>
        /// Gets the username.
        /// </summary>
        public string UserName
        {
            get { return textBoxUserName.Text.Trim(); }
        }

        /// <summary>
        /// Gets the password.
        /// </summary>
        public string Password
        {
            get { return textBoxPassword.Text.Trim(); }
        }


        /// <summary>
        /// Performs login and upson success closes dialog.
        /// </summary>
        private void buttonOK_Click(object sender, EventArgs e)
        {
            try
            {
                _loginPresenter.Login();
                this.Close();
            }
            catch (ApplicationException ex)
            {
                MessageBox.Show(ex.Message, "Login failed");
                _cancelClose = true;
            }
        }

        /// <summary>
        /// Cancel was requested. Now closes dialog.
        /// </summary>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Displays valid demo credentials
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabelValid_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("You can use the following credentials: \r\n\r\n" +
                "    UserName:    debbie \r\n" +
                "    PassWord:    secret123", "Login Credentials");
        }

        /// <summary>
        /// Provides opportunity to cancel the dialog close.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormLogin_Closing(object sender, CancelEventArgs e)
        {
            e.Cancel = _cancelClose;
            _cancelClose = false;
        }
    }
}
