using Elements.Database;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace C868_Performance_Assessment
{
    public partial class CustomerForm : Form
    {
        public CustomerForm()
        {
            InitializeComponent();
        }

        private void CustomerForm_Load(object sender, EventArgs e)
        {
            ToggleButtons();
            PopulateCustomerList();
            PopulateCountryList();
            ClearFields();
        }

        private void cboCustomerList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCustomerList.SelectedIndex == -1)
            {
                ClearFields();
                ToggleButtons();
            }
            else
            {
                LoadSelectedCustomer();
                ToggleButtons();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboCustomerList.SelectedIndex > -1)
                {
                    if (MessageBox.Show("Delete " + cboCustomerList.Text + " from customer list?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        MySqlDatabase.spl.Add(new MySqlParameter("@CustomerId", cboCustomerList.SelectedValue.ToString()));
                        MySqlDatabase.ExecuteNonQuery("delete from appointment where customerId = @CustomerId", MySqlDatabase.spl, GlobalVariables.DbConn);

                        MySqlDatabase.spl.Add(new MySqlParameter("@CustomerId", cboCustomerList.SelectedValue.ToString()));
                        MySqlDatabase.ExecuteNonQuery("delete from customer where customerId = @CustomerId", MySqlDatabase.spl, GlobalVariables.DbConn);

                        PopulateCustomerList();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem deleting customer data." + Environment.NewLine + Environment.NewLine + ex.Message);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            cboCustomerList.SelectedIndex = -1;
            ToggleButtons();
            ClearFields();
        }

        private void cboCountry_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cboCountry.Text)) { PopulateCityList(); }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Add " + txtName.Text + " to customer list?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (CheckValidFields())
                    {
                        string countryId = CountryData();
                        string cityId = CityData(countryId);
                        string addressId = AddressData(cityId);

                        MySqlDatabase.spl.Add(new MySqlParameter("@CustomerName", txtName.Text));
                        MySqlDatabase.spl.Add(new MySqlParameter("@AddressId", addressId));
                        MySqlDatabase.spl.Add(new MySqlParameter("@Active", cbActive.Checked));
                        MySqlDatabase.spl.Add(new MySqlParameter("@CurUtcTime", DateTime.UtcNow));
                        MySqlDatabase.spl.Add(new MySqlParameter("@User", GlobalVariables.UserName));
                        MySqlDatabase.ExecuteNonQuery("insert into customer (customerName, addressId, active, createDate, createdBy, lastUpdate, lastUpdateBy) values (@CustomerName, @AddressId, @Active, @CurUtcTime, @User, @CurUtcTime, @User)", MySqlDatabase.spl, GlobalVariables.DbConn);

                        PopulateCustomerList();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem saving customer data." + Environment.NewLine + Environment.NewLine + ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Make changes to " + cboCustomerList.Text + "?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (CheckValidFields())
                    {
                        string countryId = CountryData();
                        string cityId = CityData(countryId);
                        string addressId = AddressData(cityId);

                        MySqlDatabase.spl.Add(new MySqlParameter("@CustomerId", cboCustomerList.SelectedValue.ToString()));
                        MySqlDatabase.spl.Add(new MySqlParameter("@CustomerName", txtName.Text));
                        MySqlDatabase.spl.Add(new MySqlParameter("@AddressId", addressId));
                        MySqlDatabase.spl.Add(new MySqlParameter("@Active", cbActive.Checked));
                        MySqlDatabase.spl.Add(new MySqlParameter("@CurUtcTime", DateTime.UtcNow));
                        MySqlDatabase.spl.Add(new MySqlParameter("@User", GlobalVariables.UserName));
                        MySqlDatabase.ExecuteNonQuery("update customer set customerName = @CustomerName, addressId = @addressId, active = @Active, lastUpdate = @CurUtcTime, lastUpdateBy = @User where customerId = @CustomerId", MySqlDatabase.spl, GlobalVariables.DbConn);

                        PopulateCustomerList();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem updating customer data." + Environment.NewLine + Environment.NewLine + ex.Message);
            }
        }

        private bool CheckValidFields()
        {
            bool isValid = true;
            string errorMessage = "";
            if (string.IsNullOrEmpty(cboCountry.Text)) { isValid = false; errorMessage += "Country field cannot be blank." + Environment.NewLine; }
            if (string.IsNullOrEmpty(txtName.Text)) { isValid = false; errorMessage += "Name field cannot be blank." + Environment.NewLine; }
            if (string.IsNullOrEmpty(txtAddress1.Text)) { isValid = false; errorMessage += "Address field cannot be blank." + Environment.NewLine; }
            if (string.IsNullOrEmpty(cboCity.Text)) { isValid = false; errorMessage += "City field cannot be blank." + Environment.NewLine; }
            if (string.IsNullOrEmpty(txtPostalCode.Text)) { isValid = false; errorMessage += "Postal Code field cannot be blank." + Environment.NewLine; }
            if (string.IsNullOrEmpty(txtPhone.Text)) { isValid = false; errorMessage += "Phone field cannot be blank."; }
            if (!string.IsNullOrEmpty(errorMessage)) { MessageBox.Show(errorMessage); }
            return isValid;
        }

        private string CountryData()
        {
            MySqlDataReader dr;
            string countryId = "";

            if (cboCountry.SelectedValue != null)
            {
                countryId = cboCountry.SelectedValue.ToString();
            }
            else
            {
                MySqlDatabase.spl.Add(new MySqlParameter("@Country", cboCountry.Text));
                dr = MySqlDatabase.ExecuteReader("select countryId from country where country = @Country", MySqlDatabase.spl, GlobalVariables.DbConn);
                if (dr != null && dr.HasRows)
                {
                    dr.Read();
                    countryId = dr.GetString(0);
                }
                else
                {
                    MySqlDatabase.spl.Add(new MySqlParameter("@Country", cboCountry.Text));
                    MySqlDatabase.spl.Add(new MySqlParameter("@CurUtcTime", DateTime.UtcNow));
                    MySqlDatabase.spl.Add(new MySqlParameter("@User", GlobalVariables.UserName));
                    MySqlDatabase.ExecuteNonQuery("insert into country (country, createDate, createdBy, lastUpdate, lastUpdateBy) values (@Country, @CurUtcTime, @User, @CurUtcTime, @User)", MySqlDatabase.spl, GlobalVariables.DbConn);

                    MySqlDatabase.spl.Add(new MySqlParameter("@Country", cboCountry.Text));
                    dr = MySqlDatabase.ExecuteReader("select countryId from country where country = @Country", MySqlDatabase.spl, GlobalVariables.DbConn);
                    if (dr != null && dr.HasRows)
                    {
                        dr.Read();
                        countryId = dr.GetString(0);
                    }
                }
            }

            return countryId;
        }

        private string CityData(string countryId)
        {
            MySqlDataReader dr;
            string cityId = "";

            MySqlDatabase.spl.Add(new MySqlParameter("@City", cboCity.Text));
            MySqlDatabase.spl.Add(new MySqlParameter("@CountryId", countryId));
            dr = MySqlDatabase.ExecuteReader("select cityId from city where city = @City and countryId = @CountryId", MySqlDatabase.spl, GlobalVariables.DbConn);
            if (dr != null && dr.HasRows)
            {
                dr.Read();
                cityId = dr.GetString(0);
            }
            else
            {
                MySqlDatabase.spl.Add(new MySqlParameter("@City", cboCity.Text));
                MySqlDatabase.spl.Add(new MySqlParameter("@CountryId", countryId));
                MySqlDatabase.spl.Add(new MySqlParameter("@CurUtcTime", DateTime.UtcNow));
                MySqlDatabase.spl.Add(new MySqlParameter("@User", GlobalVariables.UserName));
                MySqlDatabase.ExecuteNonQuery("insert into city (city, countryId, createDate, createdBy, lastUpdate, lastUpdateBy) values (@City, @CountryId, @CurUtcTime, @User, @CurUtcTime, @User)", MySqlDatabase.spl, GlobalVariables.DbConn);

                MySqlDatabase.spl.Add(new MySqlParameter("@City", cboCity.Text));
                MySqlDatabase.spl.Add(new MySqlParameter("@CountryId", countryId));
                dr = MySqlDatabase.ExecuteReader("select cityId from city where city = @City and countryId = @CountryId", MySqlDatabase.spl, GlobalVariables.DbConn);
                if (dr != null && dr.HasRows)
                {
                    dr.Read();
                    cityId = dr.GetString(0);
                }
            }

            return cityId;
        }

        private string AddressData(string cityId)
        {
            MySqlDataReader dr;
            string addressId = "";

            MySqlDatabase.spl.Add(new MySqlParameter("@Address", txtAddress1.Text));
            MySqlDatabase.spl.Add(new MySqlParameter("@Address2", txtAddress2.Text));
            MySqlDatabase.spl.Add(new MySqlParameter("@CityId", cityId));
            MySqlDatabase.spl.Add(new MySqlParameter("@PostalCode", txtPostalCode.Text));
            MySqlDatabase.spl.Add(new MySqlParameter("@Phone", txtPhone.Text));
            dr = MySqlDatabase.ExecuteReader("select addressId from address where address = @Address and address2 = Address2 and cityId = @CityId and postalCode = @PostalCode and phone = @Phone", MySqlDatabase.spl, GlobalVariables.DbConn);
            if (dr != null && dr.HasRows)
            {
                dr.Read();
                addressId = dr.GetString(0);
            }
            else
            {
                MySqlDatabase.spl.Add(new MySqlParameter("@Address", txtAddress1.Text));
                MySqlDatabase.spl.Add(new MySqlParameter("@Address2", txtAddress2.Text));
                MySqlDatabase.spl.Add(new MySqlParameter("@CityId", cityId));
                MySqlDatabase.spl.Add(new MySqlParameter("@PostalCode", txtPostalCode.Text));
                MySqlDatabase.spl.Add(new MySqlParameter("@Phone", txtPhone.Text));
                MySqlDatabase.spl.Add(new MySqlParameter("@CurUtcTime", DateTime.UtcNow));
                MySqlDatabase.spl.Add(new MySqlParameter("@User", GlobalVariables.UserName));
                MySqlDatabase.ExecuteNonQuery("insert into address (address, address2, cityId, postalCode, phone, createDate, createdBy, lastUpdate, lastUpdateBy) values (@Address, @Address2, @CityId, @PostalCode, @Phone, @CurUtcTime, @User, @CurUtcTime, @User)", MySqlDatabase.spl, GlobalVariables.DbConn);

                MySqlDatabase.spl.Add(new MySqlParameter("@Address", txtAddress1.Text));
                MySqlDatabase.spl.Add(new MySqlParameter("@Address2", txtAddress2.Text));
                MySqlDatabase.spl.Add(new MySqlParameter("@CityId", cityId));
                MySqlDatabase.spl.Add(new MySqlParameter("@PostalCode", txtPostalCode.Text));
                MySqlDatabase.spl.Add(new MySqlParameter("@Phone", txtPhone.Text));
                dr = MySqlDatabase.ExecuteReader("select addressId from address where address = @Address and address2 = Address2 and cityId = @CityId and postalCode = @PostalCode and phone = @Phone", MySqlDatabase.spl, GlobalVariables.DbConn);
                if (dr != null && dr.HasRows)
                {
                    dr.Read();
                    addressId = dr.GetString(0);
                }
            }

            return addressId;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
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

        private void LoadSelectedCustomer()
        {
            if (cboCustomerList.SelectedValue != null)
            {
                MySqlDatabase.spl.Add(new MySqlParameter("CustomerId", cboCustomerList.SelectedValue.ToString()));
                MySqlDataReader dr = MySqlDatabase.ExecuteReader("select c.active, c.customerName, a.address, a.address2, a.postalCode, a.phone, ci.city, co.country from customer c inner join address a on a.addressId = c.addressId inner join city ci on ci.cityId = a.cityId inner join country co on co.countryId = ci.countryId where c.customerId = @CustomerId", MySqlDatabase.spl, GlobalVariables.DbConn);
                if (dr != null && dr.HasRows)
                {
                    dr.Read();
                    cbActive.Checked = dr.GetBoolean(0);
                    cboCountry.Text = dr.GetString(7);
                    PopulateCityList();
                    txtName.Text = dr.GetString(1);
                    txtAddress1.Text = dr.GetString(2);
                    txtAddress2.Text = dr.GetString(3);
                    txtPostalCode.Text = dr.GetString(4);
                    txtPhone.Text = dr.GetString(5);
                    cboCity.Text = dr.GetString(6);
                }
            }
        }

        private void PopulateCountryList()
        {
            cboCountry.DataSource = null;
            List<CountryList> countryList = new List<CountryList>();
            MySqlDataReader dr = MySqlDatabase.ExecuteReader("select countryId, country from country order by country", MySqlDatabase.spl, GlobalVariables.DbConn);
            if (dr != null && dr.HasRows)
            {
                while (dr.Read())
                {
                    countryList.Add(new CountryList { CountryId = dr.GetInt32(0), CountryName = dr.GetString(1) });
                }
                cboCountry.DisplayMember = "CountryName";
                cboCountry.ValueMember = "CountryId";
                cboCountry.DataSource = countryList;
            }
            cboCountry.SelectedIndex = -1;
        }

        private void PopulateCityList()
        {
            cboCity.Items.Clear();
            if (cboCountry.SelectedValue != null)
            {
                MySqlDatabase.spl.Add(new MySqlParameter("@Country", cboCountry.SelectedValue.ToString()));
                MySqlDataReader dr = MySqlDatabase.ExecuteReader("select city from city where countryId = @Country order by city", MySqlDatabase.spl, GlobalVariables.DbConn);
                if (dr != null && dr.HasRows)
                {
                    while (dr.Read())
                    {
                        cboCity.Items.Add(dr.GetString(0));
                    }
                }
                cboCity.SelectedIndex = -1;
            }
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
            cbActive.Checked = false;
            txtName.Text = "";
            txtAddress1.Text = "";
            txtAddress2.Text = "";
            txtPostalCode.Text = "";
            txtPhone.Text = "";
            PopulateCountryList();
            cboCity.Text = "";
            cboCity.Items.Clear();
        }
    }
}

public class CountryList
{
    public int CountryId { get; set; }
    public string CountryName { get; set; }
}