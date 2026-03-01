using UnityEngine;

public class HeadbobSystem : MonoBehaviour
{
    public Animator canAnim;

    void Update()
    {
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            canAnim.SetTrigger("walk");
        }
        else
        {
            canAnim.SetTrigger("idle");
        }
    }
}
