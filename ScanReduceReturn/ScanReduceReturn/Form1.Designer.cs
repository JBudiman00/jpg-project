﻿namespace ScanReduceReturn
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
            this.pbCropped = new System.Windows.Forms.PictureBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCropped)).BeginInit();
            this.SuspendLayout();
            // 
            // pbView
            // 
            this.pbView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbView.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.pbView.Location = new System.Drawing.Point(12, 12);
            this.pbView.Name = "pbView";
            this.pbView.Size = new System.Drawing.Size(819, 800);
            this.pbView.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbView.TabIndex = 0;
            this.pbView.TabStop = false;
            this.pbView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbView_MouseDown);
            this.pbView.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbView_MouseMove);
            this.pbView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbView_MouseUp);
            // 
            // btnInsertFile
            // 
            this.btnInsertFile.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInsertFile.Location = new System.Drawing.Point(918, 118);
            this.btnInsertFile.Name = "btnInsertFile";
            this.btnInsertFile.Size = new System.Drawing.Size(75, 23);
            this.btnInsertFile.TabIndex = 1;
            this.btnInsertFile.Text = "Insert File";
            this.btnInsertFile.UseVisualStyleBackColor = true;
            this.btnInsertFile.Click += new System.EventHandler(this.btnInsertFile_Click);
            // 
            // btnCrop
            // 
            this.btnCrop.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCrop.Location = new System.Drawing.Point(918, 180);
            this.btnCrop.Name = "btnCrop";
            this.btnCrop.Size = new System.Drawing.Size(75, 23);
            this.btnCrop.TabIndex = 0;
            this.btnCrop.Text = "Crop Image";
            this.btnCrop.Click += new System.EventHandler(this.btnCrop_Click);
            // 
            // lblTest
            // 
            this.lblTest.AutoSize = true;
            this.lblTest.Location = new System.Drawing.Point(1064, 290);
            this.lblTest.Name = "lblTest";
            this.lblTest.Size = new System.Drawing.Size(0, 13);
            this.lblTest.TabIndex = 2;
            // 
            // pbCropped
            // 
            this.pbCropped.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbCropped.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.pbCropped.Location = new System.Drawing.Point(865, 335);
            this.pbCropped.Name = "pbCropped";
            this.pbCropped.Size = new System.Drawing.Size(474, 477);
            this.pbCropped.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbCropped.TabIndex = 3;
            this.pbCropped.TabStop = false;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(918, 254);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1064, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "SCANREDUCERETURN";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(1214, 254);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 6;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1367, 845);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCrop);
            this.Controls.Add(this.btnInsertFile);
            this.Controls.Add(this.pbView);
            this.Controls.Add(this.pbCropped);
            this.Controls.Add(this.lblTest);
            this.Name = "Form1";
            this.Text = "ScReRe";
            ((System.ComponentModel.ISupportInitialize)(this.pbView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCropped)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbView;
        private System.Windows.Forms.Button btnInsertFile;
        private System.Windows.Forms.Button btnCrop;
        private System.Windows.Forms.Label lblTest;
        private System.Windows.Forms.PictureBox pbCropped;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnClear;
    }
}

