using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingItem : MonoBehaviour
{

    protected float vx; //x velocity
    protected float vy; //y velocity
    protected Vector3 velocity; //Velocity
    protected Vector3 tempVel;
    protected static bool paused = false;

    protected void Move()
    {
        if (paused) return;
        transform.position = transform.position + velocity;
    }

    public static void PauseGame()
    {
        paused = true;
    }

    public static void ResumeGame()
    {
        paused = false;
    }

    public static void RestartGame()
    {
        EnemyMove[] en = FindObjectsOfType(typeof(EnemyMove)) as EnemyMove[];
        for(int i = 0; i < en.Length; i++) {
            Destroy(en[i].gameObject);
        }

        ProjectileMove[] pm = FindObjectsOfType(typeof(ProjectileMove)) as ProjectileMove[];
        for (int i = 1; i < pm.Length; i++)
        {
            Destroy(pm[i].gameObject);
        }

        paused = false;
    }

    public static void GameOver()
    {
        //Destroy(this);
    }

    public static bool GetPaused()
    {
        return paused;
    }
}
