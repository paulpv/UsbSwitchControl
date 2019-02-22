﻿namespace WindowsFormsApp1
{
    partial class Form1
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
            this.comboBoxDevicesPrimaryLocal = new System.Windows.Forms.ComboBox();
            this.buttonDevicesRefresh = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxDevicesPrimaryRemote = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxDevicesSecondaryLocal = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxDevicesSecondaryRemote = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxDevicesFilter = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // comboBoxDevicesPrimaryLocal
            // 
            this.comboBoxDevicesPrimaryLocal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxDevicesPrimaryLocal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDevicesPrimaryLocal.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxDevicesPrimaryLocal.FormattingEnabled = true;
            this.comboBoxDevicesPrimaryLocal.Location = new System.Drawing.Point(119, 67);
            this.comboBoxDevicesPrimaryLocal.Name = "comboBoxDevicesPrimaryLocal";
            this.comboBoxDevicesPrimaryLocal.Size = new System.Drawing.Size(480, 22);
            this.comboBoxDevicesPrimaryLocal.TabIndex = 0;
            // 
            // buttonDevicesRefresh
            // 
            this.buttonDevicesRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDevicesRefresh.Location = new System.Drawing.Point(12, 38);
            this.buttonDevicesRefresh.Name = "buttonDevicesRefresh";
            this.buttonDevicesRefresh.Size = new System.Drawing.Size(587, 23);
            this.buttonDevicesRefresh.TabIndex = 2;
            this.buttonDevicesRefresh.Text = "Refresh Devices";
            this.buttonDevicesRefresh.UseVisualStyleBackColor = true;
            this.buttonDevicesRefresh.Click += new System.EventHandler(this.buttonDevicesRefresh_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Primary Local:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Primary Remote:";
            // 
            // comboBoxDevicesPrimaryRemote
            // 
            this.comboBoxDevicesPrimaryRemote.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxDevicesPrimaryRemote.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDevicesPrimaryRemote.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxDevicesPrimaryRemote.FormattingEnabled = true;
            this.comboBoxDevicesPrimaryRemote.Location = new System.Drawing.Point(119, 95);
            this.comboBoxDevicesPrimaryRemote.Name = "comboBoxDevicesPrimaryRemote";
            this.comboBoxDevicesPrimaryRemote.Size = new System.Drawing.Size(480, 22);
            this.comboBoxDevicesPrimaryRemote.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 126);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Secondary Local:";
            // 
            // comboBoxDevicesSecondaryLocal
            // 
            this.comboBoxDevicesSecondaryLocal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxDevicesSecondaryLocal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDevicesSecondaryLocal.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxDevicesSecondaryLocal.FormattingEnabled = true;
            this.comboBoxDevicesSecondaryLocal.Location = new System.Drawing.Point(119, 123);
            this.comboBoxDevicesSecondaryLocal.Name = "comboBoxDevicesSecondaryLocal";
            this.comboBoxDevicesSecondaryLocal.Size = new System.Drawing.Size(480, 22);
            this.comboBoxDevicesSecondaryLocal.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 154);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Secondary Remote:";
            // 
            // comboBoxDevicesSecondaryRemote
            // 
            this.comboBoxDevicesSecondaryRemote.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxDevicesSecondaryRemote.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDevicesSecondaryRemote.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxDevicesSecondaryRemote.FormattingEnabled = true;
            this.comboBoxDevicesSecondaryRemote.Location = new System.Drawing.Point(119, 151);
            this.comboBoxDevicesSecondaryRemote.Name = "comboBoxDevicesSecondaryRemote";
            this.comboBoxDevicesSecondaryRemote.Size = new System.Drawing.Size(480, 22);
            this.comboBoxDevicesSecondaryRemote.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Filter:";
            // 
            // textBoxDevicesFilter
            // 
            this.textBoxDevicesFilter.Location = new System.Drawing.Point(119, 12);
            this.textBoxDevicesFilter.Name = "textBoxDevicesFilter";
            this.textBoxDevicesFilter.Size = new System.Drawing.Size(480, 20);
            this.textBoxDevicesFilter.TabIndex = 11;
            this.textBoxDevicesFilter.Text = "VID_0557&PID_2405";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(611, 234);
            this.Controls.Add(this.textBoxDevicesFilter);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboBoxDevicesSecondaryRemote);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBoxDevicesSecondaryLocal);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBoxDevicesPrimaryRemote);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonDevicesRefresh);
            this.Controls.Add(this.comboBoxDevicesPrimaryLocal);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxDevicesPrimaryLocal;
        private System.Windows.Forms.Button buttonDevicesRefresh;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxDevicesPrimaryRemote;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxDevicesSecondaryLocal;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxDevicesSecondaryRemote;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxDevicesFilter;
    }
}

