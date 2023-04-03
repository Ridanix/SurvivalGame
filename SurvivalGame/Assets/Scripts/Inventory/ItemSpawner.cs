using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public List<GameObject> itemsToSpawn = new List<GameObject>();
    public float hitsBeforeDestruction;
    public WeaponType vulnerability;

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
        else
        {
            Debug.Log("Wrong Item");
        }

    }
}
