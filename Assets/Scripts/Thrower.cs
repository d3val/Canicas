using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrower : MonoBehaviour
{
    public List<GameObject> marbles;
    private int currentMarbleIndex;
    [SerializeField] GameObject currentMarble;

    public float horizontalInput;
    public float verticalInput;

    public float speedMove = 10;

    public GameObject directionInidicator;
    public Vector3 initialPosDirectionIndicator;
    public float angleSpeedMove = 20;

    public Vector3 throwPosition;
    public GameObject angleIndicator;
    public float throwForce;

    public bool movePhase = true;
    public bool anglePhase = false;
    public bool forcePhase = false;
    public GameObject forceBar;

    public Vector3 initialMarbleOrganizePosition;
    private float OrganizeOffsetY = 2;
    private float OrganizeOffsetZ = -0.5f;

    private bool avaibleMarbles;
    public bool gameOver = false;
    private GameManager gameManager;

    private bool ready = true;


    private void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        currentMarbleIndex = 0;
        currentMarble = marbles[currentMarbleIndex];
        currentMarble.GetComponent<MarbleFunctions>().SetPosition(throwPosition);
        currentMarble.GetComponent<MarbleFunctions>().waiting = false;
        currentMarble.GetComponent<MarbleFunctions>().objectRB.isKinematic = true;

    }

    // Update is called once per frame
    void Update()
    {
        CheckAvaibleMarbles();
        if (gameOver)
        {
            gameManager.GameOver();
        }

        if (CountWaitingMarbles() == 1 && !ready)
        {
            ready = true;
            SetNextCurrentMarble();
        }

        if (currentMarble.GetComponent<MarbleFunctions>().waiting)
        {
            currentMarble.GetComponent<MarbleFunctions>().waiting = false;
            PrepareMarble();
        }

        ReadInput();

        if (movePhase)
        {
            MoveHorizontal();
        }
        else if (anglePhase)
        {
            angleIndicator.transform.position = currentMarble.transform.position;

            VisualAngleIndicator();
            SetAngle();
        }
        else if (forcePhase)
        {
            // Force bar code
        }

        OrganizeWaitingMarbles();
    }

    // Sets the position for marbles that are not using.
    private void OrganizeWaitingMarbles()
    {
        Vector3 vectorOffset = Vector3.zero;
        foreach (GameObject marble in marbles)
        {
            if (marble.GetComponent<MarbleFunctions>().waiting)
            {
                marble.transform.position = initialMarbleOrganizePosition - vectorOffset;
                vectorOffset += new Vector3(0, OrganizeOffsetY, OrganizeOffsetZ);
            }
        }
    }

    // Reads input from keyboard and controls thrower phases depending on the input.
    private void ReadInput()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");

        if (Input.GetKeyUp(KeyCode.Return) && movePhase)
        {
            movePhase = false;
            anglePhase = true;
            angleIndicator.SetActive(true);

        }
        else if (Input.GetKeyUp(KeyCode.Return) && anglePhase)
        {
            anglePhase = false;
            forcePhase = true;
            forceBar.SetActive(true);
        }

        if (forcePhase && Input.GetKeyDown(KeyCode.Space))
        {
            ThrowObject();
            angleIndicator.SetActive(false);

            forcePhase = false;
        }
    }

    // Move the marble horizontally
    private void MoveHorizontal()
    {
        float step = horizontalInput * speedMove * Time.deltaTime;
        currentMarble.transform.Translate(Vector3.left * step);
    }

    // Move the direction indicator horizontally
    private void SetAngle()
    {
        float step = horizontalInput * angleSpeedMove * Time.deltaTime;
        directionInidicator.transform.Translate(Vector3.left * step);
    }

    // Add an impulse force to the gameObject with a direction to directionIndicator's position
    private void ThrowObject()
    {
        ForceBar forceBarComp = forceBar.GetComponent<ForceBar>();
        float forceModifier = forceBarComp.GetSliderValue();
        Vector3 direction = directionInidicator.transform.position - currentMarble.transform.position;
        Vector3 finalForce = direction.normalized * throwForce * forceModifier;
        forceBarComp.Stop();

        currentMarble.GetComponent<MarbleFunctions>().objectRB.isKinematic = false;
        currentMarble.GetComponent<Rigidbody>().AddForce(finalForce, ForceMode.Impulse);
        currentMarble.GetComponent<MarbleFunctions>().rolling = true;
        currentMarble.GetComponent<MarbleFunctions>().waiting = false;

        if (CountWaitingMarbles() < 1)
        {
            ready = false;
        }
        else
        {
            SetNextCurrentMarble();
        }
    }

    // Sets thrower phases to his original values
    private void ResetPhases()
    {
        directionInidicator.transform.position = initialPosDirectionIndicator;
        movePhase = true;
        anglePhase = false;
        forcePhase = false;
        forceBar.SetActive(false);
    }

    // Sets position and rotation for the next marble that will be throw
    private void PrepareMarble()
    {
        currentMarble.GetComponent<MarbleFunctions>().waiting = false;
        currentMarble.GetComponent<MarbleFunctions>().SetPosition(throwPosition);
        currentMarble.transform.rotation = Quaternion.identity;
    }

    // Sets the next marble that will be throw
    private void SetNextCurrentMarble()
    {
        if (CheckAvaibleMarbles())
        {
            currentMarbleIndex += 1;
            if (currentMarbleIndex >= marbles.Count)
            {
                currentMarbleIndex = 0;
            }
            currentMarble = marbles[currentMarbleIndex];
            MarbleFunctions marbledata = currentMarble.GetComponent<MarbleFunctions>();


            if (marbledata.waiting)
            {
                PrepareMarble();
                ResetPhases();
            }
            else
            {
                SetNextCurrentMarble();
            }
        }

    }

    // Revises if there is at least one marble to throw
    private bool CheckAvaibleMarbles()
    {
        bool ans = false;
        foreach (GameObject marble in marbles)
        {
            if (!marble.GetComponent<MarbleFunctions>().inHole)
            {
                ans = true;
            }
        }
        if (!ans)
        {
            gameOver = true;
        }
        return ans;
    }

    private void VisualAngleIndicator()
    {
        Vector3 lookDirection = angleIndicator.transform.position - directionInidicator.transform.position;
        angleIndicator.transform.rotation = Quaternion.LookRotation(lookDirection, Vector3.up);
    }

    // Return the number of marbles in waiting state
    private int CountWaitingMarbles()
    {
        int n = 0;
        foreach (GameObject marble in marbles)
        {
            if (marble.GetComponent<MarbleFunctions>().waiting)
            {
                n++;
            }
        }
        return n;
    }


}
