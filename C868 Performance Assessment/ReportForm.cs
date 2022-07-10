using Elements.Database;
using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace C868_Performance_Assessment
{
    public partial class ReportForm : Form
    {
        public ReportForm()
        {
            InitializeComponent();
        }

        private void cboReportList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboReportList.SelectedIndex == -1)
            {
                dgvReportView.DataSource = null;
            }
            else if (cboReportList.SelectedIndex == 0)
            {
                AppointmentTypesByMonth();
            }
            else if (cboReportList.SelectedIndex == 1)
            {
                ScheduleByUser();
            }
            else if (cboReportList.SelectedIndex == 2)
            {
                InactiveCustomers();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            cboReportList.SelectedIndex = -1;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AppointmentTypesByMonth()
        {
            dgvReportView.DataSource = MySqlDatabase.FillDataTable("select year(a.start) as 'Year', a.type as 'Appointment Type', count(case month(a.start) when 1 then a.type end) as 'Jan', count(case month(a.start) when 2 then a.type end) as 'Feb', " +
                "count(case month(a.start) when 3 then a.type end) as 'Mar', count(case month(a.start) when 4 then a.type end) as 'Apr', count(case month(a.start) when 5 then a.type end) as 'May', " +
                "count(case month(a.start) when 6 then a.type end) as 'Jun', count(case month(a.start) when 7 then a.type end) as 'Jul', count(case month(a.start) when 8 then a.type end) as 'Aug', " +
                "count(case month(a.start) when 9 then a.type end) as 'Sep', count(case month(a.start) when 10 then a.type end) as 'Oct', count(case month(a.start) when 11 then a.type end) as 'Nov', " +
                "count(case month(a.start) when 12 then a.type end) as 'Dec' from appointment a group by year(a.start), a.type order by year(a.start), a.type", MySqlDatabase.spl, GlobalVariables.DbConn);
        }

        private void ScheduleByUser()
        {
            MySqlDatabase.spl.Add(new MySqlParameter("@TimeZone", TimeZoneInfo.Local.GetUtcOffset(DateTimeOffset.Now).ToString().Substring(0, 6)));
            dgvReportView.DataSource = MySqlDatabase.FillDataTable("select u.userName as 'User Name', c.customerName as 'Customer Name', a.title as 'Appt Title', a.description as 'Appt Description', a.location as 'Appt Location', a.contact as 'Appt Contact', " +
                "a.type as 'Appt Type', a.url as 'Appt URL', convert_tz(a.start, '+00:00', @TimeZone) as 'Appt Start', convert_tz(a.end, '+00:00', @TimeZone) as 'Appt End' from appointment a " +
                "inner join user u on u.userId = a.userId inner join customer c on c.customerId = a.customerId order by u.userName, a.start", MySqlDatabase.spl, GlobalVariables.DbConn);
        }

        private void InactiveCustomers()
        {
            dgvReportView.DataSource = MySqlDatabase.FillDataTable("select c.customerName as 'Customer Name', a.address as 'Address 1', a.address2 as 'Address 2', ci.city as 'City', a.postalCode as 'Postal Code', co.country as 'Country', a.phone as 'Phone' " +
                "from customer c inner join address a on a.addressId = c.addressId inner join city ci on ci.cityId = a.cityId inner join country co on co.countryId = ci.countryId where c.active = 0", MySqlDatabase.spl, GlobalVariables.DbConn);
        }
    }
}