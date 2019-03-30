using UnityEngine;
using System.Collections;
using UnityEditor;
[CustomEditor (typeof (WorldMapGenerator))]
public class GenerateWorldMap : Editor {
    public override void OnInspectorGUI()
    {
       WorldMapGenerator worldGen = (WorldMapGenerator)target;
        if (DrawDefaultInspector());
        {
            if (worldGen.autoUpdate)
            {
                worldGen.BuildMap();
            }
        }
        if (GUILayout.Button("Build World Map"))
        {
            worldGen.BuildMap();
        }
        /*
        if (GUILayout.Button("Save Settings"))
        {
            
            binaryData.Write("hmsettings.bin", worldGen.hmSettings);
            binaryData.Write("thischunksettings.bin", worldGen.thisChunk);
            binaryData.Write("mmsettings.bin", worldGen.mmSettings);
            
            worldGen.SaveSettings();
    }   
        if (GUILayout.Button("Load Settings"))
        {
            
            heightMapSettings hmSettings =binaryData.Read<heightMapSettings>("hmsettings.bin");
            worldGen.hmSettings = hmSettings;
            worldChunkSettings thisChunk = binaryData.Read<worldChunkSettings>("thischunksettings.bin");
            moistureMapSettings mmSettings = binaryData.Read<moistureMapSettings>("mmsettings.bin");
            
            worldGen.LoadSettings();
        }    
*/    
    if (GUILayout.Button("Save PNG"))
        {
            worldGen.SavePng();
        }
        
    }
}
