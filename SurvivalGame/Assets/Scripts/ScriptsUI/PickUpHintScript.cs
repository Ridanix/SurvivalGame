using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PickUpHintScript : MonoBehaviour
{
    public static PickUpHintScript instance;

    public TMP_Text textOfMessage;
    public Image fill;
    public GameObject main;
    public float fillAmount = 0f;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        main.SetActive(false);
    }

    public void FixedUpdate()
    {
        if(main.activeInHierarchy)
            LoadFill();
    }

    public void Show(string textToDisplay)
    {
        Debug.Log("enter");
        main.SetActive(true);
        instance.textOfMessage.text = textToDisplay;
    }

    public void Hide()
    {
        Debug.Log("exit"); 
        instance.textOfMessage.text = default;
        instance.main.SetActive(false);
    }

    public void LoadFill()
    {
        instance.fill.fillAmount = fillAmount;
    }
}
