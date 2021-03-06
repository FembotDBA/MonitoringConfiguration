﻿namespace MonitoringConfiguration
{
    partial class Config
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
            this.txtConnectionString = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.lvServers = new System.Windows.Forms.ListView();
            this.btnTask = new System.Windows.Forms.Button();
            this.lblResult = new System.Windows.Forms.Label();
            this.lvMonitoredTasks = new BrightIdeasSoftware.ObjectListView();
            this.colMonitoredTask_Name = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            ((System.ComponentModel.ISupportInitialize)(this.lvMonitoredTasks)).BeginInit();
            this.SuspendLayout();
            // 
            // txtConnectionString
            // 
            this.txtConnectionString.Location = new System.Drawing.Point(148, 14);
            this.txtConnectionString.Name = "txtConnectionString";
            this.txtConnectionString.Size = new System.Drawing.Size(435, 20);
            this.txtConnectionString.TabIndex = 0;
            this.txtConnectionString.TextChanged += new System.EventHandler(this.txtConnectionString_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Connection String";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(627, 13);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 3;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // lvServers
            // 
            this.lvServers.FullRowSelect = true;
            this.lvServers.Location = new System.Drawing.Point(31, 53);
            this.lvServers.MultiSelect = false;
            this.lvServers.Name = "lvServers";
            this.lvServers.Size = new System.Drawing.Size(776, 200);
            this.lvServers.TabIndex = 4;
            this.lvServers.UseCompatibleStateImageBehavior = false;
            this.lvServers.View = System.Windows.Forms.View.Details;
            // 
            // btnTask
            // 
            this.btnTask.Location = new System.Drawing.Point(732, 288);
            this.btnTask.Name = "btnTask";
            this.btnTask.Size = new System.Drawing.Size(75, 23);
            this.btnTask.TabIndex = 5;
            this.btnTask.Text = "Run Task";
            this.btnTask.UseVisualStyleBackColor = true;
            this.btnTask.Click += new System.EventHandler(this.btnTask_Click);
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Location = new System.Drawing.Point(366, 330);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(0, 13);
            this.lblResult.TabIndex = 6;
            // 
            // lvMonitoredTasks
            // 
            this.lvMonitoredTasks.AllColumns.Add(this.colMonitoredTask_Name);
            this.lvMonitoredTasks.CheckBoxes = true;
            this.lvMonitoredTasks.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colMonitoredTask_Name});
            this.lvMonitoredTasks.Location = new System.Drawing.Point(31, 288);
            this.lvMonitoredTasks.Name = "lvMonitoredTasks";
            this.lvMonitoredTasks.ShowGroups = false;
            this.lvMonitoredTasks.Size = new System.Drawing.Size(359, 287);
            this.lvMonitoredTasks.TabIndex = 7;
            this.lvMonitoredTasks.UseCompatibleStateImageBehavior = false;
            this.lvMonitoredTasks.View = System.Windows.Forms.View.Details;
            // 
            // colMonitoredTask_Name
            // 
            this.colMonitoredTask_Name.AspectName = "MonitoredTaskName";
            this.colMonitoredTask_Name.CheckBoxes = true;
            this.colMonitoredTask_Name.Text = "Monitored Task";
            // 
            // Config
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(846, 615);
            this.Controls.Add(this.lvMonitoredTasks);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.btnTask);
            this.Controls.Add(this.lvServers);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtConnectionString);
            this.Name = "Config";
            this.Text = "Configuration";
            this.Load += new System.EventHandler(this.Config_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lvMonitoredTasks)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtConnectionString;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.ListView lvServers;
        private System.Windows.Forms.Button btnTask;
        private System.Windows.Forms.Label lblResult;
        private BrightIdeasSoftware.ObjectListView lvMonitoredTasks;
        private BrightIdeasSoftware.OLVColumn colMonitoredTask_Name;
    }
}

