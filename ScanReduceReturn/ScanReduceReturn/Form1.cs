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
        bool Hprecision = false;
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

        string corner = "";
        ToolTip t1 = new ToolTip();

        private float TopBarGrayHeight { get; set; }
        private float LeftBarGrayHeight { get; set; }
        private bool UseXScale { get; set; }
        private float PbX { get; set; }
        private float PbY { get; set; }

        public Form1()
        {
            t1.ShowAlways = true;
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
            lblImageSave.Text = "";
            if (hasImage == true && finalX < pbView.Width && finalY < pbView.Height && hasRect == true)   //Add safeguarding against cropping image from actual image size
            {
                croppedImage = getImageRect();

                Color tempColor = new Color();

                int xS = -1, yS = 0, xE = 0, yE = 0;  //Represent starting and ending dialgonals of image
                Bitmap CroppedBitMap = new Bitmap(croppedImage);

                int tempCount = 0;

                int checkCount;   //Total pixel checks
                int pixelRGB = 240;     //RGB pixel precision 
                int countPass;

                if (Hprecision == true)
                {
                    checkCount = 100;
                    countPass = (int)(checkCount * 0.75);
                }
                else { 
                    checkCount = 8;
                    countPass = checkCount - 1;
                }
                    

                

                int BitMapHeight = CroppedBitMap.Height / checkCount;

                for (int x = 0; x < CroppedBitMap.Width; x+=2)      //Variability/precision of cropping
                {
                    tempCount = 0;
                    for (int y = 1; y < checkCount; y++)
                    {
                        tempColor = CroppedBitMap.GetPixel(x, BitMapHeight * y);
                        if (tempColor.R < pixelRGB && tempColor.G < pixelRGB && tempColor.B < pixelRGB)
                        {
                            tempCount++;
                        }
                    }
                    if (tempCount >= countPass)
                    {
                        xS = x;
                        break;
                    }
                }

                for (int x = CroppedBitMap.Width - 1; x > 0 ; x--)
                {
                    tempCount = 0;
                    for (int y = 1; y < checkCount; y++)
                    {
                        tempColor = CroppedBitMap.GetPixel(x, BitMapHeight * y);
                        if (tempColor.R < pixelRGB && tempColor.G < pixelRGB && tempColor.B < pixelRGB)
                        {
                            tempCount++;
                        }
                    }
                    if (tempCount >= countPass)
                    {
                        xE = x;
                        break;
                    }
                }

                int BitMapWidth = CroppedBitMap.Width / checkCount;

                for (int y = 0; y < CroppedBitMap.Height; y++)
                {
                    tempCount = 0;
                    for (int x = 1; x < checkCount; x++)
                    {
                        tempColor = CroppedBitMap.GetPixel(BitMapWidth * x, y);
                        if (tempColor.R < pixelRGB && tempColor.G < pixelRGB && tempColor.B < pixelRGB)
                        {
                            tempCount++;
                        }
                    }
                    if (tempCount >= countPass)
                    {
                        yS = y;
                        break;
                    }
                }

                for (int y = CroppedBitMap.Height - 1; y > 0; y--)
                {
                    tempCount = 0;
                    for (int x = 1; x < checkCount; x++)
                    {
                        tempColor = CroppedBitMap.GetPixel(BitMapWidth * x, y);
                        if (tempColor.R < pixelRGB && tempColor.G < pixelRGB && tempColor.B < pixelRGB)
                        {
                            tempCount++;
                        }
                    }
                    if (tempCount >= countPass)
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
                int width = Math.Abs(e.X - initialX), height = Math.Abs(e.Y - initialY);

                //Rectangle rect = new Rectangle(initialPt.X, initialPt.Y, Cursor.Position.X - initialPt.X, Cursor.Position.Y - initialPt.Y);
                Rectangle rect = new Rectangle();
                //pbView.CreateGraphics().DrawRectangle(drawPen, rect);

                if (e.X > initialX && e.Y > initialY)   //Corner value is position of mouse at corner of rectangle
                {
                    rect = new Rectangle(initialX, initialY, width, height);
                    corner = "bottomright";
                }
                else if (e.X > initialX && e.Y < initialY)
                {
                    rect = new Rectangle(initialX, initialY - height, width, height);
                    corner = "topright";
                }

                else if (e.X < initialX && e.Y < initialY)
                {
                    rect = new Rectangle(initialX - width, initialY - height, width, height);
                    corner = "topleft";
                }
                else if (e.X < initialX && e.Y > initialY)
                {
                    rect = new Rectangle(initialX - width, initialY, width, height);
                    corner = "bottomleft";
                }

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

            float scale;
            if (UseXScale == true)
                scale = xScale;
            else
                scale = yScale;

            PbX = (initialX - LeftBarGrayHeight) * scale;
            PbY = (initialY - TopBarGrayHeight) * scale;

            float PbFX = (finalX - LeftBarGrayHeight) * scale;
            float PbFY = (finalY - TopBarGrayHeight) * scale;

            int xI = (int)PbX;
            int yI = (int)PbY;
            int xF = (int)PbFX;
            int yF = (int)PbFY;

            int w = Math.Abs(xI - xF);
            int h = Math.Abs(yI - yF);

            if (corner.Equals("bottomright"))
                croppedRect = new Rectangle(xI, yI, w, h);
            else if (corner.Equals("topright"))
                croppedRect = new Rectangle(xI, yI - h, w, h);
            else if (corner.Equals("bottomleft"))
                croppedRect = new Rectangle(xI - w, yI, w, h);
            else if (corner.Equals("topleft"))
                croppedRect = new Rectangle(xI - w, yI - h, w, h);

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
                string fileN = "OldPics" + counter.ToString() + ".png";
                string imagePath = saveFile + fileN;
                croppedImage.Save(imagePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                hasCropped = false;
                lblImageSave.Text = "Image saved";  //Set to change later
                counter++;
                lblCounter.Text = string.Format("Counter: {0}", counter);
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
            lblImageSave.Text = "";
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
            lblImageSave.Text = "";
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            if (tb.Text != null)
                lblCounter.Text = string.Format("Counter: {0}", tb.Text);
            counter = Int32.Parse(tb.Text);
        }

        private void btnPrecision_Click(object sender, EventArgs e)
        {
            if (Hprecision == false)
            {
                btnPrecision.Text = "Low Precision";
                Hprecision = true;
            }
            else
            {
                btnPrecision.Text = "High Precision";
                Hprecision = false;
            }
        }

        private void btnPrecision_Hover(object sender, EventArgs e)
        {
            t1.SetToolTip(btnPrecision, "The precision button determines the amount of " +
                "\nerror allowed between crops.\n" +
                "The higher the precision, the more accurate the image \nand the smaller the amount of user crop error allowed");
        }
    }
}
