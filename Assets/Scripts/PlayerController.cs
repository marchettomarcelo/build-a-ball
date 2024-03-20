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
    public TMP_Text countText;

    public GameObject winTextObject;
    public GameObject loseTextObject;
    public GameObject restartButton;

    private bool ganhouJogo;
    private bool perdeuJogo;

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


    public AudioSource src;
    public AudioClip src1, src2;

    // Start is called before the first frame update
    void Start()
    {

        perdeuJogo = false;
        ganhouJogo = false;

        rb = GetComponent<Rigidbody>();

        count = 0;

        SetCountText();

        timeIsRunning = true;
        timeRemaining = 11;

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

        if (ganhouJogo == true) return;
        if (perdeuJogo == true) return;


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

        if (count == 15)
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

            src.clip = src1;
            src.Play();


            other.gameObject.SetActive(false);
            count++;

            timeRemaining += 5;

            SetCountText();

        }

        if (other.gameObject.CompareTag("Enemy"))
        {

            src.clip = src2;
            src.Play();

            LostScreen();

        }



    }

    private void WinScreen()
    {
        ganhouJogo = true;
        if (perdeuJogo == true) return;

        countText.text = "";
        timeText.text = "";
        winTextObject.SetActive(true);
        restartButton.SetActive(true);
        fundoTela.SetActive(true);

    }

    private void LostScreen()
    {
        perdeuJogo = true;
        if (ganhouJogo == true) return;
        countText.text = "";
        timeText.text = "";
        timeIsRunning = false;


        restartButton.SetActive(true);
        loseTextObject.SetActive(true);

        fundoTela.SetActive(true);

    }

}
