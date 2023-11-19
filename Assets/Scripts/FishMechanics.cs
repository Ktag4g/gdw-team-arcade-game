using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMechanics : MonoBehaviour
{
    //Game board bondaries
    int BOARDBOUNDS = 9;

    //Player Position

    int x;
    int y;

    //Movement Varriables
    private float horizontalInput;
    private float verticalInput;
    public float speed = 10.0f;

    //Dash Variables
    public float dashTime;
    public float restTime;
    public float dash = 2;
    public float rest = 8;

    // Start is called before the first frame update
    void Start()
    {
        //Randomizes player start position
        do
        {
            x = Random.Range(-BOARDBOUNDS, BOARDBOUNDS + 1);
            y = Random.Range(-BOARDBOUNDS, BOARDBOUNDS + 1);
        }
        while (IsMovable(new Vector3(x, y)) == false);
        gameObject.transform.position = new Vector3(x, y);
    }

    // Update is called once per frame
    void Update()
    {
        //Move Player Horizontally
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);
        //Move Player Horizontally
        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.up * verticalInput * Time.deltaTime * speed);

        /*if (horizontalInput > 0)
        {
            transform.Rotate(0, 0, 90);
        }
        if (horizontalInput < 0)
        {
            transform.Rotate(0, 0, -90);
        }
        if (verticalInput > 0)
        {
            transform.Rotate(0, 0, 0);
        }
        if (verticalInput < 0)
        {
            transform.Rotate(0, 0, 180);
        }*/

        //Horizontal Movement Boundaries
        if (transform.position.x < -BOARDBOUNDS)
        {
            transform.position = new Vector3(-BOARDBOUNDS, transform.position.y, transform.position.z);
        }
        if (transform.position.x > BOARDBOUNDS)
        {
            transform.position = new Vector3(BOARDBOUNDS, transform.position.y, transform.position.z);
        }
        //Vertical Movement Boundaries
        if (transform.position.y < -BOARDBOUNDS)
        {
            transform.position = new Vector3(transform.position.x, -BOARDBOUNDS, transform.position.z);
        }
        if (transform.position.y > BOARDBOUNDS)
        {
            transform.position = new Vector3(transform.position.x, BOARDBOUNDS, transform.position.z);
        }

        //Dash
        if (Input.GetKeyDown(KeyCode.LeftShift /*change for arcade*/))
        {
            speed *= dash;
            StartCoroutine(TimeOutDash());
        }
    }

    IEnumerator TimeOutDash()
    {
        yield return new WaitForSeconds(dashTime);
        speed /= rest;
        StartCoroutine(TimeOutRest());
    }
    IEnumerator TimeOutRest()
    {
        yield return new WaitForSeconds(restTime);
        speed *= rest / 2;
    }

    bool IsMovable(Vector3 newSpot)
    {
        if (newSpot.x <= BOARDBOUNDS && newSpot.x >= -BOARDBOUNDS &&
            newSpot.y <= BOARDBOUNDS && newSpot.y >= -BOARDBOUNDS)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "AttackAOE")
        {
            Destroy(gameObject);
            Debug.Log("GAME OVER: Fish has been hit!");
        }
    }
}