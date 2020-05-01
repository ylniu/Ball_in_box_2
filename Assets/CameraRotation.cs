using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    // store a public reference to the Player game object, so we can refer to it's Transform
    public GameObject player;

    // Store a Vector3 offset from the player (a distance to place the camera from the player at all times)
    private Vector3 offset;

    // At the start of the game..
    void Start()
    {
        // Create an offset by subtracting the Camera's position from the player's position
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // After the standard 'Update()' loop runs, and just before each frame is rendered..
    void LateUpdate()
    {
        if (player)
        {
            float angle = 0;
            if (Input.GetMouseButton(0)) angle = 1;
            if (Input.GetMouseButton(1)) angle = -1;

            transform.RotateAround(
                player.transform.position,
                player.transform.up,
                angle);
        }
    }


}
