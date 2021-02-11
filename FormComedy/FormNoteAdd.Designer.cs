namespace FormComedia
{
    partial class FormNoteAdd
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
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.textBoxBook = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxEnd = new System.Windows.Forms.TextBox();
            this.labelEnd = new System.Windows.Forms.Label();
            this.textBoxStart = new System.Windows.Forms.TextBox();
            this.labelStart = new System.Windows.Forms.Label();
            this.textBoxCanto = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxTerm = new System.Windows.Forms.CheckBox();
            this.textBoxCommentary = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.comboBoxTermItemType = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxName
            // 
            this.textBoxName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxName.Location = new System.Drawing.Point(3, 3);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(162, 20);
            this.textBoxName.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(171, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Add";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.textBoxBook, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.textBoxEnd, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.labelEnd, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.textBoxStart, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.labelStart, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.textBoxCanto, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(171, 38);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 24.99813F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.00062F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.00063F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.00063F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(132, 144);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // textBoxBook
            // 
            this.textBoxBook.Location = new System.Drawing.Point(69, 3);
            this.textBoxBook.Name = "textBoxBook";
            this.textBoxBook.Size = new System.Drawing.Size(34, 20);
            this.textBoxBook.TabIndex = 10;
            this.textBoxBook.Text = "1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Book";
            // 
            // textBoxEnd
            // 
            this.textBoxEnd.Location = new System.Drawing.Point(69, 110);
            this.textBoxEnd.Name = "textBoxEnd";
            this.textBoxEnd.Size = new System.Drawing.Size(34, 20);
            this.textBoxEnd.TabIndex = 8;
            this.textBoxEnd.Text = "9";
            // 
            // labelEnd
            // 
            this.labelEnd.AutoSize = true;
            this.labelEnd.Location = new System.Drawing.Point(3, 107);
            this.labelEnd.Name = "labelEnd";
            this.labelEnd.Size = new System.Drawing.Size(26, 13);
            this.labelEnd.TabIndex = 7;
            this.labelEnd.Text = "End";
            // 
            // textBoxStart
            // 
            this.textBoxStart.Location = new System.Drawing.Point(69, 74);
            this.textBoxStart.Name = "textBoxStart";
            this.textBoxStart.Size = new System.Drawing.Size(34, 20);
            this.textBoxStart.TabIndex = 7;
            this.textBoxStart.Text = "1";
            // 
            // labelStart
            // 
            this.labelStart.AutoSize = true;
            this.labelStart.Location = new System.Drawing.Point(3, 71);
            this.labelStart.Name = "labelStart";
            this.labelStart.Size = new System.Drawing.Size(29, 13);
            this.labelStart.TabIndex = 6;
            this.labelStart.Text = "Start";
            // 
            // textBoxCanto
            // 
            this.textBoxCanto.Location = new System.Drawing.Point(69, 38);
            this.textBoxCanto.Name = "textBoxCanto";
            this.textBoxCanto.Size = new System.Drawing.Size(34, 20);
            this.textBoxCanto.TabIndex = 4;
            this.textBoxCanto.Text = "1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Canto";
            // 
            // checkBoxTerm
            // 
            this.checkBoxTerm.AutoSize = true;
            this.checkBoxTerm.Checked = true;
            this.checkBoxTerm.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxTerm.Location = new System.Drawing.Point(3, 3);
            this.checkBoxTerm.Name = "checkBoxTerm";
            this.checkBoxTerm.Size = new System.Drawing.Size(66, 17);
            this.checkBoxTerm.TabIndex = 8;
            this.checkBoxTerm.Text = "Be Term";
            this.checkBoxTerm.UseVisualStyleBackColor = true;
            // 
            // textBoxCommentary
            // 
            this.textBoxCommentary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxCommentary.Location = new System.Drawing.Point(3, 38);
            this.textBoxCommentary.Multiline = true;
            this.textBoxCommentary.Name = "textBoxCommentary";
            this.textBoxCommentary.Size = new System.Drawing.Size(162, 183);
            this.textBoxCommentary.TabIndex = 9;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.textBoxName, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.textBoxCommentary, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.button1, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel1, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.panel1, 0, 3);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(306, 329);
            this.tableLayoutPanel2.TabIndex = 10;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.comboBoxTermItemType);
            this.panel1.Controls.Add(this.checkBoxTerm);
            this.panel1.Location = new System.Drawing.Point(3, 297);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(162, 29);
            this.panel1.TabIndex = 11;
            // 
            // comboBoxTermItemType
            // 
            this.comboBoxTermItemType.FormattingEnabled = true;
            this.comboBoxTermItemType.Items.AddRange(new object[] {
            "Item",
            "Place",
            "Person",
            "Poet",
            "Character",
            "Politician",
            "Poem"});
            this.comboBoxTermItemType.Location = new System.Drawing.Point(75, 0);
            this.comboBoxTermItemType.Name = "comboBoxTermItemType";
            this.comboBoxTermItemType.Size = new System.Drawing.Size(84, 21);
            this.comboBoxTermItemType.TabIndex = 9;
            // 
            // FormNoteAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(336, 408);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Name = "FormNoteAdd";
            this.Text = "FormNoteAdd";
            this.Load += new System.EventHandler(this.FormNoteAdd_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox textBoxBook;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxEnd;
        private System.Windows.Forms.Label labelEnd;
        private System.Windows.Forms.TextBox textBoxStart;
        private System.Windows.Forms.Label labelStart;
        private System.Windows.Forms.TextBox textBoxCanto;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBoxTerm;
        private System.Windows.Forms.TextBox textBoxCommentary;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox comboBoxTermItemType;
    }
}