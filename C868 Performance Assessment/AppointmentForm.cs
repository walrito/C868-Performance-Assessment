using Elements.Database;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace C868_Performance_Assessment
{
    public partial class AppointmentForm : Form
    {
        public AppointmentForm()
        {
            InitializeComponent();
        }

        private void AppointmentForm_Load(object sender, EventArgs e)
        {
            dtpStart.CustomFormat = "yyyy-MM-dd HH:mm";
            dtpEnd.CustomFormat = "yyyy-MM-dd HH:mm";
            ToggleButtons();
            PopulateCustomerList();
            PopulateAppointmentList();
            ClearFields();
        }

        private void cboAppointmentList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboAppointmentList.SelectedIndex == -1)
            {
                ClearFields();
                ToggleButtons();
            }
            else
            {
                LoadSelectedAppointment();
                ToggleButtons();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboAppointmentList.SelectedIndex > -1)
                {
                    if (MessageBox.Show("Delete '" + cboAppointmentList.Text + "' from appointment list?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        MySqlDatabase.spl.Add(new MySqlParameter("@AppointmentId", cboAppointmentList.SelectedValue.ToString()));
                        MySqlDatabase.ExecuteNonQuery("delete from appointment where appointmentId = @AppointmentId", MySqlDatabase.spl, GlobalVariables.DbConn);

                        PopulateAppointmentList();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem deleting appointment data." + Environment.NewLine + Environment.NewLine + ex.Message);
            }
        }


        private void btnNew_Click(object sender, EventArgs e)
        {
            cboAppointmentList.SelectedIndex = -1;
            ToggleButtons();
            ClearFields();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Add '" + txtTitle.Text + "' to appointment list?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (CheckValidFields())
                    {
                        string businessHourMessage = WithinBusinessHours(dtpStart.Value, dtpEnd.Value);
                        if (string.IsNullOrEmpty(businessHourMessage))
                        {
                            if (!HasConflictingAppointments("", cboCustomerList.SelectedValue.ToString(), dtpStart.Value, dtpEnd.Value))
                            {
                                MySqlDatabase.spl.Add(new MySqlParameter("@CustomerId", cboCustomerList.SelectedValue.ToString()));
                                MySqlDatabase.spl.Add(new MySqlParameter("@UserId", GlobalVariables.UserId.ToString()));
                                MySqlDatabase.spl.Add(new MySqlParameter("@Title", txtTitle.Text));
                                MySqlDatabase.spl.Add(new MySqlParameter("@Description", txtDescription.Text));
                                MySqlDatabase.spl.Add(new MySqlParameter("@Location", txtLocation.Text));
                                MySqlDatabase.spl.Add(new MySqlParameter("@Contact", txtContact.Text));
                                MySqlDatabase.spl.Add(new MySqlParameter("@Type", txtType.Text));
                                MySqlDatabase.spl.Add(new MySqlParameter("@Url", txtUrl.Text));
                                MySqlDatabase.spl.Add(new MySqlParameter("@Start", TimeZoneInfo.ConvertTimeToUtc(dtpStart.Value, TimeZoneInfo.Local)));
                                MySqlDatabase.spl.Add(new MySqlParameter("@End", TimeZoneInfo.ConvertTimeToUtc(dtpEnd.Value, TimeZoneInfo.Local)));
                                MySqlDatabase.spl.Add(new MySqlParameter("@CurUtcTime", DateTime.UtcNow));
                                MySqlDatabase.spl.Add(new MySqlParameter("@User", GlobalVariables.UserName));
                                MySqlDatabase.ExecuteNonQuery("insert into appointment (customerId, userId, title, description, location, contact, type, url, start, end, createDate, createdBy, lastUpdate, lastUpdateBy) " +
                                    "values (@CustomerId, @UserId, @Title, @Description, @Location, @Contact, @Type, @Url, @Start, @End, @CurUtcTime, @User, @CurUtcTime, @User)", MySqlDatabase.spl, GlobalVariables.DbConn);

                                PopulateAppointmentList();
                            }
                            else
                            {
                                MessageBox.Show("Selected appointment times conflict with existing appointment for " + cboCustomerList.Text + ".");
                            }
                        }
                        else
                        {
                            MessageBox.Show(businessHourMessage);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem saving appointment data." + Environment.NewLine + Environment.NewLine + ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Make changes to '" + cboAppointmentList.Text + "'?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (CheckValidFields())
                    {
                        string businessHourMessage = WithinBusinessHours(dtpStart.Value, dtpEnd.Value);
                        if (string.IsNullOrEmpty(businessHourMessage))
                        {
                            if (!HasConflictingAppointments(cboAppointmentList.SelectedValue.ToString(), cboCustomerList.SelectedValue.ToString(), dtpStart.Value, dtpEnd.Value))
                            {
                                MySqlDatabase.spl.Add(new MySqlParameter("@AppointmentId", cboAppointmentList.SelectedValue.ToString()));
                                MySqlDatabase.spl.Add(new MySqlParameter("@CustomerId", cboCustomerList.SelectedValue.ToString()));
                                MySqlDatabase.spl.Add(new MySqlParameter("@UserId", GlobalVariables.UserId));
                                MySqlDatabase.spl.Add(new MySqlParameter("@Title", txtTitle.Text));
                                MySqlDatabase.spl.Add(new MySqlParameter("@Description", txtDescription.Text));
                                MySqlDatabase.spl.Add(new MySqlParameter("@Location", txtLocation.Text));
                                MySqlDatabase.spl.Add(new MySqlParameter("@Contact", txtContact.Text));
                                MySqlDatabase.spl.Add(new MySqlParameter("@Type", txtType.Text));
                                MySqlDatabase.spl.Add(new MySqlParameter("@Url", txtUrl.Text));
                                MySqlDatabase.spl.Add(new MySqlParameter("@Start", TimeZoneInfo.ConvertTimeToUtc(dtpStart.Value, TimeZoneInfo.Local)));
                                MySqlDatabase.spl.Add(new MySqlParameter("@End", TimeZoneInfo.ConvertTimeToUtc(dtpEnd.Value, TimeZoneInfo.Local)));
                                MySqlDatabase.spl.Add(new MySqlParameter("@CurUtcTime", DateTime.UtcNow));
                                MySqlDatabase.spl.Add(new MySqlParameter("@User", GlobalVariables.UserName));
                                MySqlDatabase.ExecuteNonQuery("update appointment set customerId = @CustomerId, userId = @UserId, title = @Title, description = @Description, location = @Location, contact = @Contact, type = @Type, url = @Url, " +
                                    "start = @Start, end = @End, createDate = @CurUtcTime, createdBy = @User, lastUpdate = @CurUtcTime, lastUpdateBy = @User where appointmentId = @AppointmentId", MySqlDatabase.spl, GlobalVariables.DbConn);

                                PopulateAppointmentList();
                            }
                            else
                            {
                                MessageBox.Show("Selected appointment times conflict with existing appointment for " + cboCustomerList.Text + ".");
                            }
                        }
                        else
                        {
                            MessageBox.Show(businessHourMessage);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem updating appointment data." + Environment.NewLine + Environment.NewLine + ex.Message);
            }
        }


        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        public void LoadSelectedAppointment()
        {
            if (cboAppointmentList.SelectedValue != null)
            {
                MySqlDatabase.spl.Add(new MySqlParameter("@AppointmentId", cboAppointmentList.SelectedValue.ToString()));
                MySqlDataReader dr = MySqlDatabase.ExecuteReader("select customerId, title, description, location, contact, type, url, start, end from appointment where appointmentId = @AppointmentId", MySqlDatabase.spl, GlobalVariables.DbConn);
                if (dr != null && dr.HasRows)
                {
                    dr.Read();
                    cboCustomerList.SelectedValue = dr.GetInt32(0);
                    txtTitle.Text = dr.GetString(1);
                    txtDescription.Text = dr.GetString(2);
                    txtLocation.Text = dr.GetString(3);
                    txtContact.Text = dr.GetString(4);
                    txtType.Text = dr.GetString(5);
                    txtUrl.Text = dr.GetString(6);
                    dtpStart.Value = TimeZoneInfo.ConvertTimeFromUtc(dr.GetDateTime(7), TimeZoneInfo.Local);
                    dtpEnd.Value = TimeZoneInfo.ConvertTimeFromUtc(dr.GetDateTime(8), TimeZoneInfo.Local);
                }
            }
        }

        private void PopulateAppointmentList()
        {
            cboAppointmentList.DataSource = null;
            List<AppointmentList> appointmentList = new List<AppointmentList>();
            MySqlDatabase.spl.Add(new MySqlParameter("@CurUtcTime", DateTime.UtcNow));
            MySqlDataReader dr = MySqlDatabase.ExecuteReader("select appointmentId, title from appointment where start >= @CurUtcTime order by start", MySqlDatabase.spl, GlobalVariables.DbConn);
            if (dr != null && dr.HasRows)
            {
                while (dr.Read())
                {
                    appointmentList.Add(new AppointmentList { AppointmentId = dr.GetInt32(0), AppointmentTitle = dr.GetString(1) });
                }
                cboAppointmentList.DisplayMember = "AppointmentTitle";
                cboAppointmentList.ValueMember = "AppointmentId";
                cboAppointmentList.DataSource = appointmentList;
            }
            cboAppointmentList.SelectedIndex = -1;
        }

        private void PopulateCustomerList()
        {
            cboCustomerList.DataSource = null;
            List<CustomerList> customerList = new List<CustomerList>();
            MySqlDataReader dr = MySqlDatabase.ExecuteReader("select customerId, customerName from customer order by customerName", MySqlDatabase.spl, GlobalVariables.DbConn);
            if (dr != null && dr.HasRows)
            {
                while (dr.Read())
                {
                    customerList.Add(new CustomerList(dr.GetInt32(0), dr.GetString(1)));
                }
                cboCustomerList.DisplayMember = "CustomerName";
                cboCustomerList.ValueMember = "CustomerId";
                cboCustomerList.DataSource = customerList;
            }
            cboCustomerList.SelectedIndex = -1;
        }

        public string WithinBusinessHours(DateTime start, DateTime end)
        {
            TimeSpan businessStart = TimeSpan.Parse("08:00");
            TimeSpan businessEnd = TimeSpan.Parse("17:00");

            if (start.Date == end.Date)
            {
                if (start.TimeOfDay <= end.TimeOfDay)
                {
                    if (start.Hour < businessStart.Hours) { return "Meetings cannot be scheduled to start before business opens at 8:00AM."; }
                    else if (end.Hour > businessEnd.Hours) { return "Meetings cannot be scheduled to end after business closes at 5:00PM."; }
                    else { return ""; }
                }
                else
                {
                    return "Start time cannot be after end time.";
                }
            }
            else
            {
                return "Meetings cannot be scheduled across multiple days.";
            }
        }

        public bool HasConflictingAppointments(string appointmentId, string customerId, DateTime start, DateTime end)
        {
            string appointmentClause = "";
            if (!string.IsNullOrEmpty(appointmentId)) { appointmentClause = " and appointmentId <> @AppointmentId"; }
            MySqlDatabase.spl.Add(new MySqlParameter("@AppointmentId", appointmentId));
            MySqlDatabase.spl.Add(new MySqlParameter("@CustomerId", customerId));
            MySqlDatabase.spl.Add(new MySqlParameter("@Start", TimeZoneInfo.ConvertTimeToUtc(start).ToString("yyyy-MM-dd HH:mm")));
            MySqlDatabase.spl.Add(new MySqlParameter("@End", TimeZoneInfo.ConvertTimeToUtc(end).ToString("yyyy-MM-dd HH:mm")));
            long conflictFound = (MySqlDatabase.GetCount("select count(*) from appointment where customerId = @CustomerId and ((@Start >= start and @End <= end) or (@Start <= start and @End > start) or (@Start < end and @End >= end) or (@Start <= start and @End >= end))" + appointmentClause, MySqlDatabase.spl, GlobalVariables.DbConn));
            if (conflictFound > 0) { return true; } else { return false; }
        }

        private bool CheckValidFields()
        {
            bool isValid = true;
            string errorMessage = "";
            if (string.IsNullOrEmpty(cboCustomerList.Text)) { isValid = false; errorMessage += "Customer field cannot be blank." + Environment.NewLine; }
            if (string.IsNullOrEmpty(txtTitle.Text)) { isValid = false; errorMessage += "Title field cannot be blank." + Environment.NewLine; }
            if (string.IsNullOrEmpty(txtDescription.Text)) { isValid = false; errorMessage += "Description field cannot be blank." + Environment.NewLine; }
            if (string.IsNullOrEmpty(txtLocation.Text)) { isValid = false; errorMessage += "Location field cannot be blank." + Environment.NewLine; }
            if (string.IsNullOrEmpty(txtContact.Text)) { isValid = false; errorMessage += "Contact field cannot be blank." + Environment.NewLine; }
            if (string.IsNullOrEmpty(txtType.Text)) { isValid = false; errorMessage += "Type field cannot be blank." + Environment.NewLine; }
            if (string.IsNullOrEmpty(txtUrl.Text)) { isValid = false; errorMessage += "URL field cannot be blank."; }
            if (!string.IsNullOrEmpty(errorMessage)) { MessageBox.Show(errorMessage); }
            return isValid;
        }

        private void ToggleButtons()
        {
            if (cboCustomerList.SelectedIndex == -1)
            {
                btnAdd.Enabled = true;
                btnAdd.Visible = true;
                btnUpdate.Enabled = false;
                btnUpdate.Visible = false;
            }
            else
            {
                btnAdd.Enabled = false;
                btnAdd.Visible = false;
                btnUpdate.Enabled = true;
                btnUpdate.Visible = true;
            }
        }

        private void ClearFields()
        {
            DateTime curDateTime = DateTime.Now;
            cboCustomerList.SelectedIndex = -1;
            txtTitle.Text = "";
            txtDescription.Text = "";
            txtLocation.Text = "";
            txtContact.Text = "";
            txtType.Text = "";
            txtUrl.Text = "";
            dtpStart.Value = curDateTime;
            dtpEnd.Value = curDateTime;
        }
    }
}

public class AppointmentList
{
    public int AppointmentId { get; set; }
    public string AppointmentTitle { get; set; }
}