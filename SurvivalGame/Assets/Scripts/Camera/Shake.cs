using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    public bool start = false;
    public static bool isActive = false;
    public float duration = 0.1f;

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            isActive = true;
            start = false;
            StartCoroutine(Shaking());
        }
    }
    IEnumerator Shaking()
    {
        Vector3 startPostion = transform.position;
        float elapsedTime = 0f;

        while(elapsedTime < duration)
        {
            startPostion = transform.position;
            elapsedTime += Time.deltaTime;
            transform.position = startPostion + (Random.insideUnitSphere * 0.1f);
            if (transform.localPosition.magnitude> 3 )
            {
                transform.position = transform.parent.position; 
            }
            yield return null;
        }

        //transform.position = startPostion;

    }
}
