namespace PrestaWinClient
{
    partial class SetImageForm
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
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
            this.galleryControl1 = new DevExpress.XtraBars.Ribbon.GalleryControl();
            this.galleryControlClient1 = new DevExpress.XtraBars.Ribbon.GalleryControlClient();
            this.galleryEntityImages = new DevExpress.XtraBars.Ribbon.GalleryControl();
            this.galleryControlClient2 = new DevExpress.XtraBars.Ribbon.GalleryControlClient();
            this.simpleButton4 = new DevExpress.XtraEditors.SimpleButton();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.galleryControl1)).BeginInit();
            this.galleryControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.galleryEntityImages)).BeginInit();
            this.galleryEntityImages.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureEdit1.Location = new System.Drawing.Point(630, 12);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.pictureEdit1.Size = new System.Drawing.Size(507, 316);
            this.pictureEdit1.TabIndex = 1;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.simpleButton1.Location = new System.Drawing.Point(422, 542);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 37);
            this.simpleButton1.TabIndex = 4;
            this.simpleButton1.Text = "Добавить";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.simpleButton2.Location = new System.Drawing.Point(532, 542);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(75, 37);
            this.simpleButton2.TabIndex = 5;
            this.simpleButton2.Text = "Убрать";
            // 
            // simpleButton3
            // 
            this.simpleButton3.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.simpleButton3.Location = new System.Drawing.Point(644, 542);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new System.Drawing.Size(75, 37);
            this.simpleButton3.TabIndex = 6;
            this.simpleButton3.Text = "Главная";
            // 
            // galleryControl1
            // 
            this.galleryControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.galleryControl1.Controls.Add(this.galleryControlClient1);
            this.galleryControl1.DesignGalleryGroupIndex = 0;
            this.galleryControl1.DesignGalleryItemIndex = 0;
            // 
            // galleryControlGallery1
            // 
            this.galleryControl1.Gallery.ItemSize = new System.Drawing.Size(200, 250);
            this.galleryControl1.Gallery.ItemClick += new DevExpress.XtraBars.Ribbon.GalleryItemClickEventHandler(this.galleryControlGallery1_ItemClick);
            this.galleryControl1.Location = new System.Drawing.Point(13, 12);
            this.galleryControl1.Name = "galleryControl1";
            this.galleryControl1.Size = new System.Drawing.Size(611, 524);
            this.galleryControl1.TabIndex = 7;
            this.galleryControl1.Text = "galleryControl1";
            // 
            // galleryControlClient1
            // 
            this.galleryControlClient1.GalleryControl = this.galleryControl1;
            this.galleryControlClient1.Location = new System.Drawing.Point(2, 2);
            this.galleryControlClient1.Size = new System.Drawing.Size(590, 520);
            // 
            // galleryEntityImages
            // 
            this.galleryEntityImages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.galleryEntityImages.Controls.Add(this.galleryControlClient2);
            this.galleryEntityImages.DesignGalleryGroupIndex = 0;
            this.galleryEntityImages.DesignGalleryItemIndex = 0;
            // 
            // galleryControlGallery2
            // 
            this.galleryEntityImages.Gallery.ShowItemText = true;
            this.galleryEntityImages.Location = new System.Drawing.Point(630, 334);
            this.galleryEntityImages.Name = "galleryEntityImages";
            this.galleryEntityImages.Size = new System.Drawing.Size(498, 200);
            this.galleryEntityImages.TabIndex = 8;
            this.galleryEntityImages.Text = "galleryControl2";
            // 
            // galleryControlClient2
            // 
            this.galleryControlClient2.GalleryControl = this.galleryEntityImages;
            this.galleryControlClient2.Location = new System.Drawing.Point(2, 2);
            this.galleryControlClient2.Size = new System.Drawing.Size(477, 196);
            // 
            // simpleButton4
            // 
            this.simpleButton4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.simpleButton4.Location = new System.Drawing.Point(12, 540);
            this.simpleButton4.Name = "simpleButton4";
            this.simpleButton4.Size = new System.Drawing.Size(106, 37);
            this.simpleButton4.TabIndex = 9;
            this.simpleButton4.Text = "Загрузить с диска";
            this.simpleButton4.Click += new System.EventHandler(this.simpleButton4_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg";
            this.openFileDialog1.Multiselect = true;
            // 
            // SetImageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1149, 589);
            this.Controls.Add(this.simpleButton4);
            this.Controls.Add(this.galleryEntityImages);
            this.Controls.Add(this.galleryControl1);
            this.Controls.Add(this.simpleButton3);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.pictureEdit1);
            this.KeyPreview = true;
            this.Name = "SetImageForm";
            this.Text = "SetImageForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SetImageForm_FormClosing);
            this.Load += new System.EventHandler(this.SetImageForm_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.SetImageForm_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.galleryControl1)).EndInit();
            this.galleryControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.galleryEntityImages)).EndInit();
            this.galleryEntityImages.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton simpleButton3;
        private DevExpress.XtraBars.Ribbon.GalleryControl galleryControl1;
        private DevExpress.XtraBars.Ribbon.GalleryControlClient galleryControlClient1;
        private DevExpress.XtraBars.Ribbon.GalleryControl galleryEntityImages;
        private DevExpress.XtraBars.Ribbon.GalleryControlClient galleryControlClient2;
        private DevExpress.XtraEditors.SimpleButton simpleButton4;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}