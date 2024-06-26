using UnityEngine;

public class Scythe : Projectile, IUpdateWeaponStats
{
    private float rotationSpeed = 575f;

    public void UpdateWeaponStats(float newDamage)
    {
        damage = newDamage;
    }

    protected override void CustomReset()
    {
        currentLifetime = lifetime;
    }

    protected override void Movement()
    {
        //DÉPLACEMENT
        transform.position += moveSpeed * Time.deltaTime * movement;

        //ROTATION
        transform.Rotate(rotationSpeed * Time.deltaTime * Vector3.forward);
    }
}
