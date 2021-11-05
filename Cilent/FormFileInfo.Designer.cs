
namespace Cilent
{
    partial class FormFileInfo
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.txtDateCreated = new DevExpress.XtraEditors.TextEdit();
            this.txtDateModified = new DevExpress.XtraEditors.TextEdit();
            this.txtPath = new DevExpress.XtraEditors.TextEdit();
            this.txtSize = new DevExpress.XtraEditors.TextEdit();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.checkEdit1 = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateCreated.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateModified.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSize.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.checkEdit1);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.txtDateCreated);
            this.panelControl1.Controls.Add(this.txtDateModified);
            this.panelControl1.Controls.Add(this.txtPath);
            this.panelControl1.Controls.Add(this.txtSize);
            this.panelControl1.Controls.Add(this.txtName);
            this.panelControl1.Controls.Add(this.labelControl4);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.labelControl7);
            this.panelControl1.Controls.Add(this.labelControl8);
            this.panelControl1.Controls.Add(this.labelControl5);
            this.panelControl1.Controls.Add(this.labelControl6);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(418, 351);
            this.panelControl1.TabIndex = 0;
            // 
            // txtDateCreated
            // 
            this.txtDateCreated.Location = new System.Drawing.Point(144, 208);
            this.txtDateCreated.Margin = new System.Windows.Forms.Padding(8, 8, 8, 8);
            this.txtDateCreated.Name = "txtDateCreated";
            this.txtDateCreated.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtDateCreated.Properties.Appearance.Options.UseBackColor = true;
            this.txtDateCreated.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.txtDateCreated.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtDateCreated.Properties.ReadOnly = true;
            this.txtDateCreated.Size = new System.Drawing.Size(240, 22);
            this.txtDateCreated.TabIndex = 25;
            // 
            // txtDateModified
            // 
            this.txtDateModified.Location = new System.Drawing.Point(144, 246);
            this.txtDateModified.Margin = new System.Windows.Forms.Padding(8, 8, 8, 8);
            this.txtDateModified.Name = "txtDateModified";
            this.txtDateModified.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtDateModified.Properties.Appearance.Options.UseBackColor = true;
            this.txtDateModified.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.txtDateModified.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtDateModified.Properties.ReadOnly = true;
            this.txtDateModified.Size = new System.Drawing.Size(240, 22);
            this.txtDateModified.TabIndex = 24;
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(144, 166);
            this.txtPath.Margin = new System.Windows.Forms.Padding(8, 8, 8, 8);
            this.txtPath.Name = "txtPath";
            this.txtPath.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtPath.Properties.Appearance.Options.UseBackColor = true;
            this.txtPath.Properties.ReadOnly = true;
            this.txtPath.Size = new System.Drawing.Size(240, 22);
            this.txtPath.TabIndex = 23;
            // 
            // txtSize
            // 
            this.txtSize.Location = new System.Drawing.Point(144, 123);
            this.txtSize.Margin = new System.Windows.Forms.Padding(8, 8, 8, 8);
            this.txtSize.Name = "txtSize";
            this.txtSize.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtSize.Properties.Appearance.Options.UseBackColor = true;
            this.txtSize.Properties.ReadOnly = true;
            this.txtSize.Size = new System.Drawing.Size(240, 22);
            this.txtSize.TabIndex = 22;
            this.txtSize.EditValueChanged += new System.EventHandler(this.txtSize_EditValueChanged);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(144, 80);
            this.txtName.Margin = new System.Windows.Forms.Padding(8, 8, 8, 8);
            this.txtName.Name = "txtName";
            this.txtName.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtName.Properties.Appearance.Options.UseBackColor = true;
            this.txtName.Properties.ReadOnly = true;
            this.txtName.Size = new System.Drawing.Size(240, 22);
            this.txtName.TabIndex = 21;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(32, 83);
            this.labelControl4.Margin = new System.Windows.Forms.Padding(8, 8, 8, 8);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(38, 16);
            this.labelControl4.TabIndex = 16;
            this.labelControl4.Text = "Name:";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(165, 25);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(8, 8, 8, 8);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(85, 24);
            this.labelControl1.TabIndex = 15;
            this.labelControl1.Text = "File Info";
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(32, 253);
            this.labelControl7.Margin = new System.Windows.Forms.Padding(8, 8, 8, 8);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(83, 16);
            this.labelControl7.TabIndex = 19;
            this.labelControl7.Text = "Date Modified:";
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(32, 126);
            this.labelControl8.Margin = new System.Windows.Forms.Padding(8, 8, 8, 8);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(29, 16);
            this.labelControl8.TabIndex = 20;
            this.labelControl8.Text = "Size:";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(32, 169);
            this.labelControl5.Margin = new System.Windows.Forms.Padding(8, 8, 8, 8);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(30, 16);
            this.labelControl5.TabIndex = 17;
            this.labelControl5.Text = "Path:";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(32, 211);
            this.labelControl6.Margin = new System.Windows.Forms.Padding(8, 8, 8, 8);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(80, 16);
            this.labelControl6.TabIndex = 18;
            this.labelControl6.Text = "Date Created:";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(32, 293);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(10, 10, 10, 10);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(60, 16);
            this.labelControl2.TabIndex = 26;
            this.labelControl2.Text = "Attributes:";
            // 
            // checkEdit1
            // 
            this.checkEdit1.Location = new System.Drawing.Point(144, 291);
            this.checkEdit1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkEdit1.Name = "checkEdit1";
            this.checkEdit1.Properties.Caption = "Read-only";
            this.checkEdit1.Size = new System.Drawing.Size(94, 21);
            this.checkEdit1.TabIndex = 27;
            this.checkEdit1.CheckedChanged += new System.EventHandler(this.checkEdit1_CheckedChanged);
            // 
            // FormFileInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(418, 351);
            this.Controls.Add(this.panelControl1);
            this.Name = "FormFileInfo";
            this.Text = "FormDetail";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateCreated.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateModified.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSize.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.TextEdit txtDateCreated;
        private DevExpress.XtraEditors.TextEdit txtDateModified;
        private DevExpress.XtraEditors.TextEdit txtPath;
        private DevExpress.XtraEditors.TextEdit txtSize;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.CheckEdit checkEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
    }
}