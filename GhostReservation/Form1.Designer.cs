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
            this.SuspendLayout();
            // 
            // storeIdBox
            // 
            this.storeIdBox.Location = new System.Drawing.Point(100, 64);
            this.storeIdBox.Name = "storeIdBox";
            this.storeIdBox.Size = new System.Drawing.Size(67, 23);
            this.storeIdBox.TabIndex = 0;
            // 
            // articleIDBox
            // 
            this.articleIDBox.Location = new System.Drawing.Point(100, 93);
            this.articleIDBox.Name = "articleIDBox";
            this.articleIDBox.Size = new System.Drawing.Size(178, 23);
            this.articleIDBox.TabIndex = 1;
            // 
            // resultBox
            // 
            this.resultBox.Location = new System.Drawing.Point(100, 122);
            this.resultBox.Multiline = true;
            this.resultBox.Name = "resultBox";
            this.resultBox.Size = new System.Drawing.Size(503, 275);
            this.resultBox.TabIndex = 2;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(100, 35);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 3;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click_1);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(212, 35);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(44, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Clear";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(508, 92);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(95, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "Copy (ctrl + c)";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.resultBox);
            this.Controls.Add(this.articleIDBox);
            this.Controls.Add(this.storeIdBox);
            this.Name = "Form1";
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
    }
}