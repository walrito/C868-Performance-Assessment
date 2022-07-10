
namespace C868_Performance_Assessment
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnCustomers = new System.Windows.Forms.Button();
            this.btnAppointments = new System.Windows.Forms.Button();
            this.btnReports = new System.Windows.Forms.Button();
            this.lblAppointments = new System.Windows.Forms.Label();
            this.btnLogToggle = new System.Windows.Forms.Button();
            this.rbViewWeek = new System.Windows.Forms.RadioButton();
            this.rbViewMonth = new System.Windows.Forms.RadioButton();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.rbViewAll = new System.Windows.Forms.RadioButton();
            this.flpAppointments = new System.Windows.Forms.FlowLayoutPanel();
            this.tmrAutoRefresh = new System.Windows.Forms.Timer(this.components);
            this.nudAutoRefresh = new System.Windows.Forms.NumericUpDown();
            this.cbAutoRefresh = new System.Windows.Forms.CheckBox();
            this.btnSearch = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudAutoRefresh)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCustomers
            // 
            this.btnCustomers.Enabled = false;
            this.btnCustomers.Location = new System.Drawing.Point(12, 81);
            this.btnCustomers.Name = "btnCustomers";
            this.btnCustomers.Size = new System.Drawing.Size(82, 34);
            this.btnCustomers.TabIndex = 1;
            this.btnCustomers.Text = "Manage Customers";
            this.btnCustomers.UseVisualStyleBackColor = true;
            this.btnCustomers.Click += new System.EventHandler(this.btnCustomers_Click);
            // 
            // btnAppointments
            // 
            this.btnAppointments.Enabled = false;
            this.btnAppointments.Location = new System.Drawing.Point(12, 121);
            this.btnAppointments.Name = "btnAppointments";
            this.btnAppointments.Size = new System.Drawing.Size(82, 34);
            this.btnAppointments.TabIndex = 2;
            this.btnAppointments.Text = "Manage Appointments";
            this.btnAppointments.UseVisualStyleBackColor = true;
            this.btnAppointments.Click += new System.EventHandler(this.btnAppointments_Click);
            // 
            // btnReports
            // 
            this.btnReports.Enabled = false;
            this.btnReports.Location = new System.Drawing.Point(12, 190);
            this.btnReports.Name = "btnReports";
            this.btnReports.Size = new System.Drawing.Size(82, 23);
            this.btnReports.TabIndex = 3;
            this.btnReports.Text = "Reports";
            this.btnReports.UseVisualStyleBackColor = true;
            this.btnReports.Click += new System.EventHandler(this.btnReports_Click);
            // 
            // lblAppointments
            // 
            this.lblAppointments.AutoSize = true;
            this.lblAppointments.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAppointments.Location = new System.Drawing.Point(130, 18);
            this.lblAppointments.Name = "lblAppointments";
            this.lblAppointments.Size = new System.Drawing.Size(184, 20);
            this.lblAppointments.TabIndex = 4;
            this.lblAppointments.Text = "Upcoming Appointments";
            // 
            // btnLogToggle
            // 
            this.btnLogToggle.Location = new System.Drawing.Point(12, 12);
            this.btnLogToggle.Name = "btnLogToggle";
            this.btnLogToggle.Size = new System.Drawing.Size(82, 23);
            this.btnLogToggle.TabIndex = 0;
            this.btnLogToggle.Text = "Log In";
            this.btnLogToggle.UseVisualStyleBackColor = true;
            this.btnLogToggle.Click += new System.EventHandler(this.btnLogToggle_Click);
            // 
            // rbViewWeek
            // 
            this.rbViewWeek.AutoSize = true;
            this.rbViewWeek.Enabled = false;
            this.rbViewWeek.Location = new System.Drawing.Point(544, 367);
            this.rbViewWeek.Name = "rbViewWeek";
            this.rbViewWeek.Size = new System.Drawing.Size(54, 17);
            this.rbViewWeek.TabIndex = 10;
            this.rbViewWeek.Text = "Week";
            this.rbViewWeek.UseVisualStyleBackColor = true;
            this.rbViewWeek.CheckedChanged += new System.EventHandler(this.rbViewWeek_CheckedChanged);
            // 
            // rbViewMonth
            // 
            this.rbViewMonth.AutoSize = true;
            this.rbViewMonth.Enabled = false;
            this.rbViewMonth.Location = new System.Drawing.Point(604, 367);
            this.rbViewMonth.Name = "rbViewMonth";
            this.rbViewMonth.Size = new System.Drawing.Size(55, 17);
            this.rbViewMonth.TabIndex = 11;
            this.rbViewMonth.Text = "Month";
            this.rbViewMonth.UseVisualStyleBackColor = true;
            this.rbViewMonth.CheckedChanged += new System.EventHandler(this.rbViewMonth_CheckedChanged);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(12, 364);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(82, 23);
            this.btnExit.TabIndex = 12;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Enabled = false;
            this.btnRefresh.Location = new System.Drawing.Point(577, 12);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(82, 23);
            this.btnRefresh.TabIndex = 5;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // rbViewAll
            // 
            this.rbViewAll.AutoSize = true;
            this.rbViewAll.Checked = true;
            this.rbViewAll.Enabled = false;
            this.rbViewAll.Location = new System.Drawing.Point(502, 367);
            this.rbViewAll.Name = "rbViewAll";
            this.rbViewAll.Size = new System.Drawing.Size(36, 17);
            this.rbViewAll.TabIndex = 9;
            this.rbViewAll.TabStop = true;
            this.rbViewAll.Text = "All";
            this.rbViewAll.UseVisualStyleBackColor = true;
            this.rbViewAll.CheckedChanged += new System.EventHandler(this.rbViewAll_CheckedChanged);
            // 
            // flpAppointments
            // 
            this.flpAppointments.AutoScroll = true;
            this.flpAppointments.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flpAppointments.Location = new System.Drawing.Point(134, 41);
            this.flpAppointments.Name = "flpAppointments";
            this.flpAppointments.Size = new System.Drawing.Size(525, 320);
            this.flpAppointments.TabIndex = 6;
            // 
            // tmrAutoRefresh
            // 
            this.tmrAutoRefresh.Enabled = true;
            this.tmrAutoRefresh.Interval = 60000;
            this.tmrAutoRefresh.Tick += new System.EventHandler(this.tmrAutoRefresh_Tick);
            // 
            // nudAutoRefresh
            // 
            this.nudAutoRefresh.Enabled = false;
            this.nudAutoRefresh.Location = new System.Drawing.Point(268, 367);
            this.nudAutoRefresh.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.nudAutoRefresh.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudAutoRefresh.Name = "nudAutoRefresh";
            this.nudAutoRefresh.Size = new System.Drawing.Size(46, 20);
            this.nudAutoRefresh.TabIndex = 13;
            this.nudAutoRefresh.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudAutoRefresh.ValueChanged += new System.EventHandler(this.nudAutoRefresh_ValueChanged);
            // 
            // cbAutoRefresh
            // 
            this.cbAutoRefresh.AutoSize = true;
            this.cbAutoRefresh.Enabled = false;
            this.cbAutoRefresh.Location = new System.Drawing.Point(134, 368);
            this.cbAutoRefresh.Name = "cbAutoRefresh";
            this.cbAutoRefresh.Size = new System.Drawing.Size(128, 17);
            this.cbAutoRefresh.TabIndex = 14;
            this.cbAutoRefresh.Text = "Auto-refresh (minutes)";
            this.cbAutoRefresh.UseVisualStyleBackColor = true;
            this.cbAutoRefresh.CheckedChanged += new System.EventHandler(this.cbAutoRefresh_CheckedChanged);
            // 
            // btnSearch
            // 
            this.btnSearch.Enabled = false;
            this.btnSearch.Location = new System.Drawing.Point(12, 161);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(82, 23);
            this.btnSearch.TabIndex = 15;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // MainForm
            // 
            this.AcceptButton = this.btnRefresh;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(671, 399);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.cbAutoRefresh);
            this.Controls.Add(this.nudAutoRefresh);
            this.Controls.Add(this.flpAppointments);
            this.Controls.Add(this.rbViewAll);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.rbViewMonth);
            this.Controls.Add(this.rbViewWeek);
            this.Controls.Add(this.btnLogToggle);
            this.Controls.Add(this.lblAppointments);
            this.Controls.Add(this.btnReports);
            this.Controls.Add(this.btnAppointments);
            this.Controls.Add(this.btnCustomers);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Client Scheduler";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudAutoRefresh)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCustomers;
        private System.Windows.Forms.Button btnAppointments;
        private System.Windows.Forms.Button btnReports;
        private System.Windows.Forms.Label lblAppointments;
        private System.Windows.Forms.Button btnLogToggle;
        private System.Windows.Forms.RadioButton rbViewWeek;
        private System.Windows.Forms.RadioButton rbViewMonth;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.RadioButton rbViewAll;
        private System.Windows.Forms.FlowLayoutPanel flpAppointments;
        private System.Windows.Forms.Timer tmrAutoRefresh;
        private System.Windows.Forms.NumericUpDown nudAutoRefresh;
        private System.Windows.Forms.CheckBox cbAutoRefresh;
        private System.Windows.Forms.Button btnSearch;
    }
}

