using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeoutDestroy : MonoBehaviour
{
    public float timeout;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TimeOutDestroy());
    }

    IEnumerator TimeOutDestroy()
    {
        yield return new WaitForSeconds(timeout);
        Destroy(gameObject);
    }
}
