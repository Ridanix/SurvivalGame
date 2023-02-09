using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAxe : MonoBehaviour
{
    [SerializeField] float projectileDmg;
    [SerializeField] LayerMask playerLayer;

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
            //entityInventory.HealOrDamage(-projectileDmg);
        }
        Destroy(gameObject);
    }
}
