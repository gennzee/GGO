using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Mirror;

public abstract class NPC : MonoBehaviour
{
    [SerializeField]
    private GameObject InteractIcon;
    [SerializeField]
    private GameObject ChatUiBtn;

    public abstract void Interact();

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == NetworkClient.localPlayer.gameObject)
        {
            InteractIcon.SetActive(true);
            ChatUiBtn.SetActive(true);
            NetworkClient.localPlayer.gameObject.GetComponent<PlayerBehavior>().NpcInteraction = transform.gameObject;
        }
    }

    public virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == NetworkClient.localPlayer.gameObject)
        {
            InteractIcon.SetActive(false);
            ChatUiBtn.SetActive(false);
            NetworkClient.localPlayer.gameObject.GetComponent<PlayerBehavior>().NpcInteraction = null;
        }
    }
}
