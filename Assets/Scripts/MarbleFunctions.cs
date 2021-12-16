using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleFunctions : MonoBehaviour
{
    private float limitBoundY = 8.5f;
    private float limitBoundX = 14.5f;

    public bool rolling = false;
    public bool waiting = true;
    public Rigidbody objectRB;
    public float rotationSpeed = 10;
    public bool inHole = false;
    public TrailRenderer trailRenderer;
    public ParticleSystem sparkExplosion;

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
        if (rolling)
        {
            trailRenderer.enabled = true;
        }
        else
        {
            trailRenderer.enabled = false;
        }
    }

    // Marble's behavior when is waiting
    private void WaitingBehavior()
    {
        if (waiting)
        {
            objectRB.isKinematic = true;
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }

    }

    // Reposition the gameObject when is out of bounds
    private void OutOfBounds()
    {
        if (transform.position.y < -limitBoundY)
        {
            SetPosition();
            rolling = false;
            waiting = true;
        }

        if (transform.position.x < -limitBoundX)
        {
            transform.position = new Vector3(-limitBoundX, transform.position.y, transform.position.z);
        }

        if (transform.position.x > limitBoundX)
        {
            transform.position = new Vector3(limitBoundX, transform.position.y, transform.position.z);
        }

    }

    // Moves the gameObject to Vector3.zero
    public void SetPosition()
    {
        transform.position = Vector3.zero;
    }

    // Moves the gameObject to a provided specific vector3
    public void SetPosition(Vector3 newPosition)
    {
        transform.position = newPosition;
    }

    public void PlaySparkExplosion()
    {
        sparkExplosion.Play();
    }

}
