using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float startSpeed = 10.0f;
    public int speedDamage = 0;

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
        bool pierce = false;

        // In order to change the ball's speed I need to normalize the vector, then multiply that by startSpeed and SpeedMult
        // When ball hits the paddle, determine the speed multiplier, then set the speed of the ball to the start speed times the speed multiplier,
        // set the amount of speeddamage the ball has, and change the color accordingly
        if (other.gameObject.CompareTag("Paddle"))
        {
            if (GameManager.mouseSpeed >= 4)
            {
                speedMult = 2f;
                speedDamage = 3;
                GetComponent<Renderer>().material.color = Color.red;
            }
            else if (GameManager.mouseSpeed >= 2)
            {
                speedMult = 1.75f;
                speedDamage = 2;
                GetComponent<Renderer>().material.color = Color.yellow;
            }
            else if (GameManager.mouseSpeed >= 1.1)
            {
                speedMult = 1.3f;
                speedDamage = 1;
                GetComponent<Renderer>().material.color = Color.green;
            }
            else
            {
                speedMult = 1f;
                speedDamage = 0;
                GetComponent<Renderer>().material.color = Color.gray;
            }
            Debug.Log("THE SPEED MULTIPLIER IS " + speedMult);

            rb.velocity = rb.velocity.normalized * startSpeed * speedMult;

        }
        //If ball collides with brick, 
        if (other.gameObject.CompareTag("Brick"))
        {
            //lower the balls speed by startSpeed/2 magnitude (5 mag at startspeed 10) every time the ball collides with a brick and has more than the Starting magnitude + the amount of brick friction
            //Remember that normalized vector is the direction with a magnitude of 1
            float brickFriction = startSpeed / 2;

            if (rb.velocity.magnitude >= startVector.magnitude + brickFriction) {
                rb.velocity = rb.velocity.normalized * (velo.magnitude - brickFriction);
            }

            //determine remaining speedDamage
            if (speedDamage > 0)
            {
                speedDamage -= other.gameObject.GetComponent<Brick>().brickHealth;
                if (speedDamage < 0)
                {
                    speedDamage = 0;
                }
            }

            //Determine the color of the ball based on the magnitude, this should go from red -> yellow -> green -> gray as the ball bounces around 
            if (magnitude >= startSpeed * 2f)
            {
                GetComponent<Renderer>().material.color = Color.red;
            }
            else if (magnitude >= startSpeed * 1.75f)
            {
                GetComponent<Renderer>().material.color = Color.yellow;
            }
            else if (magnitude >= startSpeed * 1.3f)
            {
                GetComponent<Renderer>().material.color = Color.green;
            }
            else
            {
                GetComponent<Renderer>().material.color = Color.gray;
            }


        }
        Debug.Log("THE NUMBER OF DAMAGE LEFT IS " + speedDamage);

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

        //THIS CODE DOESNT WORK AND SHOULDNT BE NECESSARY ANYMORE
        //Check if the ball ever drops below the starting speed and if it does, set it to the starting speed. Uses magnitude here because Vector3s cannot be compared using less/greater than
        if (rb.velocity.magnitude <= startMagnitude -0.1f)
        {
            rb.velocity = rb.velocity.normalized * startMagnitude;
        } 
    }
}
