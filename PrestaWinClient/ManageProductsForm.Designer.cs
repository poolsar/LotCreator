namespace PrestaWinClient
{
    partial class ManageProductsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManageProductsForm));
            this.treeSuppliers = new DevExpress.XtraTreeList.TreeList();
            this.colTitle = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.treeShop = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn4 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.repositoryItemPictureEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
            this.colShopTitle = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colShopInShop = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colShopPrice = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.repositoryItemCalcEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.repositoryItemCalcEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit();
            this.treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.repositoryItemCalcEdit4 = new DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit();
            this.treeListColumn3 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.repositoryItemCalcEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit();
            this.repositoryItemImageComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.btnRefreshSupliers = new DevExpress.XtraEditors.SimpleButton();
            this.btnClearShop = new DevExpress.XtraEditors.SimpleButton();
            this.btnDeleteNode = new System.Windows.Forms.Button();
            this.txtMarga = new DevExpress.XtraEditors.CalcEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.btnRecalcPrices = new DevExpress.XtraEditors.SimpleButton();
            this.btnExpandTreeShop = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.btnOnlineCatalog = new DevExpress.XtraEditors.SimpleButton();
            this.btnSyncShop = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            ((System.ComponentModel.ISupportInitialize)(this.treeSuppliers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeShop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCalcEdit3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCalcEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCalcEdit4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCalcEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMarga.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeSuppliers
            // 
            this.treeSuppliers.AllowDrop = true;
            this.treeSuppliers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeSuppliers.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colTitle});
            this.treeSuppliers.Location = new System.Drawing.Point(0, 82);
            this.treeSuppliers.Name = "treeSuppliers";
            this.treeSuppliers.OptionsBehavior.DragNodes = true;
            this.treeSuppliers.SelectImageList = this.imageList1;
            this.treeSuppliers.Size = new System.Drawing.Size(298, 411);
            this.treeSuppliers.TabIndex = 0;
            this.treeSuppliers.GetSelectImage += new DevExpress.XtraTreeList.GetSelectImageEventHandler(this.treeSuppliers_GetSelectImage);
            // 
            // colTitle
            // 
            this.colTitle.Caption = "Название";
            this.colTitle.FieldName = "Title";
            this.colTitle.MinWidth = 33;
            this.colTitle.Name = "colTitle";
            this.colTitle.Visible = true;
            this.colTitle.VisibleIndex = 0;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Проставщик2.png");
            this.imageList1.Images.SetKeyName(1, "Категории2.png");
            this.imageList1.Images.SetKeyName(2, "Товары2.png");
            // 
            // treeShop
            // 
            this.treeShop.AllowDrop = true;
            this.treeShop.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeShop.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn4,
            this.colShopTitle,
            this.colShopInShop,
            this.colShopPrice,
            this.treeListColumn1,
            this.treeListColumn2,
            this.treeListColumn3});
            this.treeShop.Location = new System.Drawing.Point(1, 82);
            this.treeShop.Name = "treeShop";
            this.treeShop.OptionsBehavior.DragNodes = true;
            this.treeShop.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1,
            this.repositoryItemCalcEdit1,
            this.repositoryItemCalcEdit2,
            this.repositoryItemCalcEdit3,
            this.repositoryItemCalcEdit4,
            this.repositoryItemImageComboBox1,
            this.repositoryItemPictureEdit1});
            this.treeShop.SelectImageList = this.imageList1;
            this.treeShop.Size = new System.Drawing.Size(888, 411);
            this.treeShop.TabIndex = 0;
            this.treeShop.GetSelectImage += new DevExpress.XtraTreeList.GetSelectImageEventHandler(this.treeShop_GetSelectImage);
            this.treeShop.CellValueChanged += new DevExpress.XtraTreeList.CellValueChangedEventHandler(this.treeShop_CellValueChanged);
            // 
            // treeListColumn4
            // 
            this.treeListColumn4.Caption = "Картинка";
            this.treeListColumn4.ColumnEdit = this.repositoryItemPictureEdit1;
            this.treeListColumn4.FieldName = "Картинка";
            this.treeListColumn4.Name = "treeListColumn4";
            this.treeListColumn4.OptionsColumn.AllowEdit = false;
            this.treeListColumn4.Visible = true;
            this.treeListColumn4.VisibleIndex = 1;
            this.treeListColumn4.Width = 92;
            // 
            // repositoryItemPictureEdit1
            // 
            this.repositoryItemPictureEdit1.CustomHeight = 50;
            this.repositoryItemPictureEdit1.Name = "repositoryItemPictureEdit1";
            this.repositoryItemPictureEdit1.ReadOnly = true;
            this.repositoryItemPictureEdit1.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
            this.repositoryItemPictureEdit1.ZoomPercent = 120;
            // 
            // colShopTitle
            // 
            this.colShopTitle.Caption = "Название";
            this.colShopTitle.FieldName = "Title";
            this.colShopTitle.MinWidth = 33;
            this.colShopTitle.Name = "colShopTitle";
            this.colShopTitle.Visible = true;
            this.colShopTitle.VisibleIndex = 0;
            this.colShopTitle.Width = 203;
            // 
            // colShopInShop
            // 
            this.colShopInShop.Caption = "В продаже";
            this.colShopInShop.ColumnEdit = this.repositoryItemCheckEdit1;
            this.colShopInShop.FieldName = "InShop";
            this.colShopInShop.Name = "colShopInShop";
            this.colShopInShop.Visible = true;
            this.colShopInShop.VisibleIndex = 2;
            this.colShopInShop.Width = 59;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Caption = "Check";
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // colShopPrice
            // 
            this.colShopPrice.Caption = "Продажа";
            this.colShopPrice.ColumnEdit = this.repositoryItemCalcEdit3;
            this.colShopPrice.FieldName = "Price";
            this.colShopPrice.Name = "colShopPrice";
            this.colShopPrice.Visible = true;
            this.colShopPrice.VisibleIndex = 3;
            this.colShopPrice.Width = 59;
            // 
            // repositoryItemCalcEdit3
            // 
            this.repositoryItemCalcEdit3.AutoHeight = false;
            this.repositoryItemCalcEdit3.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemCalcEdit3.Mask.EditMask = "c";
            this.repositoryItemCalcEdit3.Mask.UseMaskAsDisplayFormat = true;
            this.repositoryItemCalcEdit3.Name = "repositoryItemCalcEdit3";
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.Caption = "Покупка";
            this.treeListColumn1.ColumnEdit = this.repositoryItemCalcEdit1;
            this.treeListColumn1.FieldName = "Покупка";
            this.treeListColumn1.Name = "treeListColumn1";
            this.treeListColumn1.Visible = true;
            this.treeListColumn1.VisibleIndex = 4;
            this.treeListColumn1.Width = 59;
            // 
            // repositoryItemCalcEdit1
            // 
            this.repositoryItemCalcEdit1.AutoHeight = false;
            this.repositoryItemCalcEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemCalcEdit1.Mask.EditMask = "c";
            this.repositoryItemCalcEdit1.Mask.UseMaskAsDisplayFormat = true;
            this.repositoryItemCalcEdit1.Name = "repositoryItemCalcEdit1";
            this.repositoryItemCalcEdit1.ReadOnly = true;
            // 
            // treeListColumn2
            // 
            this.treeListColumn2.Caption = "Маржа";
            this.treeListColumn2.ColumnEdit = this.repositoryItemCalcEdit4;
            this.treeListColumn2.FieldName = "Маржа";
            this.treeListColumn2.Name = "treeListColumn2";
            this.treeListColumn2.Visible = true;
            this.treeListColumn2.VisibleIndex = 5;
            this.treeListColumn2.Width = 59;
            // 
            // repositoryItemCalcEdit4
            // 
            this.repositoryItemCalcEdit4.AutoHeight = false;
            this.repositoryItemCalcEdit4.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemCalcEdit4.Mask.EditMask = "c";
            this.repositoryItemCalcEdit4.Mask.UseMaskAsDisplayFormat = true;
            this.repositoryItemCalcEdit4.Name = "repositoryItemCalcEdit4";
            // 
            // treeListColumn3
            // 
            this.treeListColumn3.Caption = "Маржа (%)";
            this.treeListColumn3.ColumnEdit = this.repositoryItemCalcEdit2;
            this.treeListColumn3.FieldName = "Маржа (%)";
            this.treeListColumn3.Name = "treeListColumn3";
            this.treeListColumn3.Visible = true;
            this.treeListColumn3.VisibleIndex = 6;
            this.treeListColumn3.Width = 60;
            // 
            // repositoryItemCalcEdit2
            // 
            this.repositoryItemCalcEdit2.AutoHeight = false;
            this.repositoryItemCalcEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemCalcEdit2.Mask.EditMask = "P";
            this.repositoryItemCalcEdit2.Mask.UseMaskAsDisplayFormat = true;
            this.repositoryItemCalcEdit2.Name = "repositoryItemCalcEdit2";
            // 
            // repositoryItemImageComboBox1
            // 
            this.repositoryItemImageComboBox1.AutoHeight = false;
            this.repositoryItemImageComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageComboBox1.Name = "repositoryItemImageComboBox1";
            // 
            // btnRefreshSupliers
            // 
            this.btnRefreshSupliers.Location = new System.Drawing.Point(5, 24);
            this.btnRefreshSupliers.Name = "btnRefreshSupliers";
            this.btnRefreshSupliers.Size = new System.Drawing.Size(75, 23);
            this.btnRefreshSupliers.TabIndex = 4;
            this.btnRefreshSupliers.Text = "Обновить";
            this.btnRefreshSupliers.Click += new System.EventHandler(this.btnRefreshSupliers_Click);
            // 
            // btnClearShop
            // 
            this.btnClearShop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearShop.Location = new System.Drawing.Point(772, 24);
            this.btnClearShop.Name = "btnClearShop";
            this.btnClearShop.Size = new System.Drawing.Size(112, 23);
            this.btnClearShop.TabIndex = 5;
            this.btnClearShop.Text = "Сбросить ветрину";
            this.btnClearShop.Click += new System.EventHandler(this.btnClearShop_Click);
            // 
            // btnDeleteNode
            // 
            this.btnDeleteNode.AllowDrop = true;
            this.btnDeleteNode.Image = global::PrestaWinClient.Properties.Resources.edittrash;
            this.btnDeleteNode.Location = new System.Drawing.Point(5, 24);
            this.btnDeleteNode.Name = "btnDeleteNode";
            this.btnDeleteNode.Size = new System.Drawing.Size(56, 52);
            this.btnDeleteNode.TabIndex = 7;
            this.btnDeleteNode.UseVisualStyleBackColor = true;
            // 
            // txtMarga
            // 
            this.txtMarga.Location = new System.Drawing.Point(308, 26);
            this.txtMarga.Name = "txtMarga";
            this.txtMarga.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtMarga.Properties.Mask.EditMask = "P";
            this.txtMarga.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtMarga.Size = new System.Drawing.Size(78, 20);
            this.txtMarga.TabIndex = 8;
            this.txtMarga.ValueChanged += new System.EventHandler(this.txtMarga_ValueChanged);
            this.txtMarga.EditValueChanged += new System.EventHandler(this.txtMarga_EditValueChanged);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(268, 29);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(34, 13);
            this.labelControl3.TabIndex = 9;
            this.labelControl3.Text = "Маржа";
            // 
            // btnRecalcPrices
            // 
            this.btnRecalcPrices.Location = new System.Drawing.Point(268, 53);
            this.btnRecalcPrices.Name = "btnRecalcPrices";
            this.btnRecalcPrices.Size = new System.Drawing.Size(118, 23);
            this.btnRecalcPrices.TabIndex = 10;
            this.btnRecalcPrices.Text = "Пересчитать цены";
            this.btnRecalcPrices.Click += new System.EventHandler(this.btnRecalcPrices_Click);
            // 
            // btnExpandTreeShop
            // 
            this.btnExpandTreeShop.Location = new System.Drawing.Point(166, 24);
            this.btnExpandTreeShop.Name = "btnExpandTreeShop";
            this.btnExpandTreeShop.Size = new System.Drawing.Size(93, 23);
            this.btnExpandTreeShop.TabIndex = 11;
            this.btnExpandTreeShop.Text = "Развернуть все";
            this.btnExpandTreeShop.Click += new System.EventHandler(this.btnExpandTreeShop_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.Location = new System.Drawing.Point(166, 53);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(93, 23);
            this.simpleButton2.TabIndex = 12;
            this.simpleButton2.Text = "Включить все";
            // 
            // simpleButton3
            // 
            this.simpleButton3.Appearance.Options.UseTextOptions = true;
            this.simpleButton3.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.simpleButton3.Location = new System.Drawing.Point(67, 24);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new System.Drawing.Size(93, 52);
            this.simpleButton3.TabIndex = 13;
            this.simpleButton3.Text = "Назначить картинки по умолчанию";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.btnOnlineCatalog);
            this.groupControl1.Controls.Add(this.btnSyncShop);
            this.groupControl1.Controls.Add(this.treeShop);
            this.groupControl1.Controls.Add(this.simpleButton3);
            this.groupControl1.Controls.Add(this.btnClearShop);
            this.groupControl1.Controls.Add(this.simpleButton2);
            this.groupControl1.Controls.Add(this.btnDeleteNode);
            this.groupControl1.Controls.Add(this.btnExpandTreeShop);
            this.groupControl1.Controls.Add(this.txtMarga);
            this.groupControl1.Controls.Add(this.btnRecalcPrices);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(889, 493);
            this.groupControl1.TabIndex = 14;
            this.groupControl1.Text = "Магазин";
            // 
            // btnOnlineCatalog
            // 
            this.btnOnlineCatalog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOnlineCatalog.Appearance.Options.UseTextOptions = true;
            this.btnOnlineCatalog.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.btnOnlineCatalog.Image = global::PrestaWinClient.Properties.Resources.ico_store;
            this.btnOnlineCatalog.Location = new System.Drawing.Point(454, 24);
            this.btnOnlineCatalog.Name = "btnOnlineCatalog";
            this.btnOnlineCatalog.Size = new System.Drawing.Size(153, 52);
            this.btnOnlineCatalog.TabIndex = 15;
            this.btnOnlineCatalog.Text = "Online каталог";
            this.btnOnlineCatalog.Click += new System.EventHandler(this.btnOnlineCatalog_Click);
            // 
            // btnSyncShop
            // 
            this.btnSyncShop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSyncShop.Appearance.Options.UseTextOptions = true;
            this.btnSyncShop.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.btnSyncShop.Image = global::PrestaWinClient.Properties.Resources.sync;
            this.btnSyncShop.Location = new System.Drawing.Point(613, 24);
            this.btnSyncShop.Name = "btnSyncShop";
            this.btnSyncShop.Size = new System.Drawing.Size(153, 52);
            this.btnSyncShop.TabIndex = 14;
            this.btnSyncShop.Text = "Синхронизировать с магазином";
            this.btnSyncShop.Click += new System.EventHandler(this.btnSyncShop_Click);
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.treeSuppliers);
            this.groupControl2.Controls.Add(this.btnRefreshSupliers);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(0, 0);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(299, 493);
            this.groupControl2.TabIndex = 15;
            this.groupControl2.Text = "Поставщики";
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.groupControl2);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.groupControl1);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1193, 493);
            this.splitContainerControl1.SplitterPosition = 299;
            this.splitContainerControl1.TabIndex = 5;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // ManageProductsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1193, 493);
            this.Controls.Add(this.splitContainerControl1);
            this.KeyPreview = true;
            this.Name = "ManageProductsForm";
            this.Text = "ManageProductsForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.ManageProductsForm_Load);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ManageProductsForm_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.treeSuppliers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeShop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCalcEdit3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCalcEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCalcEdit4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCalcEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMarga.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTreeList.TreeList treeSuppliers;
        private DevExpress.XtraTreeList.TreeList treeShop;
        private DevExpress.XtraEditors.SimpleButton btnRefreshSupliers;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colTitle;
        private System.Windows.Forms.ImageList imageList1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colShopTitle;
        private DevExpress.XtraEditors.SimpleButton btnClearShop;
        private System.Windows.Forms.Button btnDeleteNode;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colShopInShop;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colShopPrice;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit repositoryItemCalcEdit1;
        private DevExpress.XtraEditors.CalcEdit txtMarga;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.SimpleButton btnRecalcPrices;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn2;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn3;
        private DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit repositoryItemCalcEdit2;
        private DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit repositoryItemCalcEdit3;
        private DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit repositoryItemCalcEdit4;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn4;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox1;
        private DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit repositoryItemPictureEdit1;
        private DevExpress.XtraEditors.SimpleButton btnExpandTreeShop;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton simpleButton3;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.SimpleButton btnSyncShop;
        private DevExpress.XtraEditors.SimpleButton btnOnlineCatalog;
    }
}