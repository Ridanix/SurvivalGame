using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileArrow : MonoBehaviour
{
    //Transform player; //Hlavní hráèská postava
    // [SerializeField] Golem golem;
    [SerializeField] float projectileDmg;
    //[SerializeField] EntityInventory entityInventory;
    [SerializeField] LayerMask playerLayer;
    //[SerializeField] LayerMask enemyLayer;

    // Start is called before the first frame update
    void Start()
    {
        //transform.Rotate(Vector3.up, 90);
        //transform.Rotate(Vector3.right, 90);
        //transform.Rotate(Vector3.forward, 90);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.layer == enemyLayer) return;
        //if (collision.gameObject.layer == playerLayer)
        if (collision.gameObject.name == "PlayerPrefab")
        {
            //Debug.Log("arrowhit");
            //entityInventory.HealOrDamage(-projectileDmg);
        }
        Destroy(gameObject);
    }
}
