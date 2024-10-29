using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public int brickHealth = 2;
    void OnCollisionEnter(Collision other)
    {
        brickHealth--;
        transform.localScale -= new Vector3(0.3f, 1, 0.3f);
        brickHealth -= other.gameObject.GetComponent<Ball>().speedDamage;

        //Kills object if # of times hit = brick health
        if (brickHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

}
