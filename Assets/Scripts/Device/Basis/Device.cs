using UnityEngine;

public abstract class Device : MonoBehaviour
{
    [SerializeField] protected LayerMask mask;
    [SerializeField] protected KeyCode button;

    public TypesDevice typeDevice;

    public abstract void UseDevice();
    public virtual void Enable(bool value)
    {
        gameObject.SetActive(value);
    }
}