using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingPointScript : MonoBehaviour
{
    private TextMesh textMesh;

    private void Awake()
    {
        textMesh = GetComponent<TextMesh>();
    }

    public void SetDamagePopUp(int damage)
    {
        textMesh.text = damage.ToString();
    }

    public void DestroyFloatingPoint()
    {
        Destroy(transform.parent.gameObject);
    }
}
