using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TileManager : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    public GameObject[] levelHardTilePrefabs;
    public GameObject[] levelComplexTilePrefabs;
    public GameObject[] levelBadComplexTilePrefabs;
    public GameObject[] levelDeathTilePrefabs;
    public GameObject[] levelHellTilePrefabs;
    private Transform playerTransform;
    private List<GameObject> activateTiles;
    private float spanZ = -100.0f;
    private float safeZone = 100.0f;
    private float tileLength = 100.0f;
    private int amtTilesOnScreen = 3;

    float timer = 0.0f;

   
    private enum GameLevel { EASY, HARD, COMPLEX, BADCOMPLEX, DEATH, HELL };
    private GameLevel gameLevel;
    // Start is called before the first frame update
    void Start()
    {
        gameLevel = GameLevel.EASY;
        activateTiles = new List<GameObject>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        for (int i = 0; i < amtTilesOnScreen; i++)
        {
            SpawnTile(0);
        }
        
    }

    private void UpdateLevel()
    {
        // get the distance and update the game level
        timer += Time.deltaTime;

        //Debug.Log(timer);
        //Debug.Log(gameLevel);
        //Debug.Log(activateTiles);
        if (timer > 60.0f)
        {
            //Player.GetInstance().SetSpeed(1);
            gameLevel = GameLevel.HARD;
        }

        if (timer > 180.0f)
        {
            
            gameLevel = GameLevel.COMPLEX;
        }

        if (timer > 240.0f)
        {
            
            gameLevel = GameLevel.BADCOMPLEX;
        }

        if (timer > 320.0f)
        {
            
            gameLevel = GameLevel.DEATH;
        }

        if (timer > 460.0f)
        {
            
            gameLevel = GameLevel.HELL;
        }

    }
    // Update is called once per frame
    void Update()
    {
        // update level
        UpdateLevel();

        if (playerTransform.position.z - safeZone > (spanZ - amtTilesOnScreen * tileLength))
        {
            SpawnTile();
            DeleteTile();
        }
    }

    private void SpawnTile(int prefabIndex = -1)
    {
        GameObject go;
        go = Instantiate(tilePrefabs[0]) as GameObject;
        if (prefabIndex != -1)
        {
            go = Instantiate(tilePrefabs[prefabIndex]) as GameObject;
        }
        else
        {
            if (gameLevel == GameLevel.EASY)
            {
                go = Instantiate(tilePrefabs[RandomTile()]) as GameObject;
            }
            if (gameLevel == GameLevel.HARD)
            {
                go = Instantiate(levelHardTilePrefabs[RandomTile()]) as GameObject;
            }

            if (gameLevel == GameLevel.COMPLEX)
            {
                go = Instantiate(levelComplexTilePrefabs[RandomTile()]) as GameObject;
            }

            if (gameLevel == GameLevel.BADCOMPLEX)
            {
                go = Instantiate(levelBadComplexTilePrefabs[RandomTile()]) as GameObject;
            }

            if (gameLevel == GameLevel.DEATH)
            {
                go = Instantiate(levelDeathTilePrefabs[RandomTile()]) as GameObject;
            }

            if (gameLevel == GameLevel.HELL)
            {
                go = Instantiate(levelHellTilePrefabs[RandomTile()]) as GameObject;
            }
        }

        go.transform.SetParent(transform);
        go.transform.position = Vector3.forward * spanZ;
        spanZ += tileLength;
        activateTiles.Add(go);
    }

    private void DeleteTile()
    {
        Destroy(activateTiles[0]);
        activateTiles.RemoveAt(0);
    }

    private int RandomTile()
    {
        switch (gameLevel)
        {
            case GameLevel.EASY:
                return Random.Range(0, tilePrefabs.Length);
            case GameLevel.HARD:
               
                return Random.Range(0, levelHardTilePrefabs.Length);
            case GameLevel.COMPLEX:
                return Random.Range(0, levelComplexTilePrefabs.Length);
            case GameLevel.BADCOMPLEX:
                return Random.Range(0, levelBadComplexTilePrefabs.Length);
            case GameLevel.DEATH:
                return Random.Range(0, levelDeathTilePrefabs.Length);
            case GameLevel.HELL:
                return Random.Range(0, levelHellTilePrefabs.Length);
        }
        //if(tilePrefabs.Length <=1)
        //{
        //    return 0;
        //}
        return 0;
    }
}
