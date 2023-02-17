using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class SkillSlot : MonoBehaviour
{
    public Skill skill;
    [SerializeField] Slider loadingSlider;
    [SerializeField] TMP_Text timerText;
    [SerializeField] Image slotImage;
    float timer;

    AbilityState state = AbilityState.ready;
    public KeyCode key;
    
    private void Awake()
    {
        if (skill== null)
            slotImage.gameObject.SetActive(false);
    }

    void Update()
    {
        if (skill.isActiveInSkillManager)
        {
            switch (state)
            {
                case AbilityState.ready:
                    timerText.gameObject.SetActive(false);
                    if (Input.GetKeyDown(key))
                    {
                        skill.Activate();
                        state = AbilityState.active;
                        timer = 0;
                        loadingSlider.maxValue = skill.activeTime;
                        loadingSlider.value = 0f;
                    }
                    break;
                
                
                case AbilityState.active:
                    timerText.gameObject.SetActive(true);
                    if (timer<skill.activeTime)
                    {
                        timer += Time.deltaTime;
                        loadingSlider.value += Time.deltaTime;
                        timerText.text = timer.ToString();
                    }
                    else
                    {
                        state = AbilityState.onColdown;
                        loadingSlider.maxValue = skill.coolDownTime;
                        timer = skill.coolDownTime;
                    }
                    break;
                
                
                case AbilityState.onColdown:
                    if (timer>0f)
                    {
                        timer -= Time.deltaTime;
                        loadingSlider.value = timer;
                        timerText.text = timer.ToString();
                    }
                    else
                    {
                        state = AbilityState.ready;
                    }
                    break;
            }
        }
    }

    //DALE JEN FUNKCE PRO UI SKILSETU

    public void SetSkillInThisSlot(Skill newSkill)
    {
        skill.isActiveInSkillManager = false;
        skill = newSkill;
        if (skill != null)
        {
            slotImage.gameObject.SetActive(true);
            slotImage.sprite = newSkill.skillImage;
            skill.isActiveInSkillManager = true;
        }
        else
        {
            slotImage.gameObject.SetActive(false);
        }
    }
}

public enum AbilityState
{
    ready,
    active,
    onColdown
}
