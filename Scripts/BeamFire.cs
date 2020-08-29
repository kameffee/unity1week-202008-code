using UnityEngine;

public class BeamFire : Weapon
{
    [SerializeField]
    private Transform muzzle;

    [SerializeField]
    private GameObject beamPrefab;

    [SerializeField]
    private ParticleSystem fireEffect;

    [SerializeField]
    private Transform aimTarget;

    public override bool Available()
    {
        return true;
    }

    public override float Interval { get; } = 0.5f;

    public override void Fire()
    {
        var beam = Instantiate(beamPrefab, muzzle.position, muzzle.rotation);
        var vector = (aimTarget.position - muzzle.position).normalized;
        beam.GetComponent<Rigidbody2D>().velocity = vector * 30;

        fireEffect.Play();
    }
}