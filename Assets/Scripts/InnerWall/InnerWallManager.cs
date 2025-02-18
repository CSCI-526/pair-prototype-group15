using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerWallManager : MonoBehaviour
{
    
    public static InnerWallManager Instance; // Singleton pattern

    private void Awake()
    {
        Instance = this;
    } 

    public void StartRespawnWalls(GameObject obj)
    {
        StartCoroutine(RespawnInnerWall(obj));
    }

    private IEnumerator RespawnInnerWall(GameObject obj)
    {
        obj.SetActive(false);
        yield return new WaitForSeconds(5f);
        obj.SetActive(true);
    }
}
