﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Level_Generator : MonoBehaviour
{
    //Define the wall prefab
    public GameObject wall;

    //Define the quarter container prefabs
    public GameObject q1, q2, q3, q4;
    
    //The number of different options for each quarter
    private int q1Maps, q2Maps, q3Maps, q4Maps;

    //Which option was actually picked by the RNG
    private int q1MapPick, q2MapPick, q3MapPick, q4MapPick;

    void Start()
    {
        GenerateMaps();
        PickMaps();
        GenerateQ1();
        GenerateQ2();
        GenerateQ3();
        GenerateQ4();
        CombineMeshes();
    }

    //Define how many options there are for each quarter
    public void GenerateMaps()
    {
        q1Maps = 4;
        q2Maps = 4;
        q3Maps = 4;
        q4Maps = 4;
    }

    //Pick the random number defining each quarter
    public void PickMaps()
    {
        q1MapPick = Random.Range(0, q1Maps);
        q2MapPick = Random.Range(0, q2Maps);
        q3MapPick = Random.Range(0, q3Maps);
        q4MapPick = Random.Range(0, q4Maps);

        Debug.Log("Q1: " + q1MapPick);
        Debug.Log("Q2: " + q2MapPick);
        Debug.Log("Q3: " + q3MapPick);
        Debug.Log("Q4: " + q4MapPick);
    }

    //Instaniate the different walls for the chosen quarter 1 option, parented to the quarter container
    public void GenerateQ1()
    {
        switch (q1MapPick)
        {
            case 0:
                Instantiate(wall, new Vector3(-20, 1, 22.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q1.transform); //row 1

                Instantiate(wall, new Vector3(-12.5f, 1, 20), Quaternion.Euler(0.0f, 90.0f, 0.0f), q1.transform); //row 1.5
                Instantiate(wall, new Vector3(-7.5f, 1, 20), Quaternion.Euler(0.0f, 90.0f, 0.0f), q1.transform);

                Instantiate(wall, new Vector3(-20, 1, 17.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q1.transform); //row 2
                Instantiate(wall, new Vector3(-15, 1, 17.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q1.transform);

                Instantiate(wall, new Vector3(-7.5f, 1, 15), Quaternion.Euler(0.0f, 90.0f, 0.0f), q1.transform); //row 2.5
                Instantiate(wall, new Vector3(-2.5f, 1, 15), Quaternion.Euler(0.0f, 90.0f, 0.0f), q1.transform);

                Instantiate(wall, new Vector3(-15, 1, 12.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q1.transform); //row 3

                Instantiate(wall, new Vector3(-12.5f, 1, 10), Quaternion.Euler(0.0f, 90.0f, 0.0f), q1.transform); //row 3.5
                Instantiate(wall, new Vector3(-7.5f, 1, 10), Quaternion.Euler(0.0f, 90.0f, 0.0f), q1.transform);
                Instantiate(wall, new Vector3(-2.5f, 1, 10), Quaternion.Euler(0.0f, 90.0f, 0.0f), q1.transform);

                Instantiate(wall, new Vector3(-20, 1, 7.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q1.transform); //row 4
                Instantiate(wall, new Vector3(-15, 1, 7.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q1.transform);

                Instantiate(wall, new Vector3(-2.5f, 1, 5), Quaternion.Euler(0.0f, 90.0f, 0.0f), q1.transform); //row 4.5

                Instantiate(wall, new Vector3(-20, 1, 2.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q1.transform); //row 5
                Instantiate(wall, new Vector3(-15, 1, 2.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q1.transform);
                Instantiate(wall, new Vector3(-10, 1, 2.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q1.transform);
                Instantiate(wall, new Vector3(-5, 1, 2.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q1.transform);
                break;

            case 1:
                Instantiate(wall, new Vector3(-10, 1, 22.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q1.transform); //row 1

                Instantiate(wall, new Vector3(-17.5f, 1, 20), Quaternion.Euler(0.0f, 90.0f, 0.0f), q1.transform); //row 1.5
                Instantiate(wall, new Vector3(-12.5f, 1, 20), Quaternion.Euler(0.0f, 90.0f, 0.0f), q1.transform);

                Instantiate(wall, new Vector3(-20, 1, 17.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q1.transform); //row 2
                Instantiate(wall, new Vector3(-15, 1, 17.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q1.transform);
                Instantiate(wall, new Vector3(-5, 1, 17.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q1.transform);

                Instantiate(wall, new Vector3(-7.5f, 1, 15), Quaternion.Euler(0.0f, 90.0f, 0.0f), q1.transform); //row 2.5
                Instantiate(wall, new Vector3(-2.5f, 1, 15), Quaternion.Euler(0.0f, 90.0f, 0.0f), q1.transform);

                Instantiate(wall, new Vector3(-15, 1, 12.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q1.transform); //row 3
                Instantiate(wall, new Vector3(-5, 1, 12.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q1.transform);

                Instantiate(wall, new Vector3(-17.5f, 1, 10), Quaternion.Euler(0.0f, 90.0f, 0.0f), q1.transform); //row 3.5

                Instantiate(wall, new Vector3(-15, 1, 7.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q1.transform); //row 4
                Instantiate(wall, new Vector3(-10, 1, 7.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q1.transform);
                Instantiate(wall, new Vector3(-5, 1, 7.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q1.transform);

                Instantiate(wall, new Vector3(-7.5f, 1, 5), Quaternion.Euler(0.0f, 90.0f, 0.0f), q1.transform); //row 4.5

                Instantiate(wall, new Vector3(-20, 1, 2.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q1.transform); //row 5
                break;

            case 2:
                Instantiate(wall, new Vector3(-5, 1, 22.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q1.transform); //row 1

                Instantiate(wall, new Vector3(-17.5f, 1, 20), Quaternion.Euler(0.0f, 90.0f, 0.0f), q1.transform); //row 1.5
                Instantiate(wall, new Vector3(-12.5f, 1, 20), Quaternion.Euler(0.0f, 90.0f, 0.0f), q1.transform);

                Instantiate(wall, new Vector3(-10, 1, 17.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q1.transform); //row 2
                Instantiate(wall, new Vector3(-5, 1, 17.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q1.transform);

                Instantiate(wall, new Vector3(-22.5f, 1, 15), Quaternion.Euler(0.0f, 90.0f, 0.0f), q1.transform); //row 2.5
                Instantiate(wall, new Vector3(-17.5f, 1, 15), Quaternion.Euler(0.0f, 90.0f, 0.0f), q1.transform);

                Instantiate(wall, new Vector3(-15, 1, 12.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q1.transform); //row 3
                Instantiate(wall, new Vector3(-10, 1, 12.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q1.transform);

                Instantiate(wall, new Vector3(-17.5f, 1, 10), Quaternion.Euler(0.0f, 90.0f, 0.0f), q1.transform); //row 3.5
                Instantiate(wall, new Vector3(-7.5f, 1, 10), Quaternion.Euler(0.0f, 90.0f, 0.0f), q1.transform);

                Instantiate(wall, new Vector3(-10, 1, 7.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q1.transform); //row 4

                Instantiate(wall, new Vector3(-22.5f, 1, 5), Quaternion.Euler(0.0f, 90.0f, 0.0f), q1.transform); //row 4.5
                Instantiate(wall, new Vector3(-17.5f, 1, 5), Quaternion.Euler(0.0f, 90.0f, 0.0f), q1.transform);
                Instantiate(wall, new Vector3(-12.5f, 1, 5), Quaternion.Euler(0.0f, 90.0f, 0.0f), q1.transform);

                Instantiate(wall, new Vector3(-20, 1, 2.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q1.transform); //row 5
                Instantiate(wall, new Vector3(-5, 1, 2.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q1.transform);
                break;

            case 3:
                Instantiate(wall, new Vector3(-10, 1, 22.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q1.transform); //row 1

                Instantiate(wall, new Vector3(-22.5f, 1, 20), Quaternion.Euler(0.0f, 90.0f, 0.0f), q1.transform); //row 1.5
                Instantiate(wall, new Vector3(-17.5f, 1, 20), Quaternion.Euler(0.0f, 90.0f, 0.0f), q1.transform);
                Instantiate(wall, new Vector3(-2.5f, 1, 20), Quaternion.Euler(0.0f, 90.0f, 0.0f), q1.transform);

                Instantiate(wall, new Vector3(-5, 1, 17.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q1.transform); //row 2

                Instantiate(wall, new Vector3(-12.5f, 1, 15), Quaternion.Euler(0.0f, 90.0f, 0.0f), q1.transform); //row 2.5
                Instantiate(wall, new Vector3(-7.5f, 1, 15), Quaternion.Euler(0.0f, 90.0f, 0.0f), q1.transform);

                Instantiate(wall, new Vector3(-20, 1, 12.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q1.transform); //row 3
                Instantiate(wall, new Vector3(-5, 1, 12.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q1.transform);

                Instantiate(wall, new Vector3(-17.5f, 1, 10), Quaternion.Euler(0.0f, 90.0f, 0.0f), q1.transform); //row 3.5
                Instantiate(wall, new Vector3(-12.5f, 1, 10), Quaternion.Euler(0.0f, 90.0f, 0.0f), q1.transform);
                Instantiate(wall, new Vector3(-2.5f, 1, 10), Quaternion.Euler(0.0f, 90.0f, 0.0f), q1.transform);

                Instantiate(wall, new Vector3(-10, 1, 7.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q1.transform); //row 4

                Instantiate(wall, new Vector3(-22.5f, 1, 5), Quaternion.Euler(0.0f, 90.0f, 0.0f), q1.transform); //row 4.5
                Instantiate(wall, new Vector3(-17.5f, 1, 5), Quaternion.Euler(0.0f, 90.0f, 0.0f), q1.transform);
                Instantiate(wall, new Vector3(-7.5f, 1, 5), Quaternion.Euler(0.0f, 90.0f, 0.0f), q1.transform);
                Instantiate(wall, new Vector3(-2.5f, 1, 5), Quaternion.Euler(0.0f, 90.0f, 0.0f), q1.transform);

                Instantiate(wall, new Vector3(-10, 1, 2.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q1.transform); //row 5
                break;
        }
    }

    //Instaniate the different walls for the chosen quarter 2 option, parented to the quarter container
    public void GenerateQ2()
    {
        switch (q2MapPick)
        {
            case 0:
                //row 1 has nothing

                Instantiate(wall, new Vector3(12.5f, 1, 20), Quaternion.Euler(0.0f, 90.0f, 0.0f), q2.transform); //row 1.5
                Instantiate(wall, new Vector3(7.5f, 1, 20), Quaternion.Euler(0.0f, 90.0f, 0.0f), q2.transform);

                Instantiate(wall, new Vector3(20, 1, 17.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q2.transform); //row 2
                Instantiate(wall, new Vector3(15, 1, 17.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q2.transform);

                Instantiate(wall, new Vector3(22.5f, 1, 15), Quaternion.Euler(0.0f, 90.0f, 0.0f), q2.transform); //row 2.5
                Instantiate(wall, new Vector3(7.5f, 1, 15), Quaternion.Euler(0.0f, 90.0f, 0.0f), q2.transform);
                Instantiate(wall, new Vector3(2.5f, 1, 15), Quaternion.Euler(0.0f, 90.0f, 0.0f), q2.transform);

                Instantiate(wall, new Vector3(15, 1, 12.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q2.transform); //row 3
                Instantiate(wall, new Vector3(10, 1, 12.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q2.transform);

                Instantiate(wall, new Vector3(17.5f, 1, 10), Quaternion.Euler(0.0f, 90.0f, 0.0f), q2.transform); //row 3.5
                Instantiate(wall, new Vector3(2.5f, 1, 10), Quaternion.Euler(0.0f, 90.0f, 0.0f), q2.transform);

                Instantiate(wall, new Vector3(20, 1, 7.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q2.transform); //row 4
                Instantiate(wall, new Vector3(10, 1, 7.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q2.transform);
                Instantiate(wall, new Vector3(5, 1, 7.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q2.transform);

                Instantiate(wall, new Vector3(12.5f, 1, 5), Quaternion.Euler(0.0f, 90.0f, 0.0f), q2.transform); //row 4.5

                Instantiate(wall, new Vector3(20, 1, 2.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q2.transform); //row 5
                break;

            case 1:
                Instantiate(wall, new Vector3(15, 1, 22.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q2.transform); //row 1

                Instantiate(wall, new Vector3(12.5f, 1, 20), Quaternion.Euler(0.0f, 90.0f, 0.0f), q2.transform); //row 1.5

                Instantiate(wall, new Vector3(20, 1, 17.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q2.transform); //row 2
                Instantiate(wall, new Vector3(10, 1, 17.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q2.transform);
                Instantiate(wall, new Vector3(5, 1, 17.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q2.transform);

                Instantiate(wall, new Vector3(17.5f, 1, 15), Quaternion.Euler(0.0f, 90.0f, 0.0f), q2.transform); //row 2.5
                Instantiate(wall, new Vector3(2.5f, 1, 15), Quaternion.Euler(0.0f, 90.0f, 0.0f), q2.transform);

                Instantiate(wall, new Vector3(20, 1, 12.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q2.transform); //row 3
                Instantiate(wall, new Vector3(15, 1, 12.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q2.transform);
                Instantiate(wall, new Vector3(5, 1, 12.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q2.transform);

                Instantiate(wall, new Vector3(12.5f, 1, 10), Quaternion.Euler(0.0f, 90.0f, 0.0f), q2.transform); //row 3.5
                Instantiate(wall, new Vector3(7.5f, 1, 10), Quaternion.Euler(0.0f, 90.0f, 0.0f), q2.transform);
                Instantiate(wall, new Vector3(2.5f, 1, 10), Quaternion.Euler(0.0f, 90.0f, 0.0f), q2.transform);

                Instantiate(wall, new Vector3(20, 1, 7.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q2.transform); //row 4

                Instantiate(wall, new Vector3(17.5f, 1, 5), Quaternion.Euler(0.0f, 90.0f, 0.0f), q2.transform); //row 4.5
                Instantiate(wall, new Vector3(12.5f, 1, 5), Quaternion.Euler(0.0f, 90.0f, 0.0f), q2.transform);
                Instantiate(wall, new Vector3(7.5f, 1, 5), Quaternion.Euler(0.0f, 90.0f, 0.0f), q2.transform);

                Instantiate(wall, new Vector3(20, 1, 2.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q2.transform); //row 5
                Instantiate(wall, new Vector3(5, 1, 2.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q2.transform);
                break;
            case 2:
                Instantiate(wall, new Vector3(15, 1, 22.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q2.transform); //row 1

                Instantiate(wall, new Vector3(12.5f, 1, 20), Quaternion.Euler(0.0f, 90.0f, 0.0f), q2.transform); //row 1.5
                Instantiate(wall, new Vector3(7.5f, 1, 20), Quaternion.Euler(0.0f, 90.0f, 0.0f), q2.transform);
                Instantiate(wall, new Vector3(2.5f, 1, 20), Quaternion.Euler(0.0f, 90.0f, 0.0f), q2.transform);

                Instantiate(wall, new Vector3(20, 1, 17.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q2.transform); //row 2
                Instantiate(wall, new Vector3(15, 1, 17.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q2.transform); 
                Instantiate(wall, new Vector3(5, 1, 17.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q2.transform);

                Instantiate(wall, new Vector3(17.5f, 1, 15), Quaternion.Euler(0.0f, 90.0f, 0.0f), q2.transform); //row 2.5

                Instantiate(wall, new Vector3(10, 1, 12.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q2.transform); //row 3

                Instantiate(wall, new Vector3(17.5f, 1, 10), Quaternion.Euler(0.0f, 90.0f, 0.0f), q2.transform); //row 3.5
                Instantiate(wall, new Vector3(12.5f, 1, 10), Quaternion.Euler(0.0f, 90.0f, 0.0f), q2.transform); 
                Instantiate(wall, new Vector3(7.5f, 1, 10), Quaternion.Euler(0.0f, 90.0f, 0.0f), q2.transform);

                Instantiate(wall, new Vector3(20, 1, 7.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q2.transform); //row 4
                Instantiate(wall, new Vector3(10, 1, 7.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q2.transform);

                //row 4.5 has nothing

                Instantiate(wall, new Vector3(20, 1, 2.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q2.transform); //row 5
                Instantiate(wall, new Vector3(15, 1, 2.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q2.transform); 
                Instantiate(wall, new Vector3(10, 1, 2.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q2.transform); 
                Instantiate(wall, new Vector3(5, 1, 2.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q2.transform); 
                break;

            case 3:
                Instantiate(wall, new Vector3(5, 1, 22.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q2.transform); //row 1

                Instantiate(wall, new Vector3(12.5f, 1, 20), Quaternion.Euler(0.0f, 90.0f, 0.0f), q2.transform); //row 1.5
                Instantiate(wall, new Vector3(2.5f, 1, 20), Quaternion.Euler(0.0f, 90.0f, 0.0f), q2.transform);

                Instantiate(wall, new Vector3(20, 1, 17.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q2.transform); //row 2
                Instantiate(wall, new Vector3(15, 1, 17.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q2.transform);

                Instantiate(wall, new Vector3(22.5f, 1, 15), Quaternion.Euler(0.0f, 90.0f, 0.0f), q2.transform); //row 2.5
                Instantiate(wall, new Vector3(7.5f, 1, 15), Quaternion.Euler(0.0f, 90.0f, 0.0f), q2.transform);
                Instantiate(wall, new Vector3(2.5f, 1, 15), Quaternion.Euler(0.0f, 90.0f, 0.0f), q2.transform);

                Instantiate(wall, new Vector3(15, 1, 12.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q2.transform); //row 3
                Instantiate(wall, new Vector3(10, 1, 12.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q2.transform);

                Instantiate(wall, new Vector3(17.5f, 1, 10), Quaternion.Euler(0.0f, 90.0f, 0.0f), q2.transform); //row 3.5
                Instantiate(wall, new Vector3(12.5f, 1, 10), Quaternion.Euler(0.0f, 90.0f, 0.0f), q2.transform);
                Instantiate(wall, new Vector3(2.5f, 1, 10), Quaternion.Euler(0.0f, 90.0f, 0.0f), q2.transform);

                Instantiate(wall, new Vector3(10, 1, 7.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q2.transform); //row 4

                Instantiate(wall, new Vector3(22.5f, 1, 5), Quaternion.Euler(0.0f, 90.0f, 0.0f), q2.transform); //row 4.5
                Instantiate(wall, new Vector3(17.5f, 1, 5), Quaternion.Euler(0.0f, 90.0f, 0.0f), q2.transform);
                Instantiate(wall, new Vector3(7.5f, 1, 5), Quaternion.Euler(0.0f, 90.0f, 0.0f), q2.transform);
                Instantiate(wall, new Vector3(2.5f, 1, 5), Quaternion.Euler(0.0f, 90.0f, 0.0f), q2.transform);

                //row 5 has nothing
                break;
        }
    }

    //Instaniate the different walls for the chosen quarter 3 option, parented to the quarter container
    public void GenerateQ3()
    {
        switch (q3MapPick)
        {
            case 0:
                Instantiate(wall, new Vector3(5, 1, -22.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q3.transform); //row 1

                Instantiate(wall, new Vector3(17.5f, 1, -20), Quaternion.Euler(0.0f, 90.0f, 0.0f), q3.transform); //row 1.5
                Instantiate(wall, new Vector3(12.5f, 1, -20), Quaternion.Euler(0.0f, 90.0f, 0.0f), q3.transform);
                Instantiate(wall, new Vector3(2.5f, 1, -20), Quaternion.Euler(0.0f, 90.0f, 0.0f), q3.transform);

                Instantiate(wall, new Vector3(10, 1, -17.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q3.transform); //row 2
                Instantiate(wall, new Vector3(5, 1, -17.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q3.transform);

                Instantiate(wall, new Vector3(17.5f, 1, -15), Quaternion.Euler(0.0f, 90.0f, 0.0f), q3.transform); //row 2.5

                Instantiate(wall, new Vector3(15, 1, -12.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q3.transform); //row 3
                Instantiate(wall, new Vector3(10, 1, -12.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q3.transform);
                Instantiate(wall, new Vector3(5, 1, -12.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q3.transform);

                Instantiate(wall, new Vector3(22.5f, 1, -10), Quaternion.Euler(0.0f, 90.0f, 0.0f), q3.transform); //row 3.5
                Instantiate(wall, new Vector3(17.5f, 1, -10), Quaternion.Euler(0.0f, 90.0f, 0.0f), q3.transform);
                Instantiate(wall, new Vector3(7.5f, 1, -10), Quaternion.Euler(0.0f, 90.0f, 0.0f), q3.transform);
                Instantiate(wall, new Vector3(2.5f, 1, -10), Quaternion.Euler(0.0f, 90.0f, 0.0f), q3.transform);

                Instantiate(wall, new Vector3(15, 1, -7.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q3.transform); //row 4
                Instantiate(wall, new Vector3(5, 1, -7.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q3.transform);

                Instantiate(wall, new Vector3(12.5f, 1, -5), Quaternion.Euler(0.0f, 90.0f, 0.0f), q3.transform); //row 4.5
                Instantiate(wall, new Vector3(2.5f, 1, -5), Quaternion.Euler(0.0f, 90.0f, 0.0f), q3.transform);

                Instantiate(wall, new Vector3(20, 1, -2.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q3.transform); //row 5
                break;

            case 1:
                Instantiate(wall, new Vector3(15, 1, -22.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q3.transform); //row 1
                Instantiate(wall, new Vector3(5, 1, -22.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q3.transform);

                Instantiate(wall, new Vector3(17.5f, 1, -20), Quaternion.Euler(0.0f, 90.0f, 0.0f), q3.transform); //row 1.5

                Instantiate(wall, new Vector3(10, 1, -17.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q3.transform); //row 2

                Instantiate(wall, new Vector3(17.5f, 1, -15), Quaternion.Euler(0.0f, 90.0f, 0.0f), q3.transform); //row 2.5
                Instantiate(wall, new Vector3(12.5f, 1, -15), Quaternion.Euler(0.0f, 90.0f, 0.0f), q3.transform);
                Instantiate(wall, new Vector3(7.5f, 1, -15), Quaternion.Euler(0.0f, 90.0f, 0.0f), q3.transform);
                Instantiate(wall, new Vector3(2.5f, 1, -15), Quaternion.Euler(0.0f, 90.0f, 0.0f), q3.transform);

                Instantiate(wall, new Vector3(15, 1, -12.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q3.transform); //row 3

                Instantiate(wall, new Vector3(22.5f, 1, -10), Quaternion.Euler(0.0f, 90.0f, 0.0f), q3.transform); //row 3.5
                Instantiate(wall, new Vector3(12.5f, 1, -10), Quaternion.Euler(0.0f, 90.0f, 0.0f), q3.transform);
                Instantiate(wall, new Vector3(2.5f, 1, -10), Quaternion.Euler(0.0f, 90.0f, 0.0f), q3.transform);

                Instantiate(wall, new Vector3(15, 1, -7.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q3.transform); //row 4

                Instantiate(wall, new Vector3(17.5f, 1, -5), Quaternion.Euler(0.0f, 90.0f, 0.0f), q3.transform); //row 4.5
                Instantiate(wall, new Vector3(7.5f, 1, -5), Quaternion.Euler(0.0f, 90.0f, 0.0f), q3.transform);
                Instantiate(wall, new Vector3(2.5f, 1, -5), Quaternion.Euler(0.0f, 90.0f, 0.0f), q3.transform);

                Instantiate(wall, new Vector3(20, 1, -2.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q3.transform); //row 5
                Instantiate(wall, new Vector3(15, 1, -2.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q3.transform);
                Instantiate(wall, new Vector3(10, 1, -2.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q3.transform);
                break;

            case 2:
                Instantiate(wall, new Vector3(20, 1, -22.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q3.transform); //row 1
                Instantiate(wall, new Vector3(5, 1, -22.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q3.transform);

                Instantiate(wall, new Vector3(12.5f, 1, -20), Quaternion.Euler(0.0f, 90.0f, 0.0f), q3.transform); //row 1.5

                Instantiate(wall, new Vector3(20, 1, -17.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q3.transform); //row 2
                Instantiate(wall, new Vector3(15, 1, -17.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q3.transform);
                Instantiate(wall, new Vector3(5, 1, -17.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q3.transform);

                Instantiate(wall, new Vector3(12.5f, 1, -15), Quaternion.Euler(0.0f, 90.0f, 0.0f), q3.transform); //row 2.5
                Instantiate(wall, new Vector3(7.5f, 1, -15), Quaternion.Euler(0.0f, 90.0f, 0.0f), q3.transform);
                Instantiate(wall, new Vector3(2.5f, 1, -15), Quaternion.Euler(0.0f, 90.0f, 0.0f), q3.transform);

                Instantiate(wall, new Vector3(20, 1, -12.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q3.transform); //row 3
                Instantiate(wall, new Vector3(5, 1, -12.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q3.transform);

                Instantiate(wall, new Vector3(17.5f, 1, -10), Quaternion.Euler(0.0f, 90.0f, 0.0f), q3.transform); //row 3.5
                Instantiate(wall, new Vector3(12.5f, 1, -10), Quaternion.Euler(0.0f, 90.0f, 0.0f), q3.transform);

                Instantiate(wall, new Vector3(20, 1, -7.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q3.transform); //row 4
                Instantiate(wall, new Vector3(5, 1, -7.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q3.transform);

                Instantiate(wall, new Vector3(12.5f, 1, -5), Quaternion.Euler(0.0f, 90.0f, 0.0f), q3.transform); //row 4.5
                Instantiate(wall, new Vector3(7.5f, 1, -5), Quaternion.Euler(0.0f, 90.0f, 0.0f), q3.transform);

                Instantiate(wall, new Vector3(15, 1, -2.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q3.transform); //row 5
                break;

            case 3:
                Instantiate(wall, new Vector3(5, 1, -22.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q3.transform); //row 1

                Instantiate(wall, new Vector3(12.5f, 1, -20), Quaternion.Euler(0.0f, 90.0f, 0.0f), q3.transform); //row 1.5
                Instantiate(wall, new Vector3(7.5f, 1, -20), Quaternion.Euler(0.0f, 90.0f, 0.0f), q3.transform);

                Instantiate(wall, new Vector3(20, 1, -17.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q3.transform); //row 2
                Instantiate(wall, new Vector3(15, 1, -17.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q3.transform);
                Instantiate(wall, new Vector3(5, 1, -17.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q3.transform);

                //row 2.5 has nothing

                Instantiate(wall, new Vector3(20, 1, -12.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q3.transform); //row 3
                Instantiate(wall, new Vector3(10, 1, -12.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q3.transform);

                Instantiate(wall, new Vector3(17.5f, 1, -10), Quaternion.Euler(0.0f, 90.0f, 0.0f), q3.transform); //row 3.5
                Instantiate(wall, new Vector3(12.5f, 1, -10), Quaternion.Euler(0.0f, 90.0f, 0.0f), q3.transform);
                Instantiate(wall, new Vector3(7.5f, 1, -10), Quaternion.Euler(0.0f, 90.0f, 0.0f), q3.transform);
                Instantiate(wall, new Vector3(2.5f, 1, -10), Quaternion.Euler(0.0f, 90.0f, 0.0f), q3.transform);

                Instantiate(wall, new Vector3(20, 1, -7.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q3.transform); //row 4
                Instantiate(wall, new Vector3(10, 1, -7.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q3.transform);

                Instantiate(wall, new Vector3(12.5f, 1, -5), Quaternion.Euler(0.0f, 90.0f, 0.0f), q3.transform); //row 4.5

                Instantiate(wall, new Vector3(20, 1, -2.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q3.transform); //row 5
                Instantiate(wall, new Vector3(5, 1, -2.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q3.transform);
                break;
        }
    }

    //Instaniate the different walls for the chosen quarter 4 option, parented to the quarter container
    public void GenerateQ4()
    {
        switch (q4MapPick)
        {
            case 0:
                Instantiate(wall, new Vector3(-15, 1, -22.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q4.transform); //row 1
                Instantiate(wall, new Vector3(-5, 1, -22.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q4.transform);

                Instantiate(wall, new Vector3(-17.5f, 1, -20), Quaternion.Euler(0.0f, 90.0f, 0.0f), q4.transform); //row 1.5

                Instantiate(wall, new Vector3(-20, 1, -17.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q4.transform); //row 2
                Instantiate(wall, new Vector3(-10, 1, -17.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q4.transform);
                Instantiate(wall, new Vector3(-5, 1, -17.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q4.transform);

                Instantiate(wall, new Vector3(-12.5f, 1, -15), Quaternion.Euler(0.0f, 90.0f, 0.0f), q4.transform); //row 2.5
                Instantiate(wall, new Vector3(-2.5f, 1, -15), Quaternion.Euler(0.0f, 90.0f, 0.0f), q4.transform);

                Instantiate(wall, new Vector3(-20, 1, -12.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q4.transform); //row 3
                Instantiate(wall, new Vector3(-10, 1, -12.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q4.transform);

                Instantiate(wall, new Vector3(-17.5f, 1, -10), Quaternion.Euler(0.0f, 90.0f, 0.0f), q4.transform); //row 3.5
                Instantiate(wall, new Vector3(-7.5f, 1, -10), Quaternion.Euler(0.0f, 90.0f, 0.0f), q4.transform);
                Instantiate(wall, new Vector3(-2.5f, 1, -10), Quaternion.Euler(0.0f, 90.0f, 0.0f), q4.transform);

                Instantiate(wall, new Vector3(-15, 1, -7.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q4.transform); //row 4
                Instantiate(wall, new Vector3(-5, 1, -7.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q4.transform);

                Instantiate(wall, new Vector3(-22.5f, 1, -5), Quaternion.Euler(0.0f, 90.0f, 0.0f), q4.transform); //row 4.5
                Instantiate(wall, new Vector3(-12.5f, 1, -5), Quaternion.Euler(0.0f, 90.0f, 0.0f), q4.transform);

                Instantiate(wall, new Vector3(-20, 1, -2.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q4.transform); //row 5
                Instantiate(wall, new Vector3(-10, 1, -2.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q4.transform);
                Instantiate(wall, new Vector3(-5, 1, -2.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q4.transform);
                break;

            case 1:
                Instantiate(wall, new Vector3(-10, 1, -22.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q4.transform); //row 1

                Instantiate(wall, new Vector3(-17.5f, 1, -20), Quaternion.Euler(0.0f, 90.0f, 0.0f), q4.transform); //row 1.5

                Instantiate(wall, new Vector3(-20, 1, -17.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q4.transform); //row 2
                Instantiate(wall, new Vector3(-10, 1, -17.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q4.transform);
                Instantiate(wall, new Vector3(-5, 1, -17.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q4.transform);

                Instantiate(wall, new Vector3(-12.5f, 1, -15), Quaternion.Euler(0.0f, 90.0f, 0.0f), q4.transform); //row 2.5
                Instantiate(wall, new Vector3(-7.5f, 1, -15), Quaternion.Euler(0.0f, 90.0f, 0.0f), q4.transform);

                Instantiate(wall, new Vector3(-20, 1, -12.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q4.transform); //row 3

                Instantiate(wall, new Vector3(-17.5f, 1, -10), Quaternion.Euler(0.0f, 90.0f, 0.0f), q4.transform); //row 3.5
                Instantiate(wall, new Vector3(-12.5f, 1, -10), Quaternion.Euler(0.0f, 90.0f, 0.0f), q4.transform);
                Instantiate(wall, new Vector3(-7.5f, 1, -10), Quaternion.Euler(0.0f, 90.0f, 0.0f), q4.transform);

                Instantiate(wall, new Vector3(-20, 1, -7.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q4.transform); //row 4
                Instantiate(wall, new Vector3(-5, 1, -7.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q4.transform);

                Instantiate(wall, new Vector3(-22.5f, 1, -5), Quaternion.Euler(0.0f, 90.0f, 0.0f), q4.transform); //row 4.5
                Instantiate(wall, new Vector3(-12.5f, 1, -5), Quaternion.Euler(0.0f, 90.0f, 0.0f), q4.transform);

                Instantiate(wall, new Vector3(-15, 1, -2.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q4.transform); //row 5
                Instantiate(wall, new Vector3(-5, 1, -2.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q4.transform);
                break;

            case 2:
                Instantiate(wall, new Vector3(-15, 1, -22.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q4.transform); //row 1

                Instantiate(wall, new Vector3(-17.5f, 1, -20), Quaternion.Euler(0.0f, 90.0f, 0.0f), q4.transform); //row 1.5
                Instantiate(wall, new Vector3(-2.5f, 1, -20), Quaternion.Euler(0.0f, 90.0f, 0.0f), q4.transform);

                Instantiate(wall, new Vector3(-10, 1, -17.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q4.transform); //row 2

                Instantiate(wall, new Vector3(-17.5f, 1, -15), Quaternion.Euler(0.0f, 90.0f, 0.0f), q4.transform); //row 2.5
                Instantiate(wall, new Vector3(-12.5f, 1, -15), Quaternion.Euler(0.0f, 90.0f, 0.0f), q4.transform);
                Instantiate(wall, new Vector3(-7.5f, 1, -15), Quaternion.Euler(0.0f, 90.0f, 0.0f), q4.transform);
                Instantiate(wall, new Vector3(-2.5f, 1, -15), Quaternion.Euler(0.0f, 90.0f, 0.0f), q4.transform);

                Instantiate(wall, new Vector3(-10, 1, -12.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q4.transform); //row 3

                Instantiate(wall, new Vector3(-22.5f, 1, -10), Quaternion.Euler(0.0f, 90.0f, 0.0f), q4.transform); //row 3.5
                Instantiate(wall, new Vector3(-17.5f, 1, -10), Quaternion.Euler(0.0f, 90.0f, 0.0f), q4.transform);
                Instantiate(wall, new Vector3(-2.5f, 1, -10), Quaternion.Euler(0.0f, 90.0f, 0.0f), q4.transform);

                Instantiate(wall, new Vector3(-15, 1, -7.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q4.transform); //row 4
                Instantiate(wall, new Vector3(-10, 1, -7.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q4.transform);

                Instantiate(wall, new Vector3(-7.5f, 1, -5), Quaternion.Euler(0.0f, 90.0f, 0.0f), q4.transform); //row 4.5

                Instantiate(wall, new Vector3(-20, 1, -2.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q4.transform); //row 5
                Instantiate(wall, new Vector3(-15, 1, -2.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q4.transform);
                break;

            case 3:
                Instantiate(wall, new Vector3(-10, 1, -22.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q4.transform); //row 1

                Instantiate(wall, new Vector3(-17.5f, 1, -20), Quaternion.Euler(0.0f, 90.0f, 0.0f), q4.transform); //row 1.5
                Instantiate(wall, new Vector3(-2.5f, 1, -20), Quaternion.Euler(0.0f, 90.0f, 0.0f), q4.transform);

                Instantiate(wall, new Vector3(-20, 1, -17.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q4.transform); //row 2
                Instantiate(wall, new Vector3(-15, 1, -17.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q4.transform);
                Instantiate(wall, new Vector3(-10, 1, -17.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q4.transform);

                //row 2.5 has nothing

                Instantiate(wall, new Vector3(-15, 1, -12.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q4.transform); //row 3
                Instantiate(wall, new Vector3(-10, 1, -12.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q4.transform);
                Instantiate(wall, new Vector3(-5, 1, -12.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q4.transform);

                Instantiate(wall, new Vector3(-22.5f, 1, -10), Quaternion.Euler(0.0f, 90.0f, 0.0f), q4.transform); //row 3.5
                Instantiate(wall, new Vector3(-17.5f, 1, -10), Quaternion.Euler(0.0f, 90.0f, 0.0f), q4.transform);
                Instantiate(wall, new Vector3(-7.5f, 1, -10), Quaternion.Euler(0.0f, 90.0f, 0.0f), q4.transform);

                Instantiate(wall, new Vector3(-15, 1, -7.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q4.transform); //row 4
                Instantiate(wall, new Vector3(-5, 1, -7.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q4.transform);

                //row 4.5 has nothing

                Instantiate(wall, new Vector3(-20, 1, -2.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q4.transform); //row 5
                Instantiate(wall, new Vector3(-10, 1, -2.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q4.transform);
                Instantiate(wall, new Vector3(-5, 1, -2.5f), Quaternion.Euler(0.0f, 0.0f, 0.0f), q4.transform);
                break;
        }
    }

    //Combine all the seperate walls into one object / mesh
    public void CombineMeshes()
    {
        MeshFilter[] q1MeshFilters = q1.GetComponentsInChildren<MeshFilter>();
        CombineInstance[] q1Combine = new CombineInstance[q1MeshFilters.Length];

        MeshFilter[] q2MeshFilters = q2.GetComponentsInChildren<MeshFilter>();
        CombineInstance[] q2Combine = new CombineInstance[q2MeshFilters.Length];

        MeshFilter[] q3MeshFilters = q3.GetComponentsInChildren<MeshFilter>();
        CombineInstance[] q3Combine = new CombineInstance[q3MeshFilters.Length];

        MeshFilter[] q4MeshFilters = q4.GetComponentsInChildren<MeshFilter>();
        CombineInstance[] q4Combine = new CombineInstance[q4MeshFilters.Length];

        //Q1

        GameObject q1Object = new GameObject("Q1 Mesh");
        q1Object.transform.parent = q1.transform;
        q1Object.AddComponent<MeshFilter>();
        q1Object.AddComponent<MeshRenderer>();
        q1Object.GetComponent<MeshRenderer>().material = wall.GetComponent<MeshRenderer>().sharedMaterial;
        
        for (int i = 0; i < q1MeshFilters.Length; i++)
        {
            q1Combine[i].mesh = q1MeshFilters[i].sharedMesh;
            q1Combine[i].transform = q1MeshFilters[i].transform.localToWorldMatrix;
            //q1MeshFilters[i].gameObject.SetActive(false);
            Destroy(q1MeshFilters[i].gameObject);
        }

        q1Object.transform.GetComponent<MeshFilter>().mesh = new Mesh();
        q1Object.transform.GetComponent<MeshFilter>().mesh.CombineMeshes(q1Combine);
        q1Object.AddComponent<MeshCollider>();
        q1Object.transform.gameObject.SetActive(true);

        //Q2

        GameObject q2Object = new GameObject("Q2 Mesh");
        q2Object.transform.parent = q2.transform;
        q2Object.AddComponent<MeshFilter>();
        q2Object.AddComponent<MeshRenderer>();
        q2Object.GetComponent<MeshRenderer>().material = wall.GetComponent<MeshRenderer>().sharedMaterial;

        for (int i = 0; i < q2MeshFilters.Length; i++)
        {
            q2Combine[i].mesh = q2MeshFilters[i].sharedMesh;
            q2Combine[i].transform = q2MeshFilters[i].transform.localToWorldMatrix;
            //q2MeshFilters[i].gameObject.SetActive(false);
            Destroy(q2MeshFilters[i].gameObject);
        }

        q2Object.transform.GetComponent<MeshFilter>().mesh = new Mesh();
        q2Object.transform.GetComponent<MeshFilter>().mesh.CombineMeshes(q2Combine);
        q2Object.AddComponent<MeshCollider>();
        q2Object.transform.gameObject.SetActive(true);

        //Q3

        GameObject q3Object = new GameObject("Q3 Mesh");
        q3Object.transform.parent = q3.transform;
        q3Object.AddComponent<MeshFilter>();
        q3Object.AddComponent<MeshRenderer>();
        q3Object.GetComponent<MeshRenderer>().material = wall.GetComponent<MeshRenderer>().sharedMaterial;

        for (int i = 0; i < q3MeshFilters.Length; i++)
        {
            q3Combine[i].mesh = q3MeshFilters[i].sharedMesh;
            q3Combine[i].transform = q3MeshFilters[i].transform.localToWorldMatrix;
            //q3MeshFilters[i].gameObject.SetActive(false);
            Destroy(q3MeshFilters[i].gameObject);
        }

        q3Object.transform.GetComponent<MeshFilter>().mesh = new Mesh();
        q3Object.transform.GetComponent<MeshFilter>().mesh.CombineMeshes(q3Combine);
        q3Object.AddComponent<MeshCollider>();
        q3Object.transform.gameObject.SetActive(true);

        //Q4

        GameObject q4Object = new GameObject("Q4 Mesh");
        q4Object.transform.parent = q4.transform;
        q4Object.AddComponent<MeshFilter>();
        q4Object.AddComponent<MeshRenderer>();
        q4Object.GetComponent<MeshRenderer>().material = wall.GetComponent<MeshRenderer>().sharedMaterial;

        for (int i = 0; i < q4MeshFilters.Length; i++)
        {
            q4Combine[i].mesh = q4MeshFilters[i].sharedMesh;
            q4Combine[i].transform = q4MeshFilters[i].transform.localToWorldMatrix;
            //q4MeshFilters[i].gameObject.SetActive(false);
            Destroy(q4MeshFilters[i].gameObject);
        }

        q4Object.transform.GetComponent<MeshFilter>().mesh = new Mesh();
        q4Object.transform.GetComponent<MeshFilter>().mesh.CombineMeshes(q4Combine);
        q4Object.AddComponent<MeshCollider>();
        q4Object.transform.gameObject.SetActive(true);
    }
}
