using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAxe : MonoBehaviour
{
    GameObject player;
    [SerializeField] float projectileDmg;
    [SerializeField] LayerMask playerLayer;
    //[SerializeField] LayerMask enemyLayer;

    void Start()
    {
        player = GameObject.Find("PlayerPrefab");
        transform.Rotate(transform.rotation.x - 90, transform.rotation.y, transform.rotation.z - 90);
    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        transform.Rotate(transform.rotation.x, transform.rotation.y + 10, transform.rotation.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.layer == enemyLayer) return;
        //if (collision.gameObject.layer == playerLayer)
        if (collision.gameObject.name == "PlayerPrefab")
        {
            player.GetComponent<Player_Data>().HealOrDamage(-projectileDmg);
        }
        Destroy(gameObject);
    }
}
