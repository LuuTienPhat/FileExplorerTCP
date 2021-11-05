
using System;

namespace Cilent
{
    partial class Client
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
            Environment.Exit(Environment.ExitCode);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Client));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.btnReconnect = new DevExpress.XtraBars.BarButtonItem();
            this.btnExit = new DevExpress.XtraBars.BarButtonItem();
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.lbDetail = new DevExpress.XtraBars.BarStaticItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.controlPanel = new DevExpress.XtraEditors.PanelControl();
            this.lbStatus = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.btnDisconnect = new DevExpress.XtraEditors.SimpleButton();
            this.txtPort = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtHost = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnConnect = new DevExpress.XtraEditors.SimpleButton();
            this.resultPanel = new DevExpress.XtraEditors.PanelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.cbxFilter = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.htnRefreshConsole = new DevExpress.XtraEditors.SimpleButton();
            this.btnClearConsole = new DevExpress.XtraEditors.SimpleButton();
            this.directoryView = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btnShow = new DevExpress.XtraEditors.SimpleButton();
            this.txtDirectory = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.treeListLookUpEdit1TreeList = new DevExpress.XtraTreeList.TreeList();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.popupMenu = new DevExpress.XtraBars.PopupMenu(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlPanel)).BeginInit();
            this.controlPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPort.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHost.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.resultPanel)).BeginInit();
            this.resultPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbxFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDirectory.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeListLookUpEdit1TreeList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1,
            this.bar2});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btnReconnect,
            this.btnExit,
            this.barButtonItem3,
            this.lbDetail,
            this.barButtonItem1});
            this.barManager1.MainMenu = this.bar1;
            this.barManager1.MaxItemId = 5;
            this.barManager1.StatusBar = this.bar2;
            // 
            // bar1
            // 
            this.bar1.BarName = "Custom 2";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnReconnect),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnExit)});
            this.bar1.OptionsBar.DrawBorder = false;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.MultiLine = true;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Custom 2";
            // 
            // btnReconnect
            // 
            this.btnReconnect.Caption = "Reconnect";
            this.btnReconnect.Id = 0;
            this.btnReconnect.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnReconnect.ImageOptions.SvgImage")));
            this.btnReconnect.Name = "btnReconnect";
            this.btnReconnect.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnReconnect.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnReconnect_ItemClick);
            // 
            // btnExit
            // 
            this.btnExit.Caption = "Exit";
            this.btnExit.Id = 1;
            this.btnExit.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnExit.ImageOptions.SvgImage")));
            this.btnExit.Name = "btnExit";
            this.btnExit.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnExit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnExit_ItemClick);
            // 
            // bar2
            // 
            this.bar2.BarName = "Custom 3";
            this.bar2.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.lbDetail)});
            this.bar2.OptionsBar.AllowQuickCustomization = false;
            this.bar2.OptionsBar.DrawDragBorder = false;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Custom 3";
            // 
            // lbDetail
            // 
            this.lbDetail.Id = 3;
            this.lbDetail.Name = "lbDetail";
            this.lbDetail.Size = new System.Drawing.Size(200, 0);
            this.lbDetail.Width = 200;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.barDockControlTop.Size = new System.Drawing.Size(547, 27);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 460);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.barDockControlBottom.Size = new System.Drawing.Size(547, 21);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 27);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 433);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(547, 27);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 433);
            // 
            // barButtonItem3
            // 
            this.barButtonItem3.Caption = "barButtonItem3";
            this.barButtonItem3.Id = 2;
            this.barButtonItem3.Name = "barButtonItem3";
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "Download";
            this.barButtonItem1.Id = 4;
            this.barButtonItem1.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItem1.ImageOptions.SvgImage")));
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // controlPanel
            // 
            this.controlPanel.Controls.Add(this.lbStatus);
            this.controlPanel.Controls.Add(this.labelControl3);
            this.controlPanel.Controls.Add(this.btnDisconnect);
            this.controlPanel.Controls.Add(this.txtPort);
            this.controlPanel.Controls.Add(this.labelControl2);
            this.controlPanel.Controls.Add(this.txtHost);
            this.controlPanel.Controls.Add(this.labelControl1);
            this.controlPanel.Controls.Add(this.btnConnect);
            this.controlPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controlPanel.Location = new System.Drawing.Point(0, 27);
            this.controlPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.controlPanel.Name = "controlPanel";
            this.controlPanel.Size = new System.Drawing.Size(547, 433);
            this.controlPanel.TabIndex = 5;
            // 
            // lbStatus
            // 
            this.lbStatus.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbStatus.Appearance.Options.UseFont = true;
            this.lbStatus.Appearance.Options.UseTextOptions = true;
            this.lbStatus.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lbStatus.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lbStatus.Location = new System.Drawing.Point(192, 20);
            this.lbStatus.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(180, 17);
            this.lbStatus.TabIndex = 26;
            this.lbStatus.Text = "Not Connected";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(135, 20);
            this.labelControl3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(39, 17);
            this.labelControl3.TabIndex = 25;
            this.labelControl3.Text = "Status";
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnDisconnect.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnDisconnect.ImageOptions.SvgImage")));
            this.btnDisconnect.ImageOptions.SvgImageSize = new System.Drawing.Size(15, 15);
            this.btnDisconnect.Location = new System.Drawing.Point(291, 121);
            this.btnDisconnect.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(81, 24);
            this.btnDisconnect.TabIndex = 24;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // txtPort
            // 
            this.txtPort.EditValue = "5555";
            this.txtPort.Location = new System.Drawing.Point(192, 89);
            this.txtPort.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(180, 20);
            this.txtPort.TabIndex = 23;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(135, 89);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(20, 13);
            this.labelControl2.TabIndex = 22;
            this.labelControl2.Text = "Port";
            // 
            // txtHost
            // 
            this.txtHost.EditValue = "127.0.0.1";
            this.txtHost.Location = new System.Drawing.Point(192, 66);
            this.txtHost.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtHost.Name = "txtHost";
            this.txtHost.Size = new System.Drawing.Size(180, 20);
            this.txtHost.TabIndex = 21;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(135, 66);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(22, 13);
            this.labelControl1.TabIndex = 20;
            this.labelControl1.Text = "Host";
            // 
            // btnConnect
            // 
            this.btnConnect.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnConnect.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnConnect.ImageOptions.SvgImage")));
            this.btnConnect.ImageOptions.SvgImageSize = new System.Drawing.Size(15, 15);
            this.btnConnect.Location = new System.Drawing.Point(192, 121);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(81, 24);
            this.btnConnect.TabIndex = 19;
            this.btnConnect.Text = "Connect";
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // resultPanel
            // 
            this.resultPanel.Controls.Add(this.labelControl5);
            this.resultPanel.Controls.Add(this.cbxFilter);
            this.resultPanel.Controls.Add(this.htnRefreshConsole);
            this.resultPanel.Controls.Add(this.btnClearConsole);
            this.resultPanel.Controls.Add(this.directoryView);
            this.resultPanel.Controls.Add(this.btnShow);
            this.resultPanel.Controls.Add(this.txtDirectory);
            this.resultPanel.Controls.Add(this.labelControl4);
            this.resultPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.resultPanel.Location = new System.Drawing.Point(0, 195);
            this.resultPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.resultPanel.Name = "resultPanel";
            this.resultPanel.Size = new System.Drawing.Size(547, 265);
            this.resultPanel.TabIndex = 10;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(67, 49);
            this.labelControl5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(24, 13);
            this.labelControl5.TabIndex = 39;
            this.labelControl5.Text = "Filter";
            // 
            // cbxFilter
            // 
            this.cbxFilter.EditValue = "all";
            this.cbxFilter.Location = new System.Drawing.Point(146, 46);
            this.cbxFilter.MenuManager = this.barManager1;
            this.cbxFilter.Name = "cbxFilter";
            this.cbxFilter.Properties.AllowMultiSelect = true;
            this.cbxFilter.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbxFilter.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.CheckedListBoxItem[] {
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("all", "All Files and Folders", System.Windows.Forms.CheckState.Checked),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("Compressed", "Compressed Files"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("sound", "Sound Files"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("video", "Video Files"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("text", "Text Files"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("image", "Image Files"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("folder", "Only Folders")});
            this.cbxFilter.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.cbxFilter.Size = new System.Drawing.Size(220, 20);
            this.cbxFilter.TabIndex = 38;
            // 
            // htnRefreshConsole
            // 
            this.htnRefreshConsole.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.htnRefreshConsole.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("htnRefreshConsole.ImageOptions.SvgImage")));
            this.htnRefreshConsole.ImageOptions.SvgImageSize = new System.Drawing.Size(16, 16);
            this.htnRefreshConsole.Location = new System.Drawing.Point(487, 113);
            this.htnRefreshConsole.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.htnRefreshConsole.Name = "htnRefreshConsole";
            this.htnRefreshConsole.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.htnRefreshConsole.Size = new System.Drawing.Size(36, 24);
            this.htnRefreshConsole.TabIndex = 37;
            this.htnRefreshConsole.ToolTip = "Refresh Console";
            this.htnRefreshConsole.Click += new System.EventHandler(this.btnRefreshConsole_Click);
            // 
            // btnClearConsole
            // 
            this.btnClearConsole.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnClearConsole.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnClearConsole.ImageOptions.SvgImage")));
            this.btnClearConsole.ImageOptions.SvgImageSize = new System.Drawing.Size(16, 16);
            this.btnClearConsole.Location = new System.Drawing.Point(487, 85);
            this.btnClearConsole.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnClearConsole.Name = "btnClearConsole";
            this.btnClearConsole.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnClearConsole.Size = new System.Drawing.Size(36, 24);
            this.btnClearConsole.TabIndex = 36;
            this.btnClearConsole.ToolTip = "Clear Console";
            this.btnClearConsole.Click += new System.EventHandler(this.btnClearConsole_Click);
            // 
            // directoryView
            // 
            this.directoryView.BackColor = System.Drawing.Color.White;
            this.directoryView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.directoryView.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(70)))), ((int)(((byte)(68)))));
            this.directoryView.Location = new System.Drawing.Point(67, 85);
            this.directoryView.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.directoryView.Name = "directoryView";
            this.directoryView.Size = new System.Drawing.Size(414, 156);
            this.directoryView.StateImageList = this.imageList1;
            this.directoryView.TabIndex = 35;
            this.directoryView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.directoryView_NodeMouseClick);
            this.directoryView.MouseMove += new System.Windows.Forms.MouseEventHandler(this.directoryView_MouseMove);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "folder.png");
            this.imageList1.Images.SetKeyName(1, "file.png");
            // 
            // btnShow
            // 
            this.btnShow.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnShow.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnShow.ImageOptions.SvgImage")));
            this.btnShow.ImageOptions.SvgImageSize = new System.Drawing.Size(15, 15);
            this.btnShow.Location = new System.Drawing.Point(400, 42);
            this.btnShow.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(81, 24);
            this.btnShow.TabIndex = 34;
            this.btnShow.Text = "Show";
            this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
            // 
            // txtDirectory
            // 
            this.txtDirectory.EditValue = "C:\\Users\\Phat\\Documents\\NetBeansProjects\\LTM2";
            this.txtDirectory.Location = new System.Drawing.Point(146, 21);
            this.txtDirectory.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtDirectory.Name = "txtDirectory";
            this.txtDirectory.Size = new System.Drawing.Size(220, 20);
            this.txtDirectory.TabIndex = 33;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(67, 24);
            this.labelControl4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(44, 13);
            this.labelControl4.TabIndex = 32;
            this.labelControl4.Text = "Directory";
            // 
            // treeListLookUpEdit1TreeList
            // 
            this.treeListLookUpEdit1TreeList.Location = new System.Drawing.Point(0, 0);
            this.treeListLookUpEdit1TreeList.Name = "treeListLookUpEdit1TreeList";
            this.treeListLookUpEdit1TreeList.OptionsView.ShowIndentAsRowStyle = true;
            this.treeListLookUpEdit1TreeList.Size = new System.Drawing.Size(400, 200);
            this.treeListLookUpEdit1TreeList.TabIndex = 0;
            // 
            // popupMenu
            // 
            this.popupMenu.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem1)});
            this.popupMenu.Manager = this.barManager1;
            this.popupMenu.Name = "popupMenu";
            // 
            // Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(547, 481);
            this.Controls.Add(this.resultPanel);
            this.Controls.Add(this.controlPanel);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.IconOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("Client.IconOptions.SvgImage")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Client";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Client2";
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlPanel)).EndInit();
            this.controlPanel.ResumeLayout(false);
            this.controlPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPort.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHost.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.resultPanel)).EndInit();
            this.resultPanel.ResumeLayout(false);
            this.resultPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbxFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDirectory.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeListLookUpEdit1TreeList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem btnReconnect;
        private DevExpress.XtraBars.BarButtonItem btnExit;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barButtonItem3;
        private DevExpress.XtraEditors.PanelControl controlPanel;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraEditors.LabelControl lbStatus;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.SimpleButton btnDisconnect;
        private DevExpress.XtraEditors.TextEdit txtPort;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtHost;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btnConnect;
        private DevExpress.XtraBars.BarStaticItem lbDetail;
        private DevExpress.XtraEditors.PanelControl resultPanel;
        private DevExpress.XtraEditors.SimpleButton btnShow;
        private DevExpress.XtraEditors.TextEdit txtDirectory;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraTreeList.TreeList treeListLookUpEdit1TreeList;
        private System.Windows.Forms.TreeView directoryView;
        private System.Windows.Forms.ToolTip toolTip;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.PopupMenu popupMenu;
        private System.Windows.Forms.ImageList imageList1;
        private DevExpress.XtraEditors.SimpleButton btnClearConsole;
        private DevExpress.XtraEditors.SimpleButton htnRefreshConsole;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.CheckedComboBoxEdit cbxFilter;
    }
}