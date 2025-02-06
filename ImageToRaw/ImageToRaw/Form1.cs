using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ImageToRaw
{
    public partial class Form1 : Form
    {
        private Logger logger;

        public Form1()
        {
            InitializeComponent();
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            logger = Logger.GetInstance("vision_log.log", true);
        }

        private void btnLoadImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files|*.bmp;*.jpg;*.jpeg;*.png"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                Bitmap bitmap = new Bitmap(filePath);
                pictureBox.Image = bitmap;
                logger.Info($"Image loaded: {filePath}");
            }
        }

        private void btnSaveRawImage_Click(object sender, EventArgs e)
        {
            if (pictureBox.Image != null)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Raw Image Files|*.raw"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;
                    Bitmap bitmap = new Bitmap(pictureBox.Image);
                    byte[] pixelData = ImageProcessor.GetRawDataFromBitmap(bitmap);
                    File.WriteAllBytes(filePath, pixelData);
                    logger.Info($"Raw image saved: {filePath}");
                }
            }
            else
            {
                MessageBox.Show("No image loaded to save!");
                logger.Warn("Attempted to save raw image without loading an image.");
            }
        }
    }
}
