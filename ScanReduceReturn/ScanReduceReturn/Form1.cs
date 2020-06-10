using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace ScanReduceReturn
{
    public partial class Form1 : Form
    {
        bool isDown = false;
        bool hasImage = false;  //Prevents cropping b4 there is an image
        bool hasRect = false;   //Prevents cropping b4 there is a rectangle
        bool hasCropped = false;    //Prevent saving before there is a cropped image
        int counter = 0;
        int initialX;
        int initialY;
        int finalX;
        int finalY;
        Rectangle croppedRect;
        public static string file;
        Image OrgImage;
        Image croppedImage;
        List<string> fName = new List<string>();
        string saveFile;

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
            openDialog.Multiselect = true;
            openDialog.Title = "My Image Browser";

            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (string f in openDialog.FileNames)
                {
                    this.cb.Items.Add(System.IO.Path.GetFileName(f));
                    fName.Add(f);
                }
            }
        }
        private void btnCrop_Click(object sender, EventArgs e)  //Assumption: All images are square/rectangular
        {
            if (hasImage == true && finalX < pbView.Width && finalY < pbView.Height && hasRect == true)   //Add safeguarding against cropping image from actual image size
            {
                croppedImage = getImageRect();

                Color tempColor = new Color();

                int xS = -1, yS = 0, xE = 0, yE = 0;  //Represent starting and ending dialgonals of image
                Bitmap CroppedBitMap = new Bitmap(croppedImage);

                int BitMapHeight = CroppedBitMap.Height / 4;
                int tempCount = 0;

                for (int x = 0; x < CroppedBitMap.Width; x+=2)  
                {
                    tempCount = 0;
                    for (int y = 1; y < 4; y++)
                    {
                        tempColor = CroppedBitMap.GetPixel(x, BitMapHeight * y);
                        if (tempColor.R < 245 && tempColor.G < 245 && tempColor.B < 245)
                        {
                            tempCount++;
                        }
                    }
                    if (tempCount == 3)
                    {
                        xS = x;
                        break;
                    }
                }

                for (int x = CroppedBitMap.Width - 1; x > 0 ; x--)
                {
                    tempCount = 0;
                    for (int y = 1; y < 4; y++)
                    {
                        tempColor = CroppedBitMap.GetPixel(x, BitMapHeight * y);
                        if (tempColor.R < 245 && tempColor.G < 245 && tempColor.B < 245)
                        {
                            tempCount++;
                        }
                    }
                    if (tempCount == 3)
                    {
                        xE = x;
                        break;
                    }
                }

                int BitMapWidth = CroppedBitMap.Width / 4;

                for (int y = 0; y < CroppedBitMap.Height; y++)
                {
                    tempCount = 0;
                    for (int x = 1; x < 4; x++)
                    {
                        tempColor = CroppedBitMap.GetPixel(BitMapWidth * x, y);
                        if (tempColor.R < 245 && tempColor.G < 245 && tempColor.B < 245)
                        {
                            tempCount++;
                        }
                    }
                    if (tempCount == 3)
                    {
                        yS = y;
                        break;
                    }
                }

                for (int y = CroppedBitMap.Height - 1; y > 0; y--)
                {
                    tempCount = 0;
                    for (int x = 1; x < 4; x++)
                    {
                        tempColor = CroppedBitMap.GetPixel(BitMapWidth * x, y);
                        if (tempColor.R < 245 && tempColor.G < 245 && tempColor.B < 245)
                        {
                            tempCount++;
                        }
                    }
                    if (tempCount == 3)
                    {
                        yE = y;
                        break;
                    }
                }

                //Create a new Cropped image containing only the actual image, no white space
                if (yE != 0)
                {
                    lblTest.Text = "Here's the cropped image";
                    Rectangle temp = new Rectangle(xS, yS, xE - xS, yE - yS);
                    croppedImage = cropImage(croppedImage, temp);
                    pbCropped.Image = croppedImage;
                    hasCropped = true;
                }
                else
                    lblTest.Text = "Error, Retry cropping";
                hasRect = false;
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
            hasRect = true;
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
            if (hasCropped == true)
            {
                string fileN = counter.ToString() + ".png";
                string imagePath = saveFile + fileN; 
                croppedImage.Save(imagePath, ImageFormat.Png);
                hasCropped = false;

                lblImageSave.Text = "Image saved";  //Set to change later
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            pbCropped.Image = null;
            lblTest.Text = "";
        }

        private void cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = this.cb.GetItemText(this.cb.SelectedItem);
            foreach (string fileName in fName)
            {
                if (System.IO.Path.GetFileName(fileName).Equals(selected))
                {
                    file = fileName;
                    OrgImage = Image.FromFile(file);
                    pbView.Image = OrgImage;
                    hasImage = true;
                }
            }
        }

        private void btnSaveToFile_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
                saveFile = dialog.SelectedPath;
            lblS.Text = saveFile;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (fName != null)  //Also works when no files are set or selected
            {
                bool next = false;
                foreach (string fileName in fName)
                {
                    if (next == true)
                    {
                        file = fileName;
                        OrgImage = Image.FromFile(file);
                        pbView.Image = OrgImage;
                        hasImage = true;
                        cb.SelectedIndex = cb.FindString(System.IO.Path.GetFileName(fileName));
                        break;
                    }

                    if (fileName.Equals(file))
                    {
                        next = true;
                    }
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (fName != null)  //Also works when no files are set or selected
            {
                bool before = false;
                for (int x = fName.Count - 1; x >= 0; x--)
                {
                    if (before == true)
                    {
                        file = fName[x];
                        OrgImage = Image.FromFile(file);
                        pbView.Image = OrgImage;
                        hasImage = true;
                        cb.SelectedIndex = cb.FindString(System.IO.Path.GetFileName(fName[x]));
                        break;
                    }

                    if (fName[x].Equals(file))
                    {
                        before = true;
                    }
                }
            }
        }
    }
}
