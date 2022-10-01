using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Player_Controler : NetworkBehaviour
{
    //MOVEMENT
    private Rigidbody rb;
    private float moveZ, moveX;
    [SerializeField] float speed = 5.0f;

    //LOOK_AT_MOUSE
    [SerializeField] Camera player_camera;
    //CAMERA ROTATION
    [SerializeField] GameObject model;
    [SerializeField] GameObject camera_;
    private bool camera_rotation = false;

    public void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void FixedUpdate()
    {
        if (!IsOwner) return;
       
        moveX = Input.GetAxis("Horizontal") * speed;
        moveZ = Input.GetAxis("Vertical") * speed;
        rb.velocity = new Vector3(moveX, 0, moveZ);

        Look_at_mouse();



        if(Input.GetKey(KeyCode.Mouse2) == true)
        {
            camera_rotation = false;
        }
        else
        {
            camera_rotation = true;
        }

    }

    public void Look_at_mouse()
    {
        Ray mouse_Cam_ray = player_camera.ScreenPointToRay(Input.mousePosition);
        Plane ground_plane = new Plane(Vector3.up, Vector3.zero);
        float ray_length;

        if (ground_plane.Raycast(mouse_Cam_ray, out ray_length))
        {
            Vector3 look_there = mouse_Cam_ray.GetPoint(ray_length);
            Transform t;
            if(camera_rotation)
                t = model.transform;
            else
                t = camera_.transform;


            t.LookAt(new Vector3(look_there.x, t.position.y, look_there.z));
        }
    }
}
