using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    void Update()
    {
        if ((PlayerController.me != null) && (!PlayerController.me.dead))
        {
            //Debug.Log("PlayerController.me :: " + PlayerController.me);
            //Debug.Log("PlayerController.me.dead :: " + PlayerController.me.dead);
            Vector3 targetPosition = PlayerController.me.transform.position;
            targetPosition.z = -1;
            transform.position = targetPosition;
        }
    }
}
