using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public static class textureFromMap
{
    //build texture from color values
    public static Texture2D BuildTexture(IMap map)
    {
        var size = map.GetSize();
        //Debug.Log("size of arry:"+size);
        var texture = new Texture2D(size, size, TextureFormat.ARGB32, false);
        var pixiels = new Color[size * size];
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                pixiels[x + y * size] = map.GetColor(x,y);
            }
        }

    //Debug.Log("Color data:" + pixiels[34 * size]);
    texture.SetPixels(pixiels);
    texture.Apply();
    return texture;
    }
}    
    

