using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportAnimation : MonoBehaviour
{
    float rotation = 1f;
    private void FixedUpdate()
    {
        //rotateX += 0.01f;
        transform.Rotate(new Vector3(0, 0, rotation));
    }
}
