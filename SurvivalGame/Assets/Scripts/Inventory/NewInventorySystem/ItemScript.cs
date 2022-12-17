using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemScript
{
    public enum ItemType
    {
        HealthPotion,
        Gladius,
        Staf,
        Bow,
        Coin
    }

    public ItemType itemType;
    public int amount;

    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.HealthPotion: return ItemSprites.Instance.healthPotionSprite;
            case ItemType.Gladius:      return ItemSprites.Instance.gladiusSprite;
        }
    }


    public bool IsStackable()
    {
        switch (itemType)
        {
            default:
            case ItemType.Gladius:
            case ItemType.HealthPotion:
                return true;
        }

    }

}

