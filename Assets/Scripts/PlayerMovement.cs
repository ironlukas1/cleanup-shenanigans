using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float gravity = -20f;
    [SerializeField] private Transform cameraTransform;
    private CharacterController ch;
    private Vector3 velocity;

    void Awake()
    {
        ch = GetComponent<CharacterController>();
        transform.rotation = Quaternion.identity;
        if (cameraTransform == null)
            cameraTransform = Camera.main.transform;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 cameraForward = new Vector3(cameraTransform.forward.x, 0, cameraTransform.forward.z).normalized;
        Vector3 cameraRight = new Vector3(cameraTransform.right.x, 0, cameraTransform.right.z).normalized;

        Vector3 moveDirection = (cameraForward * vertical + cameraRight * horizontal).normalized;
        Vector3 movement = moveDirection * moveSpeed;

        if (ch.isGrounded)
            velocity.y = -1f;
        else
            velocity.y += gravity * Time.deltaTime;

        movement.y = velocity.y;

        ch.Move(movement * Time.deltaTime);
    }
}