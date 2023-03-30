using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ToltipManager : MonoBehaviour
{
    public static ToltipManager instance;
    public static bool tolTipIsShown = false;

    public TMP_Text itemNameText;
    public TMP_Text itemDescriptionText;
    public TMP_Text itemStatsText;
    public Image itemImage;
    public bool show = false;

    void Start()
    {
        instance = this;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(tolTipIsShown == true)
        {
            transform.position = Input.mousePosition;
        }
    }

    public void SetAndShowTolTip(string name,string description, Sprite image, string stats)
    {
        tolTipIsShown = true;
        gameObject.SetActive(tolTipIsShown);
        itemNameText.text = name;
        itemDescriptionText.text = description;
        itemImage.sprite = image;
        itemStatsText.text = stats;
    }

    public void HideTolTip()
    {
        tolTipIsShown = false;
        gameObject.SetActive(tolTipIsShown);
        itemNameText.text = string.Empty;
        itemDescriptionText.text = string.Empty;
        itemStatsText.text = string.Empty;
        itemImage.sprite = null;
    }

}
