using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldPos : MonoBehaviour
{
    public Camera playerCamera;
    public float pickupDist = 2f;

    void Update()
    {
        // Set the position of this object to the center of the screen
        transform.position = playerCamera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, pickupDist));
    }

}
