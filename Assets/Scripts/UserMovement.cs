using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserMovement : MonoBehaviour
{
    public CharacterController controller;
    public GameObject contextMenu;

    public float speed;
    public float gravity;
    public float maxDistance;
    public Vector3 targetNPC;

    Vector3 velocity;

    // Update is called once per frame
    void Update()
    {
        // Player movement

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if (targetNPC != Vector3.zero) // Only check the distance if targetNPC is not equal to 0
        {
            // If we are far away from the NPC, close the context menu
            if (Vector3.Distance(transform.position, targetNPC) > maxDistance)
            {
                contextMenu.SetActive(false);
                targetNPC = Vector3.zero;
                Debug.Log("Called");
            }
        }

        if (Input.GetKeyDown(KeyCode.X)) // Exit context menu
        {
            contextMenu.SetActive(false);
        }

        if(Input.GetKeyDown(KeyCode.Escape)) // Exit game
        {
            Application.Quit();
        }
    }
}
