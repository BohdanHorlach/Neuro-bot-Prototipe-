using UnityEngine;


public class ActivatorByLayer : AbstractActivator
{
    [SerializeField] private FinderObjectFromSpace finder;


    private void OnEnable()
    {
        finder.OnChangeSpace += Activation;
    }


    private void OnDisable()
    {
        finder.OnChangeSpace -= Activation;
    }
}