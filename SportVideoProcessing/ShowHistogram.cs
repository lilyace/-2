extern alias newWorld;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;

using System.Windows.Forms;

namespace SportVideoProcessing
{
    public partial class ShowHistogram : Form
    {
        public ShowHistogram(newWorld::Emgu.CV.Mat _src)
        {
            InitializeComponent();
            newWorld::Emgu.CV.IImage  _tempImage = _src.ToImage<newWorld::Emgu.CV.Structure.Gray, byte>();
            histogramBox1.ClearHistogram();
            histogramBox1.GenerateHistograms(_tempImage, 256);
            histogramBox1.Refresh();
        }
    }
}
