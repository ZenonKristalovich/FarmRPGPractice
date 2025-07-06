using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public TileManager tileManager;
    public ObjectManager objectManager;

    public ItemDB itemDB;

    public SceneHandler sceneHandler;

    //Date
    public int day;
    public int season;
    public int year;
    public ClockDisplay clockDisplay;

    public Player player;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);

        tileManager = GetComponent<TileManager>();
        objectManager = GetComponent<ObjectManager>();
        player = FindObjectOfType<Player>();
        sceneHandler = FindObjectOfType<SceneHandler>();
    }
}
