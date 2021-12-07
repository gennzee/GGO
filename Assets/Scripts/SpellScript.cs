using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class SpellScript : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private int hitCount;
    [SerializeField]
    private float timeDelayEachHit;
    [SerializeField]
    private GameObject floatingPointObject;

    private Rigidbody2D rigidbody2D;
    private GameObject player;
    private Spell spellInfo;

    private float direction;
    // Is called by Reference before Start() can be call
    public void Instantiate(GameObject caster, Spell spell)
    {
        player = caster;
        spellInfo = spell;
    }
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        //set tag of skill
        transform.gameObject.tag = "Skill";
        //flip spell based on Player
        transform.localScale = new Vector2(player.transform.localScale.x, player.transform.localScale.y);
        //direction based on Player
        direction = player.transform.localScale.x;     
    }
    // Update is called once per frame
    void Update()
    {
        rigidbody2D.velocity = new Vector2(direction * speed, rigidbody2D.velocity.y);
    }        
    private IEnumerator HitTarget(Vector2 position, GameObject target)
    {
        for (int i = 0; i < hitCount; i++)
        {
            // init Floating Point prefab and assign damage to it
            FloatingPointScript fps = Instantiate(floatingPointObject, position, Quaternion.identity).GetComponentInChildren<FloatingPointScript>();
            //fixme: debug only
            int damage = Random.RandomRange(0, spellInfo.GetDamage);
            //fixme: end
            fps.SetDamagePopUp(damage);
            // check HitEffectPrefab 
            if (spellInfo.GetHitPrefab != null)
            {
                // find closest point on hit
                Vector2 closestPoint = target.GetComponent<BoxCollider2D>().ClosestPoint(transform.position);
                // init hit prefab on closest point
                Instantiate(spellInfo.GetHitPrefab, closestPoint, Quaternion.identity);
            }
            // play Hurt animation from mob
            target.GetComponent<Animator>().SetTrigger("Hurt");
            // decrease mob health
            target.GetComponent<Mob>().DecreaseHealth(damage);
            yield return new WaitForSeconds(timeDelayEachHit);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // check if spell hit mobs
        if (collision.tag == "Monster")
        {
            GameObject target = collision.gameObject;
            // start hitting target
            StartCoroutine(HitTarget(collision.transform.position, target));            
            // enemy onDamaged
            target.GetComponent<Mob>().OnDamaged(player);
        }
    }
}
