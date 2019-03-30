using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using LibNoise;
using LibNoise.Operator;
using LibNoise.Generator;
public class combinedHeightHeat : IMap
{
    public enum HeatLevel
    {
        Coldest,
        Colder,
        Cold,
        Warm,
        Warmer,
        Warmest
    }
    private float[,] noiseData;
    private Cache combinedCache;
    private Add combo;
    public combinedHeightHeat(IMap heightMap, IMap heatMap, worldChunkSettings chunkSettings)
    {
        
        Const black = new Const(-1);
        Select heightselect = new Select(heightMap.GetCache(), black,heightMap.GetCache());
        heightselect.SetBounds(.5, 1, .2);
        this.combo = new Add(heatMap.GetCache(), heightselect);
        //Invert invert = new Invert(combo);

        Noise2D final = new Noise2D(chunkSettings.mapWidth, chunkSettings.mapHeight, combo);
        final.GeneratePlanar(chunkSettings.left, chunkSettings.right, chunkSettings.top, chunkSettings.bottom, isSeamless:true);
        combinedCache = new Cache(combo);
        noiseData = final.GetData();
    }
        
        
    public float GetValue(int x, int y)
    {
        return (float)noiseData[x, y];
    }
    public int GetSize()
    {
      return noiseData.GetLength(0);
    }

    public Color GetColor(int x, int y)
    {
        float normalizedColor = (noiseData[x, y] + 1.0f) / 2.0f;
        float r = normalizedColor;
        float g = normalizedColor;
        float b = normalizedColor;
        Color newcolor = new Color(r, g, b, 1);
        return newcolor;
    }

    public HeatLevel GetHeat(int x, int y)
    {
        float normalizedHeat = (noiseData[x, y] + 1.0f) / 2.0f;
        if (normalizedHeat <= .05f)
        {
            return HeatLevel.Coldest;
        }
        else if (normalizedHeat <= .18f)
        {
            return HeatLevel.Colder;
        }
        else if (normalizedHeat<=.4f)
        {
            return HeatLevel.Colder;
        }
        else if (normalizedHeat<=.6f)
        {
            return HeatLevel.Warm;
        }
        else if (normalizedHeat<=.8f)
        {
            return HeatLevel.Warmer;
        }
        return HeatLevel.Warmest;
    }
    public Cache GetCache()
    {
        return combinedCache;
    }
}
