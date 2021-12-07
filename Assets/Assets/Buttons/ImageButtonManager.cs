using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ImageButtonManager : MonoBehaviour
{
    private Image iconImage;
    private void Start()
    {
        iconImage = GetComponent<Image>();
        if (iconImage.sprite == null) {
            iconImage.color = ChangeAlphaColor(iconImage.color, 0);
        } 
        else
        {
            iconImage.color = ChangeAlphaColor(iconImage.color, 255);
        }
    }

    private Color ChangeAlphaColor(Color color, int alpha)
    {
        Color c = color;
        c.a = alpha;
        return c;
    }
}
