using UnityEngine;
using Random = UnityEngine.Random;

public class RandomObjectGenerator : MonoBehaviour
{
    public GameObject objectToPlace;
    public int numberOfObjects = 10;
    public Transform roomBounds;
    public Transform targetPlane; // Assign your plane GameObject here
    public bool clearExistingObjects = true;
    public string generatedObjectNamePrefix = "GeneratedObject_";

    public void GenerateObjects()
    {
        if (objectToPlace == null || roomBounds == null || targetPlane == null)
        {
            Debug.LogError("Object to place, room bounds, or target plane not assigned!");
            return;
        }

        if (clearExistingObjects)
        {
            foreach (Transform child in transform)
            {
                if (child.name.StartsWith(generatedObjectNamePrefix))
                {
                    DestroyImmediate(child.gameObject);
                }
            }
        }

        Bounds bounds = new Bounds(roomBounds.position, roomBounds.localScale);

        for (int i = 0; i < numberOfObjects; i++)
        {
            float randomX = Random.Range(bounds.min.x, bounds.max.x);
            float randomZ = Random.Range(bounds.min.z, bounds.max.z);

            Vector3 rayOrigin = new Vector3(randomX, bounds.max.y + 1f, randomZ); // Cast from slightly above the room
            Ray ray = new Ray(rayOrigin, Vector3.down);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.transform == targetPlane) // Optional: Only place if we hit the target plane
                {
                    GameObject newObject = Instantiate(objectToPlace, hit.point, Quaternion.identity);
                    newObject.name = generatedObjectNamePrefix + i;
                    newObject.transform.SetParent(transform);
                }
            }
            else
            {
                Debug.LogWarning($"Could not find the target plane at X:{randomX}, Z:{randomZ}.");
            }
        }

        Debug.Log($"Attempted to generate {numberOfObjects} objects snapped to the target plane within the room's XZ bounds.");
    }
}
