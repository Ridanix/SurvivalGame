using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;
    
    private void Awake()
    {
        instance = this;
    }

    public SkillSlot[] skillSlots;
    public Skill skillChosenToDisplay;
    public TMP_Text skillName;
    public Image skillImage;

    public void displaySkillParametrs()
    {
        instance.skillName.text = instance.skillChosenToDisplay.name;
        instance.skillImage.sprite = instance.skillChosenToDisplay.skillImage;
    }
}




