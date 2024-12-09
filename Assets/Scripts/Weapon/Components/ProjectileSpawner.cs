using UnityEngine;

public class ProjectileSpawner : WeaponComponent<ProjectileSpawnerData, AttackProjectileSpawner>
{
    private Hammo hammo;
    private void SpawnProjectile()
    {
        GameObject projectile = Instantiate(data.AttackData.Projectile);


        projectile.GetComponent<IProjectile>().Spawn(
            transform.position,
            weapon.TargetPosition,
            data.Speed,
            data.Damage,
            data.DetectableLayers,
            weapon.User
        );
    }

    protected override void Start()
    {
        base.Start();

        // Try to get the Hammo component if it exists
        hammo = GetComponent<Hammo>();

        EventHandler.OnAttack += HandleAttack;
    }

    private void HandleAttack()
    {
        if (hammo != null)
        {
            // Check if there is enough ammo
            if (hammo.CanUseHammo(1)) // Assuming each projectile uses 1 ammo
            {
                hammo.UseHammo(1);
                SpawnProjectile();
            }
            else
            {
                // Handle case when there is not enough hammo
                Debug.Log("Not enough hammo to fire projectile.");
            }
        }
        else
        {
            // Fire projectile without checking for ammo
            SpawnProjectile();
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        EventHandler.OnAttack -= SpawnProjectile;
    }
}