namespace FormComedia
{
    partial class FormNotes
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
            this.components = new System.ComponentModel.Container();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.textBoxBook = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxEnd = new System.Windows.Forms.TextBox();
            this.labelEnd = new System.Windows.Forms.Label();
            this.textBoxStart = new System.Windows.Forms.TextBox();
            this.labelStart = new System.Windows.Forms.Label();
            this.textBoxCanto = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxCommentary = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.buttonRead = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonExport = new System.Windows.Forms.Button();
            this.buttonImport = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(3, 51);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(535, 155);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridView1_DataBindingComplete);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(233, 3);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(75, 23);
            this.buttonAdd.TabIndex = 1;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
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
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 6);
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
            // textBoxCommentary
            // 
            this.textBoxCommentary.Location = new System.Drawing.Point(289, 64);
            this.textBoxCommentary.Multiline = true;
            this.textBoxCommentary.Name = "textBoxCommentary";
            this.textBoxCommentary.Size = new System.Drawing.Size(132, 26);
            this.textBoxCommentary.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(286, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Commentary";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(141, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Name";
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(141, 64);
            this.textBoxName.Multiline = true;
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(132, 26);
            this.textBoxName.TabIndex = 10;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(314, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 12;
            this.button1.Text = "Update";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // buttonRead
            // 
            this.buttonRead.Location = new System.Drawing.Point(3, 3);
            this.buttonRead.Name = "buttonRead";
            this.buttonRead.Size = new System.Drawing.Size(75, 23);
            this.buttonRead.TabIndex = 13;
            this.buttonRead.Text = "Read";
            this.buttonRead.UseVisualStyleBackColor = true;
            this.buttonRead.Click += new System.EventHandler(this.buttonRead_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.panel1, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.dataGridView1, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(12, 27);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(541, 370);
            this.tableLayoutPanel2.TabIndex = 14;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonAdd);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.textBoxName);
            this.panel1.Controls.Add(this.textBoxCommentary);
            this.panel1.Location = new System.Drawing.Point(3, 212);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(432, 155);
            this.panel1.TabIndex = 15;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.buttonImport);
            this.panel2.Controls.Add(this.buttonExport);
            this.panel2.Controls.Add(this.buttonSave);
            this.panel2.Controls.Add(this.buttonRead);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(503, 42);
            this.panel2.TabIndex = 16;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(101, 3);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 14;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonExport
            // 
            this.buttonExport.Location = new System.Drawing.Point(198, 3);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(75, 23);
            this.buttonExport.TabIndex = 15;
            this.buttonExport.Text = "Export";
            this.buttonExport.UseVisualStyleBackColor = true;
            this.buttonExport.Click += new System.EventHandler(this.buttonExport_Click);
            // 
            // buttonImport
            // 
            this.buttonImport.Location = new System.Drawing.Point(299, 3);
            this.buttonImport.Name = "buttonImport";
            this.buttonImport.Size = new System.Drawing.Size(75, 23);
            this.buttonImport.TabIndex = 16;
            this.buttonImport.Text = "Import";
            this.buttonImport.UseVisualStyleBackColor = true;
            this.buttonImport.Click += new System.EventHandler(this.buttonImport_Click);
            // 
            // FormNotes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Name = "FormNotes";
            this.Text = "FormNotes";
            this.Load += new System.EventHandler(this.FormNotes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox textBoxBook;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxEnd;
        private System.Windows.Forms.Label labelEnd;
        private System.Windows.Forms.TextBox textBoxStart;
        private System.Windows.Forms.Label labelStart;
        private System.Windows.Forms.TextBox textBoxCanto;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxCommentary;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonRead;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonImport;
        private System.Windows.Forms.Button buttonExport;
    }
}