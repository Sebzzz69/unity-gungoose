using UnityEngine;

[CreateAssetMenu]
public class WeaponData : ScriptableObject
{
    public GameObject bulletPrefab;
    public int bulletSpeed;
    public int bloomRange;
    public int ammunitionAmount;
    public int reloadTimeInSeconds;
    public int damageAmount;

    public bool isShotgun;
}
