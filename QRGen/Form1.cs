using QRCoder;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace QRGen
{
    public partial class Form1 : Form
    {
        private Bitmap qrCodeImage;
        public Form1()
        {
            InitializeComponent();
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtInput.Clear();
            pictureBoxQRCode.Image = null;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (qrCodeImage == null)
            {
                MessageBox.Show("Сначала сгенерируйте QR-код.", "QRGen");
                return;
            }

            try
            {
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string filePath = Path.Combine(desktopPath, "QRCode.png");
                qrCodeImage.Save(filePath);
                MessageBox.Show($"QR-код сохранен на рабочий стол: {filePath}", "QRGen");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении QR-кода: {ex.Message}", "QRGen");
            }
        }
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            string inputText = txtInput.Text;

            if (string.IsNullOrWhiteSpace(inputText))
            {
                MessageBox.Show("Пожалуйста, введите текст для генерации QR-кода.", "QRGen");
                return;
            }

            try
            {
                using QRCodeGenerator qrGenerator = new();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(inputText, QRCodeGenerator.ECCLevel.Q);
                using QRCode qrCode = new(qrCodeData);
                qrCodeImage = qrCode.GetGraphic(20);
                Bitmap resizedImage = new(qrCodeImage, new Size(400, 400));
                pictureBoxQRCode.Image = resizedImage;
                pictureBoxQRCode.BackgroundImageLayout = ImageLayout.Stretch;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при генерации QR-кода: {ex.Message}", "QRGen");
            }
        }
    }
    
}
