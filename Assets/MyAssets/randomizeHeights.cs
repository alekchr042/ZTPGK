using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class randomizeHeights : MonoBehaviour
{
    // Start is called before the first frame update
    Terrain terrain;
    TerrainData tData;
    int xRes;
    int yRes;
    float[,] heights;
    public Texture2D texture;
    void Start()
    {
        terrain = transform.GetComponent<Terrain>();
        tData = terrain.terrainData;
        xRes = tData.heightmapWidth;
        yRes = tData.heightmapHeight;
        //resetPoints();
        //readHeightsFromFile();
    }
    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 25), "Wrinkle"))
        {
            randomizePoints(0.1f);
        }
        if (GUI.Button(new Rect(10, 40, 100, 25), "Reset"))
        {
            resetPoints();
        }
        if (GUI.Button(new Rect(10, 70, 100, 25), "Read file"))
        {
            readHeightsFromFile(0.1f);
        }
    }

    void readHeightsFromFile(float strength)
    {
        heights = tData.GetHeights(0, 0, xRes, yRes);

        for (int y = 0; y < yRes; y++)
        {
            for (int x = 0; x < xRes; x++)
            {
                var a = texture.GetPixel(x, y);
                var value = (a.grayscale == 0) ? 1 : 0;
                heights[y, x] = value * strength;
            }
            tData.SetHeights(0, 0, heights);
        }

    }
    void randomizePoints(float strength)
    {
        heights = tData.GetHeights(0, 0, xRes, yRes);
        for (int y = 0; y < yRes; y++)
        {
            for (int x = 0; x < xRes; x++)
            {
                heights[x, y] = Random.Range(0.0f, strength) * 0.5f;
            }
            tData.SetHeights(0, 0, heights);
        }
    }
    void resetPoints()
    {
        var heights = tData.GetHeights(0, 0, xRes, yRes);
        for (int y = 0; y < yRes; y++)
        {
            for (int x = 0; x < xRes; x++)
            {
                heights[x, y] = 0;
            }
        }
        tData.SetHeights(0, 0, heights);
    }
    // Update is called once per frame
    void Update()
    {
    }
}

