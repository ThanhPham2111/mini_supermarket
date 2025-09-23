namespace mini_supermarket.GUI.SideBar
{
    partial class Form_Sidebar
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null!;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.sidebarPanel = new System.Windows.Forms.Panel();
            this.navButton3 = new System.Windows.Forms.Button();
            this.navButton2 = new System.Windows.Forms.Button();
            this.navButton1 = new System.Windows.Forms.Button();
            this.logoPanel = new System.Windows.Forms.Panel();
            this.logoLabel = new System.Windows.Forms.Label();
            this.mainContentPanel = new System.Windows.Forms.Panel();
            this.contentHostPanel = new System.Windows.Forms.Panel();
            this.headerPanel = new System.Windows.Forms.Panel();
            this.mainTitleLabel = new System.Windows.Forms.Label();
            this.sidebarPanel.SuspendLayout();
            this.logoPanel.SuspendLayout();
            this.mainContentPanel.SuspendLayout();
            this.headerPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // sidebarPanel
            // 
            this.sidebarPanel.BackColor = System.Drawing.Color.FromArgb(33, 37, 41);
            this.sidebarPanel.Controls.Add(this.navButton3);
            this.sidebarPanel.Controls.Add(this.navButton2);
            this.sidebarPanel.Controls.Add(this.navButton1);
            this.sidebarPanel.Controls.Add(this.logoPanel);
            this.sidebarPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.sidebarPanel.Location = new System.Drawing.Point(0, 0);
            this.sidebarPanel.Margin = new System.Windows.Forms.Padding(0);
            this.sidebarPanel.Name = "sidebarPanel";
            this.sidebarPanel.Padding = new System.Windows.Forms.Padding(0, 24, 0, 24);
            this.sidebarPanel.Size = new System.Drawing.Size(220, 900);
            this.sidebarPanel.TabIndex = 0;
            // 
            // navButton3
            // 
            this.navButton3.BackColor = System.Drawing.Color.FromArgb(52, 58, 64);
            this.navButton3.Dock = System.Windows.Forms.DockStyle.Top;
            this.navButton3.FlatAppearance.BorderSize = 0;
            this.navButton3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.navButton3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.navButton3.ForeColor = System.Drawing.Color.White;
            this.navButton3.Location = new System.Drawing.Point(0, 274);
            this.navButton3.Margin = new System.Windows.Forms.Padding(0);
            this.navButton3.Name = "navButton3";
            this.navButton3.Padding = new System.Windows.Forms.Padding(16, 0, 0, 0);
            this.navButton3.Size = new System.Drawing.Size(220, 50);
            this.navButton3.TabIndex = 3;
            this.navButton3.Text = "Bao cao";
            this.navButton3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.navButton3.UseVisualStyleBackColor = false;
            this.navButton3.Click += new System.EventHandler(this.navButton3_Click);
            // 
            // navButton2
            // 
            this.navButton2.BackColor = System.Drawing.Color.FromArgb(52, 58, 64);
            this.navButton2.Dock = System.Windows.Forms.DockStyle.Top;
            this.navButton2.FlatAppearance.BorderSize = 0;
            this.navButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.navButton2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.navButton2.ForeColor = System.Drawing.Color.White;
            this.navButton2.Location = new System.Drawing.Point(0, 224);
            this.navButton2.Margin = new System.Windows.Forms.Padding(0);
            this.navButton2.Name = "navButton2";
            this.navButton2.Padding = new System.Windows.Forms.Padding(16, 0, 0, 0);
            this.navButton2.Size = new System.Drawing.Size(220, 50);
            this.navButton2.TabIndex = 2;
            this.navButton2.Text = "Quan ly san pham";
            this.navButton2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.navButton2.UseVisualStyleBackColor = false;
            this.navButton2.Click += new System.EventHandler(this.navButton2_Click);
            // 
            // navButton1
            // 
            this.navButton1.BackColor = System.Drawing.Color.FromArgb(52, 58, 64);
            this.navButton1.Dock = System.Windows.Forms.DockStyle.Top;
            this.navButton1.FlatAppearance.BorderSize = 0;
            this.navButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.navButton1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.navButton1.ForeColor = System.Drawing.Color.White;
            this.navButton1.Location = new System.Drawing.Point(0, 174);
            this.navButton1.Margin = new System.Windows.Forms.Padding(0);
            this.navButton1.Name = "navButton1";
            this.navButton1.Padding = new System.Windows.Forms.Padding(16, 0, 0, 0);
            this.navButton1.Size = new System.Drawing.Size(220, 50);
            this.navButton1.TabIndex = 1;
            this.navButton1.Text = "Ban hang";
            this.navButton1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.navButton1.UseVisualStyleBackColor = false;
            this.navButton1.Click += new System.EventHandler(this.navButton1_Click);
            // 
            // logoPanel
            // 
            this.logoPanel.Controls.Add(this.logoLabel);
            this.logoPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.logoPanel.Location = new System.Drawing.Point(0, 24);
            this.logoPanel.Margin = new System.Windows.Forms.Padding(0);
            this.logoPanel.Name = "logoPanel";
            this.logoPanel.Padding = new System.Windows.Forms.Padding(0, 16, 0, 16);
            this.logoPanel.Size = new System.Drawing.Size(220, 150);
            this.logoPanel.TabIndex = 0;
            // 
            // logoLabel
            // 
            this.logoLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logoLabel.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.logoLabel.ForeColor = System.Drawing.Color.White;
            this.logoLabel.Location = new System.Drawing.Point(0, 16);
            this.logoLabel.Name = "logoLabel";
            this.logoLabel.Size = new System.Drawing.Size(220, 118);
            this.logoLabel.TabIndex = 0;
            this.logoLabel.Text = "Mini Market";
            this.logoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // mainContentPanel
            // 
            this.mainContentPanel.BackColor = System.Drawing.Color.White;
            this.mainContentPanel.Controls.Add(this.contentHostPanel);
            this.mainContentPanel.Controls.Add(this.headerPanel);
            this.mainContentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainContentPanel.Location = new System.Drawing.Point(220, 0);
            this.mainContentPanel.Margin = new System.Windows.Forms.Padding(0);
            this.mainContentPanel.Name = "mainContentPanel";
            this.mainContentPanel.Size = new System.Drawing.Size(1220, 900);
            this.mainContentPanel.TabIndex = 1;
            // 
            // contentHostPanel
            // 
            this.contentHostPanel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.contentHostPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentHostPanel.Location = new System.Drawing.Point(0, 90);
            this.contentHostPanel.Margin = new System.Windows.Forms.Padding(0);
            this.contentHostPanel.Name = "contentHostPanel";
            this.contentHostPanel.Padding = new System.Windows.Forms.Padding(24);
            this.contentHostPanel.Size = new System.Drawing.Size(1220, 810);
            this.contentHostPanel.TabIndex = 1;
            // 
            // headerPanel
            // 
            this.headerPanel.BackColor = System.Drawing.Color.White;
            this.headerPanel.Controls.Add(this.mainTitleLabel);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Location = new System.Drawing.Point(0, 0);
            this.headerPanel.Margin = new System.Windows.Forms.Padding(0);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Padding = new System.Windows.Forms.Padding(24, 24, 24, 16);
            this.headerPanel.Size = new System.Drawing.Size(1220, 90);
            this.headerPanel.TabIndex = 0;
            // 
            // mainTitleLabel
            // 
            this.mainTitleLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTitleLabel.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.mainTitleLabel.ForeColor = System.Drawing.Color.FromArgb(33, 37, 41);
            this.mainTitleLabel.Location = new System.Drawing.Point(24, 24);
            this.mainTitleLabel.Margin = new System.Windows.Forms.Padding(0);
            this.mainTitleLabel.Name = "mainTitleLabel";
            this.mainTitleLabel.Size = new System.Drawing.Size(1172, 50);
            this.mainTitleLabel.TabIndex = 0;
            this.mainTitleLabel.Text = "Ban hang";
            this.mainTitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Form_Sidebar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1440, 900);
            this.Controls.Add(this.mainContentPanel);
            this.Controls.Add(this.sidebarPanel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_Sidebar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dieu huong";
            this.sidebarPanel.ResumeLayout(false);
            this.logoPanel.ResumeLayout(false);
            this.mainContentPanel.ResumeLayout(false);
            this.headerPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel sidebarPanel;
        private System.Windows.Forms.Button navButton3;
        private System.Windows.Forms.Button navButton2;
        private System.Windows.Forms.Button navButton1;
        private System.Windows.Forms.Panel logoPanel;
        private System.Windows.Forms.Label logoLabel;
        private System.Windows.Forms.Panel mainContentPanel;
        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label mainTitleLabel;
        private System.Windows.Forms.Panel contentHostPanel;
    }
}
