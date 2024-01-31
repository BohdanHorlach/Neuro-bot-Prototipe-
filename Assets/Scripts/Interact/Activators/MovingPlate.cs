using UnityEngine;


public class MovingPlate : MonoBehaviour, IActivated
{
    [SerializeField] private Transform[] transitionsPoints;
    [SerializeField] private Rigidbody2D rgbody;
    [SerializeField] private float movingSpeed;
    [SerializeField] private bool isALoopingPath;
    [SerializeField] private bool IsActivated = false;

    private Vector3 currentPos;
    private Vector3 nextPos;
    private int currentIndex = 0;
    private bool isFirstStart = true;
    private bool isReverses = false;


    private void OnEnable()
    {
        if(IsActivated == true)
            SetStartPosition();

        rgbody.isKinematic = true;
    }


    private void FixedUpdate()
    {
        if (IsActivated == true)
        {
            Step();
            CheckConditions();
            Move();
        }
    }


    private void SetStartPosition()
    {
        transform.position = transitionsPoints[currentIndex].position;

        currentPos = transform.position;
        nextPos = transform.position;

        isFirstStart = false;
    }


    private void CheckConditions()
    {
        if (currentIndex == transitionsPoints.Length)
        {
            if (isALoopingPath == false)
            {
                isReverses = true;
                currentIndex = transitionsPoints.Length - 2;
            }
            else
            {
                currentIndex = 0;
            }
        }
        else if (currentIndex == 0)
        {
            isReverses = false;
        }
    }


    private void Move()
    {
        currentPos = transform.position;
        nextPos = transitionsPoints[currentIndex].position;

        Vector3 newPos = Vector3.MoveTowards(currentPos, nextPos, movingSpeed * Time.deltaTime);

        rgbody.MovePosition(newPos);
    }


    private void Step()
    {
        if (Vector3.Distance(currentPos, nextPos) > 0.1f)
            return;

        if(isReverses == false)
            currentIndex++;
        else
            currentIndex--;
    }


    public void Activated(bool value)
    {
        IsActivated = value;

        if (value == true && isFirstStart == true)
            SetStartPosition();
    }
}
