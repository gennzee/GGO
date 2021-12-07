using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Button[] buttons;

    private KeyCode keyAction1;
    private KeyCode keyAction2;
    private KeyCode keyAction3;
    private KeyCode keyAction4;

    // Start is called before the first frame update
    void Start()
    {
        keyAction1 = KeyCode.Alpha1;
        keyAction2 = KeyCode.Alpha2;
        keyAction3 = KeyCode.Alpha3;
        keyAction4 = KeyCode.Alpha4;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyAction1))
        {
            ActionButtonOnclick(0);
        }
        if (Input.GetKeyDown(keyAction2))
        {
            ActionButtonOnclick(1);
        }
        if (Input.GetKeyDown(keyAction3))
        {
            ActionButtonOnclick(2);
        }
        if (Input.GetKeyDown(keyAction4))
        {
            ActionButtonOnclick(3);
        }
    }

    private void ActionButtonOnclick(int buttonInx)
    {
        buttons[buttonInx].onClick.Invoke();
    }
}
