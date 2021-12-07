using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellBook : MonoBehaviour
{
    [SerializeField]
    private Image castingBar;
    [SerializeField]
    private Text castingText;
    [SerializeField]
    private Text spellName;
    [SerializeField]
    private Image icon;
    [SerializeField]
    private CanvasGroup canvasGroup;

    private Coroutine fadeRoutine;
    private Coroutine spellRoutine;

    [SerializeField]
    private Spell[] spells;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Spell CastSpell(int spellIndex)
    {
        spellName.text = spells[spellIndex].GetName;
        icon.sprite = spells[spellIndex].GetIcon;
        fadeRoutine = StartCoroutine(FadeBar());
        spellRoutine = StartCoroutine(Progress(spellIndex));
        return spells[spellIndex];  
    }
    public void StopCasting()
    {
        if (fadeRoutine != null)
        {
            StopCoroutine(fadeRoutine);
            fadeRoutine = null;
            canvasGroup.alpha = 0f;
        }
        if (spellRoutine != null)
        {
            StopCoroutine(spellRoutine);
            spellRoutine = null;
        }
    }
    private IEnumerator Progress(int spellIndex)
    {
        castingBar.fillAmount = 0.0f;
        float timePassed = Time.deltaTime;
        float rate = 1.0f / spells[spellIndex].GetCastTime;
        float progress = 0.0f;
        while (progress <= 1.0f)
        {
            castingBar.fillAmount = Mathf.Lerp(0, 1, progress);
            progress += rate * Time.deltaTime;

            timePassed += Time.deltaTime;
            castingText.text = (spells[spellIndex].GetCastTime - timePassed).ToString("F2");
            if (spells[spellIndex].GetCastTime - timePassed < 0f)
            {
                castingText.text = "0.00";
            }
            yield return null;
        }
    }
    private IEnumerator FadeBar()
    {
        float rate = 1.0f / 0.25f;
        float progress = 0.0f;
        while (progress <= 1.0f)
        {
            canvasGroup.alpha = Mathf.Lerp(0, 1, progress);
            progress += rate * Time.deltaTime;
            yield return null;
        }
    }    
}
