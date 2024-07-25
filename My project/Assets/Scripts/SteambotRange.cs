using UnityEngine;

public class SteambotRange : MonoBehaviour
{
    [Header("Health")]
    public float maxhealth = 100f;
    private float currenthealth;
    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;

    [Header("Ranged Attack")]
    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject[] fireballs;

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;

    [SerializeField] public Behaviour[] componentsRange;

    [Header("Healthbar")]

    public RangeHealth Healthbar;

    //References
    private Animator anim;
    private RangePatrol enemyPatrol;

    void Start()
    {
        currenthealth = maxhealth;
        Healthbar.SetHealthRange(currenthealth, maxhealth);
    }


    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<RangePatrol>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        //Attack only when player in sight?
        if (PlayerInSight())
        {
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                anim.SetTrigger("rangeAttack");
            }
        }

        if (enemyPatrol != null)
            enemyPatrol.enabled = !PlayerInSight();
    }

    
    
    private bool PlayerInSight()
    {
        RaycastHit2D hit =
            Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

        return hit.collider != null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    public void TakeDamageRange(float damage)     
    {
        currenthealth -= damage;
        Healthbar.SetHealthRange(currenthealth, maxhealth);
    
        if (currenthealth <= 0)
        {
            Die();

        }
    }

    void Die()
    {
        anim.SetBool("Modar", true);

        
        
        foreach (Behaviour component in componentsRange)
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

    void destroyRange()
    {
        Destroy(this.gameObject);
    }
}