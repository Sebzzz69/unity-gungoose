using UnityEngine;

[CreateAssetMenu]
public class WeaponData : ScriptableObject
{
    public GameObject bulletPrefab;
    public int BulletSpeed;
    public int BloomRange;
    public int AmmunitionAmount;
    public int MaxWeaponClips;
    public int ReloadTimeInSeconds;
    public int DamageAmount;

    public bool IsShotgun;
}
