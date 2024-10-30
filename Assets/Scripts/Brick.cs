using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public int brickHealth = 2;

    public int OnBrickHit (int speedDamage)
    {
        // Subtract 1 from the brick health and change the scale
        brickHealth--;

        int ballToBrickDamage = brickHealth;

        transform.localScale -= new Vector3(0.3f, 1, 0.3f);

        brickHealth -= speedDamage;

        //Kills object if # of times hit = brick health
        if (brickHealth <= 0)
        {
            Destroy(gameObject);
            GameObject.Find("GameManager").GetComponent<GameManager>().brickCount--;
            Debug.Log(GameObject.Find("GameManager").GetComponent<GameManager>().brickCount);
        }
        return ballToBrickDamage;
    }

}
