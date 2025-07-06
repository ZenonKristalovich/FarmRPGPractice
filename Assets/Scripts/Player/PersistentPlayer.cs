using UnityEngine;

public class PersistentPlayer : MonoBehaviour
{
    private void Awake()
    {
        // Ensure the object persists across scenes
        DontDestroyOnLoad(gameObject);
        Debug.Log("Player object marked as Don't Destroy On Load.");
    }
}
