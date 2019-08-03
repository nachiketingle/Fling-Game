using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reload : MonoBehaviour
{
    public GameObject proj; //Projectile

    private static Reload myReload; //Instance of Reload script
    private Score score;

    private Vector3 spawnLoc; //Spawn location
    private float dt = 0.5F; //delta time(sec); how often we spawn

    private float spawnTime;
    private bool setTime;
    private bool needProj;

    // Start is called before the first frame update
    void Start()
    {
        spawnLoc = new Vector3(0, -3, 0);
        //Instantiate(proj, spawnLoc, transform.rotation);
        setTime = false;

        score = Score.Instance();
        needProj = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (MovingItem.GetPaused())
            return;

        if(needProj)
        {
            if(!setTime) //If we did not get initial time, do so
            {
                spawnTime = Time.time;
                setTime = true;
            }
            else if(Time.time - spawnTime >= dt) //Once dt has passed, create new proj
            {
                //Instantiate(proj, transform);
                Instantiate(proj, spawnLoc, transform.rotation);
                needProj = false;
                setTime = false;
            }

        }

    }

    public void newProj()
    {
       
        needProj = true;
    }

    //Returns an instance of the script
    public static Reload Instance()
    {
        if (!myReload)
        {
            myReload = FindObjectOfType(typeof(Reload)) as Reload;
            if (!myReload)
                Debug.LogError("There needs to be one active Reload script");
        }

        return myReload;
    }
}

