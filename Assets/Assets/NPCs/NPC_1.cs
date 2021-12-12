using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_1 : NPC
{
    public override void Interact()
    {
        Debug.Log("Interact with " + transform.gameObject.name);
    }
}
