using Elements.Database;
using Elements.Logger;
using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Linq;
using System.Windows.Forms;

namespace C868_Performance_Assessment
{
    public partial class MainForm : Form
    {

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            GlobalVariables.DbConn = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).ConnectionStrings.ConnectionStrings["C868_Performance_Assessment.Properties.Settings.dbConnStr"].ConnectionString;
            GlobalVariables.UserId = 0;
            GlobalVariables.UserName = "";
            GlobalVariables.LoggedIn = false;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (GlobalVariables.LoggedIn) { Logging.LogMessage("UserLoginActivity", GlobalVariables.UserName + " (user ID " + GlobalVariables.UserId + ") logged out", "log", "LogOut"); }
        }

        private void btnLogToggle_Click(object sender, EventArgs e)
        {
            if (!GlobalVariables.LoggedIn) { LogIn(); } else { LogOut(); }
        }

        private void LogIn()
        {
            Form f = new LoginForm();
            f.ShowDialog();
            if (GlobalVariables.LoggedIn && GlobalVariables.UserId > 0)
            {
                Logging.LogMessage("UserLoginActivity", GlobalVariables.UserName + " (user ID " + GlobalVariables.UserId + ") logged in", "log", "LogIn");
                btnLogToggle.Text = "Log Out";
                btnCustomers.Enabled = true;
                btnAppointments.Enabled = true;
                btnSearch.Enabled = true;
                btnReports.Enabled = true;
                lblAppointments.Text = "Upcoming Appointments for " + GlobalVariables.UserName;
                btnRefresh.Enabled = true;
                flpAppointments.Enabled = true;
                cbAutoRefresh.Enabled = true;
                cbAutoRefresh.Checked = true;
                nudAutoRefresh.Value = 1;
                rbViewAll.Enabled = true;
                rbViewAll.Checked = true;
                rbViewWeek.Enabled = true;
                rbViewWeek.Checked = false;
                rbViewMonth.Enabled = true;
                rbViewMonth.Checked = false;
                LoadUpcomingAppointments();
                AlertUpcomingAppointment();
            }
        }

        private void LogOut()
        {
            Logging.LogMessage("UserLoginActivity", GlobalVariables.UserName + " (user ID " + GlobalVariables.UserId + ") logged out", "log", "LogOut");
            btnLogToggle.Text = "Log In";
            btnCustomers.Enabled = false;
            btnAppointments.Enabled = false;
            btnSearch.Enabled = false;
            btnReports.Enabled = false;
            lblAppointments.Text = "Upcoming Appointments";
            btnRefresh.Enabled = false;
            flpAppointments.Controls.Clear();
            flpAppointments.Enabled = false;
            cbAutoRefresh.Enabled = false;
            cbAutoRefresh.Checked = false;
            nudAutoRefresh.Value = 1;
            rbViewAll.Enabled = false;
            rbViewAll.Checked = true;
            rbViewWeek.Enabled = false;
            rbViewWeek.Checked = false;
            rbViewMonth.Enabled = false;
            rbViewMonth.Checked = false;
            GlobalVariables.UserId = 0;
            GlobalVariables.UserName = "";
            GlobalVariables.LoggedIn = false;
        }

        private void LoadUpcomingAppointments()
        {
            MySqlDataReader dr = null;
            DateTime curUtcDateTime = DateTime.UtcNow;

            GlobalVariables.appointmentList.Clear();
            tmrAutoRefresh.Stop();

            MySqlDatabase.spl.Add(new MySqlParameter("@UserId", GlobalVariables.UserId));
            MySqlDatabase.spl.Add(new MySqlParameter("@Now", curUtcDateTime));
            if (rbViewAll.Checked)
            {
                dr = MySqlDatabase.ExecuteReader("select a.appointmentId, c.customerName, a.title, a.description, a.location, a.contact, a.type, a.url, a.start, a.end, a.createDate from appointment a inner join customer c on c.customerId = a.customerId where userId = @UserId and end >= @Now order by start", MySqlDatabase.spl, GlobalVariables.DbConn);
            }
            else if (rbViewWeek.Checked)
            {
                MySqlDatabase.spl.Add(new MySqlParameter("@FromNow", curUtcDateTime.AddDays(7)));
                dr = MySqlDatabase.ExecuteReader("select a.appointmentId, c.customerName, a.title, a.description, a.location, a.contact, a.type, a.url, a.start, a.end, a.createDate from appointment a inner join customer c on c.customerId = a.customerId where userId = @UserId and end >= @Now and end <= @FromNow order by start", MySqlDatabase.spl, GlobalVariables.DbConn);
            }
            else if (rbViewMonth.Checked)
            {
                MySqlDatabase.spl.Add(new MySqlParameter("@FromNow", curUtcDateTime.AddMonths(1)));
                dr = MySqlDatabase.ExecuteReader("select a.appointmentId, c.customerName, a.title, a.description, a.location, a.contact, a.type, a.url, a.start, a.end, a.createDate from appointment a inner join customer c on c.customerId = a.customerId where userId = @UserId and end >= @Now and end <= @FromNow order by start", MySqlDatabase.spl, GlobalVariables.DbConn);
            }

            if (dr != null && dr.HasRows)
            {
                while (dr.Read())
                {
                    GlobalVariables.appointmentList.Add(new Appointment(dr.GetInt32(0), dr.GetString(1), dr.GetString(2), dr.GetString(3), dr.GetString(4), dr.GetString(5), dr.GetString(6), dr.GetString(7), TimeZoneInfo.ConvertTimeFromUtc(dr.GetDateTime(8), TimeZoneInfo.Local), TimeZoneInfo.ConvertTimeFromUtc(dr.GetDateTime(9), TimeZoneInfo.Local)));
                }
            }

            PopulateAppointmentList();
            tmrAutoRefresh.Start();
        }

        private void PopulateAppointmentList()
        {
            flpAppointments.Controls.Clear();

            if (GlobalVariables.appointmentList.Count > 0)
            {
                string appointmentInfo;

                //Lambda used to simplify foreach statement
                GlobalVariables.appointmentList.ForEach(a =>
                    {
                        appointmentInfo = "";
                        appointmentInfo += "Appointment for " + a.CustomerName + " from " + a.Start.ToString("yyyy-MM-dd HH:mm") + " to " + a.End.ToString("yyyy-MM-dd HH:mm") + Environment.NewLine;
                        appointmentInfo += "Title: " + a.Title + Environment.NewLine;
                        appointmentInfo += "Description: " + a.Description + Environment.NewLine;
                        appointmentInfo += "Contact: " + a.Contact + Environment.NewLine;
                        appointmentInfo += "Location: " + a.Location + Environment.NewLine;
                        appointmentInfo += "URL: " + a.Url + Environment.NewLine;
                        appointmentInfo += "Meeting type: " + a.Type;
                        TextBox t = new TextBox
                        {
                            Text = appointmentInfo,
                            AutoSize = false,
                            Width = 500,
                            Height = 100,
                            BorderStyle = BorderStyle.FixedSingle,
                            ScrollBars = ScrollBars.Vertical,
                            ReadOnly = true,
                            Multiline = true,
                            WordWrap = true
                        };
                        if (GlobalVariables.appointmentList.Count <= 3) { t.Width = 517; }
                        flpAppointments.Controls.Add(t);
                    });
            }
        }

        private void btnCustomers_Click(object sender, EventArgs e)
        {
            Form f = new CustomerForm();
            f.ShowDialog();
        }

        private void btnAppointments_Click(object sender, EventArgs e)
        {
            Form f = new AppointmentForm();
            f.ShowDialog();
            LoadUpcomingAppointments();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Form f = new SearchForm();
            f.ShowDialog();
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            Form f = new ReportForm();
            f.ShowDialog();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadUpcomingAppointments();
        }

        private void tmrAutoRefresh_Tick(object sender, EventArgs e)
        {
            LoadUpcomingAppointments();
        }

        private void cbAutoRefresh_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAutoRefresh.Checked)
            {
                nudAutoRefresh.Enabled = true;
                tmrAutoRefresh.Start();
            }
            else
            {
                nudAutoRefresh.Enabled = false;
                tmrAutoRefresh.Stop();
            }
        }

        private void nudAutoRefresh_ValueChanged(object sender, EventArgs e)
        {
            tmrAutoRefresh.Interval = (int)nudAutoRefresh.Value * 60000;
        }

        private void rbViewAll_CheckedChanged(object sender, EventArgs e)
        {
            if (rbViewAll.Checked) { LoadUpcomingAppointments(); }
        }

        private void rbViewWeek_CheckedChanged(object sender, EventArgs e)
        {
            if (rbViewWeek.Checked) { LoadUpcomingAppointments(); }
        }

        private void rbViewMonth_CheckedChanged(object sender, EventArgs e)
        {
            if (rbViewMonth.Checked) { LoadUpcomingAppointments(); }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (GlobalVariables.LoggedIn) { LogOut(); }
            Application.Exit();
        }

        private void AlertUpcomingAppointment()
        {
            if (GlobalVariables.appointmentList.Count > 0)
            {
                try
                {
                    //Used lambda expression here to simplify the process of finding the earliest start date of future appointments that fall within a 15 minute window of logging in
                    Appointment upcomingAppointment = GlobalVariables.appointmentList.Where(s => s.Start.AddMinutes(-15) <= DateTime.Now).Where(s => s.Start > DateTime.Now).OrderBy(s => s.Start).First();
                    if (upcomingAppointment != null) { MessageBox.Show("Upcoming meeting with " + upcomingAppointment.CustomerName + " starting in " + Math.Ceiling(upcomingAppointment.Start.Subtract(DateTime.Now).TotalSeconds / 60.0).ToString() + " minutes."); }
                }
                catch
                {

                }
            }
        }
    }
}