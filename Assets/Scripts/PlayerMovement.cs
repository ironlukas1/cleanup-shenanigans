using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private CharacterController ch;
    void Awake()
    {
        ch = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        
        // Set initial rotation to face forward
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    void Update()
    {
        float x = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float z = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        ch.Move(new Vector3(x, 0, z));
    }
}
