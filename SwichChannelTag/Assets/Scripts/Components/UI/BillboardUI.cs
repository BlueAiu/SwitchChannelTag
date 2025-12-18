using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardUI : MonoBehaviour
{
    void FixedUpdate()
    {
        transform.LookAt(transform.position + Vector3.forward);
    }
}
