namespace AI_Analysis_GUI
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.MainImageBox = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ButtonOpenImage = new System.Windows.Forms.Button();
            this.ButtonAnalysisImage = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ButtonOpenVideo = new System.Windows.Forms.Button();
            this.ButtonAnalysisVideo = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ButtonOpenOkCard = new System.Windows.Forms.Button();
            this.ButtonAnalysisOkCard = new System.Windows.Forms.Button();
            this.ButtonCloseOkCard = new System.Windows.Forms.Button();
            this.ButtonStopVideo = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.MainImageBox)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainImageBox
            // 
            this.MainImageBox.BackColor = System.Drawing.SystemColors.Window;
            this.MainImageBox.Location = new System.Drawing.Point(215, 12);
            this.MainImageBox.Name = "MainImageBox";
            this.MainImageBox.Size = new System.Drawing.Size(792, 592);
            this.MainImageBox.TabIndex = 0;
            this.MainImageBox.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Location = new System.Drawing.Point(13, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(196, 592);
            this.panel1.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ButtonAnalysisImage);
            this.groupBox1.Controls.Add(this.ButtonOpenImage);
            this.groupBox1.Location = new System.Drawing.Point(4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(185, 123);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "本地图片分析";
            // 
            // ButtonOpenImage
            // 
            this.ButtonOpenImage.Image = ((System.Drawing.Image)(resources.GetObject("ButtonOpenImage.Image")));
            this.ButtonOpenImage.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ButtonOpenImage.Location = new System.Drawing.Point(7, 26);
            this.ButtonOpenImage.Name = "ButtonOpenImage";
            this.ButtonOpenImage.Size = new System.Drawing.Size(171, 34);
            this.ButtonOpenImage.TabIndex = 0;
            this.ButtonOpenImage.Text = "打开图片";
            this.ButtonOpenImage.UseVisualStyleBackColor = true;
            this.ButtonOpenImage.Click += new System.EventHandler(this.ButtonOpenImage_Click);
            // 
            // ButtonAnalysisImage
            // 
            this.ButtonAnalysisImage.Image = ((System.Drawing.Image)(resources.GetObject("ButtonAnalysisImage.Image")));
            this.ButtonAnalysisImage.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ButtonAnalysisImage.Location = new System.Drawing.Point(6, 72);
            this.ButtonAnalysisImage.Name = "ButtonAnalysisImage";
            this.ButtonAnalysisImage.Size = new System.Drawing.Size(171, 34);
            this.ButtonAnalysisImage.TabIndex = 1;
            this.ButtonAnalysisImage.Text = "分析图片";
            this.ButtonAnalysisImage.UseVisualStyleBackColor = true;
            this.ButtonAnalysisImage.Click += new System.EventHandler(this.ButtonAnalysisImage_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ButtonStopVideo);
            this.groupBox2.Controls.Add(this.ButtonAnalysisVideo);
            this.groupBox2.Controls.Add(this.ButtonOpenVideo);
            this.groupBox2.Location = new System.Drawing.Point(4, 134);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(185, 165);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "本地视频分析";
            // 
            // ButtonOpenVideo
            // 
            this.ButtonOpenVideo.Image = ((System.Drawing.Image)(resources.GetObject("ButtonOpenVideo.Image")));
            this.ButtonOpenVideo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ButtonOpenVideo.Location = new System.Drawing.Point(6, 27);
            this.ButtonOpenVideo.Name = "ButtonOpenVideo";
            this.ButtonOpenVideo.Size = new System.Drawing.Size(171, 34);
            this.ButtonOpenVideo.TabIndex = 2;
            this.ButtonOpenVideo.Text = "打开视频";
            this.ButtonOpenVideo.UseVisualStyleBackColor = true;
            this.ButtonOpenVideo.Click += new System.EventHandler(this.ButtonOpenVideo_Click);
            // 
            // ButtonAnalysisVideo
            // 
            this.ButtonAnalysisVideo.Image = ((System.Drawing.Image)(resources.GetObject("ButtonAnalysisVideo.Image")));
            this.ButtonAnalysisVideo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ButtonAnalysisVideo.Location = new System.Drawing.Point(6, 72);
            this.ButtonAnalysisVideo.Name = "ButtonAnalysisVideo";
            this.ButtonAnalysisVideo.Size = new System.Drawing.Size(171, 34);
            this.ButtonAnalysisVideo.TabIndex = 2;
            this.ButtonAnalysisVideo.Text = "分析视频";
            this.ButtonAnalysisVideo.UseVisualStyleBackColor = true;
            this.ButtonAnalysisVideo.Click += new System.EventHandler(this.ButtonAnalysisVideo_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.ButtonCloseOkCard);
            this.groupBox3.Controls.Add(this.ButtonAnalysisOkCard);
            this.groupBox3.Controls.Add(this.ButtonOpenOkCard);
            this.groupBox3.Location = new System.Drawing.Point(4, 316);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(185, 157);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "实时视频分析";
            // 
            // ButtonOpenOkCard
            // 
            this.ButtonOpenOkCard.Image = ((System.Drawing.Image)(resources.GetObject("ButtonOpenOkCard.Image")));
            this.ButtonOpenOkCard.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ButtonOpenOkCard.Location = new System.Drawing.Point(6, 25);
            this.ButtonOpenOkCard.Name = "ButtonOpenOkCard";
            this.ButtonOpenOkCard.Size = new System.Drawing.Size(171, 34);
            this.ButtonOpenOkCard.TabIndex = 3;
            this.ButtonOpenOkCard.Text = "打开采集卡";
            this.ButtonOpenOkCard.UseVisualStyleBackColor = true;
            this.ButtonOpenOkCard.Click += new System.EventHandler(this.ButtonOpenOkCard_Click);
            // 
            // ButtonAnalysisOkCard
            // 
            this.ButtonAnalysisOkCard.Image = ((System.Drawing.Image)(resources.GetObject("ButtonAnalysisOkCard.Image")));
            this.ButtonAnalysisOkCard.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ButtonAnalysisOkCard.Location = new System.Drawing.Point(6, 69);
            this.ButtonAnalysisOkCard.Name = "ButtonAnalysisOkCard";
            this.ButtonAnalysisOkCard.Size = new System.Drawing.Size(171, 34);
            this.ButtonAnalysisOkCard.TabIndex = 3;
            this.ButtonAnalysisOkCard.Text = "分析采集视频";
            this.ButtonAnalysisOkCard.UseVisualStyleBackColor = true;
            this.ButtonAnalysisOkCard.Click += new System.EventHandler(this.ButtonAnalysisOkCard_Click);
            // 
            // ButtonCloseOkCard
            // 
            this.ButtonCloseOkCard.Image = ((System.Drawing.Image)(resources.GetObject("ButtonCloseOkCard.Image")));
            this.ButtonCloseOkCard.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ButtonCloseOkCard.Location = new System.Drawing.Point(6, 113);
            this.ButtonCloseOkCard.Name = "ButtonCloseOkCard";
            this.ButtonCloseOkCard.Size = new System.Drawing.Size(171, 34);
            this.ButtonCloseOkCard.TabIndex = 4;
            this.ButtonCloseOkCard.Text = "停止采集";
            this.ButtonCloseOkCard.UseVisualStyleBackColor = true;
            this.ButtonCloseOkCard.Click += new System.EventHandler(this.ButtonCloseOkCard_Click);
            // 
            // ButtonStopVideo
            // 
            this.ButtonStopVideo.Image = ((System.Drawing.Image)(resources.GetObject("ButtonStopVideo.Image")));
            this.ButtonStopVideo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ButtonStopVideo.Location = new System.Drawing.Point(6, 119);
            this.ButtonStopVideo.Name = "ButtonStopVideo";
            this.ButtonStopVideo.Size = new System.Drawing.Size(171, 34);
            this.ButtonStopVideo.TabIndex = 5;
            this.ButtonStopVideo.Text = "停止分析";
            this.ButtonStopVideo.UseVisualStyleBackColor = true;
            this.ButtonStopVideo.Click += new System.EventHandler(this.ButtonStopVideo_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1019, 616);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.MainImageBox);
            this.Name = "Form1";
            this.Text = "B超乳腺癌分析";
            ((System.ComponentModel.ISupportInitialize)(this.MainImageBox)).EndInit();
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox MainImageBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button ButtonCloseOkCard;
        private System.Windows.Forms.Button ButtonAnalysisOkCard;
        private System.Windows.Forms.Button ButtonOpenOkCard;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button ButtonAnalysisVideo;
        private System.Windows.Forms.Button ButtonOpenVideo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button ButtonAnalysisImage;
        private System.Windows.Forms.Button ButtonOpenImage;
        private System.Windows.Forms.Button ButtonStopVideo;
    }
}

