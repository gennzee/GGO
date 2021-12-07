using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOneWayPlatformBehavior : MonoBehaviour
{    
    [SerializeField]
    private VariableJoystick joystick;
    [SerializeField]
    private float waiting;

    private GameObject oneWayPlatformObject;    
    private BoxCollider2D footCollider;
    // Start is called before the first frame update
    private void Start()
    {
        footCollider = GetComponent<BoxCollider2D>();
    }
    public void DropPlayer()
    {
        if (oneWayPlatformObject != null)
        {
            StartCoroutine(DisableCollisionToPlatform());
        }
    }
    private IEnumerator DisableCollisionToPlatform()
    {
        BoxCollider2D platformCollider = oneWayPlatformObject.GetComponent<BoxCollider2D>();
        Physics2D.IgnoreCollision(footCollider, platformCollider, true);
        yield return new WaitForSeconds(waiting);
        Physics2D.IgnoreCollision(footCollider, platformCollider, false);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "OneWayPlatform")
        {
            oneWayPlatformObject = collision.gameObject;
        }
    }
}
