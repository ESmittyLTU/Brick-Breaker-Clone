using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    void Start()
    {
        transform.position = new Vector3(0, 1, -13);
        GetComponent<Renderer>().material.color = Color.gray;
    }

    void Update()
    {
        //Get the pos of the mouse's x every frame (relative to screen and world)
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 mousePosX = new Vector3(mousePos.x, 1, -13);

        //When the mouse is inside a certain x-value range, move the paddle torwards mousePosX with a max of 50 units/frame
        //This is better than transform.position = new Vector3(mousePos.x, 1, -14), because rather than setting xpos of the paddle to xpos of mouse, which can cause clipping, i move the object towards that pos
        if (-24 <= mousePos.x && mousePos.x <= 24)
        {
            transform.position = Vector3.MoveTowards(transform.position, mousePosX, 10);
        }

        //Changes color of the paddle according to mouse speed
        if (GameManager.mouseSpeed >= 4)
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
        else if (GameManager.mouseSpeed >= 2)
        {
            GetComponent<Renderer>().material.color = Color.yellow;
        }
        else if (GameManager.mouseSpeed >= 1.1)
        {
            GetComponent<Renderer>().material.color = Color.green;
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.gray;
        }

    }
}








