using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private Transform cameraTransform;
    private CharacterController ch;
    private Vector3 velocity = Vector3.zero;
    
    void Awake()
    {
        ch = GetComponent<CharacterController>();
        transform.rotation = Quaternion.Euler(0, 0, 0);
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
        Vector3 movement = moveDirection * moveSpeed * Time.deltaTime;
        
        if (ch.isGrounded)
        {
            velocity.y = 0;
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }
        
        movement.y = velocity.y * Time.deltaTime;
        ch.Move(movement);
    }
}
