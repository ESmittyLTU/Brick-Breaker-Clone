using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Deathzone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            Destroy(other.gameObject);
            GameManager.lives--;

            GameObject.Find("GameManager").GetComponent<GameManager>().livesTMP.text = "Lives: " + GameManager.lives;

            if (GameManager.lives > 0)
            {
                GameObject.Find("GameManager").GetComponent<GameManager>().newBall();
            }
        }
    }
}
