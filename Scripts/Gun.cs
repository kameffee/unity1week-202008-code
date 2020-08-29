using UnityEngine;

public class Gun : Weapon
{
    [SerializeField]
    private float interval = 0.2f;

    public override float Interval => interval;

    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private float shotPower = 40;

    [SerializeField]
    private ParticleSystem fireEffect;

    [SerializeField]
    private Transform muzzle;

    [SerializeField]
    private Transform aimTarget;

    public override bool Available()
    {
        return true;
    }

    public override void Fire()
    {
        var bullet = Instantiate(bulletPrefab, muzzle.position, Quaternion.identity);
        var vector = (aimTarget.position - muzzle.position).normalized;
        bullet.GetComponent<Rigidbody2D>().AddForce(vector * shotPower, ForceMode2D.Force);

        fireEffect.Play();
    }
}