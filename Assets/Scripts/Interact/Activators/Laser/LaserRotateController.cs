using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class LaserRotateController : MonoBehaviour, IMagneticProcessing
{
    [SerializeField] private Transform laserTransform;
    [SerializeField, Min(0.1f)] private float controllerRotateSpeed;
    [SerializeField, Min(1)] private int rotationDecelerationForce = 1;

    private void Start()
    {
        transform.rotation = laserTransform.rotation;
    }


    public void MagneticProcessing(float force, Vector2 playerPosition)
    {
        laserTransform.Rotate(0, 0, force / rotationDecelerationForce);
        transform.Rotate(0, 0, force * controllerRotateSpeed / rotationDecelerationForce);
    }
}
