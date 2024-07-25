using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMain : MonoBehaviour
{
    [Header("Health")]
    public float maxhealth = 100f;
    private float currenthealth;

    [Header("Melee combat Parameter")]
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float attackRange;
    [SerializeField] private float sightRange;
    [SerializeField] private float colliderDistance;
    [SerializeField] private float bossDamage;
    [SerializeField] private LayerMask playerLayer;

    [Header("Boss AI")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float spinningAttackDuration;
    [SerializeField] private float spinningAttackDamage;
    [SerializeField] private float heavySlashChargeTime;
    [SerializeField] private float heavySlashDamage;

    [Header("Components")]
    [SerializeField] public Behaviour[] componentBoss;
    public Animator anime;
    private float cooldownTimer = Mathf.Infinity;
    public BossHealth Healthbar;
    private player_Combat playerHealth;

    private enum BossState { Idle, Pursuing, NormalAttack, SpinningAttack, HeavySlash }
    private BossState currentState;

    private Transform player;
    private Rigidbody2D rb;

    void Start()
    {
        currenthealth = maxhealth;
        Healthbar.SetHealthBoss(currenthealth, maxhealth);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        currentState = BossState.Idle;
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<player_Combat>();
    }

    void Update()
    {
        cooldownTimer += Time.deltaTime;

        switch (currentState)
        {
            case BossState.Idle:
                if (CanSeePlayer())
                {
                    currentState = BossState.Pursuing;
                }
                break;

            case BossState.Pursuing:
                PursuePlayer();
                if (PlayerInAttackRange())
                {
                    ChooseRandomAttack();
                }
                break;

            case BossState.NormalAttack:
            case BossState.SpinningAttack:
            case BossState.HeavySlash:
                StartCoroutine(HeavySlashCD());
                // These states are handled by animation events
                break;
        }
    }

    IEnumerator HeavySlashCD()
{
    yield return new WaitForSeconds(4f);
    currentState = BossState.Idle;
}

    bool CanSeePlayer()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * colliderDistance, 
            new Vector2(sightRange, boxCollider.bounds.size.y), 0f, Vector2.right, sightRange, playerLayer);
        return hit.collider != null;
    }

    bool PlayerInAttackRange()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * colliderDistance, 
            new Vector2(attackRange, boxCollider.bounds.size.y), 0f, Vector2.right, attackRange, playerLayer);
        return hit.collider != null;
    }

    void PursuePlayer()
    {
        if (player != null)
        {
            float directionX = Mathf.Sign(player.position.x - transform.position.x);
            rb.velocity = new Vector2(directionX * moveSpeed, 0f);

            // Flip the boss sprite if necessary
            if (directionX != 0)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -directionX, 
                                                   transform.localScale.y, 
                                                   transform.localScale.z);
            }
        }
    }

    void ChooseRandomAttack()
    {
        if (cooldownTimer >= attackCooldown)
        {
            int randomAttack = Random.Range(0, 3);
            switch (randomAttack)
            {
                case 0:
                    StartNormalAttack();
                    break;
                case 1:
                    StartSpinningAttack();
                    break;
                case 2:
                    StartHeavySlash();
                    break;
            }
            cooldownTimer = 0;
        }
    }

    void StartNormalAttack()
    {
        currentState = BossState.NormalAttack;
        anime.SetTrigger("NormalAttack");
    }

    void StartSpinningAttack()
    {
        currentState = BossState.SpinningAttack;
        anime.SetTrigger("SpinningAttack");
        StartCoroutine(PerformSpinningAttack());
    }

    public void StartHeavySlash()
    {
        currentState = BossState.HeavySlash;
        anime.SetTrigger("HeavySlash");
    }

    public void ApplyNormalAttackDamage()
    {
        if (PlayerInAttackRange())
        {
            playerHealth.tookDamage(bossDamage);
        }
    }

    public void ApplyHeavySlashDamage()
    {
        if (PlayerInAttackRange())
        {
            playerHealth.tookDamage(heavySlashDamage);
        }
    }

    IEnumerator PerformSpinningAttack()
    {
        float elapsedTime = 0f;
        while (elapsedTime < spinningAttackDuration)
        {
            if (PlayerInAttackRange())
            {
                playerHealth.tookDamage(spinningAttackDamage * Time.deltaTime);
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        currentState = BossState.Pursuing;
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

    void OnDrawGizmos()
    {
        if (boxCollider == null)
            return;

        // Attack range
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * (colliderDistance + attackRange / 2),
            new Vector3(attackRange, boxCollider.bounds.size.y, boxCollider.bounds.size.z));

        // Sight range
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * (colliderDistance + sightRange / 2),
            new Vector3(sightRange, boxCollider.bounds.size.y, boxCollider.bounds.size.z));

        // Collider distance
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(boxCollider.bounds.center, boxCollider.bounds.center + transform.right * colliderDistance);
        Gizmos.DrawLine(boxCollider.bounds.center, boxCollider.bounds.center - transform.right * colliderDistance);
    }
}
