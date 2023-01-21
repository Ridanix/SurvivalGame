using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpHintScript : MonoBehaviour
{
    public Image fill;
    public float fillAmount = 0;
    private GameObject mainCamera;

    public void Awake()
    {
        mainCamera = GameObject.Find("Main Camera");
    }
    public void FixedUpdate()
    {
        this.gameObject.transform.LookAt(transform.position + mainCamera.transform.rotation*Vector3.forward, mainCamera.transform.rotation*Vector3.up);
        
        LoadFill();
    }

    public void LoadFill()
    {
        fill.fillAmount = fillAmount;
    }
}
