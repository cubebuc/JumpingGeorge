using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Transform cameraTransform;
    [SerializeField]
    float mouseSensitivity = 2;
    [SerializeField]
    float yRotationLimit = 88;

    Vector2 rotation;

    void Start()
    {
        rotation = new Vector2(cameraTransform.rotation.x, cameraTransform.rotation.y);
    }

    void Update()
    {
        rotation.x += Input.GetAxis("Mouse X") * mouseSensitivity;
        rotation.y += Input.GetAxis("Mouse Y") * mouseSensitivity;
        rotation.y = Mathf.Clamp(rotation.y, -yRotationLimit, yRotationLimit);
        var xQuat = Quaternion.AngleAxis(rotation.x, Vector3.up);
        var yQuat = Quaternion.AngleAxis(rotation.y, Vector3.left);

        cameraTransform.rotation = xQuat * yQuat;
    }

    private void FixedUpdate()
    {
        var xQuat = Quaternion.AngleAxis(rotation.x, Vector3.up);
        transform.rotation = xQuat;
    }
}
