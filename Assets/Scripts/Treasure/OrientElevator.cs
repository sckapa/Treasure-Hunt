using UnityEngine;

public class OrientElevator : MonoBehaviour
{
    [SerializeField]
    private LayerMask layerMask;

    private void Start()
    {
        while (Physics.Raycast(transform.position, -transform.right, 4f, layerMask))
        {
            transform.parent.Rotate(new Vector3(0f, 90f, 0f));
        }
    }
}
