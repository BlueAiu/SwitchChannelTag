using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardUI : MonoBehaviour
{
    void FixedUpdate()
    {
        Vector3 cameraForward = Camera.main.transform.forward;

        transform.LookAt(transform.position + cameraForward);
    }
}
