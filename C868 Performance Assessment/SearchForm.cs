using Elements.Database;
using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace C868_Performance_Assessment
{
    public partial class SearchForm : Form
    {
        private string searchQuery;

        public SearchForm()
        {
            InitializeComponent();
        }

        private void SearchForm_Load(object sender, EventArgs e)
        {
            PopulateDropDownLists();
        }

        private void PopulateDropDownLists()
        {
            MySqlDataReader dr = null;

            dr = MySqlDatabase.ExecuteReader("select customerName from customer order by customerName", MySqlDatabase.spl, GlobalVariables.DbConn);
            if (dr != null && dr.HasRows)
            {
                while (dr.Read())
                {
                    cboCustomer.Items.Add(dr[0].ToString());
                }
            }

            dr = MySqlDatabase.ExecuteReader("select distinct location from appointment order by location", MySqlDatabase.spl, GlobalVariables.DbConn);
            if (dr != null && dr.HasRows)
            {
                while (dr.Read())
                {
                    cboLocation.Items.Add(dr[0].ToString());
                }
            }

            dr = MySqlDatabase.ExecuteReader("select distinct type from appointment order by type", MySqlDatabase.spl, GlobalVariables.DbConn);
            if (dr != null && dr.HasRows)
            {
                while (dr.Read())
                {
                    cboType.Items.Add(dr[0].ToString());
                }
            }

            dr = MySqlDatabase.ExecuteReader("select userName from user order by userName", MySqlDatabase.spl, GlobalVariables.DbConn);
            if (dr != null && dr.HasRows)
            {
                while (dr.Read())
                {
                    cboCreatedBy.Items.Add(dr[0].ToString());
                }
            }
        }

        private void BuildQuery()
        {
            searchQuery = "";
            MySqlDatabase.spl.Add(new MySqlParameter("@Now", DateTime.UtcNow));
            searchQuery += "select c.customerName, a.title, a.description, a.location, a.contact, a.type, a.url, a.start, a.end, u.userName createdBy from appointment a " +
                           "inner join customer c on c.customerId = a.customerId inner join user u on u.userId = a.userId where end >= @Now ";
            if (cboCustomer.SelectedIndex > -1)
            {
                MySqlDatabase.spl.Add(new MySqlParameter("@Customer", cboCustomer.SelectedItem.ToString()));
                searchQuery += "and c.CustomerName = @Customer ";
            }
            if (!string.IsNullOrEmpty(txtTitle.Text))
            {
                MySqlDatabase.spl.Add(new MySqlParameter("@Title", "%" + txtTitle.Text + "%"));
                searchQuery += "and a.title like @Title ";
            }
            if (!string.IsNullOrEmpty(txtDescription.Text))
            {
                MySqlDatabase.spl.Add(new MySqlParameter("@Description", "%" + txtDescription.Text + "%"));
                searchQuery += "and a.Description like @Description ";
            }
            if (cboLocation.SelectedIndex > -1)
            {
                MySqlDatabase.spl.Add(new MySqlParameter("@Location", cboLocation.SelectedItem.ToString()));
                searchQuery += "and a.location = @Location ";
            }
            if (cboType.SelectedIndex > -1)
            {
                MySqlDatabase.spl.Add(new MySqlParameter("@Type", cboType.SelectedItem.ToString()));
                searchQuery += "and a.type = @Type ";
            }
            if (cboCreatedBy.SelectedIndex > -1)
            {
                MySqlDatabase.spl.Add(new MySqlParameter("@CreatedBy", cboCreatedBy.SelectedItem.ToString()));
                searchQuery += "and u.userName = @CreatedBy ";
            }
            searchQuery += "order by start";
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            BuildQuery();
            dgvSearchResultsView.DataSource = MySqlDatabase.FillDataTable(searchQuery, MySqlDatabase.spl, GlobalVariables.DbConn);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            cboCustomer.SelectedIndex = -1;
            txtTitle.Text = "";
            txtDescription.Text = "";
            cboLocation.SelectedIndex = -1;
            cboType.SelectedIndex = -1;
            cboCreatedBy.SelectedIndex = -1;
            dgvSearchResultsView.DataSource = null;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
