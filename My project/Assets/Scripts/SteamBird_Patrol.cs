using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class SteamBird_Patrol : MonoBehaviour
{
    [Header ("Patrol points")]
    [SerializeField] private Transform rightEdge;
    [SerializeField] private Transform leftEdge;
    
    [Header ("Enemy")]
    [SerializeField] private Transform enemy;
    [SerializeField] private Transform enemy2;

     [Header ("Movement Parameters")]
    [SerializeField] private float speed;
    private bool movingLeft;

    [Header ("Idle Behaviour")]

    [SerializeField] private float idleDuration;

    private float idleTimer;
 
    private Vector3 initScale;
    private Vector3 initScale2;

    [Header ("Animator")]
    [SerializeField] private Animator anime;

    private void Awake()
    {
        initScale = enemy.localScale;
        initScale2 = enemy2.localScale;
    }
    private void OnDisable()
    {
        anime.SetBool("IsMoving", false);
    }

    private void Update()
    {
        if (movingLeft)
        {
            if(enemy.position.x >= leftEdge.position.x)
            {
                MoveInDirection(-1);
            }
            else
            {
                 DirectionChange();
            }
            
        }
        else
        {
            if(enemy.position.x <= rightEdge.position.x)
            {
                MoveInDirection(1);
            }
            else
            {
                DirectionChange();
            }
            
        }
        
    }

    private void DirectionChange()
    {
        anime.SetBool("IsMoving", false);

        idleTimer += Time.deltaTime;

        if(idleTimer > idleDuration)
        {
            movingLeft = !movingLeft;
        }
        
    }

    private void MoveInDirection(int _direction)
    {
        idleTimer = 0;

        anime.SetBool("IsMoving", true);

        
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * speed, enemy.position.y, enemy.position.z);  

        enemy2.localScale = new Vector3(Mathf.Abs(initScale2.x) * _direction,
        initScale2.y, initScale2.z);
        
    }
   
}
