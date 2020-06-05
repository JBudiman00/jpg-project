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
        public static string file;
        public Form1()
        {
            InitializeComponent();
        }

        private void pbView_Click(object sender, EventArgs e)
        {
            
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
        }
    }
}
