using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_OnCollide : MonoBehaviour
{
    [Header("Scene Transition Settings")]
    [SerializeField] private string sceneToLoad;
    [SerializeField] private Vector2 playerPosition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player != null)
        {
            player.transform.position = new Vector3(playerPosition.x, playerPosition.y, player.transform.position.z);
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneToLoad);
        }
    }
}
