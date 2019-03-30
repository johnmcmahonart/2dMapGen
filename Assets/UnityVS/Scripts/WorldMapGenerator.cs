using UnityEngine;
using System.Collections;
using LibNoise.Generator;
using LibNoise.Operator;
using LibNoise;
using System.IO;
public class WorldMapGenerator : MonoBehaviour
{
    //we need a bunch of vars
    private moistureMap MoistureMap;
    private combinedHeightHeat ComboMap;
    private heightMap HeightMap;
    private heatMap HeatMap;
    private biomeMap BiomeMap;
    private GameObject HeightDisplay; 
    private GameObject HeatDisplay;
    private GameObject ComboDisplay;
    private GameObject MoistureDisplay;
    private GameObject BiomeDisplay;
    private heightMapSettings hmSettings { get; set; }
    private worldChunkSettings thisChunk { get; set; }
    private moistureMapSettings mmSettings { get; set; }
    [SerializeField]private int octaves;
    [SerializeField]private double frequency;
    [SerializeField]private double lacunarity;
    [SerializeField]private QualityMode quality;
    [SerializeField]private int seed;
    [SerializeField]private double persistence;
    [SerializeField]private int MMoctaves;
    [SerializeField]private double MMfrequency;
    [SerializeField]private double MMlacunarity;
    [SerializeField]private QualityMode MMquality;
    [SerializeField]private int MMseed;
    [SerializeField]private double MMpersistence;
    [SerializeField]private int _mapWidth;
    [SerializeField]private int _mapHeight;

    public bool autoUpdate;
    public bool maskWater;
    public void BuildMap()
    {
        //initialzing settings objects

        hmSettings = new heightMapSettings
        {
            Octaves = octaves,
            Frequency = frequency,
            Lacunarity = lacunarity,
            Quality = quality,
            Seed = seed,
            Persistence = persistence
        };
        thisChunk = new worldChunkSettings //need to move this out of this class for tiling world chunks
        {
            mapHeight = _mapHeight,
            mapWidth = _mapWidth,
            top = 0,
            bottom = 1,
            left = 2,
            right = 3
        };
        mmSettings = new moistureMapSettings
        {
            Octaves = MMoctaves,
            Frequency = MMfrequency,
            Lacunarity = MMlacunarity,
            Quality = MMquality,
            Seed = MMseed,
            Persistence = MMpersistence
        };

        //build maps
        HeightMap = new heightMap(thisChunk, hmSettings);
        HeatMap = new heatMap(thisChunk);
        ComboMap = new combinedHeightHeat(HeightMap, HeatMap,thisChunk);
        MoistureMap = new moistureMap(thisChunk, mmSettings, HeightMap);
        BiomeMap = new biomeMap(HeightMap, ComboMap, MoistureMap, maskWater);
        //Debug.Log("map seed "+hmSettings.Seed);
        //find game objects and build textures for each map
        HeightDisplay = GameObject.Find("HeightMapObj");
        HeightDisplay.GetComponent<Renderer>().sharedMaterial.mainTexture = textureFromMap.BuildTexture(HeightMap);
        //Debug.Log("Perlin point:" + HeightMap.GetValue(100, 100));
        
        HeatDisplay = GameObject.Find("HeatMapObj");
        HeatDisplay.GetComponent<Renderer>().sharedMaterial.mainTexture = textureFromMap.BuildTexture(HeatMap);

        //Debug.Log("Gradient Noise value:" + HeatMap.GetValue(0, 10));
        ComboDisplay = GameObject.Find("ComboMapObj");
        ComboDisplay.GetComponent<Renderer>().sharedMaterial.mainTexture = textureFromMap.BuildTexture(ComboMap);

        MoistureDisplay = GameObject.Find("MoistureMapObj");
        MoistureDisplay.GetComponent<Renderer>().sharedMaterial.mainTexture = textureFromMap.BuildTexture(MoistureMap);

        BiomeDisplay = GameObject.Find("BiomeMapObj");
        BiomeDisplay.GetComponent<Renderer>().sharedMaterial.mainTexture = textureFromMap.BuildTexture(BiomeMap);
        

    }
    /* not possible to load and save settings and have those reflected in unity editor without significant work
    public void SaveSettings() //save UI settings to file
    {
        binaryData.Write("hmsettings.bin",hmSettings);
        binaryData.Write("thischunksettings.bin", thisChunk);
        binaryData.Write("mmsettings.bin", mmSettings);

    }
    
    public void LoadSettings() //load UI settings
    {
        hmSettings = binaryData.Read<heightMapSettings>("hmsettings.bin");
        thisChunk = binaryData.Read<worldChunkSettings>("thischunksettings.bin");
        mmSettings = binaryData.Read<moistureMapSettings>("mmsettings.bin");
        //Debug.Log("Seed " + hmSettings.Seed);
    }
    */
    public void SavePng() 
    {
        var tex = textureFromMap.BuildTexture(BiomeMap);
        byte[] bytes = tex.EncodeToPNG();
        File.WriteAllBytes("biomeMap.png", bytes);
    }

}

