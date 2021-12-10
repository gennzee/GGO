using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NPC_1 : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseUpAsButton()
    {
        // check if mouse was clicked on UI or not
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log("Clicked into " + transform.gameObject.name);
        }       
    }
}
