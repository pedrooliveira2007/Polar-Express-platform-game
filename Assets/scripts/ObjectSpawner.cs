using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField]
    internal Vector3 spawnPos;
    [SerializeField]
    internal GameObject obstacle1;
    [SerializeField]
    internal GameObject obstacle2;
    [SerializeField]
    internal bool forGround = false;
    [SerializeField]
    internal bool forLandscape = false;

    private void Start()
    {
        obstacle1.SetActive(false);
        obstacle1.SetActive(false);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "ground" && forGround)
        {
            collision.transform.position += spawnPos;
            GenerateObstacle();
        }

        if (collision.tag == "landscape" && forLandscape)
        {
            collision.transform.position += spawnPos;
        }

    }

    private void GenerateObstacle()
    {
        int i = Convert.ToInt32(Random.Range(1, 5));
        if (i == 1) obstacle1.SetActive(true);
        else if(i==2) obstacle2.SetActive(true);
       

    }
}
