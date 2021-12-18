using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] Vector3 initialPos;
    [SerializeField] Vector3 secondPos;
    private bool positionSwitch = true;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = initialPos;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            positionSwitch = !positionSwitch;
        }

        if (positionSwitch)
        {
            transform.position = initialPos;
        }
        else
        {
            transform.position = secondPos;
        }


    }
}
