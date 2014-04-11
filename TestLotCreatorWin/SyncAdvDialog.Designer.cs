namespace TestLotCreatorWin
{
    partial class SyncAdvDialog
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
            this.chkAll = new System.Windows.Forms.CheckBox();
            this.radioSyncMode = new DevExpress.XtraEditors.RadioGroup();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnSync = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioSyncMode.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(12, 152);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(544, 221);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            // 
            // chkAll
            // 
            this.chkAll.AutoSize = true;
            this.chkAll.Location = new System.Drawing.Point(12, 129);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(96, 17);
            this.chkAll.TabIndex = 1;
            this.chkAll.Text = "Отметить все";
            this.chkAll.UseVisualStyleBackColor = true;
            this.chkAll.CheckedChanged += new System.EventHandler(this.chkAll_CheckedChanged);
            // 
            // radioSyncMode
            // 
            this.radioSyncMode.EditValue = 1;
            this.radioSyncMode.Location = new System.Drawing.Point(12, 48);
            this.radioSyncMode.Name = "radioSyncMode";
            this.radioSyncMode.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(0, "Выбранные", false),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(1, "Опубликованные"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(2, "Все")});
            this.radioSyncMode.Size = new System.Drawing.Size(296, 75);
            this.radioSyncMode.TabIndex = 2;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 29);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(174, 13);
            this.labelControl1.TabIndex = 3;
            this.labelControl1.Text = "Товары подлежащие публикации:";
            // 
            // btnSync
            // 
            this.btnSync.Location = new System.Drawing.Point(326, 389);
            this.btnSync.Name = "btnSync";
            this.btnSync.Size = new System.Drawing.Size(112, 23);
            this.btnSync.TabIndex = 4;
            this.btnSync.Text = "Синхронизировать";
            this.btnSync.Click += new System.EventHandler(this.btnSync_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(444, 389);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(112, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Отмена";
            // 
            // SyncAdvDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(568, 424);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSync);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.radioSyncMode);
            this.Controls.Add(this.chkAll);
            this.Controls.Add(this.gridControl1);
            this.Name = "SyncAdvDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Синхронизация объявлений";
            this.Load += new System.EventHandler(this.SyncAdvDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioSyncMode.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.CheckBox chkAll;
        private DevExpress.XtraEditors.RadioGroup radioSyncMode;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btnSync;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
    }
}