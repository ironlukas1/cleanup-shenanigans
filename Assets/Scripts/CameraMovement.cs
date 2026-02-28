using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
    public Transform target;
    public Vector3 offset;
    public float speedH = 8.0f;
    public float speedV = 6.0f;
    public float verticalRotationLimit = 90f;
    private float yaw = 0.0f;
    private float pitch = 0.0f;
    void Update () {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            yaw += speedH * Input.GetAxis("Mouse X");
            pitch -= speedV * Input.GetAxis("Mouse Y");
        }
        
        pitch = Mathf.Clamp(pitch, -verticalRotationLimit, verticalRotationLimit);

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        
        Vector3 rotatedOffset = Quaternion.Euler(pitch, yaw, 0.0f) * offset;
        transform.position = target.position + rotatedOffset;
    }
}