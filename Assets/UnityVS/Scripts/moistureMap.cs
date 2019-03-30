using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibNoise;
using LibNoise.Generator;
using LibNoise.Operator;
using UnityEngine;

public class moistureMap:IMap
    {
        public enum MoistureLevel
        {
            Wettest,
            Wetter,
            Wet,
            Dry,
            Dryer,
            Dryest
        }

        private Cache moistureCache;
        private float[,] noiseData;
        public moistureMap(worldChunkSettings chunkSettings, moistureMapSettings settings, IMap heightmap)

        {
            fill(chunkSettings, settings, heightmap);
        }

        public float GetValue(int x, int y)
        {
            return (float)(noiseData[x, y]);
        }
        private void fill(worldChunkSettings chunkSettings, moistureMapSettings settings, IMap heightmap)

        {
            RidgedMultifractal baseMap = new RidgedMultifractal();
            baseMap.OctaveCount = settings.Octaves;
            baseMap.Frequency = settings.Frequency;
            baseMap.Lacunarity = settings.Lacunarity;
            baseMap.Seed = settings.Seed;
            Turbulence distortion = new Turbulence(.2, baseMap); 
            Add adjustedMoisture = new Add(distortion, heightmap.GetCache());
            this.moistureCache = new Cache(adjustedMoisture);
            Noise2D final = new Noise2D(chunkSettings.mapHeight, chunkSettings.mapWidth, adjustedMoisture);
            final.GeneratePlanar(chunkSettings.left, chunkSettings.right, chunkSettings.top, chunkSettings.bottom, isSeamless: true);
            noiseData = final.GetData();

        }
        public int GetSize() //size is assumed to be square, may need to updae later
        {
            return noiseData.GetLength(0);
        }
        public Color GetColor(int x, int y)
        {
            float normalizedColor = (noiseData[x, y] + 1.0f) / 2.0f;

            if (normalizedColor<=.1)
            {
            return extendedColors.Dryest;
            }
            else if (normalizedColor <= .27)
            {
                return extendedColors.Dryer;
            }
            else if (normalizedColor <= .4)
            {
                return extendedColors.Dry;
            }
            else if (normalizedColor <= .6)
            {
                return extendedColors.Wet;
            }
            else if (normalizedColor <= .8)
            {
                return extendedColors.Wetter;
            }
            else if (normalizedColor <= .9)
            {
                return extendedColors.Wettest;
            }
            

            return extendedColors.Wettest;
        }
    public MoistureLevel GetMoisture(int x, int y)
    {
        float normalizedMoisture = (noiseData[x, y] + 1.0f) / 2.0f;

        if (normalizedMoisture<=.1)
        {
            return MoistureLevel.Dryest;
        }
        else if (normalizedMoisture <= .27)
        {
            return MoistureLevel.Dryer;
        }
        else if (normalizedMoisture <= .4)
        {
            return MoistureLevel.Dry;
        }
        else if (normalizedMoisture <= .6)
        {
            return MoistureLevel.Wet;
        }
        else if (normalizedMoisture <= .8)
        {
            return MoistureLevel.Wetter;
        }
        else if (normalizedMoisture <= .9)
        {
            return MoistureLevel.Wettest;
        }
        return MoistureLevel.Wettest;
    }

        public Cache GetCache()
        {
            return moistureCache;
        }
}







