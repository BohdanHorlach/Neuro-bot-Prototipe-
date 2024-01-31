using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] Transform conectedTarget;
    [SerializeField, Range(0f, 1f)] float parallaxStrength = 0;
    [SerializeField] bool disableVerticalParallax = false;
    Vector3 pastTransformFromTarget;
    void OnEnable()
    {
        if (conectedTarget == null)
            conectedTarget = Camera.main.transform;

        pastTransformFromTarget = conectedTarget.position;
    }


    void Update()
    {
        Vector3 delta = conectedTarget.position - pastTransformFromTarget;

        if (disableVerticalParallax == true)
            delta.y = 0;

        pastTransformFromTarget = conectedTarget.position;
        transform.position += delta * parallaxStrength;
    }
}
