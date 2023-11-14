using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PteriMovement : MonoBehaviour
{
    //Game board boundaries
    int BOARDBOUNDS = 10;

    float speed = 10;

    /*Player Position
    float playerHeight = -2.5f;
    int x = 10;
    int y = 10; */


    // Start is called before the first frame update
    void Start()
    {
        /*Randomizes player start position
        x = Random.Range(-BOARDBOUNDS, BOARDBOUNDS + 1);
        y = Random.Range(-BOARDBOUNDS, BOARDBOUNDS + 1);
        gameObject.transform.position = new Vector3(x, y, playerHeight);*/
    }

    // Update is called once per frame
    void Update()
    {
        //Move Left on Grid
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (gameObject.transform.position.x > -BOARDBOUNDS)
            {
                if (gameObject.transform.position.y == BOARDBOUNDS || gameObject.transform.position.y == -BOARDBOUNDS)
                {
                    gameObject.transform.position = gameObject.transform.position + Vector3.left;
                }
            }
        }

        //Move Right on Grid
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (gameObject.transform.position.x < BOARDBOUNDS)
            {
                if (gameObject.transform.position.y == BOARDBOUNDS || gameObject.transform.position.y == -BOARDBOUNDS)
                {
                    gameObject.transform.position = gameObject.transform.position + Vector3.right;
                }
            }
        }

        //Move Up on Grid
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (gameObject.transform.position.y < BOARDBOUNDS)
            {
                if (gameObject.transform.position.x == BOARDBOUNDS || gameObject.transform.position.x == -BOARDBOUNDS)
                {
                    gameObject.transform.position = gameObject.transform.position + Vector3.up;
                }
            }
        }

        //Move Down on Grid
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (gameObject.transform.position.y > -BOARDBOUNDS)
            {
                if (gameObject.transform.position.x == BOARDBOUNDS || gameObject.transform.position.x == -BOARDBOUNDS)
                {
                    gameObject.transform.position = gameObject.transform.position + Vector3.down;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Space /*change for arcade*/))
        {
            Glide();
        }
    }

    void Glide()
    {

    }
}
