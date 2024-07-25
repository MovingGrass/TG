using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetulinBugScaleBulletEnemy : MonoBehaviour
{
    // Start is called before the first frame update
   [SerializeField] private Transform enemy;

    private void Update()
    {
        transform.localScale = enemy.localScale;
    }
}
