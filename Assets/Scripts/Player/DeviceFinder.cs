using UnityEngine;

public class DeviceFinder : MonoBehaviour
{
    [SerializeField] private DeviceSwitcher switcher;
    [SerializeField] private KeyCode addButton;
    [SerializeField] private LayerMask searchMask;
    [SerializeField] private float searchRadius;

    private Collider2D[] foundDevice;


    private void Update()
    {
        AddFoundDevice();
        SearchDevices();
    }


    private void SearchDevices()
    {
        foundDevice = Physics2D.OverlapCircleAll(transform.position, searchRadius, searchMask);
    }


    private void AddFoundDevice()
    {
        if (Input.GetKeyUp(addButton) && foundDevice.Length != 0)
        {
            bool resultAddition = switcher.Add(foundDevice[0].GetComponent<NotRaisedDevice>().GetDevice());

            if(resultAddition == true)
            {
                Destroy(foundDevice[0].gameObject);
            }
        }
    }
}