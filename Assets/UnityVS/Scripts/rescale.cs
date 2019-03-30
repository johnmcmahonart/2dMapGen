using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class rescale
    {
    private double rangeMin, rangeMax, domainMin, domainMax; 
    public rescale (double rangeMin,double rangeMax, double domainMin, double domainMax)
        {
        if (rangeMax>rangeMin)
        {
            this.rangeMax = rangeMax;

        }
        if (rangeMin<rangeMax)
        {
            this.rangeMin = rangeMin;
        }
            this.domainMax = domainMax;
            this.domainMin = domainMin;
        }

    public double Scale (double value)
    {
       double scaleParm = (domainMax - domainMin) / (rangeMax - rangeMin);
       return (domainMin + ((value - rangeMin) * scaleParm));
}
}
