using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public abstract bool Available();

    public abstract float Interval { get; }

    public abstract void Fire();
}