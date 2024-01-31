using Unity.VisualScripting;
using UnityEngine;

public class GrabItem : Device
{
    [SerializeField] private Rigidbody2D connectedBody;
    [SerializeField] private Vector2 offset;


    private Collider2D target;
    private Rigidbody2D rbTarget;
    private DistanceJoint2D distanceJoint;
    private float massTarget = 0.0f;
    private bool isGrab = false;


    private void OnTriggerStay2D(Collider2D collision)
    {
        bool isCorrectMask = mask == (mask | (1 << collision.gameObject.layer));

        if (isGrab == false && isCorrectMask == true)
            target = collision;
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        target = null;
    }


    private void SetPositionToObject()
    {
        offset.x *= transform.rotation.y < 0 ? -1 : 1;
        target.transform.position = (Vector2)transform.position + offset;
    }


    private void InitializedComponent()
    {
        distanceJoint = target.AddComponent<DistanceJoint2D>();
        rbTarget = target.GetComponent<Rigidbody2D>();
    }


    private void ConectedObject()
    {
        distanceJoint.autoConfigureConnectedAnchor = true;
        distanceJoint.connectedBody = connectedBody;

        rbTarget.transform.SetParent(connectedBody.transform);
    }


    private void SetObjectSetings()
    {
        massTarget = rbTarget.mass;
        rbTarget.mass = 0f;
    }


    private void Grab()
    {
        if (Input.GetKeyUp(button))
        {
            SetPositionToObject();
            InitializedComponent();
            ConectedObject();
            SetObjectSetings();

            isGrab = true;
        }
    }


    private void ResetSetingsAndComponent()
    {
        if (rbTarget != null)
        {
            Destroy(distanceJoint);
            rbTarget.transform.SetParent(null);
            rbTarget.mass = massTarget;
            rbTarget = null;

            isGrab = false;

            offset.x = Mathf.Abs(offset.x);
        }
    }


    private void Drop()
    {
        if (Input.GetKeyUp(button))
            ResetSetingsAndComponent();
    }


    public override void UseDevice()
    {
        if (isGrab == true)
        {
            Drop();
        }

        if (target != null)
        {
            Grab();
        }
    }


    public override void Enable(bool value)
    {
        ResetSetingsAndComponent();
        base.Enable(value);
    }
}