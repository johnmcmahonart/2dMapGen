using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibNoise;
using UnityEngine;
namespace LibNoise.Generator
{ 
    public class gradientHack:ModuleBase //the ugliest hack in the history of computer graphics
    {
        private double min;
        private double max;
        private rescale rescaler;
        private double domainMin = -1;
        private double domainMax = 1;

        public gradientHack(): base(0)
        {
        }

        public gradientHack (double min, double max):base(0)
        {
            this.min = min;
            this.max = max;
            this.rescaler = new rescale(this.min, this.max, domainMin, domainMax);
        }
        public override double GetValue(double x, double y, double z)
        {
            var currentPoint = rescaler.Scale(z);
            //Debug.Log("Current point scaled:" + currentPoint);
            double input1 = -0.4;
            double output1 = -0.3;
            double input2 = 0.0f;
            double output2 = 1;
            double input3 = .4f;
            double output3 = -0.3;

            if (currentPoint<=input1)
           {
               rescale newScale = new rescale (domainMin,input1,domainMin,output1);
               return newScale.Scale(currentPoint);
                
           }

            
            if (currentPoint>=input1 && currentPoint<=input2)
           {
               rescale newScale = new rescale(input1, input2,output1, output2);
               return newScale.Scale(currentPoint);

           }
           if (currentPoint >= input2 && currentPoint <= input3)
           {
               rescale newScale = new rescale(input2,input3, output2, output3);
               return newScale.Scale(currentPoint);

           }

           if (currentPoint >= input3)
           {
               rescale newScale = new rescale(input3, domainMax, output3, domainMin);
               return newScale.Scale(currentPoint);

           }
            return -1.0;
           /*

      if (currentPoint<=point1)
      {
          return Utils.InterpolateLinear(domainMin, .6, currentPoint);
      }
      if (currentPoint>=point1 && currentPoint<=point2)
      {
          return Utils.InterpolateLinear(.6, domainMax, currentPoint);
      }
     if (currentPoint>=point2 && currentPoint<=point3)
      {
          return Utils.InterpolateLinear(domainMax, .6, currentPoint);
      }
      if (currentPoint >= point3 && currentPoint <= domainMax) 
      {
          return Utils.InterpolateLinear(domainMax, domainMin, currentPoint);
      }


*/
            //return currentPoint;
            
        }


    }
}
