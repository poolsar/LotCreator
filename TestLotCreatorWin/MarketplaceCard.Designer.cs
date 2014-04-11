namespace TestLotCreatorWin
{
    partial class MarketplaceCard
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
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtActive = new System.Windows.Forms.CheckBox();
            this.bthChooseStyle = new System.Windows.Forms.Button();
            this.txtStyle = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(88, 38);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(265, 20);
            this.txtTitle.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Название";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(88, 64);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(265, 76);
            this.txtDescription.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 67);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Примечание";
            // 
            // txtActive
            // 
            this.txtActive.AutoSize = true;
            this.txtActive.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.txtActive.Checked = true;
            this.txtActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.txtActive.Location = new System.Drawing.Point(13, 217);
            this.txtActive.Name = "txtActive";
            this.txtActive.Size = new System.Drawing.Size(77, 17);
            this.txtActive.TabIndex = 31;
            this.txtActive.Text = "Вкл/Выкл";
            this.txtActive.UseVisualStyleBackColor = true;
            // 
            // bthChooseStyle
            // 
            this.bthChooseStyle.Location = new System.Drawing.Point(329, 153);
            this.bthChooseStyle.Name = "bthChooseStyle";
            this.bthChooseStyle.Size = new System.Drawing.Size(24, 20);
            this.bthChooseStyle.TabIndex = 30;
            this.bthChooseStyle.Text = "...";
            this.bthChooseStyle.UseVisualStyleBackColor = true;
            this.bthChooseStyle.Click += new System.EventHandler(this.bthChooseStyle_Click);
            // 
            // txtStyle
            // 
            this.txtStyle.Location = new System.Drawing.Point(88, 153);
            this.txtStyle.Name = "txtStyle";
            this.txtStyle.ReadOnly = true;
            this.txtStyle.Size = new System.Drawing.Size(244, 20);
            this.txtStyle.TabIndex = 29;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(12, 156);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(37, 13);
            this.label11.TabIndex = 28;
            this.label11.Text = "Стиль";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(278, 213);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 32;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtUrl
            // 
            this.txtUrl.Location = new System.Drawing.Point(87, 12);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.ReadOnly = true;
            this.txtUrl.Size = new System.Drawing.Size(265, 20);
            this.txtUrl.TabIndex = 34;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 33;
            this.label2.Text = "Адрес";
            // 
            // MarketplaceCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 246);
            this.Controls.Add(this.txtUrl);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtActive);
            this.Controls.Add(this.bthChooseStyle);
            this.Controls.Add(this.txtStyle);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.label1);
            this.Name = "MarketplaceCard";
            this.Text = "MarketplaceCard";
            this.Load += new System.EventHandler(this.MarketplaceCard_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox txtActive;
        private System.Windows.Forms.Button bthChooseStyle;
        private System.Windows.Forms.TextBox txtStyle;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Label label2;
    }
}