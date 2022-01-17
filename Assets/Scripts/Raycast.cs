using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Raycast : MonoBehaviour
{
    public UserMovement userMovement;
    Ray ray;
    RaycastHit hit;

    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (Input.GetMouseButtonDown(0))
            {
                if(hit.collider.CompareTag("NPC"))
                {
                    //Pass in the NPC data contained in the webpage 
                    JsonReader.Instance.ReadFromWebPage(hit.collider.gameObject.GetComponent<NPC>().data.url);    
                    userMovement.targetNPC = hit.collider.gameObject.transform.position;
                }
            }               
        }
    }
}
