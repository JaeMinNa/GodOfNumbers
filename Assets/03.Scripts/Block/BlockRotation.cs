using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockRotation : MonoBehaviour
{
    public int RotationSpeed;

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, RotationSpeed) * Time.deltaTime);
    }
}
