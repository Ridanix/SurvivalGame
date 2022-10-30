using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.Networking;

public class Player_Controller : NetworkBehaviour
{
    //MOVEMENT
    [SerializeField] CharacterController controler;
    [SerializeField] float speed = 5.0f;
    [SerializeField] private GameObject healingParticlePrefab;
    [SerializeField] Transform attackPoint;
    float attackRange = 0.6f;
    [SerializeField] LayerMask enemyLayers;
    GameObject goblinGameObject;

    //PLAYER ROTATION
    public float player_rotation = 0.1f;
    private float turn_smooth_velocity;
    [SerializeField] Camera player_camera;

    //CAMERA ROTATION
    [SerializeField] GameObject model;
    [SerializeField] GameObject camera_;
     

    //HEALTH
    [SerializeField] Player_Data player_data;

    private void Start()
    {
        player_camera.gameObject.SetActive(IsOwner);

    }

    public void FixedUpdate()
    {
        if (!IsOwner) return;


        

        //geting input
        float moveX = Input.GetAxis("Horizontal") * speed;
        float moveZ = Input.GetAxis("Vertical") * speed;
        Vector3 direction = new Vector3(moveX, 0, moveZ).normalized;

        //If gets any input, moves and rotates
        if (direction.magnitude >= 0.1f)
        {
            //rotates
            float target_angle = Mathf.Atan2(direction.x, direction.z)* Mathf.Rad2Deg + player_camera.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, target_angle, ref turn_smooth_velocity, player_rotation);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            //moves
            Vector3 move_direction = Quaternion.Euler(0f, target_angle, 0f)*Vector3.forward;
            controler.Move(move_direction.normalized*speed*Time.deltaTime);

            //bar reduce
            player_data.stamina -= 1f;
        }
        else
            player_data.stamina += 1f;

        //rotates camera, if you want to rotate only while standing, add code bellow like else to moverot part
        if (Input.GetKey(KeyCode.Mouse1)== true)
        {
            camera_.transform.rotation = Quaternion.Euler(0f, Input.mousePosition.magnitude, 0f);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            player_data.current_object = player_data.slots[0].transform;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            player_data.current_object = player_data.slots[1].transform;
            
            SpawnHealthParticleServerRpc();
            UsedSmallPotionServerRpc();
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {

            Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

            foreach (Collider enemy in hitEnemies)
            {
                Debug.Log("Hr·Ëtrefa");

                KillGoblinTestServerRpc();
                goblinGameObject = GameObject.Find("BasicGoblin");
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            player_data.health -= 20;
        }
    }

    [ServerRpc]
    private void SpawnHealthParticleServerRpc()
    {
        SpawnHealthParticleClientRpc();
    }

    [ServerRpc]
    private void UsedSmallPotionServerRpc()
    {
        UsedSmallPotionClientRpc();
    }
    [ServerRpc]
    private void KillGoblinTestServerRpc()
    {
        Destroy(goblinGameObject);
    }
    [ClientRpc]
    private void SpawnHealthParticleClientRpc()
    {
        Instantiate(healingParticlePrefab, transform.position, Quaternion.Euler(-90f, 0f, 0f));
    }

    [ClientRpc]
    public void UsedSmallPotionClientRpc()
    {
        player_data.health += 50;
    }

}
