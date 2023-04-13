using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClassSelectScript : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject model;
    [SerializeField] TMP_Text classText;
    [SerializeField] Texture[] modelTexture;
    private Renderer modelRenderer;
    // Start is called before the first frame update
    void Start()
    {
        modelRenderer = model.GetComponent<Renderer>();
        int randomTextLineSelector = Random.Range(0, 8);
        StartCoroutine(DestroyText());
        int selectedCharacter = PlayerPrefs.GetInt("selectedClassInt");
        switch (selectedCharacter)
        {
            default:
            case 0:
                player.transform.position = new Vector3(792, 11, 778);
                modelRenderer.material.mainTexture = modelTexture[0];
                switch (randomTextLineSelector)
                {
                    default:
                    case 0:
                        classText.text = "Behold! A Knight!";
                        break;
                    case 1:
                        classText.text = "From the Roar of the Swords a knight rose!";
                        break;
                    case 2:
                        classText.text = "Welcome, valiant knight! Your quest awaits.";
                        break;
                    case 3:
                        classText.text = "All hail the knight! Enter and be honored.";
                        break;
                    case 4:
                        classText.text = "Silver is the armor!";
                        break;
                    case 5:
                        classText.text = "Fair tidings, noble knight!";
                        break;
                    case 6:
                        classText.text = "Do you hear swords clashing?";
                        break;
                    case 7:
                        classText.text = "Welcome to our realm, knight!";
                        break;

                }
                break;
            case 1:
                player.transform.position = new Vector3(206, 11, 756);
                modelRenderer.material.mainTexture = modelTexture[1];
                switch (randomTextLineSelector)
                {
                    default:
                    case 0:
                        classText.text = "An Archer was scouted!";
                        break;
                    case 1:
                        classText.text = "Welcome, sharp-eyed!";
                        break;
                    case 2:
                        classText.text = "Greetings, hunter!";
                        break;
                    case 3:
                        classText.text = "Aye, Isn't it the master of bows?";
                        break;
                    case 4:
                        classText.text = "Hail, marksman!";
                        break;
                    case 5:
                        classText.text = "Watch, a swift arrow!";
                        break;
                    case 6:
                        classText.text = "Aim true!";
                        break;
                    case 7:
                        classText.text = "Salutations, bowman!";
                        break;

                }
                break;
            case 2:
                player.transform.position = new Vector3(636, 11, 1208);
                modelRenderer.material.mainTexture = modelTexture[2];
                switch (randomTextLineSelector)
                {
                    default:
                    case 0:
                        classText.text = "Welcome, master of magic! Show prowess.";
                        break;
                    case 1:
                        classText.text = "Welcome, wise wizard!";
                        break;
                    case 2:
                        classText.text = "Salutations, enchanted one! Enter with pride.";
                        break;
                    case 3:
                        classText.text = "Fair tidings, sorcerer!";
                        break;
                    case 4:
                        classText.text = "Greetings, mighty wizard! Your spells await.";
                        break;
                    case 5:
                        classText.text = "Enchanter supreme! Work wonders.";
                        break;
                    case 6:
                        classText.text = "Hail, a conjurer!";
                        break;
                    case 7:
                        classText.text = "I see a Warlock on the horizon!";
                        break;

                }
                break;
        }
    }
    IEnumerator DestroyText()
    {
        yield return new WaitForSeconds(4);
        Destroy(classText);
        
    }
}
