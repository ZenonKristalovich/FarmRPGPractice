using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class TileManager : MonoBehaviour
{

    [SerializeField] private Tilemap interactableMap;

    [SerializeField] private Tile hiddenInteractableTile;
    [SerializeField] private Tile plowedTile;


    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        InitializeTiles();
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        InitializeTiles();
    }

    private void InitializeTiles()
    {
        //Find Interactable map and store it in the variable
        interactableMap = GameObject.Find("InteractableMap").GetComponent<Tilemap>();
        if(interactableMap == null)
        {
            Debug.LogError("InteractableMap not found");
            return;
        }else{
            Debug.Log("InteractableMap found");
        }

        foreach(var position in interactableMap.cellBounds.allPositionsWithin)
        {
            TileBase tile = interactableMap.GetTile(position);

            if(tile != null && tile.name == "Interactable_Visible")
            {
                interactableMap.SetTile(position, hiddenInteractableTile);
            }
        }
    }

    public bool IsPlowed(Vector3Int position)
    {
        TileBase tile = interactableMap.GetTile(position);

        if(tile != null)
        {
            if(tile.name == "Summer_Plowed")
            {
                return true;
            }
        }
        return false;
    }

    public void PlowTile(Vector3Int position)
    {   
        TileBase tile = interactableMap.GetTile(position);
        if(tile != null)
        {
            if(tile.name == "Interactable")
            {
                interactableMap.SetTile(position, plowedTile);
            }
            else if(tile.name == "Summer_Plowed")
            {
                interactableMap.SetTile(position, hiddenInteractableTile);
            }
        }
    }

    public string getTileName(Vector3Int position)
    {
        if (interactableMap != null)
        {
            TileBase tile = interactableMap.GetTile(position);

            if(tile != null)
            {
                return tile.name;
            }
        }

        return null;
    }

}
