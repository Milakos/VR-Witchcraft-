using UnityEngine;

public class Locomotion : IMovementStrategy
{
    public void Move(EnemyBase enemy)
    {
        Debug.Log("Locomotion");
    }
}