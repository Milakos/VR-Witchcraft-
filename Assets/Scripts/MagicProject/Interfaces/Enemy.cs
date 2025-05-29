using UnityEngine;

public interface IEnemy
{
    void TakeDamage(float damage, Transform transform);
    void Patrol();
    void Attack();
    void EnableVFX();
    void OnDeath();
}
