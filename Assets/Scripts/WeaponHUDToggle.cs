using UnityEngine;

public class WeaponHUDToggle : MonoBehaviour
{
    public Animator hudAnimator;

    private bool weapon1Active = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            hudAnimator.SetTrigger("SwitchWeapon");
            weapon1Active = !weapon1Active;
        }
    }
}