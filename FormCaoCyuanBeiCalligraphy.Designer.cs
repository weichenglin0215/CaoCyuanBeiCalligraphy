namespace CaoCyuanBeiCalligraphy
{
    partial class FormCaoCyuanBeiCalligraphy
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCaoCyuanBeiCalligraphy));
            this.textBoxWords = new System.Windows.Forms.TextBox();
            this.buttonFolderBrowser = new System.Windows.Forms.Button();
            this.textBox_Path = new System.Windows.Forms.TextBox();
            this.listBox_Files = new System.Windows.Forms.ListBox();
            this.buttonMergeWordPic = new System.Windows.Forms.Button();
            this.pictureBoxDist = new System.Windows.Forms.PictureBox();
            this.numericUpDownPatternSize = new System.Windows.Forms.NumericUpDown();
            this.buttonReduceTextBox = new System.Windows.Forms.Button();
            this.comboBoxFeature = new System.Windows.Forms.ComboBox();
            this.checkBoxDisplayKai = new System.Windows.Forms.CheckBox();
            this.panelMenu = new System.Windows.Forms.Panel();
            this.panelImages = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDist)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPatternSize)).BeginInit();
            this.panelMenu.SuspendLayout();
            this.panelImages.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxWords
            // 
            this.textBoxWords.BackColor = System.Drawing.Color.Gainsboro;
            this.textBoxWords.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxWords.Font = new System.Drawing.Font("標楷體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxWords.Location = new System.Drawing.Point(0, 35);
            this.textBoxWords.Multiline = true;
            this.textBoxWords.Name = "textBoxWords";
            this.textBoxWords.Size = new System.Drawing.Size(410, 800);
            this.textBoxWords.TabIndex = 3;
            this.textBoxWords.Text = "天地玄黃宇宙洪荒\r\n日月盈昃辰宿列張\r\n寒來暑往秋收冬藏\r\n閏餘成歲律呂調陽\r\n雲騰致雨露結為霜\r\n金生麗水玉出崑崗\r\n劍號巨闕珠稱夜光\r\n果珍李柰菜重芥薑\r\n海" +
    "鹹河淡鱗潛羽翔\r\n龍師火帝鳥官人皇\r\n始制文字乃服衣裳\r\n推位讓國有虞陶唐\r\n";
            this.textBoxWords.TextChanged += new System.EventHandler(this.textBoxWords_TextChanged);
            // 
            // buttonFolderBrowser
            // 
            this.buttonFolderBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonFolderBrowser.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonFolderBrowser.Font = new System.Drawing.Font("標楷體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.buttonFolderBrowser.Location = new System.Drawing.Point(1045, 5);
            this.buttonFolderBrowser.Name = "buttonFolderBrowser";
            this.buttonFolderBrowser.Size = new System.Drawing.Size(160, 27);
            this.buttonFolderBrowser.TabIndex = 1;
            this.buttonFolderBrowser.Text = "指定圖片目錄...";
            this.buttonFolderBrowser.UseVisualStyleBackColor = true;
            this.buttonFolderBrowser.Click += new System.EventHandler(this.buttonFolderBrowser_Click);
            // 
            // textBox_Path
            // 
            this.textBox_Path.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_Path.Font = new System.Drawing.Font("標楷體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBox_Path.Location = new System.Drawing.Point(1211, 4);
            this.textBox_Path.Name = "textBox_Path";
            this.textBox_Path.Size = new System.Drawing.Size(300, 30);
            this.textBox_Path.TabIndex = 2;
            this.textBox_Path.Text = "曹全碑拓本單字";
            // 
            // listBox_Files
            // 
            this.listBox_Files.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox_Files.FormattingEnabled = true;
            this.listBox_Files.ItemHeight = 16;
            this.listBox_Files.Location = new System.Drawing.Point(1334, 46);
            this.listBox_Files.Name = "listBox_Files";
            this.listBox_Files.Size = new System.Drawing.Size(251, 660);
            this.listBox_Files.TabIndex = 3;
            this.listBox_Files.Visible = false;
            // 
            // buttonMergeWordPic
            // 
            this.buttonMergeWordPic.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonMergeWordPic.Font = new System.Drawing.Font("標楷體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.buttonMergeWordPic.Location = new System.Drawing.Point(254, 2);
            this.buttonMergeWordPic.Name = "buttonMergeWordPic";
            this.buttonMergeWordPic.Size = new System.Drawing.Size(143, 27);
            this.buttonMergeWordPic.TabIndex = 5;
            this.buttonMergeWordPic.Text = "顯示文字圖片";
            this.buttonMergeWordPic.UseVisualStyleBackColor = true;
            this.buttonMergeWordPic.Click += new System.EventHandler(this.buttonMergeWordPic_Click);
            // 
            // pictureBoxDist
            // 
            this.pictureBoxDist.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(34)))), ((int)(((byte)(28)))));
            this.pictureBoxDist.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxDist.Name = "pictureBoxDist";
            this.pictureBoxDist.Size = new System.Drawing.Size(300, 300);
            this.pictureBoxDist.TabIndex = 7;
            this.pictureBoxDist.TabStop = false;
            this.pictureBoxDist.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxDist_MouseDown);
            this.pictureBoxDist.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBoxDist_MouseMove);
            // 
            // numericUpDownPatternSize
            // 
            this.numericUpDownPatternSize.Font = new System.Drawing.Font("標楷體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.numericUpDownPatternSize.Location = new System.Drawing.Point(403, 4);
            this.numericUpDownPatternSize.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.numericUpDownPatternSize.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericUpDownPatternSize.Name = "numericUpDownPatternSize";
            this.numericUpDownPatternSize.Size = new System.Drawing.Size(60, 30);
            this.numericUpDownPatternSize.TabIndex = 0;
            this.numericUpDownPatternSize.Value = new decimal(new int[] {
            120,
            0,
            0,
            0});
            // 
            // buttonReduceTextBox
            // 
            this.buttonReduceTextBox.Font = new System.Drawing.Font("標楷體", 16F, System.Drawing.FontStyle.Bold);
            this.buttonReduceTextBox.Location = new System.Drawing.Point(3, 3);
            this.buttonReduceTextBox.Name = "buttonReduceTextBox";
            this.buttonReduceTextBox.Size = new System.Drawing.Size(36, 27);
            this.buttonReduceTextBox.TabIndex = 4;
            this.buttonReduceTextBox.Text = "︽";
            this.buttonReduceTextBox.UseVisualStyleBackColor = true;
            this.buttonReduceTextBox.Click += new System.EventHandler(this.buttonReduceTextBox_Click);
            // 
            // comboBoxFeature
            // 
            this.comboBoxFeature.Font = new System.Drawing.Font("標楷體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.comboBoxFeature.FormattingEnabled = true;
            this.comboBoxFeature.Items.AddRange(new object[] {
            "波橫",
            "短橫",
            "長豎",
            "豎彎鉤",
            "連折",
            "斷折",
            "點點",
            "旁糸",
            "平捺",
            "波捺",
            "長撇",
            "豎撇",
            "千字文",
            "曹全碑",
            "靜夜思",
            "月下獨酌",
            "黃鶴樓送孟浩然之廣陵",
            "贈汪倫",
            "宣州謝朓樓餞別校書叔雲",
            "送友人",
            "將進酒",
            "長相思",
            "望廬山瀑布",
            "行路難",
            "獨坐敬亭山",
            "清平調",
            "怨情",
            "早發白帝城",
            "送別(李叔同)",
            "定風波(蘇東坡)"});
            this.comboBoxFeature.Location = new System.Drawing.Point(45, 3);
            this.comboBoxFeature.Name = "comboBoxFeature";
            this.comboBoxFeature.Size = new System.Drawing.Size(203, 27);
            this.comboBoxFeature.TabIndex = 8;
            this.comboBoxFeature.Text = "請選擇筆劃或詩句";
            this.comboBoxFeature.SelectedIndexChanged += new System.EventHandler(this.comboBoxFeature_SelectIndexChange);
            // 
            // checkBoxDisplayKai
            // 
            this.checkBoxDisplayKai.AutoSize = true;
            this.checkBoxDisplayKai.Font = new System.Drawing.Font("標楷體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.checkBoxDisplayKai.Location = new System.Drawing.Point(469, 5);
            this.checkBoxDisplayKai.Name = "checkBoxDisplayKai";
            this.checkBoxDisplayKai.Size = new System.Drawing.Size(108, 23);
            this.checkBoxDisplayKai.TabIndex = 9;
            this.checkBoxDisplayKai.Text = "顯示楷書";
            this.checkBoxDisplayKai.UseVisualStyleBackColor = true;
            this.checkBoxDisplayKai.CheckedChanged += new System.EventHandler(this.checkBoxDisplayKai_CheckedChanged);
            // 
            // panelMenu
            // 
            this.panelMenu.Controls.Add(this.buttonReduceTextBox);
            this.panelMenu.Controls.Add(this.checkBoxDisplayKai);
            this.panelMenu.Controls.Add(this.comboBoxFeature);
            this.panelMenu.Controls.Add(this.buttonMergeWordPic);
            this.panelMenu.Controls.Add(this.numericUpDownPatternSize);
            this.panelMenu.Controls.Add(this.textBox_Path);
            this.panelMenu.Controls.Add(this.buttonFolderBrowser);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelMenu.Location = new System.Drawing.Point(0, 0);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(1514, 40);
            this.panelMenu.TabIndex = 1;
            // 
            // panelImages
            // 
            this.panelImages.AutoScroll = true;
            this.panelImages.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelImages.Controls.Add(this.pictureBoxDist);
            this.panelImages.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelImages.Location = new System.Drawing.Point(0, 40);
            this.panelImages.Name = "panelImages";
            this.panelImages.Size = new System.Drawing.Size(1514, 62);
            this.panelImages.TabIndex = 2;
            this.panelImages.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelImages_MouseDown);
            this.panelImages.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelImages_MouseMove);
            // 
            // FormCaoCyuanBeiCalligraphy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1514, 791);
            this.Controls.Add(this.textBoxWords);
            this.Controls.Add(this.panelImages);
            this.Controls.Add(this.panelMenu);
            this.Controls.Add(this.listBox_Files);
            this.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormCaoCyuanBeiCalligraphy";
            this.Text = "曹全碑集字 Ver-0.3.0.0";
            this.Load += new System.EventHandler(this.FormCaoCyuanBeiCalligraphy_Load);
            this.Resize += new System.EventHandler(this.FormCaoCyuanBeiCalligraphy_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDist)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPatternSize)).EndInit();
            this.panelMenu.ResumeLayout(false);
            this.panelMenu.PerformLayout();
            this.panelImages.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxWords;
        private System.Windows.Forms.Button buttonFolderBrowser;
        private System.Windows.Forms.TextBox textBox_Path;
        private System.Windows.Forms.ListBox listBox_Files;
        private System.Windows.Forms.Button buttonMergeWordPic;
        private System.Windows.Forms.PictureBox pictureBoxDist;
        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Panel panelImages;
        private System.Windows.Forms.NumericUpDown numericUpDownPatternSize;
        private System.Windows.Forms.Button buttonReduceTextBox;
        private System.Windows.Forms.ComboBox comboBoxFeature;
        private System.Windows.Forms.CheckBox checkBoxDisplayKai;

    }
}

