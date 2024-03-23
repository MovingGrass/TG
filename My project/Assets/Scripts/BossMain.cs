using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMain : MonoBehaviour
{
    [Header("Health")]
    public float maxhealth = 100f;
    private float currenthealth;
    [Header("Melee combat Parameter")]
    [SerializeField]private BoxCollider2D boxCollider;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;
    [SerializeField] private float bossDamage;
    [SerializeField] private LayerMask playerLayer;

    [Header("Components")]
    [SerializeField] public Behaviour[] componentBoss;

   
    public  Animator anime;
    private float cooldownTimer = Mathf.Infinity;
    public BossHealth Healthbar;

    private player_Combat playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        currenthealth = maxhealth;
        Healthbar.SetHealthBoss(currenthealth, maxhealth);
    }

    public void TakeDamageBoss(float damage)
    {
        currenthealth -= damage;
        Healthbar.SetHealthBoss(currenthealth, maxhealth);
    
        if (currenthealth <= 0)
        {
            Die();

        }
    }

    void Die()
    {
        anime.SetBool("Modar", true);
        
        foreach (Behaviour component in componentBoss)
        {
            component.enabled = false;
        }
        

        
    }

    

}
