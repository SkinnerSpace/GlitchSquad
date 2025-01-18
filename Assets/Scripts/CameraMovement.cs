using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public float movementSpeed = 5f;

    public Transform cameraTransform;

    public Transform target;

    private void LateUpdate()
    {
        var prevZ = cameraTransform.localPosition.z;

        var newpos = Vector2.Lerp(cameraTransform.localPosition, target.localPosition, Time.deltaTime * movementSpeed);

        cameraTransform.localPosition = new Vector3(newpos.x, newpos.y, -10);
    }
}
