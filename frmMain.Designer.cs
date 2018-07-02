namespace MyTotalCommander
{
    partial class frmMain
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
            this.lbRight = new System.Windows.Forms.Label();
            this.cbRight = new System.Windows.Forms.ComboBox();
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvLeft = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvRight = new System.Windows.Forms.ListView();
            this.lbLeft = new System.Windows.Forms.Label();
            this.cbLeft = new System.Windows.Forms.ComboBox();
            this.splitBot = new System.Windows.Forms.SplitContainer();
            this.rtbQuestion = new System.Windows.Forms.RichTextBox();
            this.splitTotalCommander = new System.Windows.Forms.SplitContainer();
            this.tbAnswer = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitBot)).BeginInit();
            this.splitBot.Panel1.SuspendLayout();
            this.splitBot.Panel2.SuspendLayout();
            this.splitBot.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitTotalCommander)).BeginInit();
            this.splitTotalCommander.Panel1.SuspendLayout();
            this.splitTotalCommander.Panel2.SuspendLayout();
            this.splitTotalCommander.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbRight
            // 
            this.lbRight.AutoSize = true;
            this.lbRight.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbRight.ForeColor = System.Drawing.Color.Blue;
            this.lbRight.Location = new System.Drawing.Point(10, 22);
            this.lbRight.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbRight.Name = "lbRight";
            this.lbRight.Size = new System.Drawing.Size(0, 17);
            this.lbRight.TabIndex = 6;
            // 
            // cbRight
            // 
            this.cbRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbRight.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbRight.ForeColor = System.Drawing.Color.Red;
            this.cbRight.FormattingEnabled = true;
            this.cbRight.Location = new System.Drawing.Point(0, 0);
            this.cbRight.Margin = new System.Windows.Forms.Padding(2);
            this.cbRight.Name = "cbRight";
            this.cbRight.Size = new System.Drawing.Size(254, 21);
            this.cbRight.TabIndex = 5;
            this.cbRight.SelectedIndexChanged += new System.EventHandler(this.cbRight_SelectedIndexChanged);
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Size";
            this.columnHeader6.Width = 100;
            // 
            // lvLeft
            // 
            this.lvLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvLeft.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader5});
            this.lvLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvLeft.Location = new System.Drawing.Point(2, 41);
            this.lvLeft.Margin = new System.Windows.Forms.Padding(2);
            this.lvLeft.Name = "lvLeft";
            this.lvLeft.Size = new System.Drawing.Size(218, 228);
            this.lvLeft.TabIndex = 4;
            this.lvLeft.UseCompatibleStateImageBehavior = false;
            this.lvLeft.View = System.Windows.Forms.View.Details;
            this.lvLeft.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvLeft_MouseDoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 300;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Type";
            this.columnHeader2.Width = 80;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Size";
            this.columnHeader5.Width = 100;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Type";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Name";
            this.columnHeader3.Width = 300;
            // 
            // lvRight
            // 
            this.lvRight.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvRight.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader6});
            this.lvRight.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvRight.Location = new System.Drawing.Point(0, 41);
            this.lvRight.Margin = new System.Windows.Forms.Padding(2);
            this.lvRight.Name = "lvRight";
            this.lvRight.Size = new System.Drawing.Size(253, 228);
            this.lvRight.TabIndex = 7;
            this.lvRight.UseCompatibleStateImageBehavior = false;
            this.lvRight.View = System.Windows.Forms.View.Details;
            this.lvRight.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvRight_MouseDoubleClick);
            // 
            // lbLeft
            // 
            this.lbLeft.AutoSize = true;
            this.lbLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLeft.ForeColor = System.Drawing.Color.Blue;
            this.lbLeft.Location = new System.Drawing.Point(9, 22);
            this.lbLeft.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbLeft.Name = "lbLeft";
            this.lbLeft.Size = new System.Drawing.Size(0, 17);
            this.lbLeft.TabIndex = 3;
            // 
            // cbLeft
            // 
            this.cbLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbLeft.ForeColor = System.Drawing.Color.Red;
            this.cbLeft.FormattingEnabled = true;
            this.cbLeft.Location = new System.Drawing.Point(0, 0);
            this.cbLeft.Margin = new System.Windows.Forms.Padding(2);
            this.cbLeft.Name = "cbLeft";
            this.cbLeft.Size = new System.Drawing.Size(221, 21);
            this.cbLeft.TabIndex = 2;
            this.cbLeft.SelectedIndexChanged += new System.EventHandler(this.cbLeft_SelectedIndexChanged);
            // 
            // splitBot
            // 
            this.splitBot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitBot.Location = new System.Drawing.Point(0, 0);
            this.splitBot.Margin = new System.Windows.Forms.Padding(2);
            this.splitBot.Name = "splitBot";
            // 
            // splitBot.Panel1
            // 
            this.splitBot.Panel1.Controls.Add(this.lvLeft);
            this.splitBot.Panel1.Controls.Add(this.lbLeft);
            this.splitBot.Panel1.Controls.Add(this.cbLeft);
            // 
            // splitBot.Panel2
            // 
            this.splitBot.Panel2.Controls.Add(this.lvRight);
            this.splitBot.Panel2.Controls.Add(this.lbRight);
            this.splitBot.Panel2.Controls.Add(this.cbRight);
            this.splitBot.Size = new System.Drawing.Size(478, 268);
            this.splitBot.SplitterDistance = 221;
            this.splitBot.SplitterWidth = 3;
            this.splitBot.TabIndex = 0;
            // 
            // rtbQuestion
            // 
            this.rtbQuestion.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbQuestion.Location = new System.Drawing.Point(0, 2);
            this.rtbQuestion.Margin = new System.Windows.Forms.Padding(2);
            this.rtbQuestion.Name = "rtbQuestion";
            this.rtbQuestion.Size = new System.Drawing.Size(474, 48);
            this.rtbQuestion.TabIndex = 2;
            this.rtbQuestion.Text = "";
            // 
            // splitTotalCommander
            // 
            this.splitTotalCommander.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitTotalCommander.Location = new System.Drawing.Point(0, 0);
            this.splitTotalCommander.Margin = new System.Windows.Forms.Padding(2);
            this.splitTotalCommander.Name = "splitTotalCommander";
            this.splitTotalCommander.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitTotalCommander.Panel1
            // 
            this.splitTotalCommander.Panel1.Controls.Add(this.tbAnswer);
            this.splitTotalCommander.Panel1.Controls.Add(this.rtbQuestion);
            // 
            // splitTotalCommander.Panel2
            // 
            this.splitTotalCommander.Panel2.Controls.Add(this.splitBot);
            this.splitTotalCommander.Size = new System.Drawing.Size(478, 349);
            this.splitTotalCommander.SplitterDistance = 78;
            this.splitTotalCommander.SplitterWidth = 3;
            this.splitTotalCommander.TabIndex = 1;
            // 
            // tbAnswer
            // 
            this.tbAnswer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbAnswer.Location = new System.Drawing.Point(3, 54);
            this.tbAnswer.Margin = new System.Windows.Forms.Padding(2);
            this.tbAnswer.Name = "tbAnswer";
            this.tbAnswer.Size = new System.Drawing.Size(474, 20);
            this.tbAnswer.TabIndex = 3;
            this.tbAnswer.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbAnswer_KeyUp);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 349);
            this.Controls.Add(this.splitTotalCommander);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmMain";
            this.Text = "My Total Commander";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.splitBot.Panel1.ResumeLayout(false);
            this.splitBot.Panel1.PerformLayout();
            this.splitBot.Panel2.ResumeLayout(false);
            this.splitBot.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitBot)).EndInit();
            this.splitBot.ResumeLayout(false);
            this.splitTotalCommander.Panel1.ResumeLayout(false);
            this.splitTotalCommander.Panel1.PerformLayout();
            this.splitTotalCommander.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitTotalCommander)).EndInit();
            this.splitTotalCommander.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lbRight;
        private System.Windows.Forms.ComboBox cbRight;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ListView lvLeft;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ListView lvRight;
        private System.Windows.Forms.Label lbLeft;
        private System.Windows.Forms.ComboBox cbLeft;
        private System.Windows.Forms.SplitContainer splitBot;
        private System.Windows.Forms.RichTextBox rtbQuestion;
        private System.Windows.Forms.SplitContainer splitTotalCommander;
        private System.Windows.Forms.TextBox tbAnswer;
    }
}

