using UnityEngine;

public class PushItem : Device
{
    [SerializeField] private float forcePushing;
   // [SerializeField] private float speedAddForce;
   // [SerializeField] private float maxAddForce;


    private float addingForce = 0;
    private Collider2D target;


    private void OnTriggerStay2D(Collider2D collision)
    {
        bool isCorrectMask = mask == (mask | (1 << collision.gameObject.layer));
        if (isCorrectMask == true)
            target = collision;
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        ResetSeting();
    }


    private Vector2 GetDirection(Transform target)
    {
        float distance = Vector2.Distance(target.position, transform.position);
        float force = distance > 0 ? 1 / distance : 1;

        return (target.position - transform.position) * force;
    }


    //private void SetAddingForce()
    //{
    //    if (Input.GetKey(button) && addingForce < maxAddForce)
    //    {
    //        addingForce += speedAddForce;
    //    }
    //}


    private void Push(Collider2D collision)
    {
        if (Input.GetKeyUp(button))
        {
            Vector2 direction = GetDirection(collision.transform);

            collision.GetComponent<Rigidbody2D>().AddForce(direction * (forcePushing + addingForce), ForceMode2D.Impulse);

            ResetSeting();
        }
    }


    private void ResetSeting()
    {
        addingForce = 0;

        target = null;
    }


    public override void UseDevice()
    {
        if (target != null)
        {
            //SetAddingForce();
            Push(target);
        }
    }
}