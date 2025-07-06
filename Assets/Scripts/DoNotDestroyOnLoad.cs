using UnityEngine;

public class DoNotDestroyOnLoad : MonoBehaviour
{
    private void Awake()
    {
        // Check for other instances of this component with the same object name
        DoNotDestroyOnLoad[] existingInstances = FindObjectsOfType<DoNotDestroyOnLoad>();
        
        foreach (DoNotDestroyOnLoad instance in existingInstances)
        {
            // If we find another object with the same name (excluding this one)
            if (instance != this && instance.gameObject.name == gameObject.name)
            {
                Destroy(gameObject);
                return;
            }
        }

        DontDestroyOnLoad(gameObject);
    }
}

