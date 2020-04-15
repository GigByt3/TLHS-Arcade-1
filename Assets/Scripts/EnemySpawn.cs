using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Runtime.InteropServices.ComVisible(true)]
[System.Serializable]

public class EnemySpawn : MonoBehaviour
{
    public GameObject levelEnemy;
    public int DangerZone = 0;
    public int gameLevel;
    private int[] quadrants; // num of enemies in: Q1 (+,+), Q2 (-,+), Q3 (-,-), Q4 (+,-)

    void Start()
    {
        InvokeRepeating("ChangeDangerZone", 20.0f, 120.0f);
    }

    // Update is called once per frame
    void Update()
    {
        quadrants = new int[] { 0, 0, 0, 0 };

        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            switch (getQuadrant(enemy))
            {
                case "Q1" : quadrants[0]++;
                    break;

                case "Q2":
                    quadrants[1]++;
                    break;

                case "Q3":
                    quadrants[2]++;
                    break;

                case "Q4":
                    quadrants[3]++;
                    break;
            }
        }

        Debug.Log(DangerZone);

        for(int count = 0; count < 4; count++)
        {
            if (count == DangerZone)
            {
                if (quadrants[count] < (gameLevel - (8/(gameLevel - 8))))
                {
                    spawnEnemy(count);
                }
            }
            else
            {
                if (quadrants[count] < gameLevel)
                {
                    spawnEnemy(count);
                }
            }
        }
    }

    private string getQuadrant(GameObject enemy)
    {
        if (enemy.transform.position.x >= 0 && enemy.transform.position.z >= 0)
            return "Q1";
        if (enemy.transform.position.x >= 0 && enemy.transform.position.z <= 0)
            return "Q2";
        if (enemy.transform.position.x <= 0 && enemy.transform.position.z <= 0)
            return "Q3";
        if (enemy.transform.position.x <= 0 && enemy.transform.position.z >= 0)
            return "Q4";
        else
            return "Q1";
    }

    private void spawnEnemy(int position)
    {
        switch (position)
        {
            case 0:
                Instantiate(levelEnemy, getPosition(new int[] { 1, 1}), levelEnemy.transform.rotation);
                break;

            case 1:
                Instantiate(levelEnemy, getPosition(new int[] { 1, -1}), levelEnemy.transform.rotation);
                break;

            case 2:
                Instantiate(levelEnemy, getPosition(new int[] { -1, -1}), levelEnemy.transform.rotation);
                break;

            case 3:
                Instantiate(levelEnemy, getPosition(new int[] { -1, 1}), levelEnemy.transform.rotation);
                break;
        }
    }

    private Vector3 getPosition(int[] quadrant)
    {
        System.Random rnd = new System.Random();
        return new Vector3(quadrant[0] * (2.5f+(rnd.Next(0, 5) * 5)), 2.5f, quadrant[1] * (2.5f + (rnd.Next(0, 5) * 5)));
    }

    void ChangeDangerZone()
    {
        DangerZone++;
        DangerZone%=4;
    }
}
