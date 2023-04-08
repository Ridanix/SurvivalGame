using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    public float rotati;
    public Transform Player11;
    // Start is called before the first frame update
    void Start()
    {
        rotati = 0.003f;
    }

    // Update is called once per frame
    void Update()
    {
        Player11.Rotate(rotati * new Vector3(1, 0, 0));
    }
}
