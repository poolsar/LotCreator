namespace TestLotCreatorWin
{
    partial class SaleCard
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.txtMarginMax = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMarginToWholesale = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMarginMin = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDateStart = new DevExpress.XtraEditors.DateEdit();
            this.txtDateExpire = new DevExpress.XtraEditors.DateEdit();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtDescountToRetail = new System.Windows.Forms.TextBox();
            this.txtUseMin = new System.Windows.Forms.RadioButton();
            this.txtUseMax = new System.Windows.Forms.RadioButton();
            this.label10 = new System.Windows.Forms.Label();
            this.txtStyle = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.bthChooseStyle = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.grUseValue = new System.Windows.Forms.GroupBox();
            this.txtActive = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateStart.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateStart.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateExpire.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateExpire.Properties)).BeginInit();
            this.grUseValue.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Название";
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(87, 32);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(265, 20);
            this.txtTitle.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(591, 226);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Сохранить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtMarginMax
            // 
            this.txtMarginMax.Location = new System.Drawing.Point(514, 59);
            this.txtMarginMax.Name = "txtMarginMax";
            this.txtMarginMax.Size = new System.Drawing.Size(100, 20);
            this.txtMarginMax.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 143);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Период";
            // 
            // txtMarginToWholesale
            // 
            this.txtMarginToWholesale.Location = new System.Drawing.Point(514, 100);
            this.txtMarginToWholesale.Name = "txtMarginToWholesale";
            this.txtMarginToWholesale.Size = new System.Drawing.Size(100, 20);
            this.txtMarginToWholesale.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(84, 143);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "С";
            // 
            // txtMarginMin
            // 
            this.txtMarginMin.Location = new System.Drawing.Point(514, 33);
            this.txtMarginMin.Name = "txtMarginMin";
            this.txtMarginMin.Size = new System.Drawing.Size(100, 20);
            this.txtMarginMin.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(225, 143);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(21, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "По";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(87, 58);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(265, 76);
            this.txtDescription.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 61);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Примечание";
            // 
            // txtDateStart
            // 
            this.txtDateStart.EditValue = null;
            this.txtDateStart.Location = new System.Drawing.Point(104, 140);
            this.txtDateStart.Name = "txtDateStart";
            this.txtDateStart.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDateStart.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDateStart.Size = new System.Drawing.Size(100, 20);
            this.txtDateStart.TabIndex = 11;
            // 
            // txtDateExpire
            // 
            this.txtDateExpire.EditValue = null;
            this.txtDateExpire.Location = new System.Drawing.Point(252, 140);
            this.txtDateExpire.Name = "txtDateExpire";
            this.txtDateExpire.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDateExpire.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDateExpire.Size = new System.Drawing.Size(100, 20);
            this.txtDateExpire.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(380, 35);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Min моржа";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(380, 103);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(128, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Наценка на закупочную";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(380, 61);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(64, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "Max моржа";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(380, 129);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(116, 13);
            this.label9.TabIndex = 18;
            this.label9.Text = "Скидка на розничную";
            // 
            // txtDescountToRetail
            // 
            this.txtDescountToRetail.Location = new System.Drawing.Point(514, 126);
            this.txtDescountToRetail.Name = "txtDescountToRetail";
            this.txtDescountToRetail.Size = new System.Drawing.Size(100, 20);
            this.txtDescountToRetail.TabIndex = 17;
            // 
            // txtUseMin
            // 
            this.txtUseMin.AutoSize = true;
            this.txtUseMin.Location = new System.Drawing.Point(10, 19);
            this.txtUseMin.Name = "txtUseMin";
            this.txtUseMin.Size = new System.Drawing.Size(96, 17);
            this.txtUseMin.TabIndex = 19;
            this.txtUseMin.Text = "Минимальное";
            this.txtUseMin.UseVisualStyleBackColor = true;
            // 
            // txtUseMax
            // 
            this.txtUseMax.AutoSize = true;
            this.txtUseMax.Checked = true;
            this.txtUseMax.Location = new System.Drawing.Point(131, 19);
            this.txtUseMax.Name = "txtUseMax";
            this.txtUseMax.Size = new System.Drawing.Size(102, 17);
            this.txtUseMax.TabIndex = 20;
            this.txtUseMax.TabStop = true;
            this.txtUseMax.Text = "Максимальное";
            this.txtUseMax.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(633, 103);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(33, 13);
            this.label10.TabIndex = 21;
            this.label10.Text = "0-9 %";
            // 
            // txtStyle
            // 
            this.txtStyle.Location = new System.Drawing.Point(87, 166);
            this.txtStyle.Name = "txtStyle";
            this.txtStyle.ReadOnly = true;
            this.txtStyle.Size = new System.Drawing.Size(244, 20);
            this.txtStyle.TabIndex = 23;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(11, 169);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(37, 13);
            this.label11.TabIndex = 22;
            this.label11.Text = "Стиль";
            // 
            // bthChooseStyle
            // 
            this.bthChooseStyle.Location = new System.Drawing.Point(328, 166);
            this.bthChooseStyle.Name = "bthChooseStyle";
            this.bthChooseStyle.Size = new System.Drawing.Size(24, 20);
            this.bthChooseStyle.TabIndex = 24;
            this.bthChooseStyle.Text = "...";
            this.bthChooseStyle.UseVisualStyleBackColor = true;
            this.bthChooseStyle.Click += new System.EventHandler(this.bthChooseStyle_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(633, 36);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(33, 13);
            this.label12.TabIndex = 25;
            this.label12.Text = "0-9 %";
            // 
            // grUseValue
            // 
            this.grUseValue.Controls.Add(this.txtUseMin);
            this.grUseValue.Controls.Add(this.txtUseMax);
            this.grUseValue.Location = new System.Drawing.Point(383, 152);
            this.grUseValue.Name = "grUseValue";
            this.grUseValue.Size = new System.Drawing.Size(240, 48);
            this.grUseValue.TabIndex = 26;
            this.grUseValue.TabStop = false;
            this.grUseValue.Text = "В итоге выбрать значение";
            // 
            // txtActive
            // 
            this.txtActive.AutoSize = true;
            this.txtActive.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.txtActive.Checked = true;
            this.txtActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.txtActive.Location = new System.Drawing.Point(12, 230);
            this.txtActive.Name = "txtActive";
            this.txtActive.Size = new System.Drawing.Size(77, 17);
            this.txtActive.TabIndex = 27;
            this.txtActive.Text = "Вкл/Выкл";
            this.txtActive.UseVisualStyleBackColor = true;
            // 
            // SaleCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(679, 261);
            this.Controls.Add(this.txtActive);
            this.Controls.Add(this.grUseValue);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.bthChooseStyle);
            this.Controls.Add(this.txtStyle);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtDescountToRetail);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtDateExpire);
            this.Controls.Add(this.txtDateStart);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtMarginMin);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtMarginToWholesale);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtMarginMax);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.label1);
            this.Name = "SaleCard";
            this.Text = "Акция";
            this.Load += new System.EventHandler(this.SaleCard_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtDateStart.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateStart.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateExpire.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateExpire.Properties)).EndInit();
            this.grUseValue.ResumeLayout(false);
            this.grUseValue.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtMarginMax;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMarginToWholesale;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtMarginMin;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label label5;
        private DevExpress.XtraEditors.DateEdit txtDateStart;
        private DevExpress.XtraEditors.DateEdit txtDateExpire;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtDescountToRetail;
        private System.Windows.Forms.RadioButton txtUseMin;
        private System.Windows.Forms.RadioButton txtUseMax;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtStyle;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button bthChooseStyle;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox grUseValue;
        private System.Windows.Forms.CheckBox txtActive;
    }
}