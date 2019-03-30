using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
[Serializable]
public class worldChunkSettings
{
    public int mapHeight { get; set; }
    public int mapWidth { get; set; }
    public int top { get; set; }
    public int bottom { get; set; }
    public int left { get; set; }
    public int right { get; set; }
}

