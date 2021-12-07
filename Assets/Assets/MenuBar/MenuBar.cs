using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBar : MonoBehaviour
{
    [SerializeField]
    private GameObject openMenuBarButton;
    [SerializeField]
    private GameObject closeMenuBarButton;
    [SerializeField]
    private GameObject menuBar;
    [SerializeField]
    private GameObject inventory;

    public void ToggleMenuBar()
    {
        if (openMenuBarButton.gameObject.activeSelf)
        {
            OpenMenuBar();
        }
        else
        {
            CloseMenuBar();
        }        
    }

    private void OpenMenuBar()
    {
        openMenuBarButton.SetActive(false);
        closeMenuBarButton.SetActive(true);
        menuBar.SetActive(true);
    }

    private void CloseMenuBar()
    {
        openMenuBarButton.SetActive(true);
        closeMenuBarButton.SetActive(false);
        menuBar.SetActive(false);
    }

    public void ToggleInventory()
    {
        if (inventory.activeSelf)
        {
            inventory.SetActive(false);
        }
        else
        {
            inventory.SetActive(true);
            CloseMenuBar();
        }

    }
}
