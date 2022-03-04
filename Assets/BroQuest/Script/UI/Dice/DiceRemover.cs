using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRemover : MonoBehaviour
{
    /*
    public Camera diceCamera;

    private void Start()
    {
        diceCamera = GameObject.FindGameObjectWithTag("Dice_Camera").GetComponent<Camera>() as Camera;
    }

    // See Order of Execution for Event Functions for information on FixedUpdate() and Update() related to physics queries
    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Input.GetMouseButtonDown(0) == true");

            // Bit shift the index of the layer (8) to get a bit mask
            int layerMask = 7;

            // This would cast rays only against colliders in layer 8.
            // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
            layerMask = ~layerMask;

            RaycastHit hit;
            Ray ray = diceCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, 100))
                print("Hit something!");
            
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);

                if (hit.collider.gameObject == this.gameObject)
                {
                    Debug.Log("Did Hit :: " + hit.collider.name);
                }
            }
            else
            {
                //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
                //Debug.Log("Did not Hit");
            }
            
        }
    }

    public void RemoveMe()
    {
        gameObject.SetActive(false);
    }
    */
}