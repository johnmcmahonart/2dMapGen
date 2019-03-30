using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibNoise;
using LibNoise.Generator;
using LibNoise.Operator;
using UnityEngine;
public class heightMap : IMap

{
    private Cache heightCache;
    private float[,] noiseData;
    public heightMap (worldChunkSettings chunkSettings, heightMapSettings settings)
        
    {
        fill(chunkSettings, settings);
    }
    /*
  fix this later, would be good to check if settings object is passed and if not create default values
        
        public heightMap(int _mapHeight, int _mapWidth)
    {
        settings = new heightMapSettings;

        fill(_mapHeight, _mapWidth);
    }
*/
    public float GetValue(int x, int y)
    {
        return (float)(noiseData[x, y]);
    }
    private void fill(worldChunkSettings chunkSettings, heightMapSettings settings)
            
    {
        RidgedMultifractal baseMap = new RidgedMultifractal();
        baseMap.OctaveCount = settings.Octaves;
        baseMap.Frequency = settings.Frequency;
        baseMap.Lacunarity = settings.Lacunarity;
        baseMap.Seed = settings.Seed;

        Perlin layer2 = new Perlin();
        layer2.OctaveCount = settings.Octaves;
        layer2.Frequency = settings.Frequency;
        layer2.Lacunarity = settings.Lacunarity;
        layer2.Seed = settings.Seed;
        Billow controller = new Billow();
        controller.Frequency = settings.Frequency;
        Blend blend = new Blend(baseMap,layer2,controller);
        Invert invert = new Invert(blend); //makes heightmap more island like
        Clamp oceanfix = new Clamp(-.7, 1, invert); //prevent ocean floor from being to low
        //Const black = new Const(-1);
        // holes in surface at top of mountains
        //Select holes = new Select(oceanfix, black, oceanfix);
        //holes.SetBounds(.85, 1, .01); //get highest elevation with little falloff
        this.heightCache = new Cache(oceanfix);

        var final = new Noise2D(chunkSettings.mapHeight, chunkSettings.mapWidth, oceanfix);
        //turbulance = new Turbulence(12, baseMap); 
        final.GeneratePlanar(chunkSettings.left, chunkSettings.right, chunkSettings.top, chunkSettings.bottom, isSeamless: true);
        //final.GenerateSpherical(1, 3,1, 3);
        /*
        for (int x=0; x<mapWidth; x++)
        {
            for (int y=0;y<mapHeight;y++)
            {
                noiseData[x,y] = (float)(baseMap.GetValue(x,0,y));
            }
        }
        */
        noiseData = final.GetData();
    }   
    public int GetSize() //size is assumed to be square, may need to updae later
    {
        return noiseData.GetLength(0);
    }
    public Color GetColor(int x, int y)
    {
        float normalizedColor = (noiseData[x, y] + 1.0f) / 2.0f;

        if (normalizedColor <=.1)
        {
            return Color.black;
        }
        else if (normalizedColor <= .3)
        {
            return extendedColors.DeepColor;
        }
        else if (normalizedColor <= .4)
        {
            return extendedColors.ShallowColor;
        }
        else if (normalizedColor <= .5)
        {
            return extendedColors.SandColor;
        }
        else if (normalizedColor <= .6)
        {
            return extendedColors.GrassColor;
        }
        else if (normalizedColor <= .7)
        {
            return extendedColors.ForestColor;
        }
        else if (normalizedColor <= .8)
        {
            return extendedColors.RockColor;
        }
        
        return extendedColors.SnowColor;
    /*
        float r = normalizedColor;
        float g = normalizedColor;
        float b = normalizedColor;
        Color newcolor = new Color(r, g, b, 1);
        return newcolor;
    */
    }
    public Cache GetCache()
    {
        return heightCache;
    }


}


