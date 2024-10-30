using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static float mouseSpeed = 0f;
    public GameObject ballPrefab;
    public GameObject paddlePrefab;
    public int brickCount = 32;

    public static int lives = 3;
    public TextMeshProUGUI statusTMP;
    public TextMeshProUGUI livesTMP;
    public GameObject newGameButton;

    public void newBall()
    {
        Instantiate(ballPrefab, new Vector3(0, 1, -10), Quaternion.identity);
    }
    // Start is called before the first frame update
    void Start()
    {
        lives = 3;
        livesTMP.text = "Lives: " + lives;

        newBall();
        newGameButton.SetActive(false);

        Cursor.visible = false;
        statusTMP.gameObject.SetActive(false);
        Instantiate(paddlePrefab, new Vector3(0, 1, -13), Quaternion.identity);
    }
    
    // Update is called once per frame
    void Update() {
        //Get the speed of the mouse and store it in public static mouseSpeed
        mouseSpeed = Mathf.Sqrt(Input.GetAxis("Mouse X") * Input.GetAxis("Mouse X") + Input.GetAxis("Mouse Y") * Input.GetAxis("Mouse Y"));

        if (brickCount <= 0 )
        {
            statusTMP.text = "YOU WIN!";
            statusTMP.gameObject.SetActive(true);
            newGameButton.SetActive(true);
            Cursor.visible = true;
            Destroy(GameObject.Find("Paddle").gameObject);
        }
        if (lives <= 0 )
        {
            statusTMP.text = "YOU LOSE!";
            statusTMP.gameObject.SetActive(true);
            newGameButton.SetActive(true);
            Cursor.visible = true;
            Destroy(GameObject.FindWithTag("Paddle"));
        }
    }

    //Method to call on starting a new game
    public void newGame()
    {
        lives = 3;
        livesTMP.text = "Lives: " + lives;

        newBall();
        newGameButton.SetActive(false);

        Cursor.visible = false;

        //Disables TMP
        statusTMP.gameObject.SetActive(false);
        Instantiate(paddlePrefab, new Vector3(0, 1, -13), Quaternion.identity);
    }
 
}
