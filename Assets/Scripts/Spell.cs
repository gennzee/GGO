using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Spell
{
    [SerializeField]
    private string name;

    [SerializeField]
    private string playerAnimation;

    [SerializeField]
    private int damage;

    [SerializeField]
    private Sprite icon;

    [SerializeField]
    private float castTime;

    [SerializeField]
    private GameObject[] spellEffectPrefabs;

    [SerializeField]
    private GameObject hitEffectPrefab;

    public string GetName { get => name; }
    public string GetPlayerAnimation { get => playerAnimation; }
    public int GetDamage { get => damage; }
    public Sprite GetIcon { get => icon; }
    public float GetCastTime { get => castTime; }
    public GameObject[] GetSpellEffectPrefabs { get => spellEffectPrefabs; }
    public GameObject GetHitPrefab { get => hitEffectPrefab; }
}
