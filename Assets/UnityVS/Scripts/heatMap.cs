using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibNoise;
using LibNoise.Generator;
using LibNoise.Operator;
using UnityEngine;
public class heatMap : IMap

{
    private Cache heatCache;
    private float[,] noiseData;
    public heatMap(worldChunkSettings chunkSettings)

    {
        fill(chunkSettings);
    }
    
    public float GetValue(int x, int y)
    {
        return (float)(noiseData[x, y]);
    }
    private void fill(worldChunkSettings chunkSettings)

    {
        var min = chunkSettings.top;
        var max = chunkSettings.bottom;
        gradientHack baseMap = new gradientHack(min, max);
        Turbulence distortion = new Turbulence(.05, baseMap);
        //Curve lower = new Curve(distortion);
        //lower.Add(-.6, -.8);
        //lower.Add(-.2, -.6);
        //lower.Add(0.0, .2);
        this.heatCache = new Cache(distortion);

        Noise2D final = new Noise2D(chunkSettings.mapHeight, chunkSettings.mapWidth, distortion);
        final.GeneratePlanar(chunkSettings.left, chunkSettings.right, chunkSettings.top, chunkSettings.bottom, isSeamless:true);
        noiseData = final.GetData();
                
    }
    public int GetSize() //size is assumed to be square, may need to updae later
    {
        return noiseData.GetLength(0);
    }
    public Color GetColor(int x, int y)
    {
        var r = ((noiseData[x, y] + 1.0f) / 2.0f);
        var g = ((noiseData[x, y] + 1.0f) / 2.0f);
        var b = ((noiseData[x, y] + 1.0f) / 2.0f);
        Color newcolor = new Color(r, g, b,1);
        return newcolor;
    }

    public Cache GetCache()
    {
        return heatCache;
    }
}


