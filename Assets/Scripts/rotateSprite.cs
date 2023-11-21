using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateSprite : MonoBehaviour
{
    //Input Variables
    private float horizontalInput;
    private float verticalInput;

    // Update is called once per frame
    void Update()
    {
        //Get Horizontal Input
        horizontalInput = Input.GetAxis("Horizontal");
        //Get Vertical Input
        verticalInput = Input.GetAxis("Vertical");

        //Rotate sprite according to player input
        if (horizontalInput > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, -90);
        }
        if (horizontalInput < 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 90);
        }
        if (verticalInput > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        if (verticalInput < 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 180);
        }

        if (horizontalInput > 0 && verticalInput > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, -45);
        }
        if (horizontalInput > 0 && verticalInput < 0)
        {
            transform.eulerAngles = new Vector3(0, 0, -135);
        }
        if (horizontalInput < 0 && verticalInput > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 45);
        }
        if (horizontalInput < 0 && verticalInput < 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 135);
        }
    }
}
