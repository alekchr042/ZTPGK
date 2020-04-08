using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Terrain terrain;
    public GameObject mushroomPrefab;

    private int terrWidth;
    private int terrHeight;
    private float gameTime = 180.0f;
    //private float gameTime = 10.0f;

    private float timeLeft;
    private bool isGameActive;
    // Start is called before the first frame update
    void Start()
    {
        terrWidth = terrain.terrainData.heightmapWidth;
        terrHeight = terrain.terrainData.heightmapHeight;
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameActive)
        {
            timeLeft = timeLeft - Time.deltaTime;

            if (timeLeft < 0)
            {
                timeLeft = 0;
                StopGame();
            }
        }

        if(!isGameActive && Input.GetKeyDown(KeyCode.R))
        {
            StartGame();
        }
    }

    private void StartGame()
    {
        GenerateMushrooms();
        isGameActive = true;
        timeLeft = gameTime;
        PlayerPrefs.SetInt("PlayerResult", 0);
    }

    private void StopGame()
    {
        isGameActive = false;

        gameObject.GetComponentsInChildren<MushroomCollision>().ToList().ForEach(x =>
        {
            Destroy(x.gameObject);
        });

        var bestResult = PlayerPrefs.GetInt("BestResult", 0);
        var currentResult = PlayerPrefs.GetInt("PlayerResult", 0);
        if (currentResult > bestResult)
        {
            PlayerPrefs.SetInt("BestResult", currentResult);
        }
    }

    private void GenerateMushrooms()
    {
        for(int i=0; i<1000; i++)
        {
            int x = UnityEngine.Random.Range(0, terrWidth);
            int z = UnityEngine.Random.Range(0, terrHeight);
            float y =5;

            var vector = new Vector3(x, y, z);

            var newMushroom = (GameObject)Instantiate(mushroomPrefab, vector, new Quaternion(), transform);
        }
    }

    void OnGUI()
    {
        var currentResult = PlayerPrefs.GetInt("PlayerResult", 0);
        var bestResult = PlayerPrefs.GetInt("BestResult", 0);
        //Fetch the PlayerPrefs settings and output them to the screen using Labels
        GUI.Label(new Rect(50, 50, 200, 30), "Score : " + currentResult);
        GUI.Label(new Rect(50, 30, 200, 30), "Time left : " + Math.Round(timeLeft));
        GUI.Label(new Rect(50, 10, 200, 30), "Best result : " + bestResult);

        if(!isGameActive)
        {
            GUI.Label(new Rect(250, 100, 200, 30), "Game over! Press R to retry.", new GUIStyle() { fontSize = 50});
            GUI.Label(new Rect(500, 150, 200, 30), "Score : " + currentResult, new GUIStyle() { fontSize = 30 });
        }
    }
}
