using UnityEngine;

public class ItemDrag : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float pickDistance = 3;
    private bool isDrag;
    private Transform dragTransform;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isDrag)
            {
                isDrag = false;
            }
            else
            {
                Ray ray = new Ray(transform.position, transform.forward);

                if (Physics.Raycast(ray, out RaycastHit hit, pickDistance, layerMask))
                {
                    Debug.Log(hit.transform.position);
                    isDrag = true;
                    dragTransform = hit.transform;
                }
            }
        }

        if (isDrag)
        {
            Vector3 newPos = transform.position + (transform.forward * pickDistance);
            dragTransform.position = newPos;
        }
    }
}
