namespace GhostReservation
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.storeIdBox = new System.Windows.Forms.TextBox();
            this.articleIDBox = new System.Windows.Forms.TextBox();
            this.resultBox = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SupplierArticleIDBox = new System.Windows.Forms.TextBox();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // storeIdBox
            // 
            this.storeIdBox.Location = new System.Drawing.Point(279, 118);
            this.storeIdBox.Margin = new System.Windows.Forms.Padding(4);
            this.storeIdBox.Name = "storeIdBox";
            this.storeIdBox.Size = new System.Drawing.Size(94, 27);
            this.storeIdBox.TabIndex = 0;
            // 
            // articleIDBox
            // 
            this.articleIDBox.Location = new System.Drawing.Point(279, 194);
            this.articleIDBox.Margin = new System.Windows.Forms.Padding(4);
            this.articleIDBox.Name = "articleIDBox";
            this.articleIDBox.Size = new System.Drawing.Size(253, 27);
            this.articleIDBox.TabIndex = 1;
            // 
            // resultBox
            // 
            this.resultBox.Location = new System.Drawing.Point(143, 322);
            this.resultBox.Margin = new System.Windows.Forms.Padding(4);
            this.resultBox.Multiline = true;
            this.resultBox.Name = "resultBox";
            this.resultBox.Size = new System.Drawing.Size(717, 440);
            this.resultBox.TabIndex = 2;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(279, 75);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(107, 29);
            this.btnSearch.TabIndex = 3;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click_1);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(470, 75);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(63, 29);
            this.button1.TabIndex = 4;
            this.button1.Text = "Clear";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(726, 285);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(136, 29);
            this.button2.TabIndex = 5;
            this.button2.Text = "Copy (ctrl + c)";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // SupplierArticleIDBox
            // 
            this.SupplierArticleIDBox.Location = new System.Drawing.Point(279, 157);
            this.SupplierArticleIDBox.Margin = new System.Windows.Forms.Padding(4);
            this.SupplierArticleIDBox.Name = "SupplierArticleIDBox";
            this.SupplierArticleIDBox.Size = new System.Drawing.Size(253, 27);
            this.SupplierArticleIDBox.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(196, 121);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 19);
            this.label1.TabIndex = 7;
            this.label1.Text = "Store ID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(113, 160);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(158, 19);
            this.label2.TabIndex = 8;
            this.label2.Text = "Supplier Article ID";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(186, 197);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 19);
            this.label3.TabIndex = 9;
            this.label3.Text = "Article ID";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1154, 874);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SupplierArticleIDBox);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.resultBox);
            this.Controls.Add(this.articleIDBox);
            this.Controls.Add(this.storeIdBox);
            this.Font = new System.Drawing.Font("Roboto Medium", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "Ghost Reservation";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox storeIdBox;
        private TextBox articleIDBox;
        private TextBox resultBox;
        private Button btnSearch;
        private Button button1;
        private Button button2;
        private TextBox SupplierArticleIDBox;
        private FontDialog fontDialog1;
        private Label label1;
        private Label label2;
        private Label label3;
    }
}