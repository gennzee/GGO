using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public abstract class Common : NetworkBehaviour
{
    protected enum State
    {
        Idle = 0,
        Walk = 1,
        Run = 2,
        Jump = 3,
        Fall = 4,
        HighJump = 5,
        Block = 6
    }
}
