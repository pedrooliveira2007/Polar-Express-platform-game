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
        if (collision.tag == "ground" && forGround)
        {
            collision.transform.position += spawnPos;
            GenerateObstacle(collision.GetComponent<Obstacles>().obstacles);
        }

        if (collision.tag == "test")
        {
            collision.transform.position += spawnPos;
            GenerateObstacle(collision.GetComponent<Obstacles>().obstacles);
        }



        else if (collision.tag == "landscape" && forLandscape)
        {
            collision.transform.position += spawnPos;
        }

        else if (collision.tag == "Player" && forPlayer && tag == "obstacle")
        {
            collision.GetComponent<PlayerSettings>().hp -= 1;
        }

        else if (collision.tag == "Player" && forPlayer && tag != "obstacle")
        {
            if (tag == "hp")
            {
                collision.GetComponent<PlayerSettings>().hp += 1;
                gameObject.SetActive(false);
            }
            if (tag == "gizo")
            {
                collision.GetComponent<PlayerSettings>().points += 500;
                gameObject.SetActive(false);
            }
            if (tag == "mug")
            {
                collision.GetComponent<PlayerSettings>().points += 200;
                gameObject.SetActive(false);
            }
            if (tag == "gameover") { SceneManager.LoadScene("GameOver"); }
        }
    }

    private void GenerateObstacle(GameObject[] obstacle)
    {
        int i = Convert.ToInt32(Random.Range(1, 6));
        Debug.Log(i);
        if (i == 1) { obstacle[0].SetActive(true); }
        if (i == 2) obstacle[1].SetActive(true);

        if (i == 3 && obstacle.Length > 2)
        {
            Debug.Log("aaaaaa");
            obstacle[2].SetActive(true);
        }

        if (i > 3)
        {
            foreach (GameObject g in obstacle)
            {
                g.SetActive(false);
            }
        }
    }
}
