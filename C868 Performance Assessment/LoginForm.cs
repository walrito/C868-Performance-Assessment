using Elements.Database;
using MySql.Data.MySqlClient;
using System;
using System.Globalization;
using System.Resources;
using System.Reflection;
using System.Windows.Forms;

namespace C868_Performance_Assessment
{
    public partial class LoginForm : Form
    {
        private ResourceManager resMan;

        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            CultureInfo.CurrentCulture.ClearCachedData();
            switch (CultureInfo.CurrentCulture.TwoLetterISOLanguageName)
            {
                case "de":
                    resMan = new ResourceManager("C868_Performance_Assessment.lang_de", Assembly.GetExecutingAssembly());
                    break;
                default:
                    resMan = new ResourceManager("C868_Performance_Assessment.lang_en", Assembly.GetExecutingAssembly());
                    break;
            }

            Text = resMan.GetString("btnLogin");
            lblUsername.Text = resMan.GetString("lblUsername");
            lblPassword.Text = resMan.GetString("lblPassword");
            btnLogin.Text = resMan.GetString("btnLogin");
            btnCancel.Text = resMan.GetString("btnCancel");
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrEmpty(mtbPassword.Text))
            {
                MessageBox.Show(resMan.GetString("emptyFields"));
            }
            else
            {
                User u = Login.SetGlobalUser(txtUsername.Text, mtbPassword.Text, GlobalVariables.DbConn);
                if (u != null)
                {
                    GlobalVariables.UserId = u.UserId;
                    GlobalVariables.UserName = u.UserName;
                    GlobalVariables.LoggedIn = true;
                    Close();
                }
                else
                {
                    MessageBox.Show(resMan.GetString("loginMismatch")); ;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void llCreateNewUser_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form f = new NewUserForm();
            f.ShowDialog();
        }
    }
}