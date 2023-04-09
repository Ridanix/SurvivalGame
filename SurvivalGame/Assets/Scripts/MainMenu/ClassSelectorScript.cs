using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ClassSelectorScript : MonoBehaviour
{
    public GameObject[] classes;
    public int selectedClass = 0;
    [SerializeField]public TMP_Text textClass;
    public void NextClass()
    {
        classes[selectedClass].SetActive(false);
        selectedClass = (selectedClass + 1) % classes.Length;
        classes[selectedClass].SetActive(true);
        switch (selectedClass)
        {
            default:
            case 0:
                textClass.text = "Knight";
                break;
            case 1:
                textClass.text = "Archer";
                break;
            case 2:
                textClass.text = "Wizard";
                break;
        }
    }
    public void PreviousClass()
    {
        classes[selectedClass].SetActive(false);
        selectedClass--;
        if (selectedClass < 0)
        {
            selectedClass += classes.Length;
        }
        classes[selectedClass].SetActive(true);
        switch (selectedClass)
        {
            default:
            case 0:
                textClass.text = "Knight";
                break;
            case 1:
                textClass.text = "Archer";
                break;
        }
    }
    public void StartGame()
    {
        PlayerPrefs.SetInt("selectedClassInt", selectedClass);
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}
