using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : Character
{    
    [SerializeField]
    private Stat health;
    [SerializeField]
    private float maxHealth;
    [SerializeField]
    private Stat mana;
    [SerializeField]
    private float maxMana;    

    private SpellBook spellBook;
    private GearSocket[] gearSocket;
    private PlayerOneWayPlatformBehavior playerOneWayPlatformBehavior;

    [Space]
    [SerializeField]
    private InventoryObject inventory;
    
    [Space]
    [SerializeField]
    private VariableJoystick joystick;
    [SerializeField]
    private float joystickDeadZone;
    [SerializeField]
    private InputDevice inputDevice;
    private enum InputDevice { PC, Mobile }
    // Start is called before the first frame update
    protected override void Start()
    {        
        spellBook = GetComponent<SpellBook>();
        health.Initialize(maxHealth, maxHealth);
        mana.Initialize(maxMana, maxMana);
        gearSocket = GetComponentsInChildren<GearSocket>();
        playerOneWayPlatformBehavior = GetComponentInChildren<PlayerOneWayPlatformBehavior>();
        base.Start();
    }
    // Update is called once per frame
    protected override void Update()
    {
        horizontal = GetRawAxisInput("Horizontal");
        vertical = GetRawAxisInput("Vertical");
        if (!isAttacking) HandleMovement();
        AnimateMovement();
        HandleInput();
        base.Update();
    }

    private void HandleMovement()
    {
        switch(inputDevice)
        {
            case InputDevice.PC:
                // pc input
                rigidbody2D.velocity = new Vector2(horizontal * speed, rigidbody2D.velocity.y);
                if (vertical > 0)
                {
                    rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpForce);
                }
                break;
            case InputDevice.Mobile:
                // mobile input
                int horizontall = 0;
                float direction = Mathf.Abs(transform.localScale.x);
                if (joystick.Horizontal >= joystickDeadZone)
                {
                    horizontall = 1;
                    transform.localScale = new Vector2(direction, transform.localScale.y);
                }
                else if (joystick.Horizontal <= -joystickDeadZone)
                {
                    horizontall = -1;
                    transform.localScale = new Vector2(-direction, transform.localScale.y);
                }                
                rigidbody2D.velocity = new Vector2(horizontall * speed, rigidbody2D.velocity.y);                
                break;
        }
    }
    private void AnimateMovement()
    {
        State state;
        if (rigidbody2D.velocity.x != 0f)
        {            
            state = State.Run;
        }
        else
        {
            state = State.Idle;
        }

        if (rigidbody2D.velocity.y > 0f)
        {
            state = State.Jump;
        }
        else if (rigidbody2D.velocity.y < 0f)
        {
            state = State.Fall;
            isHighJumping = false;
        }

        if (isHighJumping)
        {
            state = State.HighJump;
        }

        if (joystick.Vertical <= -joystickDeadZone && rigidbody2D.velocity.y == 0f && rigidbody2D.velocity.x == 0f)
        {
            state = State.Block;
            isBlocking = true;
        } 
        else
        {
            isBlocking = false;
        }
        
        animator.SetInteger("State", (int)state);
        PlayGearMovementAnimation(state);        
    }
    private IEnumerator Attack(int spellIndex)
    {
        isAttacking = true;
        Spell spell = spellBook.CastSpell(spellIndex);        
        attackTime = GetClipTime(spell.GetPlayerAnimation);

        animator.SetTrigger("Attack");
        PlayGearAttackAnimation();

        InstantiateSpell(spell);

        yield return new WaitForSeconds(attackTime);

        isAttacking = false;
        spellBook.StopCasting();
    }
    public void Jump()
    {
        if (!isAttacking)
        {
            if (joystick.Vertical >= joystickDeadZone || Input.GetKey(KeyCode.LeftControl))
            {
                isHighJumping = true;
            }
            else if ((joystick.Vertical <= -joystickDeadZone || Input.GetKey(KeyCode.RightControl)) && rigidbody2D.velocity.y == 0f)
            {
                playerOneWayPlatformBehavior.DropPlayer();
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, dropForce);
            }
            else
            {
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpForce);
            }
        }
    }
    public void HighJumping()
    {
        rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpForce * highJumpForce);
    }
    private void HandleInput()
    {
        if (Input.GetKey(KeyCode.Space) && !isAttacking)
        {
            StartCoroutine(Attack(0));
        }
    }
    // being called in ActionButton
    private void CastSpell(int spellIndex)
    {
        if (!isAttacking)
        {
            StartCoroutine(Attack(spellIndex));
        }        
    }
    public void InstantiateSpell(Spell spell)
    {
        for (int i = 0; i < spell.GetSpellEffectPrefabs.Length; i++)
        {
            SpellScript s = Instantiate(spell.GetSpellEffectPrefabs[i], existSpellPoint.transform.position, Quaternion.identity).GetComponent<SpellScript>();
            s.Instantiate(transform.gameObject, spell);
        }        
    }
    private void PlayGearMovementAnimation(State state)
    {
        foreach (GearSocket gear in gearSocket)
        {
            gear.PlayMovementAnimation("State", (int)state);
        }
    }
    private void PlayGearAttackAnimation()
    {
        foreach (GearSocket gear in gearSocket)
        {
            gear.PlayAttackAnimation("Attack");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Item")
        {
            inventory.AddItem(collision.GetComponent<Item>().item, 1);
            Destroy(collision.gameObject);
        }
    }
}
