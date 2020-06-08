using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
        private Bitmap croppedBitMap;
        Graphics g;

        public Form1()
        {
            InitializeComponent();
        }

        private void pbView_Click(object sender, EventArgs e)
        {
            lblTest.Text = finalX.ToString();
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
        private void btnCrop_Click(object sender, EventArgs e)
        {
            pbCropped.Image = croppedImage;
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

                finalX = width * Math.Sign(width);
                finalY = height * Math.Sign(height);
            }
        }

        private void pbView_MouseUp(object sender, MouseEventArgs e)
        {
            isDown = false;
            if (hasImage == true)
            {
                croppedImage = cropImage(OrgImage, croppedRect);
            }
        }

        private static Image cropImage(Image img, Rectangle cropArea)
        {
            Bitmap bmpImage = new Bitmap(img);
            Bitmap bmpCrop = bmpImage.Clone(cropArea, bmpImage.PixelFormat);
            Bitmap croppedBitMap = bmpCrop;
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
