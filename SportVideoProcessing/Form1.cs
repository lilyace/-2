using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;
using Accord.Video.FFMPEG;
using Accord.Vision;
using System.Text.RegularExpressions;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using Emgu.CV;
using Emgu.Util;
using Emgu.CV.UI;
using Emgu.CV.Cuda;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;



namespace SportVideoProcessing
{
    public partial class Form1 : Form
    {

        private VideoFileReader reader = new VideoFileReader();
        private long frameCount;
        private long frameNum;
        float motionLevel;
        string templateFile;
        Dictionary<string, Image> AugmentedRealityImages = new Dictionary<string, Image>();
        Accord.Vision.Motion.TwoFramesDifferenceDetector detector = new Accord.Vision.Motion.TwoFramesDifferenceDetector()
        {
            DifferenceThreshold = 2,
            // FramesPerBackgroundUpdate = 30,
            // KeepObjectsEdges = false,
            // MillisecondsPerBackgroundUpdate = 0,
            SuppressNoise = true
        };

        Accord.Vision.Motion.BlobCountingObjectsProcessing processor = new Accord.Vision.Motion.BlobCountingObjectsProcessing()
        {
            HighlightColor = System.Drawing.Color.Red,
            HighlightMotionRegions = true,
            MinObjectsHeight = 50,
            MinObjectsWidth = 20
        };

        Accord.Vision.Motion.MotionDetector motionDetector = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void открытьВидеоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            motionDetector = new Accord.Vision.Motion.MotionDetector(detector, processor);
            frameNum = 0;
            if (reader.IsOpen)
                reader.Close();
            string fileName;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fileName = openFileDialog1.FileName;
                ReadVideo(fileName);
                //ProcessFrame(fileName);
                //nextFrameButton.Enabled = true;
            }

        }
        void ProcessFrame(string _fileName)
        {
            reader.Open(_fileName);
            Bitmap btm = new Bitmap(_fileName);//reader.ReadVideoFrame();
            RecognizePeople(_fileName);
            pictureBox1.Image = btm;
            //  RecognizeNumbers(btm);
        }
       void RecognizePeople(string _fileName)
        {
            Mat image = new Mat(_fileName);
            long processingTime;
            Rectangle[] results;
            if (CudaInvoke.HasCuda)
            {
                using (GpuMat gpuMat = new GpuMat(image))
                    results = FindMen.Find(gpuMat, out processingTime);
            }
            else
            {
                using (UMat uImage = image.GetUMat(AccessType.ReadWrite))
                    results = FindMen.Find(uImage, out processingTime);
            }
            foreach (Rectangle rect in results)
            {
                CvInvoke.Rectangle(image, rect, new Bgr(Color.Red).MCvScalar);
            }
            Image<Bgr, Byte> im = image.ToImage<Bgr, Byte>();
            pictureBox1.Image = im.ToBitmap();
           // imageBox1.Image=im;
           // ImageViewer.Show(image);
        }
        void RecognizeNumbers(Bitmap _frame)
        {
            Ocr ocr = new Ocr();
            tessnet2.Tesseract tessocr = new tessnet2.Tesseract();
            tessocr.SetVariable("tessedit_char_whitelist", "0123456789*+-=");
            tessocr.Init(null, "eng", false);            
            List<tessnet2.Word> text = ocr.DoOCRNormal(_frame, "eng");          
                               
        }       
        void ReadVideo(string _fileName)
        {
            Regex regex = new Regex(@"Video\\.+$");
            Match match = regex.Match(_fileName);            
            //nameLabel.Text = match.Value.Remove(0,6);            
            reader.Open(_fileName);
            frameCount = reader.FrameCount;
            durationLabel.Text = frameCount.ToString();
            label5.Text = reader.Height.ToString() + "x" + reader.Width.ToString();
            Bitmap bitmap;        
                bitmap = reader.ReadVideoFrame(); // берем первый кадр и делаем на нем распознаванием           
            RecognizePeople(_fileName);

        }
        private void nextFrameButton_Click(object sender, EventArgs e)
        {
            frameNum++;
            if(frameNum==reader.FrameCount)
            {
                MessageBox.Show("Кадры видео");                
            }
            Bitmap bitmap;
            bitmap = reader.ReadVideoFrame();
            Image<Bgr, byte> tempFrame = new Image<Bgr, byte>(bitmap);
            imageBox1.Image = tempFrame;
            // motionLevel will indicate the amount of motion as a percentage.
           // motionLevel = motionDetector.ProcessFrame(videoFrame);
            numFrameLabel.Text = frameNum.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Bitmap videoFrame=null;
            for (int i=0; i<frameCount;i++)
            {
               videoFrame = reader.ReadVideoFrame();
                // process the frame somehow
                // ...
                pictureBox1.Image = videoFrame;
            }
            //videoFrame.Dispose();
        }

        private void настройкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Accord.Vision.Motion.TwoFramesDifferenceDetector tmpDet = new Accord.Vision.Motion.TwoFramesDifferenceDetector();
            Accord.Vision.Motion.BlobCountingObjectsProcessing tmpProc = new Accord.Vision.Motion.BlobCountingObjectsProcessing();
            Settings set = new Settings();
            if (set.ShowDialog()==DialogResult.OK)
            {
                if(detector.DifferenceThreshold!=set.TSVALUE)
                tmpDet.DifferenceThreshold = set.TSVALUE;
                if(detector.SuppressNoise!=set.NOISE)
                tmpDet.SuppressNoise = set.NOISE;
                if(processor.HighlightColor!=set.GETCOLOR)
                tmpProc.HighlightColor = set.GETCOLOR;
                if(processor.MinObjectsHeight!=set.HEIGHT)
                tmpProc.MinObjectsHeight = set.HEIGHT;
                if (processor.MinObjectsWidth!=set.WIDTH)
                tmpProc.MinObjectsWidth = set.WIDTH;
            }
            motionDetector.MotionDetectionAlgorithm = tmpDet;
            motionDetector.MotionProcessingAlgorithm = tmpProc;
        }     

    }
    public class Ocr
    {
        public List<tessnet2.Word> DoOCRNormal(Bitmap image, string lang)
        {
            tessnet2.Tesseract ocr = new tessnet2.Tesseract();
            ocr.Init(null, lang, false);
            List<tessnet2.Word> result = ocr.DoOCR(image, Rectangle.Empty);
            return result;
        }
    }
    //detector = new AForge.Vision.Motion.TwoFramesDifferenceDetector()
    //{
    //  DifferenceThreshold = 15,
    //  SuppressNoise = true
    //};

    //detector = new AForge.Vision.Motion.CustomFrameDifferenceDetector()
    //{
    //  DifferenceThreshold = 15,
    //  KeepObjectsEdges = true,
    //  SuppressNoise = true
    //};
    //processor = new AForge.Vision.Motion.GridMotionAreaProcessing()
    //{
    //  HighlightColor = System.Drawing.Color.Red,
    //  HighlightMotionGrid = true,
    //  GridWidth = 100,
    //  GridHeight = 100,
    //  MotionAmountToHighlight = 100F
    //};
}
    


