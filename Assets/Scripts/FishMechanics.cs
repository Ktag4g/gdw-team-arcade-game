using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMechanics : MonoBehaviour
{
    private GameManager gameManager;

    //Sprite Variables
    SpriteRenderer sprite;
    public Sprite normalSprite;
    public Sprite zoomSprite;
    public Sprite tiredSprite;

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
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        sprite = GameObject.Find("FishSprite").GetComponent<SpriteRenderer>();

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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            speed *= dash;
            sprite.sprite = zoomSprite;
            StartCoroutine(TimeOutDash());
        }
    }

    IEnumerator TimeOutDash()
    {
        yield return new WaitForSeconds(dashTime);
        speed /= rest;
        sprite.sprite = tiredSprite;
        StartCoroutine(TimeOutRest());
    }
    IEnumerator TimeOutRest()
    {
        yield return new WaitForSeconds(restTime);
        speed *= rest / 2;
        sprite.sprite = normalSprite;
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
            gameManager.FishGameover();
        }
    }
}
