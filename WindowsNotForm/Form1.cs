using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.UI;

namespace WindowsNotForm
{
    public partial class Form1 : Form
    {
        VideoCapture mainCam = new VideoCapture(0);
        private CascadeClassifier haar;
        private bool haarLoaded = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void scan(Mat image)
        {
            Image<Gray, Byte> grayImage = image.ToImage<Bgr, byte>().Convert<Gray, Byte>();


            var faces = haar.DetectMultiScale(grayImage);

            Random rint = new Random();

            foreach (var face in faces)
            {
                CvInvoke.Rectangle(image, face, new MCvScalar(0, 255, 0), 2, Emgu.CV.CvEnum.LineType.EightConnected);
            }
        }


        private void uploadScreen(object sender, EventArgs e)
        {
            Mat frame = new Mat();

            mainCam.Retrieve(frame);

            if (haarLoaded == true)
            {
                scan(frame);
            }
            


            var convertedFrame = frame.ToBitmap();
            pictureBox1.Invoke(() => pictureBox1.Image = convertedFrame);
        }

        private void changeHaar(object sender, EventArgs e)
        {
            DialogResult selected = openFileDialog1.ShowDialog(this);
            if (selected == DialogResult.OK)
            {
                haarLoaded = true;
                haar = new CascadeClassifier(openFileDialog1.FileName);
            } else { haarLoaded = false; }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            mainCam.Start();

            openFileDialog1.Filter = "XML files (*.xml)|*.xml";
            button1.Click += changeHaar;

            mainCam.ImageGrabbed += uploadScreen;
        }
    }
}
