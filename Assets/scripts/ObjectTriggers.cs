using System;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectTriggers : MonoBehaviour
{
    [SerializeField]
    internal Vector3 spawnPos;
    [SerializeField]
    internal bool forGround = false;
    [SerializeField]
    internal bool forLandscape = false;
    [SerializeField]
    internal bool forPlayer = false;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "ground" && forGround)
        {
            collision.transform.position += spawnPos;
            GenerateObstacle(collision.GetComponent<Obstacles>().obstacles[0], collision.GetComponent<Obstacles>().obstacles[1]);
        }

        else if (collision.tag == "landscape" && forLandscape)
        {
            collision.transform.position += spawnPos;
        }

        else if (collision.tag == "Player" && forPlayer && tag == "obstacle")
        {
            collision.GetComponent<PlayerSettings>().hp -= 1;
        }

        if (collision.tag == "Player" && forPlayer && tag != "obstacle")
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    private void GenerateObstacle(GameObject obstacle1, GameObject obstacle2)
    {

        int i = Convert.ToInt32(Random.Range(1, 5));
        Debug.Log(i);
        if (i == 1) { obstacle1.SetActive(true); }
        Debug.Log(i);
        if(i==2) obstacle2.SetActive(true);

        if(i > 2)
        {
            obstacle1.SetActive(false);
            obstacle2.SetActive(false);
        }
       

    }
}
