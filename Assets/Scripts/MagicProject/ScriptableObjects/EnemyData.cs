using FullOpaqueVFX;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Game/Enemy Data")]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    
    public enum Type
    {
        Enemy = 0, Player = 1
    }

    public enum Occupation
    {
        Magician = 0, Doctor = 1, Orc = 2, Goblin = 3
    }

    public Type enemyType;
    public Occupation fighterType;
    
    public float maxHealth;

    public SpellData spellData;
    public float maxMana;
    public float manaReduceRate;

    public bool play = true;
}
