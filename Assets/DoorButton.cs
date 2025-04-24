using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButton : MonoBehaviour
{
    [SerializeField] private GameObject door;

    public void Activate()
    {
        door.SetActive(!door.activeSelf);
    }
}
