using System.Collections.Generic;
using UnityEngine;

public class DeviceSwitcher : MonoBehaviour
{
    [SerializeField] private KeyCode buttonGoNextDevice;
    private List<Device> listDevices;
    private int indexCurrent = 0;


    private void Awake()
    {
        listDevices = new List<Device>(transform.GetComponentsInChildren<Device>(true));

        if (listDevices.Count == 0)
            return;

        listDevices[indexCurrent].Enable(true);

        for (int i = 0; i < indexCurrent; i++)
        {
            if (i != indexCurrent)
            {
                listDevices[i].Enable(false);
            }
        }
    }


    private void Update()
    {
        Use();
        Swap();
    }


    private void Use()
    {
        if(listDevices.Count != 0)
            listDevices[indexCurrent].UseDevice();
    }


    private void Swap()
    {
        if (Input.GetKeyUp(buttonGoNextDevice))
        {
            listDevices[indexCurrent].Enable(false);
            indexCurrent++;

            if (indexCurrent >= listDevices.Count)
            {
                indexCurrent = 0;
            }

            listDevices[indexCurrent].Enable(true);
        }
    }


    private bool IsContainsType(TypesDevice type)
    {
        for(int i = 0; i < listDevices.Count; i++)
        {
            if (listDevices[i].typeDevice == type)
            {
                return true;
            }
        }

        return false;
    }


    public bool Add(Device device)
    {
        if (device == null)
            return false;

        if (IsContainsType(device.typeDevice) == false)
        {
            GameObject addedDevice = Instantiate(device.gameObject);

            addedDevice.transform.SetParent(transform);
            addedDevice.transform.position = transform.position;
            addedDevice.transform.rotation = transform.rotation;

            listDevices.Add(addedDevice.GetComponent<Device>());

            if (listDevices.Count == 1)
            {
                listDevices[indexCurrent].Enable(true);
            }
            else
            {
                listDevices[indexCurrent].Enable(false);
                indexCurrent = listDevices.Count - 1;
                listDevices[indexCurrent].Enable(true);
            }

            addedDevice.gameObject.name = listDevices[indexCurrent].typeDevice.ToString();

            return true;
        }
        return false;
    }
}