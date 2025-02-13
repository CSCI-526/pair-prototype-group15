using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    private Vector2 pos;
    private Vector2 size = new Vector2(1, 1);
    // Start is called before the first frame update
    void Start()
    {
        pos = gameObject.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D obj){
        if(obj.CompareTag("Bullet")){
            BulletBehavior bulletScript = obj.GetComponent<BulletBehavior>();
            bulletScript.IncreaseSpeed();

            InnerWallManager.Instance.StartRespawnWalls(gameObject);
        }
    }
}
