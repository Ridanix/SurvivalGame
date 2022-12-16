using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            case ItemType.Gladius: return ItemSprites.Instance.gladiusSprite;
            case ItemType.HealthPotion: return ItemSprites.Instance.healthPotionSprite;
        }

    }

}
