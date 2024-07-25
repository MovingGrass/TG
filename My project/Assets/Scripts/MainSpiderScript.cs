using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSpiderScript : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float maxhealth = 100f;
    private float currenthealth;
    [Header("Melee combat Parameter")]
    [SerializeField]private BoxCollider2D boxCollider;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;
    [SerializeField] private float Damage;
    [SerializeField] private LayerMask playerLayer;

    [Header("Components")]
    [SerializeField] public Behaviour[] components;

    private SpiderPatrol spider;
    private  Animator anime;
    private float cooldownTimer = Mathf.Infinity;
    public SpiderHealth Healthbar;

    private player_Combat playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        currenthealth = maxhealth;
        Healthbar.SetHealthSpider(currenthealth, maxhealth);
    }

    private void Awake()
    {
        anime = GetComponent<Animator>();
        spider = GetComponentInParent<SpiderPatrol>();
    }

    

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (PlayerInSight())
        {
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                anime.SetTrigger("Attack");
            

            }
        }
        if(spider != null)
        {
            spider.enabled = !PlayerInSight();
        }

        
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 0, Vector2.left, 0, playerLayer);

        if(hit.collider != null)
        {
            playerHealth = hit.transform.GetComponent<player_Combat>();
        }
          
            
        return hit.collider != null;
    }

    public void TakeDamageSpider(float damage)
    {
        currenthealth -= damage;
        Healthbar.SetHealthSpider(currenthealth, maxhealth);
    
        if (currenthealth <= 0)
        {
            Die();

        }
    }

    private void Damageplayer()
    {
    
        if (PlayerInSight())
        {
            Debug.Log("Damaging player");
            playerHealth.tookDamage(Damage);
        }
    }

    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    void Die()
    {
        anime.SetBool("Modar", true);
        
        foreach (Behaviour component in components)
        {
            component.enabled = false;
        }
        

        
    }

    public float GetCurrentHealth()
    {
        return currenthealth;
    }

    public float GetMaxHealth()
    {
        return maxhealth;
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
    
}
