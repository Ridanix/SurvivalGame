using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject thisSpawner;
    public List<GameObject> itemsToSpawn = new List<GameObject>();
    public float hitsBeforeDestruction;
    public WeaponType vulnerability;


    //Trreeeee problemaic https://forum.unity.com/threads/finally-removing-trees-and-the-colliders.110354/
    
    
    //public void Awake()
    //{
    //    thisSpawner = this.transform.parent.gameObject;
    //    Debug.LogWarning(thisSpawner.name);
    //}

    public void TakeDamage(float destructionPoints, WeaponType damageTypeRequired)
    {
        if(damageTypeRequired == vulnerability)
        {
            hitsBeforeDestruction-= destructionPoints;
            if (hitsBeforeDestruction<=0)
            {
                foreach (GameObject item in itemsToSpawn)
                {
                    Instantiate(item, this.transform.position, Quaternion.identity);
                }
                Destroy(this.gameObject);
            }
        }
    }
}
