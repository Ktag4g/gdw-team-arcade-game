using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PteriMovement : MonoBehaviour
{
    //Game board boundaries
    int BOARDBOUNDS = 10;

    Vector3 ogSpot;
    Vector3 newSpot;

    bool isGliding = false;

    float speed = 2.0f;

    //Player Position
    float playerHeight = -2.5f;
    int x;
    int y;


    // Start is called before the first frame update
    void Start()
    {
        //Randomizes player start position
        do
        {
            x = Random.Range(-BOARDBOUNDS, BOARDBOUNDS + 1);
            y = Random.Range(-BOARDBOUNDS, BOARDBOUNDS + 1);
        }
        while (IsMovable(new Vector3(x, y, playerHeight)) == false);

        gameObject.transform.position = new Vector3(x, y, playerHeight);
    }

    // Update is called once per frame
    void Update()
    {
        //Move Left on Grid
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (IsMovable(transform.position + Vector3.left * 2) == true)
            {
                transform.position = transform.position + Vector3.left * 2;
            }            
        }

        //Move Right on Grid
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (IsMovable(transform.position + Vector3.right * 2) == true)
            {
                transform.position = transform.position + Vector3.right * 2;
            }
        }

        //Move Up on Grid
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (IsMovable(transform.position + Vector3.up * 2) == true)
            {
                transform.position = transform.position + Vector3.up * 2;
            }
        }

        //Move Down on Grid
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (IsMovable(transform.position + Vector3.down * 2) == true)
            {
                transform.position = transform.position + Vector3.down * 2;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space /*change for arcade*/))
        {
            ogSpot = transform.position;
            isGliding = true;
        }

        if (isGliding)
        {
            if (transform.position.y != BOARDBOUNDS && transform.position.y != -BOARDBOUNDS)
            {
                newSpot = new Vector3(-transform.position.x, transform.position.y, transform.position.z);

                Vector3.MoveTowards(ogSpot, newSpot, Time.deltaTime * speed);
            }
            if (transform.position.x != BOARDBOUNDS && transform.position.x != -BOARDBOUNDS)
            {
                newSpot = new Vector3(transform.position.x, -transform.position.y, transform.position.z);
                Vector3.MoveTowards(ogSpot, newSpot, Time.deltaTime * speed);
            }

            if (transform.position == newSpot)
            {
                isGliding = false;
            }
        }
    }

    void Glide()
    {
        Vector3 ogSpot = transform.position;
        Vector3 newSpot;

        if (transform.position.y != BOARDBOUNDS && transform.position.y != -BOARDBOUNDS)
        {
            newSpot = new Vector3(-transform.position.x, transform.position.y, transform.position.z);

            Vector3.MoveTowards(ogSpot, newSpot, Time.deltaTime * speed);
        }
        if (transform.position.x != BOARDBOUNDS && transform.position.x != -BOARDBOUNDS)
        {
            newSpot = new Vector3(transform.position.x, -transform.position.y, transform.position.z);
            Vector3.MoveTowards(ogSpot, newSpot, Time.deltaTime * speed);
        }
    }

    void Drop()
    {

    }

    bool IsMovable(Vector3 gridSpot)
    {
        if(gridSpot.x % 2 == 0 && gridSpot.y % 2 == 0)
        {
            if (gridSpot.x == BOARDBOUNDS || gridSpot.x == -BOARDBOUNDS)
            {
                if ((gridSpot.y <= BOARDBOUNDS && gridSpot.y >= -BOARDBOUNDS))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (gridSpot.y == BOARDBOUNDS || gridSpot.y == -BOARDBOUNDS)
            {
                if ((gridSpot.x <= BOARDBOUNDS && gridSpot.x >= -BOARDBOUNDS))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }        
        }
        else
        {
            return false;
        }
    }
}
