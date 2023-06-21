using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBaseCharacter", menuName = "ScriptableObjects/BaseCharacter")]
public class BaseCharacter : ScriptableObject
{
    public int baseHealth = 100;
    public float baseDamage = 20f;
    public float moveSpeed = 150f;
    public float armor = 10;
    public float magicResist = 10;
    public float stamina = 10f;
    public float strength = 10f;
    public float dexterity = 10f;
    public float intelligence = 10f;
    public float will = 10f;

}

