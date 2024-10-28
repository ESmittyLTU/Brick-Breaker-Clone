using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public int brickHealth = 2;
    void OnCollisionEnter(Collision collision)
    {
        transform.localScale -= new Vector3(0.3f, 1, 0.3f);
        brickHealth--;

        //Kills object if # of times hit = brick health
        if (brickHealth == 0 )
        {
            Destroy(gameObject);
        }
    }

}
