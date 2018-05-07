using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace MatchingBlocks_TSS_
{
    public class Program
    {
        //количество блоков по горизонтали и вертикали
        static int nHCount, nWCount;
        // путь к файлу
        static string path1 = @"C:\Users\user\Documents\Visual Studio 2017\Projects\MBA\MatchingBlocks(TSS)\Спорт1.png";
        static string path2 = @"C:\Users\user\Documents\Visual Studio 2017\Projects\MBA\MatchingBlocks(TSS)\Спорт2.png";
        static string path3 = @"C:\Users\user\Documents\Visual Studio 2017\Projects\MBA\MatchingBlocks(TSS)\Спорт2.png";
        static int n = 16;
         static double p = 7;
        static Bitmap currentFrame;
        static Bitmap prevFrame;
        static Bitmap fr;
       //static Bitmap block;
        static Graphics gr;
       static Pen penBlack = new Pen(Color.Black);
        static Pen penRed = new Pen(Color.Red);
        static int xGlobal, yGlobal;

        static void Main(string[] args)
        {
            currentFrame = new Bitmap(path2);
            prevFrame = new Bitmap(path1);
            fr = new Bitmap(path3);
            string name = "3";
            //MainProgram(currentFrame, prevFrame, fr, n,p, name);
            
        }
        public static List<Rectangle> MainProgram(Bitmap _currFrame, Bitmap _prevFrame, Bitmap _fr, int _n, double _p, string _name, out List<Point> motionVect)
        {

            List<Rectangle> motionRect=new List<Rectangle>();
             motionVect = new List<Point>();
            gr = Graphics.FromImage(_fr);
            prevFrame = _prevFrame;
            double t = Convert.ToDouble(_currFrame.Height) / Convert.ToDouble(_n);
            nHCount = Convert.ToInt32(Math.Round(t));
            nWCount = Convert.ToInt32(Math.Round(Convert.ToDouble(_currFrame.Width) / Convert.ToDouble(_n)));
            Point point = new Point();
            Bitmap block;
            for (int y = 0; y < nHCount-1; y++)
                for (int x = 0; x < nWCount-1; x++)
                {
                    
                    Console.WriteLine("Блок " + y.ToString() + " " + x.ToString());
                   // gr.DrawRectangle(penBlack, new Rectangle(x * _n, y * _n, _n, _n));
                    yGlobal = y * _n;
                    xGlobal = x * _n;
                    block = _currFrame.Clone(new Rectangle(x * _n, y * _n, _n, _n), _currFrame.PixelFormat);// копируем блок из кадра1
                                                                                                            // копируем 9 блоков из последующего кадра
                    DoTSS(y * n, x * n, block, _p, motionRect, out point);
                    motionVect.Add(point);
                    block.Dispose();

                }
            _fr.Save(_name+".png", System.Drawing.Imaging.ImageFormat.Png);
            return motionRect;
        }
        public static void DoTSS(int _y, int _x, Bitmap _block, double p, List<Rectangle> motionRect, out Point point)
        {
            if (p < 1)
            {
                if ((_y != yGlobal) || (_x != xGlobal))
                {
                    gr.DrawRectangle(penRed, new Rectangle(_x, _y, n, n));
                    motionRect.Add(new Rectangle(_x, _y, n, n));
                    
                }
                point = new Point(_x - xGlobal, _y - yGlobal);
               // return new Rectangle(_x, _y,n,n);
            }
            else
            {
                int p1 = Convert.ToInt32(Math.Truncate(p));
                Bitmap _tempBlock;
                Bitmap foundBlock;
                double max = 0;
                double currentMAD;
                int i = 0, j = 0;
                int newX = _x, newY = _y;
                
                foundBlock = prevFrame.Clone(new Rectangle(_x, _y, n, n), prevFrame.PixelFormat);
                Console.WriteLine("Рассматриваю блок с координатами:(" + (_x).ToString() + ", " + (_y).ToString() + ", " + ((_x + n)).ToString() + ", " + ((_y + n)).ToString() + ")");
                for (int ky = p1 * (-1); ky <= p1; ky += p1)
                {
                    for (int kx = p1 * (-1); kx <= p1; kx += p1)
                    {   // Здесь поставить проверку на невыход за границу массива
                        if (((_x + kx) >= 0) && ((_y + ky) >= 0) && (((_x + n) + kx) <= prevFrame.Width - 1) && (((_y + n) + ky) <= prevFrame.Height - 1))
                       {
                            //копируем блок из второго кадра
                            _tempBlock = prevFrame.Clone(new Rectangle(_x + kx, _y + ky, n, n), prevFrame.PixelFormat);
                            //получаем текущую разницу
                            currentMAD = GetMAD(_block, _tempBlock, n);
                            if ((currentMAD > max) && (currentMAD>0.018))
                            {
                                max = currentMAD;
                                //foundBlock = _tempBlock.Clone(new Rectangle(0, 0, n, n), _tempBlock.PixelFormat);
                                newX = _x + kx;
                                newY = _y + ky;
                            }
                            _tempBlock.Dispose();
                        }
                        j++;
                    }
                    i++;
                }
                foundBlock.Dispose();
                Console.WriteLine("Минимальная разница найдена в блоке с координатами: " + newX.ToString() + ", " + newY.ToString() + ", " + (newX + n).ToString() + "" + (newY + n).ToString() + "");
                Console.WriteLine("Уменьшаю шаг в 2 раза:p=" + p / 2);
                p = p / 2;
                point = new Point(_x - xGlobal, _y - yGlobal);
                DoTSS( newY, newX,_block, p, motionRect, out point);
                
                //return;
            }
            // min = MADs.Min();
            


        }
       static public double GetMAD(Bitmap _basicBlock, Bitmap _newBlock, int _N)
        {
            double MAD;
            double sum = 0;
            for (int i = 0; i < _N; i++)
                for (int j = 0; j < _N; j++)
                    sum += Math.Abs(_basicBlock.GetPixel(i,j).GetBrightness()-_newBlock.GetPixel(i,j).GetBrightness());
            MAD = sum / (_N * _N);
            return MAD;
        }
    }
}
