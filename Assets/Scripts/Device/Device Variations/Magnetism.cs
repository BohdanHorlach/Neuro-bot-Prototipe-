using UnityEngine;

public class Magnetism : Device
{
    [SerializeField] private KeyCode secondaryButton;
    [SerializeField] private float magneticForce;
    [SerializeField] private float maxDistance;
    [SerializeField] private float minDistance;


    private IMagneticProcessing interact = null;
    private Rigidbody2D clickedObject;
    private bool isLeftButtonClick;


    private void FindObject()
    {
        if (Input.GetKeyDown(button) || Input.GetKeyDown(secondaryButton))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, mask);
            if (hit.collider != null)
            {
                interact = hit.collider.GetComponent<IMagneticProcessing>();
                clickedObject = hit.collider.GetComponent<Rigidbody2D>();
                isLeftButtonClick = Input.GetMouseButtonDown(0);
            }
        }
    }


    private Vector2 GetDirection(Transform target)
    {
        if (isLeftButtonClick == true)
            return transform.position - target.position;
        else
            return target.position - transform.position;
    }



    private void Use()
    {
        bool isButtonClick = Input.GetKey(button);
        bool isSecondaryButtonClick = Input.GetKey(secondaryButton);

        if ((isButtonClick || isSecondaryButtonClick) && clickedObject != null)
        {
            float distance = Vector2.Distance(clickedObject.transform.position, transform.position);
            float force = distance > 0 ? 1 / distance : 0;

            if (interact != null)
            {
                InteractProcessing(force);
            }
            else
            {
                AttractAndRepel(force, distance);
            }
        }
    }


    private void InteractProcessing(float force)
    {
        float interactForce = force * magneticForce;

        Vector2 directionOfForce = GetDirection(clickedObject.transform);
        Vector2 directionOfFacing = transform.position.normalized;

        float dot = Vector2.Dot(directionOfForce, directionOfFacing);

        interactForce *= dot < 0 ? -1 : 1;

        interact.MagneticProcessing(interactForce, transform.position);
    }


    private void AttractAndRepel(float force, float distance)
    {
        if (distance <= maxDistance && (distance > minDistance || Input.GetKey(secondaryButton)))
        {
            Vector2 direction = GetDirection(clickedObject.transform);

            clickedObject.AddForce(direction * force * magneticForce);
        }
    }


    private void ResetSetings()
    {
        if (Input.GetKeyUp(button) || Input.GetKeyUp(secondaryButton))
        {
            clickedObject = null;
        }
    }


    public override void UseDevice()
    {
        FindObject();
        Use();
        ResetSetings();
    }
}