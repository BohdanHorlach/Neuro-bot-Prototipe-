using UnityEditor;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Transform laserHitPoint;
    [SerializeField] private LayerMask ignoreLayerMask;
    [SerializeField] private string laserReflectTag;
    [SerializeField] private float offsetLaserFromNormalReflect = 1f;


    private Vector3 direction;
    private Vector3 currentPosition;
    private int indexPoint;
    private bool isDone;

    private void Start()
    {
        ignoreLayerMask = ~ignoreLayerMask;
    }


    private void Update()
    {
        LaserRender();   
    }


    private void SetEndPoint(Vector3 point)
    {
        lineRenderer.SetPosition(indexPoint, point);
        laserHitPoint.position = point;
        isDone = true;
    }


    private void SetReflect(Vector3 point, Vector3 normal)
    {
        lineRenderer.SetPosition(indexPoint, point);
        currentPosition = point + normal * offsetLaserFromNormalReflect;
        Vector3 temp = Vector3.Reflect(direction, normal);
        direction = temp;
    }


    private void LaserRender()
    {
        direction = transform.up;
        currentPosition = transform.position;

        isDone = false;
        indexPoint = 0;
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(indexPoint, currentPosition);

        while (isDone == false)
        {
            RaycastHit2D hit = Physics2D.Raycast(currentPosition, direction, Mathf.Infinity, ignoreLayerMask);
            lineRenderer.positionCount++;
            indexPoint++;

            if (hit.collider == null)
            {
                break;
            }
            else if (hit.collider.tag != laserReflectTag)
            {
                SetEndPoint(hit.point);
                break;
            }
            else
            {
                SetReflect(hit.point, hit.normal);
            }
        }
    }
}