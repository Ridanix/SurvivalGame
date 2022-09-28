using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controler : MonoBehaviour
{
    //MOVEMENT
    private Rigidbody rb;
    private float moveZ, moveX;
    [SerializeField] float speed = 1.0f;

    //LOOK_AT_MOUSE
    [SerializeField] Camera player_camera;

    public void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void FixedUpdate()
    {
        moveX = Input.GetAxis("Horizontal") * speed;
        moveZ = Input.GetAxis("Vertical") * speed;
        rb.velocity = new Vector3(moveX, 0, moveZ);

        Look_at_mouse();
    }

    public void Look_at_mouse()
    {
        Ray mouse_Cam_ray = player_camera.ScreenPointToRay(Input.mousePosition);
        Plane ground_plane = new Plane(Vector3.up, Vector3.zero);
        float ray_length;

        if (ground_plane.Raycast(mouse_Cam_ray, out ray_length))
        {
            Vector3 look_there = mouse_Cam_ray.GetPoint(ray_length);
            Transform t = GameObject.Find("Model").transform;
            t.LookAt(new Vector3(look_there.x, t.position.y, look_there.z));
        }
    }
}
