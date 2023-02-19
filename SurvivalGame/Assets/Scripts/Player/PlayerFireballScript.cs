using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireballScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    int enemyMask;
    [SerializeField] LayerMask enemyLayer;
    
    

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (enemyLayer == (enemyLayer | (1 << collision.gameObject.layer)))
        {
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(50, collision.gameObject.name); 
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
}
