using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float startSpeed = 10.0f;

    void Start()
    {
        //Sets the ball to gray
        GetComponent<Renderer>().material.color = Color.gray;

        //Generates a random angle within almost 160 degress (excluding a 60 degree slice in the center), and sets the ball's rotation to that angle
        if (Random.value <= 0.5f)
        {
            transform.rotation = Quaternion.AngleAxis(Random.Range(-80, -30), Vector3.up);
        }
        else
        {
            transform.rotation = Quaternion.AngleAxis(Random.Range(30, 80), Vector3.up);
        }
        
        //Grabs rigidbody's velocity and multiplies it by startSpeed
        GetComponent<Rigidbody>().velocity = transform.forward * startSpeed;
    }

    void OnCollisionEnter(Collision other)
    {

        Rigidbody rb = GetComponent<Rigidbody>();
        Vector3 velo = rb.velocity;
        float magnitude = velo.magnitude;

        Vector3 startVector = transform.forward * startSpeed;
        float startMagnitude = startVector.magnitude;

        float speedMult;
        int piercesLeft = 0;

        // When ball hits the paddle, determine the speed multiplier, set the speed of the ball (magnitude) to the start speed times the speed multiplier,
        // set the amount of pierces the ball has, and change the color accordingly
        if (other.gameObject.CompareTag("Paddle"))
        {
            if (GameManager.mouseSpeed >= 4)
            {
                speedMult = 2f;
                GetComponent<Renderer>().material.color = Color.red;
            }
            else if (GameManager.mouseSpeed >= 2)
            {
                speedMult = 1.75f;
                GetComponent<Renderer>().material.color = Color.yellow;
            }
            else if (GameManager.mouseSpeed >= 1.1)
            {
                speedMult = 1.3f;
                GetComponent<Renderer>().material.color = Color.green;
            }
            else
            {
                speedMult = 1f;
                GetComponent<Renderer>().material.color = Color.gray;
            }
            Debug.Log("THE SPEED MULTIPLIER IS " + speedMult);

            magnitude = startMagnitude * speedMult;
            GetComponent<Rigidbody>().velocity = transform.forward * magnitude;

        }
        //If ball collides with brick, subtract the bricks health from the pierces remaining and destroy the brick if necessary
        if (other.gameObject.CompareTag("Brick"))
        {
            if (GameManager.mouseSpeed >= 4)
            {
                piercesLeft = 3;
            }
            else if (GameManager.mouseSpeed >= 2)
            {
                piercesLeft = 2;
            }
            else if (GameManager.mouseSpeed >= 1.1)
            {
                piercesLeft = 1;
            }
            else
            {
                piercesLeft = 0;
            }

            //If ball collides with brick, subtract the bricks health from the pierces remaining and destroy the brick if necessary
            int brickHealth = other.gameObject.GetComponent<Brick>().brickHealth;
            if (piercesLeft > 0)
            {
                piercesLeft = piercesLeft - brickHealth;
                if (piercesLeft < 0)
                {
                    piercesLeft = 0;
                }
            }
            Debug.Log("THE NUMBER OF PIERCES LEFT IS "+piercesLeft);
            if (brickHealth - piercesLeft <= 0)
            {
                Destroy(other.gameObject);
            }
        }
        
    }


    // Update is called once per frame
    void Update()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        Vector3 velo = rb.velocity;
        float magnitude = velo.magnitude;

        Vector3 startVector = transform.forward * startSpeed;
        float startMagnitude = startVector.magnitude;

        Debug.Log("BALL MAG IS CURRENTLY "+magnitude);

        //Check if the ball ever drops below the starting speed and if it does, set it to the starting speed. Uses magnitude here because Vector3s cannot be compared using less/greater than
        if (magnitude <= startMagnitude)
        {
            magnitude = startMagnitude;
            velo = transform.forward * velo.magnitude;
        }
    }
}
