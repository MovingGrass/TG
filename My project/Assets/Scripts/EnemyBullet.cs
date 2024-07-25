using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float Speed = 70f;
    // Start is called before the first frame update
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private int damage = 15;

    void Start()
    {
        rb.velocity = transform.right * Speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo) 
    {
        player_Combat player = hitInfo.GetComponent<player_Combat>();
        if (player != null)
        {
            player.tookDamage(damage);
            Destroy(gameObject);
            return; 
        }
    }

}
