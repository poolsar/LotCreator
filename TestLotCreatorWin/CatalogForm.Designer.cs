namespace TestLotCreatorWin
{
    partial class CatalogForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CatalogForm));
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.btnCreateImages = new System.Windows.Forms.Button();
            this.btnSpecialDescription = new System.Windows.Forms.Button();
            this.btnRescrapeInfoFromSite = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnAssignSale = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.справочникиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnStylesDict = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSalesDict = new System.Windows.Forms.ToolStripMenuItem();
            this.btnMarketplaceDict = new System.Windows.Forms.ToolStripMenuItem();
            this.дополнительноToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnClearUnreadMsg = new System.Windows.Forms.ToolStripMenuItem();
            this.объединитьВБандлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.подготовитьКартинкиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnPrepareImagesVKontakte = new System.Windows.Forms.ToolStripMenuItem();
            this.btnRefreshAll = new System.Windows.Forms.Button();
            this.btnFindImage = new System.Windows.Forms.Button();
            this.btnChangePricePosition = new System.Windows.Forms.Button();
            this.btnUnpost = new System.Windows.Forms.Button();
            this.btnTest = new System.Windows.Forms.Button();
            this.btnSyncAdvs = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.AllowDrop = true;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(579, 349);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControl1.DragDrop += new System.Windows.Forms.DragEventHandler(this.gridControl1_DragDrop);
            this.gridControl1.DragOver += new System.Windows.Forms.DragEventHandler(this.gridControl1_DragOver);
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsView.ShowAutoFilterRow = true;
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerControl1.Location = new System.Drawing.Point(12, 135);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.panelControl1);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.gridControl1);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(901, 349);
            this.splitContainerControl1.SplitterPosition = 317;
            this.splitContainerControl1.TabIndex = 2;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.txtDescription);
            this.panelControl1.Controls.Add(this.pictureEdit1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(317, 349);
            this.panelControl1.TabIndex = 0;
            // 
            // txtDescription
            // 
            this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescription.Location = new System.Drawing.Point(5, 170);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(307, 174);
            this.txtDescription.TabIndex = 4;
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.AllowDrop = true;
            this.pictureEdit1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureEdit1.EditValue = ((object)(resources.GetObject("pictureEdit1.EditValue")));
            this.pictureEdit1.Location = new System.Drawing.Point(5, 5);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
            this.pictureEdit1.Size = new System.Drawing.Size(307, 159);
            this.pictureEdit1.TabIndex = 3;
            this.pictureEdit1.DragDrop += new System.Windows.Forms.DragEventHandler(this.pictureEdit1_DragDrop);
            this.pictureEdit1.DragOver += new System.Windows.Forms.DragEventHandler(this.pictureEdit1_DragOver);
            // 
            // btnCreateImages
            // 
            this.btnCreateImages.Location = new System.Drawing.Point(248, 45);
            this.btnCreateImages.Name = "btnCreateImages";
            this.btnCreateImages.Size = new System.Drawing.Size(112, 39);
            this.btnCreateImages.TabIndex = 5;
            this.btnCreateImages.Text = "Картинки";
            this.btnCreateImages.UseVisualStyleBackColor = true;
            this.btnCreateImages.Click += new System.EventHandler(this.btnCreateImages_Click);
            // 
            // btnSpecialDescription
            // 
            this.btnSpecialDescription.Location = new System.Drawing.Point(130, 45);
            this.btnSpecialDescription.Name = "btnSpecialDescription";
            this.btnSpecialDescription.Size = new System.Drawing.Size(112, 39);
            this.btnSpecialDescription.TabIndex = 10;
            this.btnSpecialDescription.Text = "Применить спец описание";
            this.btnSpecialDescription.UseVisualStyleBackColor = true;
            this.btnSpecialDescription.Click += new System.EventHandler(this.btnSpecialDescription_Click);
            // 
            // btnRescrapeInfoFromSite
            // 
            this.btnRescrapeInfoFromSite.Location = new System.Drawing.Point(12, 90);
            this.btnRescrapeInfoFromSite.Name = "btnRescrapeInfoFromSite";
            this.btnRescrapeInfoFromSite.Size = new System.Drawing.Size(112, 39);
            this.btnRescrapeInfoFromSite.TabIndex = 11;
            this.btnRescrapeInfoFromSite.Text = "Перезагрузить инфу с сайта";
            this.btnRescrapeInfoFromSite.UseVisualStyleBackColor = true;
            this.btnRescrapeInfoFromSite.Click += new System.EventHandler(this.btnRescrapeInfoFromSite_Click);
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(12, 45);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(112, 39);
            this.btnImport.TabIndex = 12;
            this.btnImport.Text = "Импорт";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnAssignSale
            // 
            this.btnAssignSale.Location = new System.Drawing.Point(130, 90);
            this.btnAssignSale.Name = "btnAssignSale";
            this.btnAssignSale.Size = new System.Drawing.Size(112, 39);
            this.btnAssignSale.TabIndex = 14;
            this.btnAssignSale.Text = "Назначить акцию";
            this.btnAssignSale.UseVisualStyleBackColor = true;
            this.btnAssignSale.Click += new System.EventHandler(this.btnAssignSale_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.справочникиToolStripMenuItem,
            this.дополнительноToolStripMenuItem,
            this.подготовитьКартинкиToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(925, 24);
            this.menuStrip1.TabIndex = 17;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // справочникиToolStripMenuItem
            // 
            this.справочникиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnStylesDict,
            this.btnSalesDict,
            this.btnMarketplaceDict});
            this.справочникиToolStripMenuItem.Name = "справочникиToolStripMenuItem";
            this.справочникиToolStripMenuItem.Size = new System.Drawing.Size(94, 20);
            this.справочникиToolStripMenuItem.Text = "Справочники";
            // 
            // btnStylesDict
            // 
            this.btnStylesDict.Name = "btnStylesDict";
            this.btnStylesDict.Size = new System.Drawing.Size(133, 22);
            this.btnStylesDict.Text = "Стили";
            this.btnStylesDict.Click += new System.EventHandler(this.btnStylesDict_Click);
            // 
            // btnSalesDict
            // 
            this.btnSalesDict.Name = "btnSalesDict";
            this.btnSalesDict.Size = new System.Drawing.Size(133, 22);
            this.btnSalesDict.Text = "Акции";
            this.btnSalesDict.Click += new System.EventHandler(this.btnSalesDict_Click);
            // 
            // btnMarketplaceDict
            // 
            this.btnMarketplaceDict.Name = "btnMarketplaceDict";
            this.btnMarketplaceDict.Size = new System.Drawing.Size(133, 22);
            this.btnMarketplaceDict.Text = "Площадки";
            this.btnMarketplaceDict.Click += new System.EventHandler(this.btnMarketplaceDict_Click);
            // 
            // дополнительноToolStripMenuItem
            // 
            this.дополнительноToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnClearUnreadMsg,
            this.объединитьВБандлToolStripMenuItem});
            this.дополнительноToolStripMenuItem.Name = "дополнительноToolStripMenuItem";
            this.дополнительноToolStripMenuItem.Size = new System.Drawing.Size(107, 20);
            this.дополнительноToolStripMenuItem.Text = "Дополнительно";
            // 
            // btnClearUnreadMsg
            // 
            this.btnClearUnreadMsg.Name = "btnClearUnreadMsg";
            this.btnClearUnreadMsg.Size = new System.Drawing.Size(212, 22);
            this.btnClearUnreadMsg.Text = "Сбросить непроч сообщ";
            this.btnClearUnreadMsg.Click += new System.EventHandler(this.btnClearUnreadMsg_Click);
            // 
            // объединитьВБандлToolStripMenuItem
            // 
            this.объединитьВБандлToolStripMenuItem.Name = "объединитьВБандлToolStripMenuItem";
            this.объединитьВБандлToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.объединитьВБандлToolStripMenuItem.Text = "Объединить в бандл";
            // 
            // подготовитьКартинкиToolStripMenuItem
            // 
            this.подготовитьКартинкиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnPrepareImagesVKontakte});
            this.подготовитьКартинкиToolStripMenuItem.Name = "подготовитьКартинкиToolStripMenuItem";
            this.подготовитьКартинкиToolStripMenuItem.Size = new System.Drawing.Size(143, 20);
            this.подготовитьКартинкиToolStripMenuItem.Text = "Подготовить картинки";
            // 
            // btnPrepareImagesVKontakte
            // 
            this.btnPrepareImagesVKontakte.Name = "btnPrepareImagesVKontakte";
            this.btnPrepareImagesVKontakte.Size = new System.Drawing.Size(146, 22);
            this.btnPrepareImagesVKontakte.Text = "Для контакта";
            this.btnPrepareImagesVKontakte.Click += new System.EventHandler(this.btnPrepareImagesVKontakte_Click);
            // 
            // btnRefreshAll
            // 
            this.btnRefreshAll.Location = new System.Drawing.Point(248, 90);
            this.btnRefreshAll.Name = "btnRefreshAll";
            this.btnRefreshAll.Size = new System.Drawing.Size(112, 39);
            this.btnRefreshAll.TabIndex = 18;
            this.btnRefreshAll.Text = "Обновить цены, стили, картинки";
            this.btnRefreshAll.UseVisualStyleBackColor = true;
            this.btnRefreshAll.Click += new System.EventHandler(this.btnRefreshAll_Click);
            // 
            // btnFindImage
            // 
            this.btnFindImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFindImage.Location = new System.Drawing.Point(683, 45);
            this.btnFindImage.Name = "btnFindImage";
            this.btnFindImage.Size = new System.Drawing.Size(112, 39);
            this.btnFindImage.TabIndex = 19;
            this.btnFindImage.Text = "Найти Картинку";
            this.btnFindImage.UseVisualStyleBackColor = true;
            this.btnFindImage.Click += new System.EventHandler(this.btnFindImage_Click);
            // 
            // btnChangePricePosition
            // 
            this.btnChangePricePosition.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChangePricePosition.Location = new System.Drawing.Point(683, 90);
            this.btnChangePricePosition.Name = "btnChangePricePosition";
            this.btnChangePricePosition.Size = new System.Drawing.Size(112, 39);
            this.btnChangePricePosition.TabIndex = 20;
            this.btnChangePricePosition.Text = "Изменить Вид Цены";
            this.btnChangePricePosition.UseVisualStyleBackColor = true;
            this.btnChangePricePosition.Click += new System.EventHandler(this.btnChangePricePosition_Click);
            // 
            // btnUnpost
            // 
            this.btnUnpost.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUnpost.Location = new System.Drawing.Point(801, 90);
            this.btnUnpost.Name = "btnUnpost";
            this.btnUnpost.Size = new System.Drawing.Size(112, 39);
            this.btnUnpost.TabIndex = 25;
            this.btnUnpost.Text = "Снять объявление";
            this.btnUnpost.UseVisualStyleBackColor = true;
            this.btnUnpost.Click += new System.EventHandler(this.btnUnpost_Click);
            // 
            // btnTest
            // 
            this.btnTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTest.Location = new System.Drawing.Point(565, 45);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(112, 39);
            this.btnTest.TabIndex = 26;
            this.btnTest.Text = "Тест";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // btnSyncAdvs
            // 
            this.btnSyncAdvs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSyncAdvs.Location = new System.Drawing.Point(801, 45);
            this.btnSyncAdvs.Name = "btnSyncAdvs";
            this.btnSyncAdvs.Size = new System.Drawing.Size(112, 39);
            this.btnSyncAdvs.TabIndex = 27;
            this.btnSyncAdvs.Text = "Синхронизировать объявления";
            this.btnSyncAdvs.UseVisualStyleBackColor = true;
            this.btnSyncAdvs.Click += new System.EventHandler(this.btnSyncAdvs_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // CatalogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(925, 496);
            this.Controls.Add(this.btnSyncAdvs);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.btnUnpost);
            this.Controls.Add(this.btnChangePricePosition);
            this.Controls.Add(this.btnFindImage);
            this.Controls.Add(this.btnRefreshAll);
            this.Controls.Add(this.btnAssignSale);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.btnRescrapeInfoFromSite);
            this.Controls.Add(this.btnSpecialDescription);
            this.Controls.Add(this.btnCreateImages);
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "CatalogForm";
            this.Text = "ShowCaseForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CatalogForm_FormClosing);
            this.Load += new System.EventHandler(this.ShowCaseForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private System.Windows.Forms.TextBox txtDescription;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.Button btnCreateImages;
        private System.Windows.Forms.Button btnSpecialDescription;
        private System.Windows.Forms.Button btnRescrapeInfoFromSite;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnAssignSale;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem справочникиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem btnStylesDict;
        private System.Windows.Forms.ToolStripMenuItem btnSalesDict;
        private System.Windows.Forms.ToolStripMenuItem btnMarketplaceDict;
        private System.Windows.Forms.Button btnRefreshAll;
        private System.Windows.Forms.Button btnFindImage;
        private System.Windows.Forms.Button btnChangePricePosition;
        private System.Windows.Forms.ToolStripMenuItem дополнительноToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem btnClearUnreadMsg;
        private System.Windows.Forms.ToolStripMenuItem подготовитьКартинкиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem btnPrepareImagesVKontakte;
        private System.Windows.Forms.Button btnUnpost;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Button btnSyncAdvs;
        private System.Windows.Forms.ToolStripMenuItem объединитьВБандлToolStripMenuItem;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}