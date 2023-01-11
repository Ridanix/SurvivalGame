using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpTextParent : MonoBehaviour
{
    public List<ScriptableItem> itemstoDisplay = new List<ScriptableItem>();
    public GameObject TextPrefab;
    public static int generatedInstances = 0;

    public void Update()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        PickUpTextComponent.parent = this;
        for (int i = generatedInstances; i < itemstoDisplay.Count; i++)
        {
            if(i <= 5)
            {
                GenerateText(itemstoDisplay[i]);
            }
        }
    }

    public void GenerateText(ScriptableItem item)
    {
        GameObject generatedText = Instantiate(TextPrefab, this.gameObject.transform);
        PickUpTextComponent script = generatedText.GetComponent<PickUpTextComponent>();
        script.item = item;
        generatedInstances++;
    }
}
