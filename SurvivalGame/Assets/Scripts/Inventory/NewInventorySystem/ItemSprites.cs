using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSprites : MonoBehaviour
{
    public static ItemSprites Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public Transform pfItemWorld;

    public Sprite gladiusSprite;
    public Sprite healthPotionSprite;
}
