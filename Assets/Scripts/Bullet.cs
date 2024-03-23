using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] private float Speed = 20f;
    // Start is called before the first frame update
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private int damage = 50;

    void Start()
    {
        rb.velocity = transform.right * Speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo) 
    {
        SteambotScript steambot = hitInfo.GetComponent<SteambotScript>();
        if (steambot != null)
        {
            steambot.TakeDamage(damage);
            Destroy(gameObject);
            return; // Exit the method to avoid executing the next block
        }

    BossMain boss = hitInfo.GetComponent<BossMain>();
        if (boss != null)
        {
            boss.TakeDamageBoss(damage);
            Destroy(gameObject);
            return; // Exit the method if BossMain component is found
        }
    }


    // Update is called once per frame
    
}
