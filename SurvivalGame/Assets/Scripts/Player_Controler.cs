using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controler : MonoBehaviour
{
    //MOVEMENT
    [SerializeField] CharacterController controler;
    [SerializeField] float speed = 5.0f;

    //PLAYER ROTATION
    public float player_rotation = 0.1f;
    private float turn_smooth_velocity;
    [SerializeField] Camera player_camera;

    //CAMERA ROTATION
    [SerializeField] GameObject model;
    [SerializeField] GameObject camera_;

    //HEALTH
    //[SerializeField] float Maxhealth;
    //float health;
    [SerializeField] Player_Data player_data;

    public void FixedUpdate()
    {
        //geting input
        float moveX = Input.GetAxis("Horizontal") * speed;
        float moveZ = Input.GetAxis("Vertical") * speed;
        Vector3 direction = new Vector3(moveX, 0, moveZ).normalized;
        
        //If gets any input, moves and rotates
        if(direction.magnitude >= 0.1f)
        {
            //rotates
            float target_angle = Mathf.Atan2(direction.x, direction.z)* Mathf.Rad2Deg + player_camera.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, target_angle, ref turn_smooth_velocity, player_rotation);
            transform.rotation = Quaternion.Euler(0f,angle, 0f);

            //moves
            Vector3 move_direction = Quaternion.Euler(0f, target_angle, 0f)*Vector3.forward;
            controler.Move(move_direction.normalized*speed*Time.deltaTime);
        }

        //rotates camera, if you want to rotate only while standing, add code bellow like else to moverot part
        if(Input.GetKey(KeyCode.Mouse1)== true)
        {
            camera_.transform.rotation = Quaternion.Euler(0f, Input.mousePosition.magnitude, 0f);
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Player_Data.Change_health_value(-20);
        }
    }
    //public void TakeDamage(float amout)
    //{
    //    health -= amout;
    //    if (health <= 0)
    //    {
    //        Destroy(gameObject);
    //    }
    //}
}
