extern alias newWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Emgu.CV;
using System.Diagnostics;
//using Emgu.CV.Structure;
//using Emgu.CV.CvEnum;
//using Emgu.CV.Util;
#if !(__IOS__ || NETFX_CORE)
//using Emgu.CV.Cuda;
#endif

namespace SportVideoProcessing
{
    class FindMen
    {
        /// <summary>
        /// Find the pedestrian in the image
        /// </summary>
        /// <param name="image">The image</param>
        /// <param name="processingTime">The processing time in milliseconds</param>
        /// <returns>The region where pedestrians are detected</returns>
        public static Rectangle[] Find(newWorld::Emgu.CV.IInputArray image, out long processingTime)
        {
            Stopwatch watch;
            Rectangle[] regions;

            using (newWorld::Emgu.CV.InputArray iaImage = image.GetInputArray())
            {
#if !(__IOS__ || NETFX_CORE)
                //if the input array is a GpuMat
                //check if there is a compatible Cuda device to run pedestrian detection
                if (iaImage.Kind == newWorld::Emgu.CV.InputArray.Type.CudaGpuMat)
                {
                    //this is the Cuda version
                    using (newWorld::Emgu.CV.Cuda.CudaHOG des = new newWorld::Emgu.CV.Cuda.CudaHOG(new Size(64, 128), new Size(16, 16), new Size(8, 8), new Size(8, 8)))
                    {
                        des.SetSVMDetector(des.GetDefaultPeopleDetector());

                        watch = Stopwatch.StartNew();
                        using (newWorld::Emgu.CV.Cuda.GpuMat cudaBgra = new newWorld::Emgu.CV.Cuda.GpuMat())
                        using (newWorld::Emgu.CV.Util.VectorOfRect vr = new newWorld::Emgu.CV.Util.VectorOfRect())
                        {
                            newWorld::Emgu.CV.Cuda.CudaInvoke.CvtColor(image, cudaBgra, newWorld::Emgu.CV.CvEnum.ColorConversion.Bgr2Bgra);
                            des.DetectMultiScale(cudaBgra, vr);
                            regions = vr.ToArray();
                        }
                    }
                }
                else
#endif
                {
                    //this is the CPU/OpenCL version
                    using (newWorld::Emgu.CV.HOGDescriptor des = new newWorld::Emgu.CV.HOGDescriptor())
                    {
                        des.SetSVMDetector(newWorld::Emgu.CV.HOGDescriptor.GetDefaultPeopleDetector());
                        watch = Stopwatch.StartNew();

                        newWorld::Emgu.CV.Structure.MCvObjectDetection[] results = des.DetectMultiScale(image);
                        regions = new Rectangle[results.Length];
                        for (int i = 0; i < results.Length; i++)
                            regions[i] = results[i].Rect;
                        watch.Stop();
                    }
                }

                processingTime = watch.ElapsedMilliseconds;

                return regions;
            }
        }
    }
}
