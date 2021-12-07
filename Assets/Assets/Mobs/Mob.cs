using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour
{
    #region private variable
    [SerializeField]
    private float onHitForceBack;
    [SerializeField]
    private MobHealthBar healthBar;
    [SerializeField]
    private int health;
    [SerializeField]
    private int maxHealth;

    private Rigidbody2D rigidbody2D;
    private Animator animator;
    private RaycastHit2D hit;
    private GameObject target;

    private float distance; // store distance between enemy and player
    private bool attackMode;
    private bool inRange; // check if player is in range
    private bool cooling; // check if enemy is cooling after attack
    private float intTimer;
    #endregion

    #region public variable
    public Transform raycast;
    public LayerMask raycastMask;
    public float raycastLength;
    public float attackDistance; // distance for attack
    public float moveSpeed;
    public float timer; // time cooldown between attack
    #endregion
    private void OnValidate()
    {
        health = maxHealth;
    }
    // Start is called before the first frame update
    void Start()
    {    
        healthBar.UpdateHealthUI(health, maxHealth);
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        intTimer = timer;
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange)
        {
            hit = Physics2D.Raycast(raycast.position, transform.right, raycastLength, raycastMask);
            RaycastDebugger();
        }

        if (hit.collider != null)
        {
            EnemyLogic();
        }
        else if (hit.collider == null)
        {
            inRange = false;
        }

        if (!inRange)
        {
            animator.SetBool("Run", false);
            StopAttack();
        }
    }
    private void EnemyLogic()
    {
        distance = Vector2.Distance(target.transform.position, transform.position);

        if (distance > attackDistance)
        {
            Move();
            StopAttack();
        }
        else if (attackDistance >= distance && !cooling)
        {
            Attack();
        }

        if (cooling)
        {
            CoolDown();
            animator.SetBool("Attack", false);
        }
    }
    private void Move()
    {
        animator.SetBool("Run", true);
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            Vector2 targetDistance = new Vector2(target.transform.position.x, target.transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetDistance, moveSpeed * Time.deltaTime);
        }
    }
    private void Attack()
    {
        timer = intTimer;
        attackMode = true;

        animator.SetBool("Run", false);
        animator.SetBool("Attack", true);
    }
    private void StopAttack()
    {
        cooling = false;
        attackMode = false;
        animator.SetBool("Attack", false);
    }
    private void CoolDown()
    {
        timer -= Time.deltaTime;
        if (timer <= 0 && cooling && attackMode)
        {
            cooling = false;
            timer = intTimer;
        }
    }
    private void Flip()
    {
        Vector3 rotation = transform.eulerAngles;
        if (transform.position.x > target.transform.position.x)
        {
            rotation.y = 180f;
        }
        else
        {
            rotation.y = 0f;
        }

        transform.eulerAngles = rotation;
    }
    // Be called in animation event
    private void TriggerCooling()
    {
        cooling = true;
    }
    public void OnDamaged(GameObject hitter)
    {        
        //push back on damaged
        transform.localScale = new Vector2((-1f) * hitter.transform.localScale.x, hitter.transform.localScale.y);
        //change direction of enemy
        float direction;        
        if (transform.localScale.x < 0)
        {
            direction = 1f;
        } 
        else
        {
            direction = -1f;
        }
        rigidbody2D.velocity = new Vector2(direction * onHitForceBack, rigidbody2D.velocity.y);
    }
    public void DecreaseHealth(int healthDecrease)
    {
        //decrease health
        health -= healthDecrease;
        if (health > 0) healthBar.UpdateHealthUI(health, maxHealth);
        //play die animation if health < 0
        if (health < 0 && healthBar != null)
        {
            healthBar.DestroyHealthBar();
            transform.gameObject.GetComponent<Animator>().SetTrigger("Die");
        }
    }
    //being called in animation event
    public void DestroyMob()
    {
        Destroy(transform.gameObject);
    }
    private void RaycastDebugger()
    {
        if (distance > attackDistance)
        {
            Debug.DrawRay(raycast.position, transform.right * raycastLength, Color.red);
        }
        else if (attackDistance > distance)
        {
            Debug.DrawRay(raycast.position, transform.right * raycastLength, Color.green);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            target = collision.gameObject;
            inRange = true;
            Flip();
        }
    }
}
