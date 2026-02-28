using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    //[SerializeField] private float rotationSpeed = 10f;
    
    private Rigidbody rb;
    private Vector3 inputDirection;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        // Lock rotation on X and Z axes to prevent tipping
        rb.freezeRotation = true;
    }

    void Update()
    {
        //float horizontalInput = Input.GetAxis("Horizontal");
        //float verticalInput = Input.GetAxis("Vertical");
        //inputDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        rb.linearVelocity = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, rb.linearVelocity.y, Input.GetAxis("Vertical") * moveSpeed) * Time.deltaTime;
        rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, moveSpeed);
    }
    
    void FixedUpdate()
    {
        // Apply movement velocity directly for responsive control
        //Vector3 targetVelocity = inputDirection * moveSpeed;
        //rb.linearVelocity = new Vector3(targetVelocity.x, rb.linearVelocity.y, targetVelocity.z);
        
        // Rotate toward movement direction
        /*if (inputDirection.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(inputDirection);
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }*/
    }
}
