using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using LibNoise.Operator;
using LibNoise.Generator;
public class biomeMap:IMap

//biome map is same as height map but we change color based on moisture and heat

{
    private biomes[,] whittakerGraph = new biomes[6, 6]
    {
        {biomes.Ice,biomes.Tundra,biomes.Grassland,biomes.Desert,biomes.Desert,biomes.Desert},
        {biomes.Ice,biomes.Tundra,biomes.Grassland,biomes.Desert,biomes.Desert,biomes.Desert},
        {biomes.Ice,biomes.Tundra,biomes.Woodland,biomes.Woodland,biomes.Savanna,biomes.Savanna},
        {biomes.Ice,biomes.Tundra,biomes.BorealForest,biomes.Woodland,biomes.Savanna,biomes.Savanna},
        {biomes.Ice,biomes.Tundra,biomes.BorealForest,biomes.SeasonalForest,biomes.TropicalRainforest,biomes.TropicalRainforest},
        {biomes.Ice,biomes.Tundra,biomes.BorealForest,biomes.TemperateRainforest,biomes.TropicalRainforest,biomes.TropicalRainforest}
    };
    private IMap _heightmap;
    private combinedHeightHeat _comboMap;
    private moistureMap _moistureMap;
    private bool _maskWater;

    private enum biomes
    {
        Desert,
        Savanna,
        TropicalRainforest,
        Grassland,
        Woodland,
        SeasonalForest,
        TemperateRainforest,
        BorealForest,
        Tundra,
        Ice
    }
    public biomeMap(IMap Heightmap, combinedHeightHeat ComboMap, moistureMap MoistureMap, bool MaskWater)

    {
        this._heightmap = Heightmap;
        this._comboMap = ComboMap;
        this._moistureMap = MoistureMap;
        this._maskWater = MaskWater;
    }
  
    public float GetValue(int x, int y)
    {
        return (float)(_heightmap.GetValue(x,y)); //value for biome map should be same as base height map
    }
    
    public int GetSize() //size is assumed to be square, may need to updae later
    {
        return _heightmap.GetSize();
    }
    public Color GetColor(int x, int y)
    {
        var moistureLevel = _moistureMap.GetMoisture(x, y);
        var heatLevel = _comboMap.GetHeat(x, y);
        var biome = whittakerGraph[(int)moistureLevel, (int)heatLevel];
        /*
        if (_heightmap.GetColor(x,y)==Color.black) //if the height level is void we don't want it to be affected by moisture or heat
        {
            return Color.black;
        }
    */    
        if (_heightmap.GetColor(x, y) == extendedColors.DeepColor & !_maskWater) //account for water
        {
            return extendedColors.DeepColor;
        }
        else if (_heightmap.GetColor(x, y) == extendedColors.ShallowColor &!_maskWater)
            return extendedColors.ShallowColor;

        else if (_heightmap.GetColor(x, y) == extendedColors.ShallowColor & _maskWater)
            return Color.black;
        else if (_heightmap.GetColor(x, y) == extendedColors.DeepColor & _maskWater)
            return Color.black;
        else switch (biome) //color per biome
        {
            case biomes.Ice:
                return extendedColors.Ice;
            case biomes.BorealForest:
                return extendedColors.BorealForest;
            case biomes.Desert:
                return extendedColors.Desert;
            case biomes.Grassland:
                return extendedColors.Grassland;
            case biomes.Savanna:
                return extendedColors.Savanna;
            case biomes.SeasonalForest:
                return extendedColors.SeasonalForest;
            case biomes.TemperateRainforest:
                return extendedColors.TemperateRainforest;
            case biomes.TropicalRainforest:
                return extendedColors.TropicalRainforest;
             case biomes.Tundra:
                return extendedColors.Tundra;
             case biomes.Woodland:
                return extendedColors.Woodland;
         }
        
        return Color.black;   
    }
    public Cache GetCache()
    {
        return _heightmap.GetCache();
    }

}

