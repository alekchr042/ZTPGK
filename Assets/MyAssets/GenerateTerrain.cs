using System;
using UnityEngine;

public class GenerateTerrain : MonoBehaviour
{
    Terrain terrain;
    TerrainData tData;
    int xRes, yRes, xMiddle, yMiddle;

    float[,] originalTerrainSectionHeight;

    int xStart, xFinish, yStart, yFinish;

    public int radius;
    // Start is called before the first frame update
    void Start()
    {
        terrain = transform.GetComponent<Terrain>();
        tData = terrain.terrainData;
        xRes = tData.heightmapWidth;
        yRes = tData.heightmapHeight;

        xMiddle = UnityEngine.Random.Range(0 + radius, xRes - radius);
        yMiddle = UnityEngine.Random.Range(0 + radius, yRes - radius);

         xStart = xMiddle - radius;
         xFinish = (xMiddle + radius) > xRes ? xRes : xMiddle + radius;
         yStart = yMiddle - radius;
         yFinish = (yMiddle + radius) > yRes ? yRes : yMiddle + radius;

        resetPoints();
        generateRandomTerrain();
    }

    // Update is called once per frame
    void Update()
    {
        animateTerrain();
    }

    private void generateRandomTerrain()
    {
        var tmpTerrain = tData.GetHeights(0, 0, xRes, yRes);
        var randomizer = UnityEngine.Random.Range(0.1f, 2);

        for (int i = 0; i < xRes; i++)
        {
            for (int j = 0; j < yRes; j++)
            {
                float xCoeff = (float)i / xRes;
                float yCoeff = (float)j / yRes;
                tmpTerrain[i, j] = 0;
                for (int k = 0; k < 5; k++)
                {
                    tmpTerrain[i, j] += Mathf.PerlinNoise(xCoeff * randomizer, yCoeff * randomizer);
                }
                tmpTerrain[i, j] /= (float)5;
            }
        }
        tData.SetHeights(0, 0, tmpTerrain);
        originalTerrainSectionHeight = tData.GetHeights(xStart, yStart, radius * 2, radius * 2);
    }

    void animateTerrain()
    {

        var tmpHeights  = tData.GetHeights(xStart, yStart, radius * 2, radius * 2);
        for (int x = xStart, i = 0; x < xFinish; x++, i++)
        {
            for (int y = yStart, j = 0; y < yFinish; y++, j++)
            {
                try
                {
                    Vector2 point = new Vector2(x, y);
                    double distance = Vector2.Distance(point, new Vector2(xMiddle, yMiddle));
                    double difference = (radius - distance) / radius;
                    if (difference < 0) difference = 0;
                    tmpHeights[i, j] = (float)(originalTerrainSectionHeight[i, j] * (Mathf.Sin(Time.time + (float)distance * 2.0f) / 10.0f) * difference / 2.0f) + (float)difference * 0.1f + originalTerrainSectionHeight[i, j];
                }
                catch (IndexOutOfRangeException)
                {
                    Debug.Log(String.Format("Index out of range, x={0}, y={1}", i, j));
                }
            }
        }
        tData.SetHeights(xStart, yStart, tmpHeights);

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
}
