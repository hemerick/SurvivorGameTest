using UnityEngine;

public class EnemyProjectile : Projectile, IUpdateProjectileStats
{
    public void SetProjectileStats(float newDamage, float newLifetime)
    {
        damage= newDamage;
        lifetime= newLifetime;
    }

    protected override void CustomReset()
    {
        currentLifetime = lifetime;
    }

    protected override void Movement()
    {
        transform.position += moveSpeed * Time.deltaTime * movement;
    }
}
