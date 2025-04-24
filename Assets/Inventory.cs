using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<GameObject> items = new List<GameObject>();

    public void AddItem(GameObject newItem)
    {
        items.Add(newItem);
        newItem.SetActive(false);
    }

    public GameObject GetItem()
    {
        if (items.Count == 0) { return null; }

        GameObject item = items[0];
        items.Remove(item);
        item.SetActive(true);

        return item;
    }
}
