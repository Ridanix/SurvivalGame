using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Player_Controller : MonoBehaviour
{
    //MOVEMENT
    [SerializeField] CharacterController controller;
    [SerializeField] float speed = 5.0f;
    public Vector3 direction;
    [SerializeField] private GameObject healingParticlePrefab;
    [SerializeField] Transform attackPoint;
    float attackRange = 0.6f;
    public bool disabled = false;
    [SerializeField] LayerMask enemyLayers;
    [SerializeField] LayerMask suplieLayers;
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

    //ATTACK
    public static float attackDmg;
    public static WeaponType vulterablity;
    [SerializeField] PlayerAnimations playerAnimations;

    //TREE DESTRUCTION
    public Terrain mainTerain;

    //INVENTORY
    //private InventoryScript inventory;
    //[SerializeField] InventoryUIScript inventoryUI;


    private void Start()
    {
        //inventory = new InventoryScript();
        //inventoryUI.SetInventory(inventory);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.LogWarning(other.name);
    }

    public void Update()
    {
        if (Shake.isActive == false && mainCamera.transform.localPosition != new Vector3(0, 0, 0))
        {
            mainCamera.transform.localPosition = new Vector3(0, 0, 0);
        }

        if (EventSystem.current.IsPointerOverGameObject()) return;

        //geting input
        float moveX = Input.GetAxisRaw("Horizontal") * speed;
        float moveZ = Input.GetAxisRaw("Vertical") * speed;
        direction = new Vector3(moveX, 0, moveZ).normalized;

        //If gets any input, moves and rotates
        if (direction.magnitude >= 0.1f && disabled == false)
        {
            //rotates
            float target_angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCamera.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, target_angle, ref turnSmoothVelocity, playerRotation);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            //moves
            Vector3 move_direction = Quaternion.Euler(0f, target_angle, 0f) * Vector3.forward;
            controller.Move(move_direction.normalized * speed * Time.deltaTime);

           
        }
        

        //rotates camera, if you want to rotate only while standing, add code bellow like else to moverot part
        if (Input.GetKey(KeyCode.Mouse1) == true && disabled == false)
        {
            mainCamera.transform.rotation = Quaternion.Euler(0f, Input.mousePosition.magnitude, 0f);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            /*Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);
            transform.LookAt(hit.point);
            transform.Rotate(0,0,0);*/
            Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

            foreach (Collider enemy in hitEnemies)
            {
                if (playerAnimations.attackReady && hitEnemies.Length > 0)
                {
                    enemy.GetComponent<EnemyHealth>().TakeDamage(attackDmg, enemy.gameObject.name);
                }
            }

            Collider[] hitSuplies = Physics.OverlapSphere(attackPoint.position, attackRange, suplieLayers);

            foreach (Collider suply in hitSuplies)
            {
                if (hitSuplies.Length > 0)
                {
                    //float theClosestLength = 100f;
                    //int indexOfClosestTree = 0;
                    //for (int i = 0; i < mainTerain.terrainData.treeInstances.Length; i++)
                    //{
                    //    Vector3 lengtToPlayer = mainTerain.terrainData.treeInstances[i].position - transform.position;
                    //    float convertedLengt = lengtToPlayer.magnitude;
                    //    Debug.LogWarning($"{i} {convertedLengt}");
                    //    if (theClosestLength>convertedLengt)
                    //    {
                    //        theClosestLength = convertedLengt;
                    //        indexOfClosestTree = i;
                    //    }
                    //}
                    //Debug.LogWarning($"Nearest: {indexOfClosestTree}");

                    //Debug.LogWarning(mainTerain.terrainData.treeInstances[indexOfClosestTree].ToString());
                    //mainTerain.terrainData.SetTreeInstance(indexOfClosestTree, new TreeInstance());

                    //suply.GetComponent<ItemSpawner>().TakeDamage(attackDmg, vulterablity);

                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }

    }
    private void FixedUpdate()
    {
        if (direction.magnitude >= 0.1f && disabled == false)
        {
            //bar reduce
            playerData.stamina -= 1f;
        }
        else if (playerData.stamina < playerData.maxStamina) playerData.stamina += 1f;
    }



}
