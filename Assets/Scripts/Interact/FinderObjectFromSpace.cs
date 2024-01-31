using System;
using UnityEngine;

public class FinderObjectFromSpace : MonoBehaviour
{
    [SerializeField] private float checkRadius;
    [SerializeField] private LayerMask maskObject;
    private bool hasFound = false;

    public Action<bool> OnChangeSpace;

    private void Awake()
    {
        OnChangeSpace?.Invoke(false);
    }


    private void Update()
    {
        Search();
    }


    private void Search()
    {
        bool tempValue = Physics2D.OverlapCircle(transform.position, checkRadius, maskObject);

        if(tempValue != hasFound)
        {
            hasFound = tempValue;
            OnChangeSpace?.Invoke(hasFound);
        }
    }
}
