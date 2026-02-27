using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody rb;
    private Vector3 inputDirection; 
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        //GetComponent<Rigidbody>().rotation = Quaternion.Euler(0, 45, 0); // x y z
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        inputDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        //rb.MoveRotation(Quaternion.LookRotation(inputDirection));
    }
    void FixedUpdate()
    {
        rb.AddForce(inputDirection * moveSpeed * Time.fixedDeltaTime, ForceMode.VelocityChange);
        //rigidbody.MoveRotation(Quaternion.LookRotation(rigidbody.velocity));
    }
}
