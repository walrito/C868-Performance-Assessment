using Elements.Database;
using Elements.Logger;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C868_Performance_Assessment
{
    public partial class NewUserForm : Form
    {
        public NewUserForm()
        {
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlDatabase.spl.Add(new MySqlParameter("@Username", txtUsername.Text));
                if ((int)MySqlDatabase.GetCount("select count(*) from user where userName = @Username", MySqlDatabase.spl, GlobalVariables.DbConn) > 0)
                {
                    MessageBox.Show("Username already in use.");
                }
                else if (mtbPassword.Text != mtbConfirmPassword.Text)
                {
                    MessageBox.Show("Passwords do not match.");
                }
                else
                {
                    MySqlDatabase.spl.Add(new MySqlParameter("@Username", txtUsername.Text));
                    MySqlDatabase.spl.Add(new MySqlParameter("@Password", mtbPassword.Text));
                    MySqlDatabase.spl.Add(new MySqlParameter("@CurUtcTime", DateTime.UtcNow));
                    MySqlDatabase.ExecuteNonQuery("insert into user (userName, password, active, createDate, createdBy, lastUpdate, lastUpdateBy) " +
                        "values (@Username, @Password, 1, @CurUtcTime, @Username, @CurUtcTime, @Username)", MySqlDatabase.spl, GlobalVariables.DbConn);
                    MessageBox.Show("User '" + txtUsername.Text + "' created successfully.");
                    Close();
                }
            }
            catch (Exception ex)
            {
                Logging.LogMessage("ErrorLog", ex.Message, "error", "btnCreate_Click");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}