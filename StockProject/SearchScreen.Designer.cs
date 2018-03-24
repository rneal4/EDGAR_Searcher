namespace StockProject
{
    partial class SearchScreen
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
            this.txtTicketSymbol = new System.Windows.Forms.TextBox();
            this.btnStartSearch = new System.Windows.Forms.Button();
            this.lblCompanyTicker = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtTicketSymbol
            // 
            this.txtTicketSymbol.Location = new System.Drawing.Point(192, 88);
            this.txtTicketSymbol.Name = "txtTicketSymbol";
            this.txtTicketSymbol.Size = new System.Drawing.Size(100, 20);
            this.txtTicketSymbol.TabIndex = 0;
            this.txtTicketSymbol.Text = "1288776";
            // 
            // btnStartSearch
            // 
            this.btnStartSearch.Location = new System.Drawing.Point(329, 85);
            this.btnStartSearch.Name = "btnStartSearch";
            this.btnStartSearch.Size = new System.Drawing.Size(75, 23);
            this.btnStartSearch.TabIndex = 2;
            this.btnStartSearch.Text = "Start Search";
            this.btnStartSearch.UseVisualStyleBackColor = true;
            this.btnStartSearch.Click += new System.EventHandler(this.btnStartSearch_Click);
            // 
            // lblCompanyTicker
            // 
            this.lblCompanyTicker.AutoSize = true;
            this.lblCompanyTicker.Location = new System.Drawing.Point(192, 69);
            this.lblCompanyTicker.Name = "lblCompanyTicker";
            this.lblCompanyTicker.Size = new System.Drawing.Size(136, 13);
            this.lblCompanyTicker.TabIndex = 3;
            this.lblCompanyTicker.Text = "Company Ticker or Number";
            // 
            // SearchScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(643, 228);
            this.Controls.Add(this.lblCompanyTicker);
            this.Controls.Add(this.btnStartSearch);
            this.Controls.Add(this.txtTicketSymbol);
            this.Name = "SearchScreen";
            this.Text = "Search Screen";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtTicketSymbol;
        private System.Windows.Forms.Button btnStartSearch;
        private System.Windows.Forms.Label lblCompanyTicker;
    }
}

