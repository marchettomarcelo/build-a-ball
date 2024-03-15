using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

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

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        count = 0;

        SetCountText();

        fundoTela.SetActive(false);
        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);
        restartButton.SetActive(false);
    }
    

    void Update()
    {
        if(rb.position[1] < 0)
        {
            restartButton.SetActive(true);
            loseTextObject.SetActive(true);
            fundoTela.SetActive(true);
        }

        
        if (Input.GetKeyDown("space"))
        {
            Vector3 jump = new Vector3(0.0f, 200.0f, 0.0f);
            rb.AddForce(jump);
        }
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
            winTextObject.SetActive(true);
            restartButton.SetActive(true);
            fundoTela.SetActive(true);
        }

    }


    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;

            SetCountText();

        }

    }

}
