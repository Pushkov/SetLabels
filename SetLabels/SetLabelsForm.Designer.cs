namespace SetLabels
{
    partial class SetLabelsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetLabelsForm));
            this.btnExcludeChange = new System.Windows.Forms.Button();
            this.txtExlude = new System.Windows.Forms.TextBox();
            this.viewsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lbInfo = new System.Windows.Forms.TextBox();
            this.btnRename = new System.Windows.Forms.Button();
            this.btnReRead = new System.Windows.Forms.Button();
            this.titlePanel = new System.Windows.Forms.FlowLayoutPanel();
            this.checkOut = new System.Windows.Forms.CheckBox();
            this.checkColor = new System.Windows.Forms.CheckBox();
            this.btnAddSurf = new System.Windows.Forms.Button();
            this.btnDelSurf = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnExcludeChange
            // 
            this.btnExcludeChange.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnExcludeChange.Location = new System.Drawing.Point(6, 19);
            this.btnExcludeChange.Name = "btnExcludeChange";
            this.btnExcludeChange.Size = new System.Drawing.Size(102, 25);
            this.btnExcludeChange.TabIndex = 0;
            this.btnExcludeChange.Text = "Изменить";
            this.btnExcludeChange.UseVisualStyleBackColor = true;
            this.btnExcludeChange.Click += new System.EventHandler(this.btnChangeExclude_Click);
            // 
            // txtExlude
            // 
            this.txtExlude.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtExlude.Location = new System.Drawing.Point(114, 19);
            this.txtExlude.Name = "txtExlude";
            this.txtExlude.Size = new System.Drawing.Size(183, 24);
            this.txtExlude.TabIndex = 1;
            // 
            // viewsPanel
            // 
            this.viewsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.viewsPanel.AutoScroll = true;
            this.viewsPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.viewsPanel.Location = new System.Drawing.Point(5, 163);
            this.viewsPanel.Name = "viewsPanel";
            this.viewsPanel.Size = new System.Drawing.Size(905, 322);
            this.viewsPanel.TabIndex = 2;
            this.viewsPanel.WrapContents = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnExcludeChange);
            this.groupBox1.Controls.Add(this.txtExlude);
            this.groupBox1.Location = new System.Drawing.Point(12, 49);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(303, 53);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Исключенные буквы";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lbInfo);
            this.groupBox2.Location = new System.Drawing.Point(338, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(572, 87);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Инфо";
            // 
            // lbInfo
            // 
            this.lbInfo.BackColor = System.Drawing.SystemColors.Menu;
            this.lbInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbInfo.Location = new System.Drawing.Point(6, 14);
            this.lbInfo.Multiline = true;
            this.lbInfo.Name = "lbInfo";
            this.lbInfo.Size = new System.Drawing.Size(565, 65);
            this.lbInfo.TabIndex = 0;
            // 
            // btnRename
            // 
            this.btnRename.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRename.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRename.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnRename.Location = new System.Drawing.Point(573, 491);
            this.btnRename.Name = "btnRename";
            this.btnRename.Size = new System.Drawing.Size(329, 50);
            this.btnRename.TabIndex = 7;
            this.btnRename.Text = "Переименовать";
            this.btnRename.UseVisualStyleBackColor = true;
            this.btnRename.Click += new System.EventHandler(this.btnRename_Click);
            // 
            // btnReRead
            // 
            this.btnReRead.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReRead.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReRead.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnReRead.Location = new System.Drawing.Point(347, 492);
            this.btnReRead.Name = "btnReRead";
            this.btnReRead.Size = new System.Drawing.Size(220, 50);
            this.btnReRead.TabIndex = 8;
            this.btnReRead.Text = "Перечитать метки";
            this.btnReRead.UseVisualStyleBackColor = true;
            this.btnReRead.Click += new System.EventHandler(this.btnReRead_Click);
            // 
            // titlePanel
            // 
            this.titlePanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.titlePanel.Location = new System.Drawing.Point(5, 105);
            this.titlePanel.Name = "titlePanel";
            this.titlePanel.Size = new System.Drawing.Size(905, 52);
            this.titlePanel.TabIndex = 9;
            // 
            // checkOut
            // 
            this.checkOut.AutoSize = true;
            this.checkOut.Location = new System.Drawing.Point(18, 12);
            this.checkOut.Name = "checkOut";
            this.checkOut.Size = new System.Drawing.Size(279, 17);
            this.checkOut.TabIndex = 11;
            this.checkOut.Text = "Исключать из нумерации виды за полем чертежа";
            this.checkOut.UseVisualStyleBackColor = true;
            this.checkOut.CheckedChanged += new System.EventHandler(this.checkOut_CheckedChanged);
            // 
            // checkColor
            // 
            this.checkColor.AutoSize = true;
            this.checkColor.Location = new System.Drawing.Point(18, 30);
            this.checkColor.Name = "checkColor";
            this.checkColor.Size = new System.Drawing.Size(131, 17);
            this.checkColor.TabIndex = 11;
            this.checkColor.Text = "Использовать цвета";
            this.checkColor.UseVisualStyleBackColor = true;
            this.checkColor.CheckedChanged += new System.EventHandler(this.checkColor_CheckedChanged);
            // 
            // btnAddSurf
            // 
            this.btnAddSurf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddSurf.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddSurf.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnAddSurf.Location = new System.Drawing.Point(5, 492);
            this.btnAddSurf.Name = "btnAddSurf";
            this.btnAddSurf.Size = new System.Drawing.Size(89, 50);
            this.btnAddSurf.TabIndex = 12;
            this.btnAddSurf.Text = "Добавить пов-ть";
            this.btnAddSurf.UseVisualStyleBackColor = true;
            this.btnAddSurf.Click += new System.EventHandler(this.btnAddSurf_Click);
            // 
            // btnDelSurf
            // 
            this.btnDelSurf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDelSurf.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelSurf.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnDelSurf.Location = new System.Drawing.Point(100, 492);
            this.btnDelSurf.Name = "btnDelSurf";
            this.btnDelSurf.Size = new System.Drawing.Size(79, 49);
            this.btnDelSurf.TabIndex = 13;
            this.btnDelSurf.Text = "Удалить пов-ть";
            this.btnDelSurf.UseVisualStyleBackColor = true;
            this.btnDelSurf.Click += new System.EventHandler(this.btnDelSurf_Click);
            // 
            // SetLabelsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(914, 553);
            this.Controls.Add(this.btnDelSurf);
            this.Controls.Add(this.btnAddSurf);
            this.Controls.Add(this.checkColor);
            this.Controls.Add(this.checkOut);
            this.Controls.Add(this.titlePanel);
            this.Controls.Add(this.btnReRead);
            this.Controls.Add(this.btnRename);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.viewsPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(930, 1024);
            this.MinimumSize = new System.Drawing.Size(930, 350);
            this.Name = "SetLabelsForm";
            this.Text = "SetLabels ";
            this.Load += new System.EventHandler(this.SetLabelsForm_Load);
            this.Shown += new System.EventHandler(this.SetLabelsForm_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnExcludeChange;
        private System.Windows.Forms.TextBox txtExlude;
        private System.Windows.Forms.FlowLayoutPanel titlePanel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnRename;
        private System.Windows.Forms.Button btnReRead;
        private System.Windows.Forms.FlowLayoutPanel viewsPanel;
        private System.Windows.Forms.TextBox lbInfo;
        private System.Windows.Forms.CheckBox checkOut;
        private System.Windows.Forms.CheckBox checkColor;
        private System.Windows.Forms.Button btnAddSurf;
        private System.Windows.Forms.Button btnDelSurf;
    }
}

