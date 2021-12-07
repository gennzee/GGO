using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Range(30, 120)]
    [SerializeField]
    private int maxFps;
    // Start is called before the first frame update
    private void Start()
    {
        Application.targetFrameRate = maxFps;
        Physics2D.IgnoreLayerCollision(7, 8, true); //8: MonsterFoot, 7: Player Foot
        Physics2D.IgnoreLayerCollision(7, 11, true); //7: Player Foot, 11: NPC
    }
}
