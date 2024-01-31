using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class Toggle : AbstractActivator, IMagneticProcessing, IActivated
{
    [SerializeField] private Animator animator;
    [SerializeField, Min(0)] private float neededForceForActivated;

    private bool isActivated;

    public bool StartStateIsActive = false;


    private void Start()
    {
        Activated(StartStateIsActive);
    }


    public void MagneticProcessing(float force, Vector2 playerPosition)
    {
        if (Mathf.Abs(force) < neededForceForActivated)
            return;

        bool playerIsRight = playerPosition.x >= transform.position.x;

        if (playerIsRight == true)
        {
            if(isActivated == false && force > 0)
            {
                Activated(true);
            }
            else if (isActivated == true && force < 0)
            {
                Activated(false);
            }
        }
        else
        {
            if (isActivated == true && force > 0)
            {
                Activated(false);
            }
            else if(isActivated == false && force < 0)
            {
                Activated(true);
            }
        }
    }


    public void Activated(bool value)
    {
        animator.SetBool("isActivated", value);
        isActivated = value;

        Activation(value);
    }
}