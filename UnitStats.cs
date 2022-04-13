using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//General Unit Stats

[CreateAssetMenu(menuName = "AI/UnitStats")]
public class UnitStats : ScriptableObject
{
    public float attackRange;
    public float attackSpeed;
    public float damage;
    public float health;
}
