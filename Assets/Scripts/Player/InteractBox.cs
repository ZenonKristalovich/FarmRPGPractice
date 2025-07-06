using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractBox : MonoBehaviour
{
    public GameObject interactBox;
    public Player player ;
    
    private Vector3 prevOffset;

    public void FixedUpdate()
    {
        if(interactBox.activeSelf)
        {
            Vector3 parentPos = transform.position;
            
            Vector3 snappedPos = new Vector3(
                Mathf.Floor(parentPos.x) + 0.5f,  // Center on tile
                Mathf.Floor(parentPos.y) + 0.5f,  // Center on tile
                parentPos.z
            );
            
            // Update the position of the interact box to cover the tile
            interactBox.transform.position = snappedPos;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log($"[INTERACTBOX] E key pressed");
            CheckForInteractables();
        }
    }

    private void CheckForInteractables()
    {
        // Get all colliders overlapping with the interact box
        Debug.Log($"[INTERACTBOX] Checking for interactables at {interactBox.transform.position}");
        Collider2D[] colliders = Physics2D.OverlapBoxAll(interactBox.transform.position, interactBox.transform.localScale, 0f);
        
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Interactable"))
            {
                Debug.Log($"[INTERACTBOX] Interacting with {collider.gameObject.name}");
                collider.gameObject.SendMessage("OnInteract", player,SendMessageOptions.DontRequireReceiver);
            }
        }
    }

    public void UpdatePosition(Vector3 direction)
    {
        if (direction.x == 0 && direction.y == 0)
        {
        }
        else
        {
            float xOffset = 0f;
            float yOffset = 0f;

            // Set specific offsets based on direction
            if (direction.x > 0) xOffset = 0.5f;      // Right
            if (direction.x < 0) xOffset = -0.2f;     // Left
            if (direction.x == 0) xOffset = 0.15f;
            if (direction.y > 0) yOffset = 0.3f;      // Up
            if (direction.y < 0) yOffset = -0.5f;     // Down
            if (direction.y == 0) yOffset = -0.1f;

            prevOffset = new Vector3(xOffset, yOffset, 0);
            transform.localPosition = prevOffset;
        }
    }

    public void interactBoxEnable()
    {
        interactBox.SetActive(true);
    }

    public void interactBoxDisable()
    {
        interactBox.SetActive(false);   
    }

    public Vector3Int GetInteractBoxPosition()
    {
        Vector3 currentPos = interactBox.transform.position;
        // Round to nearest whole numbers to align with tile grid
        return new Vector3Int(
            Mathf.FloorToInt(currentPos.x) ,  // Center on tile
            Mathf.FloorToInt(currentPos.y) ,  // Center on tile
            0
        );  
    }


}
