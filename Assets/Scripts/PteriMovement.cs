using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PteriMovement : MonoBehaviour
{
    //Game board boundaries
    int BOARDBOUNDS = 10;

    //Gliding Variables
    Vector3 ogSpot;
    Vector3 newSpot;
    bool isGliding = false;
    float speed = 20.0f;

    //Player Position
    float playerHeight = -2.5f;
    int x;
    int y;

    //Attack variables
    int AOE = 2;
    bool hasDropped = false;
    public GameObject attackMarker;
    public bool attacked;
    public float timeout;

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

        //Starts Glide
        if (Input.GetKeyDown(KeyCode.Space /*change for arcade*/))
        {
            ogSpot = transform.position;
            isGliding = true;
            Debug.Log("GLIDING: Current Vector is: " + ogSpot);
        }
        if (isGliding)
        {
            ogSpot = transform.position;

            //If position is top side, glide DOWN to bottom side
            if (CheckPosition(ogSpot) == 1)
            {
                newSpot = new Vector3(transform.position.x, -transform.position.y, transform.position.z);
                
                Debug.Log("ON TOP SIDE: new spot is: " + newSpot);
            }
            //If position is right side, glide LEFT to left side
            else if (CheckPosition(ogSpot) == 2)
            {
                newSpot = new Vector3(-transform.position.x, transform.position.y, transform.position.z);

                Debug.Log("ON RIGHT SIDE: new spot is: " + newSpot);
            }
            //If position is bottom side, glide UP to top side
            else if (CheckPosition(ogSpot) == 3)
            {
                newSpot = new Vector3(transform.position.x, -transform.position.y, transform.position.z);

                Debug.Log("ON BOTTOM SIDE: new spot is: " + newSpot);
            }
            //If position is left side, glide RIGHT to right side
            else if (CheckPosition(ogSpot) == 4)
            {
                newSpot = new Vector3(-transform.position.x, transform.position.y, transform.position.z);

                Debug.Log("ON LEFT SIDE: new spot is: " + newSpot);
            }
            //If in the corner, do nothing
            else if (CheckPosition(ogSpot) == 11 || CheckPosition(ogSpot) == 12
                  || CheckPosition(ogSpot) == 31 || CheckPosition(ogSpot) == 32)
            {
                isGliding = false;
                Debug.Log("IN CORNER: not gliding");
            }

            //transform.Translate(Vector3.down * Time.deltaTime * speed);
            transform.position = Vector3.MoveTowards(ogSpot, newSpot, speed * Time.deltaTime);

            if (!isGliding)
            {
                transform.position = ogSpot;
            }

            //Once in position, stop gliding
            if (transform.position == newSpot)
            {
                isGliding = false;
                hasDropped = false;
                Debug.Log("REACHED POSITION: new position is" + transform.position);
            }
        }

        //Starts Attack
        if (Input.GetKeyDown(KeyCode.LeftShift /*change for arcade*/))
        {
            if (!hasDropped && isGliding)
            {
                Drop();
                hasDropped = true;
            }
        }

    }

    void Drop()
    {
        attacked = true;
        Vector3 dropSpot = transform.position + new Vector3(0, 0, 2.4f);

        Vector3 attackTopRight = dropSpot + new Vector3(AOE, AOE);
        Vector3 attackBottomLeft = dropSpot + new Vector3(-AOE, -AOE);

        Debug.Log("ATTACKED: attacked at spot: " + dropSpot + " AOE is currently" + AOE);
        Debug.Log("AOE Attack hits area between: Top and Right: " + attackTopRight + ", Bottom and Left: " + attackBottomLeft);

        Instantiate(attackMarker, dropSpot, attackMarker.transform.rotation);
        StartCoroutine(TimeOutAttack());
    }
    IEnumerator TimeOutAttack()
    {
        yield return new WaitForSeconds(timeout);
        attacked = false;
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

    int CheckPosition(Vector3 position)
    {
        //Top side = 1 
        //Right Side = 2
        //Bottom Side = 3
        //Left Side = 4

        //Top Side
        if (transform.position.y == BOARDBOUNDS)
        {
            if (transform.position.x == BOARDBOUNDS)
            {
                //Top Right Corner
                return 11;
            }
            else if (transform.position.x == -BOARDBOUNDS)
            {
                //Top Left Corner
                return 12;
            }
            else
            {
                //Top Side (No Corners)
                return 1;
            }
        }
        //Bottom Side
        else if (transform.position.y == -BOARDBOUNDS)
        {
            if (transform.position.x == BOARDBOUNDS)
            {
                //Bottom Right Corner
                return 31;
            }
            else if (transform.position.x == -BOARDBOUNDS)
            {
                //Bottom Left Corner
                return 32;
            }
            else
            {
                //Bottom side (No Corners)
                return 3;
            }
        }
        //Right Side
        else if (transform.position.x == BOARDBOUNDS)
        {
            //Right Side (No corners)
            return 2;
        }
        //Left Side
        else if (transform.position.x == -BOARDBOUNDS)
        {
            //Left Side (No corners)
            return 4;
        }
        //not on grid
        else
        {
            return 0;
        }
    }
       
}
