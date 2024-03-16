using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;
//using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;

    public GameObject winTextObject;
    public GameObject loseTextObject;
    public GameObject restartButton;

    public GameObject fundoTela;

    public float jumpSpeed;
    private float ySpeed;

    private int count;

    private Rigidbody rb;
    private float movementX;
    private float movementY;


    public float timeRemaining;
    public bool timeIsRunning = true;
    public TMP_Text timeText;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        count = 0;

        SetCountText();

        timeIsRunning = true;
        timeRemaining = 10;

        fundoTela.SetActive(false);
        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);
        restartButton.SetActive(false);
    }
    

    void Update()
    {

        if (timeIsRunning)
        {

            timeRemaining -= Time.deltaTime;
            DisplayTime(timeRemaining);
            if (timeRemaining < 0)
            {
                LostScreen();
            }

        }


        print(rb.position);

        if(rb.position[1] < 0 || rb.position[0] > 50 || rb.position[0] < -50 || rb.position[2] > 50 || rb.position[2] < -50)
        {
            LostScreen();
        }

        
        if (Input.GetKeyDown("space"))
        {
            Vector3 jump = new Vector3(0.0f, 200.0f, 0.0f);
            rb.AddForce(jump);
        }
    }

    void DisplayTime(float timeToDisplay)
    {

        
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        

        timeText.text = string.Format("{0:00} : {1:00}", minutes, seconds);


    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

        if (count == 8)
        {
            WinScreen();
            
        }

    }


    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp") && timeIsRunning)
        {
            other.gameObject.SetActive(false);
            count++;

            timeRemaining += 30;

            SetCountText();

        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            LostScreen();

        }



    }

    private void WinScreen()
    {
        winTextObject.SetActive(true);
        restartButton.SetActive(true);
        fundoTela.SetActive(true);

    }

    private void LostScreen()
    {
        countText.text = "";
        timeText.text = "";
        timeIsRunning = false;


        restartButton.SetActive(true);
        loseTextObject.SetActive(true);

        fundoTela.SetActive(true);

    }

}
