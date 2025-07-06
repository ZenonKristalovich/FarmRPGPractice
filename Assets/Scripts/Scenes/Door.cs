using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour
{
    [Header("Scene Transition Settings")]
    [SerializeField] private string sceneToLoad;
    [SerializeField] private Vector2 playerPosition;

    public void OnInteract(Player player)
    {
        player.transform.position = new Vector3(playerPosition.x, playerPosition.y, player.transform.position.z);
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneToLoad);
    }

}
