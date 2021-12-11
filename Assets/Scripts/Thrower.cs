using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrower : MonoBehaviour
{
    public List<GameObject> marbles;
    private int currentMarbleIndex;
    [SerializeField] GameObject currentMarble;
    public GameObject directionInidicator;

    public float horizontalInput;
    public float verticalInput;

    public float speedMove = 10;
    public Vector3 throwPosition;
    public float throwForce;

    public bool movePhase = true;
    public bool anglePhase = false;
    public bool forcePhase = false;

    public Vector3 initialMarbleOrganizePosition;
    private float OrganizeOffsetY = 2;
    private float OrganizeOffsetZ = -0.5f;

    private bool avaibleMarbles;
    public bool gameOver = false;
    private GameManager gameManager;


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
        if (gameOver)
        {
            Debug.Log("Game Over");
            // Insert GameOver code
        }

        ReadInput();

        if (movePhase)
        {
            MoveHorizontal();
        }
        else if (anglePhase)
        {
            SetAngle();
        }
        else if (forcePhase)
        {
            // Force bar code
        }

        OrganizeWaitingMarbles();
    }

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

    private void ReadInput()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");

        if (Input.GetKeyUp(KeyCode.Return) && movePhase)
        {
            movePhase = false;
            anglePhase = true;
        }
        else if (Input.GetKeyUp(KeyCode.Return) && anglePhase)
        {
            anglePhase = false;
            forcePhase = true;
        }

        if (forcePhase && Input.GetKeyDown(KeyCode.Space))
        {
            ThrowObject();
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
        float step = horizontalInput * speedMove * Time.deltaTime;
        directionInidicator.transform.Translate(Vector3.left * step);
    }

    // Add an impulse force to the gameObject with a direction to directionIndicator's position
    private void ThrowObject()
    {
        currentMarble.GetComponent<MarbleFunctions>().objectRB.isKinematic = false;
        Vector3 direction = directionInidicator.transform.position - currentMarble.transform.position;
        currentMarble.GetComponent<Rigidbody>().AddForce(direction.normalized * throwForce, ForceMode.Impulse);
        currentMarble.GetComponent<MarbleFunctions>().rolling = true;
        Debug.Log("XD");
        StartCoroutine(CheckMarbleState());
    }

    private void ResetPhases()
    {
        movePhase = true;
        anglePhase = false;
        forcePhase = false;
    }

    private void PrepareMarble()
    {
        currentMarble.GetComponent<MarbleFunctions>().waiting = false;
        currentMarble.GetComponent<MarbleFunctions>().SetPosition(throwPosition);
        currentMarble.transform.rotation = Quaternion.identity;
    }

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

            if (currentMarble.GetComponent<MarbleFunctions>().inHole)
            {
                SetNextCurrentMarble();
            }
            else
            {
                PrepareMarble();
                ResetPhases();
            }
        }

    }

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

    IEnumerator CheckMarbleState()
    {
        while (currentMarble.GetComponent<MarbleFunctions>().rolling)
        {
            Debug.Log("Goro");
            yield return null;
        }
        SetNextCurrentMarble();
    }
}
