using UnityEngine;

public class Thrower : MonoBehaviour
{
    public GameObject marble;
    public GameObject directionInidicator;

    public float horizontalInput;
    public float verticalInput;

    public float speedMove = 10;
    public float throwForce;

    public bool movePhase = true;
    public bool anglePhase = false;
    public bool forcePhase = false;

    // Update is called once per frame
    void Update()
    {
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

        }
    }

    // Move the marble horizontally
    private void MoveHorizontal()
    {
        float step = horizontalInput * speedMove * Time.deltaTime;
        marble.transform.Translate(Vector3.left * step);
    }

    // Move the direction indicator horizontally
    private void SetAngle()
    {
        float step = horizontalInput * speedMove * Time.deltaTime;
        directionInidicator.transform.Translate(Vector3.left * step);
    }

    // Add an impulse force to the marble with direction to directionIndicator's position
    private void ThrowObject()
    {
        Vector3 direction = directionInidicator.transform.position - marble.transform.position;
        marble.GetComponent<Rigidbody>().AddForce(direction.normalized * throwForce, ForceMode.Impulse);
    }
}
