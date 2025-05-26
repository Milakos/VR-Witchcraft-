using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Game/Enemy Data")]
public class EnemyData : ScriptableObject
{
    public string enemyName;

    public enum Type
    {
        Enemy = 0, Player = 1
    }

    public Type enemyType;
    public float maxHealth;
    public float maxMana;
}
