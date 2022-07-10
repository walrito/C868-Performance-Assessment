
namespace C868_Performance_Assessment
{
    partial class ReportForm
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
            this.lblReportList = new System.Windows.Forms.Label();
            this.cboReportList = new System.Windows.Forms.ComboBox();
            this.dgvReportView = new System.Windows.Forms.DataGridView();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReportView)).BeginInit();
            this.SuspendLayout();
            // 
            // lblReportList
            // 
            this.lblReportList.AutoSize = true;
            this.lblReportList.Location = new System.Drawing.Point(12, 9);
            this.lblReportList.Name = "lblReportList";
            this.lblReportList.Size = new System.Drawing.Size(58, 13);
            this.lblReportList.TabIndex = 0;
            this.lblReportList.Text = "Report List";
            // 
            // cboReportList
            // 
            this.cboReportList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboReportList.FormattingEnabled = true;
            this.cboReportList.Items.AddRange(new object[] {
            "Appointment Types (By Month)",
            "Schedule (By User)",
            "Inactive Customers"});
            this.cboReportList.Location = new System.Drawing.Point(12, 25);
            this.cboReportList.Name = "cboReportList";
            this.cboReportList.Size = new System.Drawing.Size(209, 21);
            this.cboReportList.TabIndex = 1;
            this.cboReportList.SelectedIndexChanged += new System.EventHandler(this.cboReportList_SelectedIndexChanged);
            // 
            // dgvReportView
            // 
            this.dgvReportView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvReportView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReportView.Location = new System.Drawing.Point(12, 52);
            this.dgvReportView.Name = "dgvReportView";
            this.dgvReportView.Size = new System.Drawing.Size(760, 368);
            this.dgvReportView.TabIndex = 3;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(697, 426);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(227, 23);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 5;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // ReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dgvReportView);
            this.Controls.Add(this.cboReportList);
            this.Controls.Add(this.lblReportList);
            this.Name = "ReportForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reports";
            ((System.ComponentModel.ISupportInitialize)(this.dgvReportView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblReportList;
        private System.Windows.Forms.ComboBox cboReportList;
        private System.Windows.Forms.DataGridView dgvReportView;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnClear;
    }
}