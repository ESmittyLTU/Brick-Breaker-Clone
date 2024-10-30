using UnityEngine;

public class Ball : MonoBehaviour
{
    public float startSpeed = 10.0f;
    public int speedDamage = 0;
    Vector3 veloBB;

    void Start()
    {
        //Sets the ball to gray
        GetComponent<Renderer>().material.color = Color.gray;
        GetComponent<TrailRenderer>().material.color = Color.gray;

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
        //bool pierce = false;

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
                GetComponent<TrailRenderer>().material.color = Color.red;
            }
            else if (GameManager.mouseSpeed >= 2)
            {
                speedMult = 1.75f;
                speedDamage = 2;
                GetComponent<Renderer>().material.color = Color.yellow;
                GetComponent<TrailRenderer>().material.color = Color.yellow;
            }
            else if (GameManager.mouseSpeed >= 1.1)
            {
                speedMult = 1.3f;
                speedDamage = 1;
                GetComponent<Renderer>().material.color = Color.green;
                GetComponent<TrailRenderer>().material.color = Color.green;
            }
            else
            {
                speedMult = 1f;
                speedDamage = 0;
                GetComponent<Renderer>().material.color = Color.gray;
                GetComponent<TrailRenderer>().material.color = Color.gray;
            }
            Debug.Log("THE SPEED MULTIPLIER IS " + speedMult);
            Debug.Log("THE NUMBER OF DAMAGE ADD IS " + speedDamage);

            rb.velocity = rb.velocity.normalized * startSpeed * speedMult;

            //Set the pierce bool to true or false if speedDamage >= brickHealth - 1
            //pierce = true;
        }
        //If ball collides with brick, 
        if (other.gameObject.CompareTag("Brick"))
        {
            //lower the balls speed by startSpeed/2 magnitude (5 mag at startspeed 10) every time the ball collides with a brick and has more than the Starting magnitude + the amount of brick friction
            //Remember that normalized vector is the direction with a magnitude of 1
            float brickFriction = startSpeed / 5;

            // Only reduce speed when bounces
            if (rb.velocity.magnitude >= startVector.magnitude + brickFriction)
            {
                rb.velocity = rb.velocity.normalized * (velo.magnitude - brickFriction);
            }

            
            int reduceSpeedDamageBy = other.gameObject.GetComponent<Brick>().OnBrickHit(speedDamage);

            //determine remaining speedDamage
            if (speedDamage > 0)
            {
                rb.velocity = veloBB;
                speedDamage -= reduceSpeedDamageBy;
                if (speedDamage < 0)
                {
                    speedDamage = 0;
                }
            }

            //Determine the color of the ball based on the magnitude, this should go from red -> yellow -> green -> gray as the ball bounces around 
            if (speedDamage == 3)
            {
                GetComponent<Renderer>().material.color = Color.red;
                GetComponent<TrailRenderer>().material.color = Color.red;
            }
            else if (speedDamage == 2)
            {
                GetComponent<Renderer>().material.color = Color.yellow;
                GetComponent<TrailRenderer>().material.color = Color.yellow;
            }
            else if (speedDamage == 1)
            {
                GetComponent<Renderer>().material.color = Color.green;
                GetComponent<TrailRenderer>().material.color = Color.green;
            }
            else
            {
                GetComponent<Renderer>().material.color = Color.gray;
                GetComponent<TrailRenderer>().material.color = Color.gray;
            }


        }

    }


    // Update is called once per frame
    void Update()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        Vector3 velo = rb.velocity;
        float magnitude = velo.magnitude;

        veloBB = rb.velocity;

        Vector3 startVector = transform.forward * startSpeed;
        float startMagnitude = startVector.magnitude;

        Debug.Log("BALL MAG IS CURRENTLY "+magnitude);

        //Check if the ball ever drops below the starting speed and if it does, set it to the starting speed. Uses magnitude here because Vector3s cannot be compared using less/greater than
        if (rb.velocity.magnitude <= startMagnitude -0.1f)
        {
            rb.velocity = (rb.velocity * 10000000000).normalized * startMagnitude;
        } 
    }
}
