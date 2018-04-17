using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;

namespace SportVideoProcessing
{
    public partial class Settings : Form
    {
        private Color clr;
        public Settings()
        {
            InitializeComponent();
        }
       public int TSVALUE {get { return Convert.ToInt32(threesoldnumericUpDown.Value); }}
      //  public float MLVALUE { get { return Convert.ToInt32(motionLvlnumericUpDown.Value); } }
      public bool NOISE { get { return noiseCheckBox.Checked; } }
        public Color GETCOLOR { get{ return clr; } }
        public int WIDTH { get{return  Convert.ToInt32(widthTextBox.Text); } }
        public int HEIGHT { get {return  Convert.ToInt32(heightTextBox.Text); } }
        private void changeColorButton_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
                clr = colorDialog1.Color;
        }
    }
}
