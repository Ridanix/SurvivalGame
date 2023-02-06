using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCanvasHealthBar : MonoBehaviour
{
    Transform cam;

    private void Start()
    {
        cam = GameObject.Find("Camera_GO").transform;
    }
    private void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
    }
}
