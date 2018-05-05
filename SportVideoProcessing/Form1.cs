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
using MatchingBlocks_TSS_;



namespace SportVideoProcessing
{
    public partial class Form1 : Form
    {
        //блок переменных общих
        Bitmap currentMainFrameBitmap;
        Bitmap prevMainFrameBitmap;
        Bitmap frameForDrawing;
        string path;
        //блок переменных для обработки кадра
        newWorld::Emgu.CV.Mat grayFrame;        
        newWorld::Emgu.CV.Image<newWorld::Emgu.CV.Structure.Bgr, Byte> mainFrameImage;
        List<Rectangle> allContours=new List<Rectangle>();
        //блок переменных для распознавания движения
        private VideoFileReader reader = new VideoFileReader();
        private long frameCount;
        private long frameNum;
        float motionLevel;
        double middleLength;
        double middleArea;
        Graphics gr;
        ContourAnalysisNS.ImageProcessor processorFrame = new ImageProcessor();
        ContourAnalysisNS.ImageProcessor processorNumbersContours;


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
            processorFrame.adaptiveThresholdBlockSize = 4;
            processorFrame.blur = false;
            InitializeComponent();
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

        }

        private void открытьВидеоToolStripMenuItem_Click(object sender, EventArgs e)
        {
           // motionDetector = new Accord.Vision.Motion.MotionDetector(detector, processor);
            frameNum = 0;
            if (reader.IsOpen)
                reader.Close();           
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                path = openFileDialog1.FileName;
               
                ReadVideo();               
                nextFrameButton.Enabled = true;
            }
        }
        
       void RecognizePeopleDefault()
        {
            listBox1.Items.Clear();        
            currentMainFrameBitmap.Save("Промежуточное изображение.png");
            allContours.Clear();
            gr = Graphics.FromImage(frameForDrawing);
            newWorld::Emgu.CV.Mat image = new newWorld::Emgu.CV.Mat("Промежуточное изображение.png");
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
            int i = 1;
            foreach (Rectangle rect in results)
            {
                allContours.Add(rect);
                listBox1.Items.Add("Спортсмен "+i.ToString());
                i++;
               // newWorld::Emgu.CV.CvInvoke.Rectangle(image, rect, new newWorld::Emgu.CV.Structure.Bgr(Color.Red).MCvScalar);
            }
            int j = 1;
            foreach (Rectangle _contour in allContours)
            {
                gr.DrawRectangle(new Pen(Color.Red), _contour);
               // gr.DrawLines(new Pen(Color.Red), _contour.ToArray());
                gr.DrawString(j.ToString(), new Font("Arial", 14), Brushes.AliceBlue, _contour.X, _contour.Y);
                j++;
            }
            
            pictureBox1.Image = frameForDrawing; // тут же подключается отрисовка
            nRecogLabel.Text = results.Length.ToString();
                     
            ToBinContours();
        }        
        public void ToBinContours()
        {
            newWorld::Emgu.CV.Image<newWorld::Emgu.CV.Structure.Gray, byte> _contour;
            Bitmap _tempToSave;//контур контуров
            Bitmap _frameInContour; //= mainFrameImage.ToImage<newWorld::Emgu.CV.Structure.Gray, byte>();
            //tessnet2.Tesseract tessocr = new tessnet2.Tesseract();
            //tessocr.SetVariable("tessedit_char_whitelist", "0123456789");
            //tessocr.Init(null, "eng", false);
            for (int i = 0; i < allContours.Count; i++)
            {
                //if ((allContours[i].Height  < 150) && (allContours[i].Height > 30) && (allContours[i].Width < 200) && (allContours[i].Width > 22))
                //{
                    _frameInContour = currentMainFrameBitmap.Clone(allContours[i], currentMainFrameBitmap.PixelFormat);
                    _contour = new newWorld::Emgu.CV.Image<newWorld::Emgu.CV.Structure.Gray, byte>(_frameInContour);
               // newWorld::Emgu.CV.CvInvoke.Threshold(_contour, _contour, 100, 250, newWorld::Emgu.CV.CvEnum.ThresholdType.Binary);

               // processorNumbersContours = new ImageProcessor();
               // processorNumbersContours.ProcessImage(_contour.Bitmap);
                string path = @"Бинаризированные приведенные контуры\" + i.ToString() + ".png";
                _contour.Save(path);
                
                //for (int j = 0; j < processorNumbersContours.contours.Count; j++)
                //{
                //    if ((processorNumbersContours.contours[j].BoundingRectangle.Width < 20) && (processorNumbersContours.contours[j].BoundingRectangle.Width > 10) && (processorNumbersContours.contours[j].BoundingRectangle.Height > 10) && (processorNumbersContours.contours[j].BoundingRectangle.Height < 20))
                //    {
                //        _tempToSave = _contour.Bitmap.Clone(processorNumbersContours.contours[j].BoundingRectangle, _contour.Bitmap.PixelFormat);
                //        //_tempToSave.Save(@"Бинаризированные приведенные контуры\" + i.ToString() + "_" + j.ToString() + ".png");
                //        // var text = tessocr.DoOCR(_tempToSave, Rectangle.Empty);
                //        _tempToSave.Dispose();
                //    }
                //    //}
                //    //    //newWorld::Emgu.CV.CvInvoke.Resize(_contour, _contour, new Size(50, 50), 0, 0, newWorld::Emgu.CV.CvEnum.Inter.Linear);
                //}
                _frameInContour.Dispose();
            }
        }
        public void ReadVideo()
        {
            Regex regex = new Regex(@"Тестовые видеофайлы\\.+$");
            Match match = regex.Match(path);            
            //nameLabel.Text = match.Value.Remove(0,6);            
            reader.Open(path);
            frameCount = reader.FrameCount;
            durationLabel.Text = frameCount.ToString();
            label5.Text = reader.Height.ToString() + "x" + reader.Width.ToString();                   
            currentMainFrameBitmap = reader.ReadVideoFrame(); // берем первый кадр и делаем на нем распознаванием     
            frameForDrawing = currentMainFrameBitmap.Clone(new Rectangle(0, 0, currentMainFrameBitmap.Width, currentMainFrameBitmap.Height), currentMainFrameBitmap.PixelFormat);
            
            RecognizePeopleDefault();// дефолтное распознавание
            //MyPeopleRecognize(); //Мое распознавание

        }
        public void MyPeopleRecognize()
        {
            grayFrame = newWorld::Emgu.CV.CvInvoke.Imread(path, newWorld::Emgu.CV.CvEnum.ImreadModes.Grayscale); //load  image

            newWorld::Emgu.CV.Mat[] bgr=new newWorld::Emgu.CV.Mat[3];   //destination array
            newWorld::Emgu.CV.Mat dst1 = new newWorld::Emgu.CV.Mat();           
            newWorld::Emgu.CV.Mat result = new newWorld::Emgu.CV.Mat();            
            newWorld::Emgu.CV.DenseHistogram Histo = new newWorld::Emgu.CV.DenseHistogram(255, new newWorld::Emgu.CV.Structure.RangeF(0, 255));
            newWorld::Emgu.CV.Image<newWorld::Emgu.CV.Structure.Gray, byte> im = grayFrame.ToImage<newWorld::Emgu.CV.Structure.Gray, byte>();
            Histo.Calculate<byte>(new newWorld::Emgu.CV.Image<newWorld::Emgu.CV.Structure.Gray, byte>[] { im },true, null);
            int i = 0;
            float[] valuesHisto = Histo.GetBinValues();

            while (valuesHisto[i] < 900)
                i++;
            int j = 254;
            while (valuesHisto[j] < 430)
                j--;
            // newWorld::Emgu.CV.CvInvoke.Threshold(src, dst1, 134, 250, newWorld::Emgu.CV.CvEnum.ThresholdType.Binary);
            newWorld::Emgu.CV.CvInvoke.InRange(grayFrame, new newWorld::Emgu.CV.ScalarArray(new newWorld::Emgu.CV.Structure.MCvScalar(i)), new newWorld::Emgu.CV.ScalarArray(new newWorld::Emgu.CV.Structure.MCvScalar(j)), dst1);
                 
                Bitmap btm = new Bitmap(dst1.Bitmap);
        
            processorFrame.ProcessImage(btm);
            for (int p = 0; p < processorFrame.contours.Count; p++)
                if ((processorFrame.contours[p].BoundingRectangle.Height < 150) && (processorFrame.contours[p].BoundingRectangle.Height > 30) && (processorFrame.contours[p].BoundingRectangle.Width < 200) && (processorFrame.contours[p].BoundingRectangle.Width > 22))
                   allContours.Add(new Rectangle(processorFrame.contours[p].BoundingRectangle.X, processorFrame.contours[p].BoundingRectangle.Y,processorFrame.contours[p].BoundingRectangle.Width, processorFrame.contours[p].BoundingRectangle.Height));//добавить добавление контуров
            middleArea =GetMiddleArea();
            pictureBox2.Image = btm;
            pictureBox1.Image = currentMainFrameBitmap;
            btm.Save("Проверка!.png");
            ToBinContours();
           
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

        private void nextFrameButton_Click(object sender, EventArgs e)
        {
            frameNum++;
            if(frameNum==reader.FrameCount)
            {
                MessageBox.Show("Видео кончилось");                
            }
            prevMainFrameBitmap = currentMainFrameBitmap.Clone(new Rectangle(0,0, currentMainFrameBitmap.Width, currentMainFrameBitmap.Height), currentMainFrameBitmap.PixelFormat);
            currentMainFrameBitmap = reader.ReadVideoFrame();
            //frameForDrawing.Dispose();
            frameForDrawing = currentMainFrameBitmap.Clone(new Rectangle(0, 0, currentMainFrameBitmap.Width, currentMainFrameBitmap.Height), currentMainFrameBitmap.PixelFormat);
            numFrameLabel.Text = frameNum.ToString();
            RecognizePeopleDefault();
     
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

        private void showHistButton_Click(object sender, EventArgs e)
        {
            ShowHistogram sh = new ShowHistogram(grayFrame);
            sh.ShowDialog();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            panel1.Enabled = checkBox1.Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            nextFrameButton.Enabled = false;
            //чистый кадр из текущего для рисовки
            frameForDrawing = currentMainFrameBitmap.Clone(new Rectangle (0,0, currentMainFrameBitmap.Width, currentMainFrameBitmap.Height), currentMainFrameBitmap.PixelFormat);
            //привязка к графике
            gr = Graphics.FromImage(frameForDrawing);
            //создание переменных для блоков сравнения
            Bitmap currTempCont, prevTempCont;
            //выбор индекса для отслеживания
            int i = listBox1.SelectedIndex;
            List<Rectangle> motionRect = new List<Rectangle>();
            
                currTempCont = currentMainFrameBitmap.Clone(allContours[i], currentMainFrameBitmap.PixelFormat);
                prevTempCont = prevMainFrameBitmap.Clone(allContours[i], prevMainFrameBitmap.PixelFormat);
                motionRect = MatchingBlocks_TSS_.Program.MainProgram(currTempCont, prevTempCont, currTempCont, 20, 7, i.ToString());
                currTempCont.Dispose();
                prevTempCont.Dispose();
            
            
            Pen pen = new Pen(Color.Green);
            foreach (Rectangle rect in motionRect)
                gr.DrawRectangle(pen, new Rectangle(allContours[i].X+rect.X, allContours[i].Y + rect.Y, 20, 20));
            //gr.DrawLine(pen, new Point(0,0), new Point(50,50));
            pictureBox1.Image = frameForDrawing;
        }

        
        public void PlayVideo()
        { }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
                button1.Enabled = true;
            else button1.Enabled = false;
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
    


