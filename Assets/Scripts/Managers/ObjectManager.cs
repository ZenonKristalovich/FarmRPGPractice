using UnityEngine;
using System.Collections.Generic;

public class ObjectManager : MonoBehaviour
{
    public List<GameObject> objectDatabase = new List<GameObject>();

    public void SpawnObject(Vector3Int position, int index)
    {
        Vector3 convertedPosition = new Vector3(position.x + 0.5f, position.y + 0.5f, 0);
        
        GameObject newObject = objectDatabase[index];
        Instantiate(newObject, convertedPosition, Quaternion.identity);
    }
}
