namespace TestLotCreatorWin
{
    partial class StylesDictForm
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
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnCreateSpecDescriptions = new System.Windows.Forms.Button();
            this.btnChoose = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnCopy = new System.Windows.Forms.Button();
            this.btnCreateSaleStyle = new System.Windows.Forms.Button();
            this.btnCreateMarketStyle = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControl1.Location = new System.Drawing.Point(12, 57);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(711, 474);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.DoubleClick += new System.EventHandler(this.gridView1_DoubleClick);
            // 
            // btnCreateSpecDescriptions
            // 
            this.btnCreateSpecDescriptions.Location = new System.Drawing.Point(12, 12);
            this.btnCreateSpecDescriptions.Name = "btnCreateSpecDescriptions";
            this.btnCreateSpecDescriptions.Size = new System.Drawing.Size(112, 39);
            this.btnCreateSpecDescriptions.TabIndex = 7;
            this.btnCreateSpecDescriptions.Text = "Создать Спец Оформление";
            this.btnCreateSpecDescriptions.UseVisualStyleBackColor = true;
            this.btnCreateSpecDescriptions.Click += new System.EventHandler(this.btnTitlesDescriptions_Click);
            // 
            // btnChoose
            // 
            this.btnChoose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChoose.Location = new System.Drawing.Point(611, 12);
            this.btnChoose.Name = "btnChoose";
            this.btnChoose.Size = new System.Drawing.Size(112, 39);
            this.btnChoose.TabIndex = 8;
            this.btnChoose.Text = "Выбрать";
            this.btnChoose.UseVisualStyleBackColor = true;
            this.btnChoose.Click += new System.EventHandler(this.btnChoose_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.Location = new System.Drawing.Point(493, 12);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(112, 39);
            this.btnEdit.TabIndex = 9;
            this.btnEdit.Text = "Редактировать";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopy.Location = new System.Drawing.Point(375, 12);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(112, 39);
            this.btnCopy.TabIndex = 10;
            this.btnCopy.Text = "Копировать";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnCreateSaleStyle
            // 
            this.btnCreateSaleStyle.Location = new System.Drawing.Point(130, 12);
            this.btnCreateSaleStyle.Name = "btnCreateSaleStyle";
            this.btnCreateSaleStyle.Size = new System.Drawing.Size(112, 39);
            this.btnCreateSaleStyle.TabIndex = 11;
            this.btnCreateSaleStyle.Text = "Создать Для Акции";
            this.btnCreateSaleStyle.UseVisualStyleBackColor = true;
            this.btnCreateSaleStyle.Click += new System.EventHandler(this.btnCreateSaleStyle_Click);
            // 
            // btnCreateMarketStyle
            // 
            this.btnCreateMarketStyle.Location = new System.Drawing.Point(248, 12);
            this.btnCreateMarketStyle.Name = "btnCreateMarketStyle";
            this.btnCreateMarketStyle.Size = new System.Drawing.Size(112, 39);
            this.btnCreateMarketStyle.TabIndex = 12;
            this.btnCreateMarketStyle.Text = "Создать Для Площадки";
            this.btnCreateMarketStyle.UseVisualStyleBackColor = true;
            this.btnCreateMarketStyle.Click += new System.EventHandler(this.btnCreateMarketStyle_Click);
            // 
            // StylesDictForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(735, 543);
            this.Controls.Add(this.btnCreateMarketStyle);
            this.Controls.Add(this.btnCreateSaleStyle);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnChoose);
            this.Controls.Add(this.btnCreateSpecDescriptions);
            this.Controls.Add(this.gridControl1);
            this.Name = "StylesDictForm";
            this.Text = "StylesDictForm";
            this.Load += new System.EventHandler(this.StylesDictForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.Button btnCreateSpecDescriptions;
        private System.Windows.Forms.Button btnChoose;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.Button btnCreateSaleStyle;
        private System.Windows.Forms.Button btnCreateMarketStyle;
    }
}