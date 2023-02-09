using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    public bool start = false;
    public float duration = 0.5f;

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
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
            elapsedTime += Time.deltaTime;
            transform.position = startPostion + (Random.insideUnitSphere * 0.1f);
            yield return null;
        }

        transform.position = startPostion;
    }
}
