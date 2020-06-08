using System;
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
            //pbCropped.Image = croppedImage;
            
            int count = 0, xS = -1, yS = 0, xE = 0, yE = 0;  //Represent starting and ending dialgonals of image
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
            }

            //Create a new Cropped image containing only the actual image, no white space
            if (yE != 0)
            {
                lblTest.Text = "Testing";
                Rectangle temp = new Rectangle(xS, yS, xE, yE);
                Image completeImage = cropImage(croppedImage, temp);
                pbCropped.Image = completeImage;
            }
            else
                lblTest.Text = "Error, Retry cropping";
        }

        private void pbView_MouseDown(object sender, MouseEventArgs e)
        {
                isDown = true;
                initialX = e.X;
                initialY = e.Y;
        }

        private void pbView_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDown == true)
            {
                this.Refresh();
                Pen drwaPen = new Pen(Color.Navy, 1);
                int width = e.X - initialX, height = e.Y - initialY;
                //Rectangle rect = new Rectangle(initialPt.X, initialPt.Y, Cursor.Position.X - initialPt.X, Cursor.Position.Y - initialPt.Y);
                //croppedRect = new Rectangle(Math.Min(e.X, initialX), Math.Min(e.X, initialY), Math.Abs(width * Math.Sign(width)), Math.Abs(height * Math.Sign(height)));
                croppedRect = new Rectangle(Math.Min(e.X, initialX), Math.Min(e.X, initialY), Math.Abs(e.X - initialX), Math.Abs(e.Y - initialY));
                pbView.CreateGraphics().DrawRectangle(drwaPen, croppedRect);

                finalX = Math.Abs(e.X - initialX);
                finalY = Math.Abs(e.Y - initialY);
            }
        }

        private void pbView_MouseUp(object sender, MouseEventArgs e)
        {
            isDown = false;
            if (hasImage == true)   //Add safeguarding against cropping image from actual image size
            {
                lblTest.Text = finalX.ToString();
                //Rectangle rectTest = new Rectangle(5, 5, 200, 200);
                croppedImage = cropImage(OrgImage, croppedRect);
            }
        }

        private static Image cropImage(Image img, Rectangle cropArea)
        {
            Bitmap bmpImage = new Bitmap(img);
            Bitmap bmpCrop = bmpImage.Clone(cropArea, bmpImage.PixelFormat);
            return (Image)(bmpCrop);
        }

        private void pbCropped_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }
    }
}
