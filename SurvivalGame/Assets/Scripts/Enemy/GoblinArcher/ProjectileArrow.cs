using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileArrow : MonoBehaviour
{
    GameObject player;
    [SerializeField] float projectileDmg;
    [SerializeField] LayerMask playerLayer;
    //[SerializeField] LayerMask enemyLayer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PlayerPrefab");
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
            player.GetComponent<Player_Data>().HealOrDamage(-projectileDmg);
        }
        Destroy(gameObject);
    }
}
