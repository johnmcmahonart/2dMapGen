using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using LibNoise.Operator;
public interface IMap
{
    float GetValue(int x, int y);
    int GetSize();
    Color GetColor(int x, int y);
    Cache GetCache();
}
