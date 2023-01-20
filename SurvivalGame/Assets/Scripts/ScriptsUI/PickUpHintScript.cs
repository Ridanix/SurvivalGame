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
    public void Update()
    {
        this.gameObject.transform.LookAt(mainCamera.transform);
        LoadFill();
    }

    public void LoadFill()
    {
        fill.fillAmount = fillAmount;
    }
}
