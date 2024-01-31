using UnityEngine;

public class LevitationObject : MonoBehaviour
{
    [SerializeField] private Transform levitationTransform;
    [SerializeField] private AnimationCurve levitationCurve;


    private float currentTime = 0;
    private float totalTime;

    public bool isLevitation { get; set; }


    private void Start()
    {
        totalTime = levitationCurve[levitationCurve.length - 1].time;
    }


    private void Update()
    {
        if(isLevitation == true)
            Levitaion();
    }


    private void Levitaion()
    {
        Vector2 offset = new Vector2(transform.position.x, transform.position.y + levitationCurve.Evaluate(currentTime));

        levitationTransform.position = offset;

        currentTime += Time.deltaTime;

        if (currentTime >= totalTime)
            currentTime = 0;
    }
}