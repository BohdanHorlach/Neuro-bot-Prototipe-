using System.Linq;
using UnityEngine;


public class AbstractActivator : MonoBehaviour
{
    [SerializeField] private MonoBehaviour[] activatedObjects;
    
    private IActivated[] mechanisms;


    protected void Awake()
    {
        mechanisms = activatedObjects.OfType<IActivated>().ToArray();
    }


    protected void Activation(bool value)
    {
        foreach (var mechanism in mechanisms)
        {
            mechanism.Activated(value);
        }
    }
}