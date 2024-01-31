using UnityEngine;

public class MechanicDoor : MonoBehaviour, IActivated
{
    [SerializeField] private Animator animator;

    private void OpenClose(bool value)
    {
        animator.SetBool("isOpen", value);
    }


    public void Activated(bool value)
    {
        OpenClose(value);
    }
}
