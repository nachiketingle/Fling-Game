using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemy; //Enemy prefab
    private static Spawner mySpawner; //Instance of Spawner script

    private float dt = 2; //delta time(sec); how often we spawn
    private float spawnTime;

    private int counter = 0; //Used for debugging

    // Start is called before the first frame update
    void Start()
    {
        spawnTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (MovingItem.GetPaused())
            return;

        counter++;

        //Spawn an enemy
        if (Time.time - spawnTime >= dt)
        {
            
            spawnTime = Time.time;
            Instantiate(enemy, transform);
        }
    }

    public static void RestartGame()
    {
        
    }

    //Returns an instance of the script
    public static Spawner Instance()
    {
        if (!mySpawner)
        {
            mySpawner = FindObjectOfType(typeof(Spawner)) as Spawner;
            if (!mySpawner)
                Debug.LogError("There needs to be one active Reload script");
        }

        return mySpawner;
    }

    private void OnGUI()
    {
        string message = "";

        message += MovingItem.GetPaused();
        message += "\n" + spawnTime;
        message += "\n" + counter;

        GUI.Label(new Rect(300, 50, 120, 100), message);
    }
}
