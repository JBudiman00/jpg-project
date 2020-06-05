namespace ScanReduceReturn
{
    partial class Form1
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
            this.pbView = new System.Windows.Forms.PictureBox();
            this.btnInsertFile = new System.Windows.Forms.Button();
            this.btnCrop = new System.Windows.Forms.Button();
            this.lblTest = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbView)).BeginInit();
            this.SuspendLayout();
            // 
            // pbView
            // 
            this.pbView.Location = new System.Drawing.Point(12, 12);
            this.pbView.Name = "pbView";
            this.pbView.Size = new System.Drawing.Size(836, 558);
            this.pbView.TabIndex = 0;
            this.pbView.TabStop = false;
            this.pbView.Click += new System.EventHandler(this.pbView_Click);
            this.pbView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbView_MouseDown);
            this.pbView.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbView_MouseMove);
            this.pbView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbView_MouseUp);
            // 
            // btnInsertFile
            // 
            this.btnInsertFile.Location = new System.Drawing.Point(891, 12);
            this.btnInsertFile.Name = "btnInsertFile";
            this.btnInsertFile.Size = new System.Drawing.Size(75, 23);
            this.btnInsertFile.TabIndex = 1;
            this.btnInsertFile.Text = "Insert File";
            this.btnInsertFile.UseVisualStyleBackColor = true;
            this.btnInsertFile.Click += new System.EventHandler(this.btnInsertFile_Click);
            // 
            // btnCrop
            // 
            this.btnCrop.Location = new System.Drawing.Point(891, 57);
            this.btnCrop.Name = "btnCrop";
            this.btnCrop.Size = new System.Drawing.Size(75, 23);
            this.btnCrop.TabIndex = 0;
            this.btnCrop.Text = "Crop Image";
            this.btnCrop.Click += new System.EventHandler(this.btnCrop_Click);
            // 
            // lblTest
            // 
            this.lblTest.AutoSize = true;
            this.lblTest.Location = new System.Drawing.Point(913, 224);
            this.lblTest.Name = "lblTest";
            this.lblTest.Size = new System.Drawing.Size(0, 13);
            this.lblTest.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1061, 582);
            this.Controls.Add(this.lblTest);
            this.Controls.Add(this.btnCrop);
            this.Controls.Add(this.btnInsertFile);
            this.Controls.Add(this.pbView);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pbView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbView;
        private System.Windows.Forms.Button btnInsertFile;
        private System.Windows.Forms.Button btnCrop;
        private System.Windows.Forms.Label lblTest;
    }
}

