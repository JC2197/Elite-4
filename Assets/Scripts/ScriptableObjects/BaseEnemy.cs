using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewBaseCharacter", menuName = "ScriptableObjects/BaseEnemy")]
public class BaseEnemy : ScriptableObject
{
    public int baseHealth = 100;
    public float physDamage = 20f;
    public float magicDamage = 20f;
    public float moveSpeed = 150f;
    public float armor = 10;
    public float magicResist = 10f;


    public float stamina = 10f;
    public float strength = 10f;
    public float dexterity = 10f;
    public float intelligence = 10f;
    public float will = 10f;
}
