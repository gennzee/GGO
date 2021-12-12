using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using Mirror;

public class CinemachineCam : MonoBehaviour
{
    private CinemachineVirtualCamera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        SetCamFollowLocalPlayer();
    }

    private void SetCamFollowLocalPlayer()
    {
        if (NetworkClient.localPlayer != null)
        {
            cam.Follow = NetworkClient.localPlayer.transform;
        }
    }
}
