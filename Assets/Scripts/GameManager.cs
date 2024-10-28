using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static float mouseSpeed = 0f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update() {
        //Get the speed of the mouse and store it in public static mouseSpeed
        mouseSpeed = Mathf.Sqrt(Input.GetAxis("Mouse X") * Input.GetAxis("Mouse X") + Input.GetAxis("Mouse Y") * Input.GetAxis("Mouse Y"));
    }

    //Method to be called on collision, speed "ranges," when mouseSpeed (how fast you flick the paddle) falls into a certain range, that's the multiplier you get when you hit the ball
    // also sets color accordingly, red = fastest, yellow = faster, green = fast, gray = no color
 
}
