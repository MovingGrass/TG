using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyGFX : MonoBehaviour
{
    public AIPath aIPath;

    // Start is called before the first frame update
   

    // Update is called once per frame
    void Update()
    {
        if (aIPath.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(3.22f, 4.91f, 1f);
        }
        else if(aIPath.desiredVelocity.x <= 0.01f)
        {
            transform.localScale = new Vector3(-3.22f, 4.91f, 1f);
        }
    }
}
