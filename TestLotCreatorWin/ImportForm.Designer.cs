namespace TestLotCreatorWin
{
    partial class ImportForm
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
            this.button1 = new System.Windows.Forms.Button();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.btnDistributorSale = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnSaveToCatalog = new System.Windows.Forms.Button();
            this.bgwSaveToCatalog = new System.ComponentModel.BackgroundWorker();
            this.button2 = new System.Windows.Forms.Button();
            this.btnChooseSelected = new System.Windows.Forms.Button();
            this.txtArticles = new System.Windows.Forms.TextBox();
            this.btnFilterByArticles = new System.Windows.Forms.Button();
            this.txtNoFindedArticles = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(102, 41);
            this.button1.TabIndex = 1;
            this.button1.Text = "Импортировать прайс";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // gridControl1
            // 
            this.gridControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControl1.Location = new System.Drawing.Point(12, 128);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(898, 309);
            this.gridControl1.TabIndex = 2;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControl1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.gridControl1_KeyUp);
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsView.ShowAutoFilterRow = true;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // btnDistributorSale
            // 
            this.btnDistributorSale.Location = new System.Drawing.Point(120, 12);
            this.btnDistributorSale.Name = "btnDistributorSale";
            this.btnDistributorSale.Size = new System.Drawing.Size(102, 41);
            this.btnDistributorSale.TabIndex = 3;
            this.btnDistributorSale.Text = "Акция от поставщика";
            this.btnDistributorSale.UseVisualStyleBackColor = true;
            this.btnDistributorSale.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(228, 12);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(102, 41);
            this.btnRefresh.TabIndex = 4;
            this.btnRefresh.Text = "Обновить";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnSaveToCatalog
            // 
            this.btnSaveToCatalog.Location = new System.Drawing.Point(509, 12);
            this.btnSaveToCatalog.Name = "btnSaveToCatalog";
            this.btnSaveToCatalog.Size = new System.Drawing.Size(102, 41);
            this.btnSaveToCatalog.TabIndex = 6;
            this.btnSaveToCatalog.Text = "Сохранить в Каталог";
            this.btnSaveToCatalog.UseVisualStyleBackColor = true;
            this.btnSaveToCatalog.Click += new System.EventHandler(this.btnSaveToCatalog_Click);
            // 
            // bgwSaveToCatalog
            // 
            this.bgwSaveToCatalog.WorkerReportsProgress = true;
            this.bgwSaveToCatalog.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwSaveToCatalog_DoWork);
            this.bgwSaveToCatalog.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgwSaveToCatalog_ProgressChanged);
            this.bgwSaveToCatalog.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwSaveToCatalog_RunWorkerCompleted);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(401, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(102, 41);
            this.button2.TabIndex = 7;
            this.button2.Text = "Картинки";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // btnChooseSelected
            // 
            this.btnChooseSelected.Location = new System.Drawing.Point(617, 12);
            this.btnChooseSelected.Name = "btnChooseSelected";
            this.btnChooseSelected.Size = new System.Drawing.Size(102, 41);
            this.btnChooseSelected.TabIndex = 8;
            this.btnChooseSelected.Text = "Отметить Выделенные";
            this.btnChooseSelected.UseVisualStyleBackColor = true;
            this.btnChooseSelected.Click += new System.EventHandler(this.btnChooseSelected_Click);
            // 
            // txtArticles
            // 
            this.txtArticles.Location = new System.Drawing.Point(12, 59);
            this.txtArticles.Multiline = true;
            this.txtArticles.Name = "txtArticles";
            this.txtArticles.Size = new System.Drawing.Size(599, 63);
            this.txtArticles.TabIndex = 9;
            this.txtArticles.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtArticles_KeyUp);
            // 
            // btnFilterByArticles
            // 
            this.btnFilterByArticles.Location = new System.Drawing.Point(808, 12);
            this.btnFilterByArticles.Name = "btnFilterByArticles";
            this.btnFilterByArticles.Size = new System.Drawing.Size(102, 41);
            this.btnFilterByArticles.TabIndex = 10;
            this.btnFilterByArticles.Text = "Отфильтровать";
            this.btnFilterByArticles.UseVisualStyleBackColor = true;
            this.btnFilterByArticles.Click += new System.EventHandler(this.btnFilterByArticles_Click);
            // 
            // txtNoFindedArticles
            // 
            this.txtNoFindedArticles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNoFindedArticles.Location = new System.Drawing.Point(617, 59);
            this.txtNoFindedArticles.Multiline = true;
            this.txtNoFindedArticles.Name = "txtNoFindedArticles";
            this.txtNoFindedArticles.Size = new System.Drawing.Size(293, 63);
            this.txtNoFindedArticles.TabIndex = 11;
            // 
            // ImportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(922, 449);
            this.Controls.Add(this.txtNoFindedArticles);
            this.Controls.Add(this.btnFilterByArticles);
            this.Controls.Add(this.txtArticles);
            this.Controls.Add(this.btnChooseSelected);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnSaveToCatalog);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnDistributorSale);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.button1);
            this.Name = "ImportForm";
            this.Text = "Склад Бегемот";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button btnDistributorSale;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnSaveToCatalog;
        private System.ComponentModel.BackgroundWorker bgwSaveToCatalog;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnChooseSelected;
        private System.Windows.Forms.TextBox txtArticles;
        private System.Windows.Forms.Button btnFilterByArticles;
        private System.Windows.Forms.TextBox txtNoFindedArticles;
    }
}