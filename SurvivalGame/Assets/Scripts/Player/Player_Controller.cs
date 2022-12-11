using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player_Controller : MonoBehaviour
{
    //MOVEMENT
    [SerializeField] CharacterController controller;
    [SerializeField] float speed = 5.0f;
    [SerializeField] private GameObject healingParticlePrefab;
    [SerializeField] Transform attackPoint;
    float attackRange = 0.6f;
    [SerializeField] LayerMask enemyLayers;
    GameObject goblinGameObject;

    //PLAYER ROTATION
    public float playerRotation = 0.1f;
    private float turnSmoothVelocity;
    [SerializeField] Camera playerCamera;

    //CAMERA ROTATION
    [SerializeField] GameObject playerModel;
    [SerializeField] GameObject mainCamera;
     

    //HEALTH
    [SerializeField] Player_Data playerData;

    private void Start()
    {
        
    }

    public void Update()
    {
        

        //geting input
        float moveX = Input.GetAxis("Horizontal") * speed;
        float moveZ = Input.GetAxis("Vertical") * speed;
        Vector3 direction = new Vector3(moveX, 0, moveZ).normalized;

        //If gets any input, moves and rotates
        if (direction.magnitude >= 0.1f)
        {
            //rotates
            float target_angle = Mathf.Atan2(direction.x, direction.z)* Mathf.Rad2Deg + playerCamera.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, target_angle, ref turnSmoothVelocity, playerRotation);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            //moves
            Vector3 move_direction = Quaternion.Euler(0f, target_angle, 0f)*Vector3.forward;
            controller.Move(move_direction.normalized*speed*Time.deltaTime);

            //bar reduce
            //playerData.stamina -= 1f; //-moment�ln� nefunguje
        }
        /*else
            if (playerData.stamina < playerData.maxStamina) playerData.stamina += 1f;*/ //-moment�ln� nefunguje


        //rotates camera, if you want to rotate only while standing, add code bellow like else to moverot part
        if (Input.GetKey(KeyCode.Mouse1)== true)
        {
            mainCamera.transform.rotation = Quaternion.Euler(0f, Input.mousePosition.magnitude, 0f);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            playerData.currentObject = playerData.slots[0].transform;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            playerData.currentObject = playerData.slots[1].transform;
            
            
            playerData.health += 50;
            Instantiate(healingParticlePrefab, transform.position, Quaternion.Euler(-90f, 0f, 0f));
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {

            Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

            foreach (Collider enemy in hitEnemies)
            {
                Debug.Log("Hr��trefa");
                
                goblinGameObject = GameObject.Find("BasicGoblin");
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerData.health -= 20;
        }
    }

  

}
