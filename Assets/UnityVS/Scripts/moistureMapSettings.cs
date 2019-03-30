using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibNoise;
[Serializable]
public class moistureMapSettings
    {
        public int Octaves { get; set; }
        public double Frequency { get; set; }
        public double Lacunarity { get; set; }
        public QualityMode Quality { get; set; }
        public int Seed { get; set; }
        public double Persistence { get; set; }


    }

