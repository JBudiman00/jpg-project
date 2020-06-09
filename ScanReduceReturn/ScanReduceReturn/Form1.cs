﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScanReduceReturn
{
    public partial class Form1 : Form
    {
        bool isDown = false;
        bool hasImage = false;
        int initialX;
        int initialY;
        int finalX;
        int finalY;
        Rectangle croppedRect;
        public static string file;
        Image OrgImage;
        Image croppedImage;

        private float TopBarGrayHeight { get; set; }
        private float LeftBarGrayHeight { get; set; }
        private bool UseXScale { get; set; }
        private float PbX { get; set; }
        private float PbY { get; set; }

        public Form1()
        {
            InitializeComponent();
        }
        private void btnInsertFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Title = "Select A File";
            openDialog.Filter = "Image Files (*.png;*.jpg)|*.png;*.jpg" + "|" +
                                "All Files (*.*)|*.*";
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                file = openDialog.FileName;
                OrgImage = Image.FromFile(file);
                pbView.Image = OrgImage;
                //pbView.SizeMode = PictureBoxSizeMode.StretchImage;
                hasImage = true;
            }
        }
        private void btnCrop_Click(object sender, EventArgs e)  //Assumption: All images are square/rectangular
        {
            if (hasImage == true && finalX < pbView.Width && finalY < pbView.Height)   //Add safeguarding against cropping image from actual image size
            {
                croppedImage = getImageRect();

                /*int count = 0, xS = -1, yS = 0, xE = 0, yE = 0;  //Represent starting and ending dialgonals of image
                Bitmap CroppedBitMap = new Bitmap(croppedImage);
                Color white = new Color();
                white = Color.FromArgb(255, 255, 255);
                for (int x = 0; x < CroppedBitMap.Width; x++)   //Change, account for zoom factor
                {
                    for (int y = 0; y < CroppedBitMap.Height; y++)
                    {
                        if (CroppedBitMap.GetPixel(x, y) != white && count == 0)
                        {
                            xS = x;
                            yS = y;
                            count++;
                        }

                        if (CroppedBitMap.GetPixel(x, y) == white && count == 1)
                        {
                            xE = x;
                            yE = y;
                        }
                    }
                }*/

                //Create a new Cropped image containing only the actual image, no white space
                //if (yE != 0)
                {
                    //lblTest.Text = "Here's the cropped image";
                    //Rectangle temp = new Rectangle(xS, yS, xE, yE);
                    //Image completeImage = cropImage(croppedImage, temp);
                    //float scaleFactorX = pbView.ClientSize.Width / pbView.Image.Size.Width;
                    //float scaleFactorY = pbView.ClientSize.Height / pbView.Image.Size.Height;
                    pbCropped.Image = croppedImage;

                }
                //else
                //lblTest.Text = "Error, Retry cropping";
            }
        }

        private void pbView_MouseDown(object sender, MouseEventArgs e)
        {
            lblTest.Text = "";
            isDown = true;
            initialX = e.X;
            initialY = e.Y;
        }

        private void pbView_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDown == true)
            {
                this.Refresh();
                Pen drawPen = new Pen(Color.Navy, 1);
                int width = e.X - initialX, height = e.Y - initialY;

                //Rectangle rect = new Rectangle(initialPt.X, initialPt.Y, Cursor.Position.X - initialPt.X, Cursor.Position.Y - initialPt.Y);
                Rectangle rect = new Rectangle(Math.Min(e.X, initialX), Math.Min(e.X, initialY), Math.Abs(width), Math.Abs(height));
                pbView.CreateGraphics().DrawRectangle(drawPen, rect);

            }
        }

        private void pbView_MouseUp(object sender, MouseEventArgs e)
        {
            isDown = false;
            finalX = e.X;
            finalY = e.Y;
        }

        private static Image cropImage(Image img, Rectangle cropArea)
        {
            Bitmap bmpImage = new Bitmap(img);
            Bitmap bmpCrop = bmpImage.Clone(cropArea, bmpImage.PixelFormat);
            return (Image)(bmpCrop);
        }

        private Image getImageRect()
        {
            findBars();

            int pbWidth = pbView.Width;
            int pbHeight = pbView.Height;

            int imageHeight = OrgImage.Height;
            int imageWidth = OrgImage.Width;

            float xScale = imageWidth / (float)pbWidth;
            float yScale = imageHeight / (float)pbHeight;

            var scale = 0F;
            if (UseXScale == true)
                scale = xScale;
            else
                scale = yScale;

            PbX = (initialX - LeftBarGrayHeight) * scale;
            PbY = (initialY - TopBarGrayHeight) * scale;

            float PbFX = (finalX - LeftBarGrayHeight) * scale;
            float PbFY = (finalY - TopBarGrayHeight) * scale;

            lblTest.Text = string.Format("{0} {1} {2} {3}", PbX, PbY, PbFX, PbFY);

            croppedRect = new Rectangle((int)PbX, (int)PbY, (int)PbFX - (int)PbX, (int)PbFY - (int)PbY);

            return cropImage(OrgImage, croppedRect);

        }

        private void findBars()
        {
            int pbWidth = pbView.Width;
            int pbHeight = pbView.Height;

            int imageHeight = OrgImage.Height;
            int imageWidth = OrgImage.Width;

            var imageAR = imageWidth / (float)imageHeight;
            var pbAR = pbWidth / (float)pbHeight;

            if (imageAR > pbAR)
            {
                LeftBarGrayHeight = 0;
                // Bars on T & B
                var xScale = pbWidth / (float)imageWidth;
                var imageHeightInPictureBox = xScale * imageHeight;
                var totalHeightOfGrayBars = pbHeight - imageHeightInPictureBox;
                TopBarGrayHeight = totalHeightOfGrayBars / 2;

                UseXScale = true;
            }
            else
            {
                TopBarGrayHeight = 0;
                // Bars on L & R
                var yScale = pbHeight / (float)imageHeight;
                var imageWidthInPictureBox = yScale * imageWidth;
                var totalWidthOfGrayBars = pbWidth - imageWidthInPictureBox;
                LeftBarGrayHeight = totalWidthOfGrayBars / 2;

                UseXScale = false;
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {

        }
    }
}
