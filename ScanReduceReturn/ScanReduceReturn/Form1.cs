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
        int initialX;
        int initialY;
        int finalX;
        int finalY;
        public static string file;
        Bitmap myBitMap;
        Graphics g;

        public Form1()
        {
            InitializeComponent();
        }

        private void pbView_Click(object sender, EventArgs e)
        {
            lblTest.Text = "Success!";
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
            }
            pbView.Image = Image.FromFile(file);
            pbView.SizeMode = PictureBoxSizeMode.StretchImage;
            myBitMap = new Bitmap(file);
        }
        private void btnCrop_Click(object sender, EventArgs e)
        {

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
                //if (Math.Sign (width) == -1) width = width 
                //Rectangle rect = new Rectangle(initialPt.X, initialPt.Y, Cursor.Position.X - initialPt.X, Cursor.Position.Y - initialPt.Y);
                Rectangle rect = new Rectangle(initialX, initialY, width * Math.Sign(width), height * Math.Sign(height));
                pbView.CreateGraphics().DrawRectangle(drwaPen, rect);

                finalX = width * Math.Sign(width);
                finalY = height * Math.Sign(height);
            }
        }

        private void pbView_MouseUp(object sender, MouseEventArgs e)
        {
            isDown = false;
        }
    }
}
