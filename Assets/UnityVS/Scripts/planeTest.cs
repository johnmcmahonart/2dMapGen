using UnityEngine;
using System.Collections;
using LibNoise;
using LibNoise.Generator;
public class planeTest : MonoBehaviour {

    // Use this for initialization
    private float[,] ppnoise {get; set;}
    void Start () {
        
        var rawPerlin = new Perlin();
        rawPerlin.OctaveCount = 6;
        rawPerlin.Frequency = 3;
        var planarPerlin = new Noise2D(64, 64, rawPerlin);
        planarPerlin.GeneratePlanar(0,1,0,1);
        ppnoise = planarPerlin.GetData();
        Debug.Log("Raw perlin:"+rawPerlin.GetValue(.5, .5, .5));
        Debug.Log("Projected perlin:"+ppnoise[1, 1]);

           

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
