extern alias newWorld;
extern alias oldLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Accord.Video.FFMPEG;
using Accord.Video;
using Accord.Vision;
using System.Text.RegularExpressions;
//using Emgu.CV.CvEnum;
//using Emgu.CV;
using ContourAnalysisNS;
//using Emgu.Util;
using Emgu.CV.UI;
//using Emgu.CV.Cuda;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;



namespace SportVideoProcessing
{
    public partial class Form1 : Form
    {
        newWorld::Emgu.CV.Mat src;
        private VideoFileReader reader = new VideoFileReader();
        private long frameCount;
        private long frameNum;
        float motionLevel;
        double middleLength;
        double middleArea;
        ContourAnalysisNS.ImageProcessor processorFrame;
        newWorld::Emgu.CV.Image<newWorld::Emgu.CV.Structure.Bgr, Byte> Myframe;
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
            newWorld::Emgu.CV.Mat image = new newWorld::Emgu.CV.Mat(_fileName);
            long processingTime;
            Rectangle[] results;
            if (newWorld::Emgu.CV.Cuda.CudaInvoke.HasCuda)
            {
                using (newWorld::Emgu.CV.Cuda.GpuMat gpuMat = new newWorld::Emgu.CV.Cuda.GpuMat(image))
                    results = FindMen.Find(gpuMat, out processingTime);
            }
            else
            {
                using (newWorld::Emgu.CV.UMat uImage = image.GetUMat(newWorld::Emgu.CV.CvEnum.AccessType.ReadWrite))
                    results = FindMen.Find(uImage, out processingTime);
            }
            foreach (Rectangle rect in results)
            {
                newWorld::Emgu.CV.CvInvoke.Rectangle(image, rect, new newWorld::Emgu.CV.Structure.Bgr(Color.Red).MCvScalar);
            }
            newWorld::Emgu.CV.Image<newWorld::Emgu.CV.Structure.Bgr, byte> im = image.ToImage<newWorld::Emgu.CV.Structure.Bgr, Byte>();
            pictureBox1.Image = im.ToBitmap();      
        }
        void RecognizeNumbers(Bitmap _frame)
        {
            Ocr ocr = new Ocr();
            tessnet2.Tesseract tessocr = new tessnet2.Tesseract();
            tessocr.SetVariable("tessedit_char_whitelist", "0123456789*+-=");
            tessocr.Init(null, "eng", false);            
            
            for (int i = 0; i < processorFrame.contours.Count; i++)
            {
                Bitmap _frameInFrame=_frame.Clone(processorFrame.contours[i].BoundingRectangle,_frame.PixelFormat);
                List<tessnet2.Word> text = ocr.DoOCRNormal(_frameInFrame, "eng");
               
                //string path = @"Найденные контуры\" + i.ToString() + ".png";
                //_frameInFrame.Save(path);
            }
            //MessageBox.Show("Сохранено");
        }       
        void ReadVideo(string _fileName)
        {
            Regex regex = new Regex(@"Тестовые видеофайлы\\.+$");
            Match match = regex.Match(_fileName);            
            //nameLabel.Text = match.Value.Remove(0,6);            
            reader.Open(_fileName);
            frameCount = reader.FrameCount;
            durationLabel.Text = frameCount.ToString();
            label5.Text = reader.Height.ToString() + "x" + reader.Width.ToString();
            Bitmap firstFrame;        
            firstFrame = reader.ReadVideoFrame(); // берем первый кадр и делаем на нем распознаванием           
            // RecognizePeople(_fileName);
            WorkWithFrame(firstFrame, _fileName);
        }
        public void WorkWithFrame(Bitmap _frame, string _filename)
        {
            src = newWorld::Emgu.CV.CvInvoke.Imread(_filename, newWorld::Emgu.CV.CvEnum.ImreadModes.Grayscale); //load  image

            newWorld::Emgu.CV.Mat[] bgr=new newWorld::Emgu.CV.Mat[3];   //destination array
            newWorld::Emgu.CV.Mat dst1 = new newWorld::Emgu.CV.Mat();
            newWorld::Emgu.CV.Mat dst2 = new newWorld::Emgu.CV.Mat();
            newWorld::Emgu.CV.Mat dst3 = new newWorld::Emgu.CV.Mat();
            newWorld::Emgu.CV.Mat result = new newWorld::Emgu.CV.Mat();

            //bgr =src.Split();//split source 

            //newWorld::Emgu.CV.CvInvoke.Threshold(bgr[0], dst1,50,250, newWorld::Emgu.CV.CvEnum.ThresholdType.Binary);
            // newWorld::Emgu.CV.CvInvoke.Threshold(bgr[1], dst2, 50, 255, newWorld::Emgu.CV.CvEnum.ThresholdType.Binary);
            //newWorld::Emgu.CV.CvInvoke.Threshold(bgr[2], dst3, 50,255, newWorld::Emgu.CV.CvEnum.ThresholdType.Binary);

            ////Note: OpenCV uses BGR color order
            //newWorld::Emgu.CV.CvInvoke.Imwrite("blue.png", bgr[0]); //blue channel
            //newWorld::Emgu.CV.CvInvoke.Imwrite("green.png", bgr[1]); //green channel
            //newWorld::Emgu.CV.CvInvoke.Imwrite("red.png", bgr[2]); //red channel
            //newWorld::Emgu.CV.CvInvoke.Imwrite("Преобразованная хрень1.png", dst1);
            //newWorld::Emgu.CV.CvInvoke.Imwrite("Преобразованная хрень2.png", dst2);
            //newWorld::Emgu.CV.CvInvoke.Imwrite("Преобразованная хрень3.png", dst3);
            newWorld::Emgu.CV.DenseHistogram Histo = new newWorld::Emgu.CV.DenseHistogram(255, new newWorld::Emgu.CV.Structure.RangeF(0, 255));
            newWorld::Emgu.CV.Image<newWorld::Emgu.CV.Structure.Gray, byte> im = src.ToImage<newWorld::Emgu.CV.Structure.Gray, byte>();
            Histo.Calculate<byte>(new newWorld::Emgu.CV.Image<newWorld::Emgu.CV.Structure.Gray, byte>[] { im },true, null);
            int i = 0;
            float[] valuesHisto = Histo.GetBinValues();

            while (valuesHisto[i] < 900)
                i++;
            int j = 254;
            while (valuesHisto[j] < 430)
                j--;
            // newWorld::Emgu.CV.CvInvoke.Threshold(src, dst1, 134, 250, newWorld::Emgu.CV.CvEnum.ThresholdType.Binary);
            newWorld::Emgu.CV.CvInvoke.InRange(src, new newWorld::Emgu.CV.ScalarArray(new newWorld::Emgu.CV.Structure.MCvScalar(i)), new newWorld::Emgu.CV.ScalarArray(new newWorld::Emgu.CV.Structure.MCvScalar(j)), dst1);
                 
                Bitmap btm = new Bitmap(dst1.Bitmap);
            processorFrame = new ImageProcessor();
        
            processorFrame.adaptiveThresholdBlockSize = 4;
            processorFrame.blur = false;
            processorFrame.ProcessImage(btm);
            middleArea=GetMiddleArea();
            pictureBox2.Image = btm;
            pictureBox1.Image = _frame;
            btm.Save("Проверка!.png");
           // RecognizeNumbers(_frame);
           
        }
        public double GetMiddleArea()
        {
            double _midArea = 0;
            int i = 0;
            for(i=0; i< processorFrame.contours.Count; i++)
            {
                _midArea += processorFrame.contours[i].Area;
            }
            _midArea = _midArea / processorFrame.contours.Count;
            return _midArea;
        }
        public double GetMiddleLength()
        {
            double _midLength = 0;
            int i = 0;
            for (i = 0; i < processorFrame.contours.Count; i++)
            {
                //_midLength += processorFrame.contours[i].le;
            }
            _midLength = _midLength / processorFrame.contours.Count;
            return _midLength;
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
            newWorld::Emgu.CV.Image<newWorld::Emgu.CV.Structure.Bgr, byte> tempFrame = new newWorld::Emgu.CV.Image<newWorld::Emgu.CV.Structure.Bgr, byte>(bitmap);
            //imageBox1.Image = tempFrame;
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

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (processorFrame != null)
            {
                foreach (oldLibrary::Emgu.CV.Contour<Point> _contour in processorFrame.contours)
                {
                    
                    if ((_contour.BoundingRectangle.Height<150) && (_contour.BoundingRectangle.Height>20) && (_contour.BoundingRectangle.Width<200) && (_contour.BoundingRectangle.Width>10))
                    {
                        Rectangle tempRect = _contour.BoundingRectangle;
                        e.Graphics.DrawRectangle(new Pen(Color.Red), tempRect);
                        //e.Graphics.DrawLines(new Pen(Color.Red), _contour.ToArray());
                    }
                }
            }
        }

        private void showHistButton_Click(object sender, EventArgs e)
        {
            ShowHistogram sh = new ShowHistogram(src);
            sh.ShowDialog();
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
    


