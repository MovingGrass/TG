using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_Combat : MonoBehaviour
{
    public Animator anim;

    [Header("Combat Parameters")]

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    [SerializeField] private float meleeDamage = 30f;
    [SerializeField] private float attackRate = 2f;
     private float nextAttack = 0f;

    public float maxthealth = 100f;

    private float currenthealth;

    public Healthbar healthbar;
    
    


    

    void Awake()
    {
        currenthealth = maxthealth;
        healthbar.SetMaxHealth((int)maxthealth);
        
    }
    

    // Update is called once per frame
    void Update()
    {


        if(Time.time >= nextAttack)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Attack();
                nextAttack = Time.time + 1f/attackRate;
            }
        }
        
    }

    public void tookDamage(float damage)
    {
        currenthealth -= damage;
        Debug.Log(currenthealth);

        healthbar.SetHealth((int)currenthealth);

        if (currenthealth <= 0)
        {
            playerDie();
        }
    }

    void playerDie()
    {
        Debug.Log("Player died");
        anim.SetBool("Meninggoi", true);
       
        
        
        
        this.enabled = false;
        GetComponent<Dashscript>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Player_Movement>().enabled = false;
        GetComponent<Gun>().enabled = false;
        
        
    }

    


    void Attack()
    {
        anim.SetTrigger("Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemyCollider in hitEnemies)
            {
                SteambotScript steambot = enemyCollider.GetComponent<SteambotScript>();
                if (steambot != null)
                {   
                    steambot.TakeDamage(meleeDamage);
                }

                BossMain boss = enemyCollider.GetComponent<BossMain>();
                if (boss != null)
                {
                    boss.TakeDamageBoss(meleeDamage);
                }

                MainSpiderScript spider = enemyCollider.GetComponent<MainSpiderScript>();
                if (spider != null)
                {   
                    spider.TakeDamageSpider(meleeDamage);
                }

                SteambotRange range = enemyCollider.GetComponent<SteambotRange>();
                if (range != null)
                {   
                    range.TakeDamageRange(meleeDamage);
                }

                SteamBirdMainScript bird = enemyCollider.GetComponent<SteamBirdMainScript>();
                if (bird != null)
                {   
                    bird.TakeDamageBird(meleeDamage);
                }
            }
    }   


   
    void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
