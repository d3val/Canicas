using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleFunctions : MonoBehaviour
{
    private float limitBoundY = 8.5f;
    public bool rolling = false;
    public bool waiting = true;
    public Rigidbody objectRB;
    public float rotationSpeed = 10;
    public bool inHole = false;

    // Start is called before the first frame update
    void Start()
    {
        objectRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        OutOfBounds();
        WaitingBehavior();
    }

    private void WaitingBehavior()
    {
        if (waiting)
        {
            objectRB.isKinematic = true;
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }

    }

    private void OutOfBounds()
    {
        if (transform.position.y < -limitBoundY)
        {
            SetPosition();
            rolling = false;
            waiting = true;
        }
    }

    public void SetPosition()
    {
        transform.position = Vector3.zero;
    }

    public void SetPosition(Vector3 newPosition)
    {
        transform.position = newPosition;
    }


}
