using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class SteamBirdMainScript : MonoBehaviour
{
    [Header("Health")]
    public float maxhealth = 50f;
    private float currenthealth;
    [Header("Melee combat Parameter")]
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;
    [SerializeField] private float steambirdDamage;
    [SerializeField] private LayerMask playerLayer;
    [Header("Detector")]
    [SerializeField] private BoxCollider2D detector;
    [SerializeField] private float detect_range;
    [SerializeField] private float detect_colliderDistance;
    
    

    [Header("Components")]
    [SerializeField] public Behaviour[] components;

    private SteamBird_Patrol steambird;
    private  Animator anime;
    private float cooldownTimer = Mathf.Infinity;
    public SteamBirdHealth Healthbar;

    private player_Combat playerHealth;

    private AIPath aiPath;
    private AIDestinationSetter aiDestinationSetter;

    private EnemyGFX enemyGFX;

    private Vector3 parentlocalScale;

    // Start is called before the first frame update
    void Start()
    {
        
        GameObject SteambirdFollow = transform.parent.gameObject;

        // Get the AIPath and AiDestinationSetter components from the parent GameObject
        aiPath = SteambirdFollow.GetComponent<AIPath>();
        aiDestinationSetter = SteambirdFollow.GetComponent<AIDestinationSetter>();
        currenthealth = maxhealth;
        Healthbar.SetHealthBird(currenthealth, maxhealth);
        enemyGFX = GetComponent<EnemyGFX>();

    }

    private void Awake()
    {
        Vector3 parentlocalScale = transform.parent.localScale;
        anime = GetComponent<Animator>();
        steambird = GetComponentInParent<SteamBird_Patrol>();
    }

    

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (PlayerInSight())
        {
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                
                Damageplayer();

            }
        }
        if (PlayerInDetector())
        {
            steambird.enabled = false;

            // Mengaktifkan AI Destination Setter dan AIPath
            
            aiPath.enabled = true;

            enemyGFX.enabled = true;
            
            aiDestinationSetter.enabled = true;
            anime.SetBool("Hostile", true);
        }
        

        
    }
    
    private bool PlayerInDetector()
    {
        RaycastHit2D hits = Physics2D.BoxCast(detector.bounds.center + transform.right * detect_range * transform.localScale.x * detect_colliderDistance, new Vector3(detector.bounds.size.x * range, detector.bounds.size.y, detector.bounds.size.z), 0, Vector2.left, 0, playerLayer);

        if(hits.collider != null)
        {
            return true;
        }

        else
        {
            return false;
        }
          
            
        return hits.collider != null;
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

    public void TakeDamageBird(float damage)     
    {
        Debug.Log(currenthealth);
        currenthealth -= damage;
        Healthbar.SetHealthBird(currenthealth, maxhealth);
    
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
            playerHealth.tookDamage(steambirdDamage);
        }
    }

    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(detector.bounds.center + transform.right * detect_range * transform.localScale.x * detect_colliderDistance, new Vector3(detector.bounds.size.x * detect_range, detector.bounds.size.y, detector.bounds.size.z));
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
        Destroy(this.gameObject);
    }
}
