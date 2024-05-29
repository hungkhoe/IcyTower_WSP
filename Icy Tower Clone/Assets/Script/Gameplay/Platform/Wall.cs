using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    // Start is called before the first frame update

    public int wallIndex = 0;    

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.platformManager.totalWall - wallIndex >= 5)
        {
            if(wallIndex != 0)
            SimplePool.Despawn(gameObject);         
        }
    }
}
