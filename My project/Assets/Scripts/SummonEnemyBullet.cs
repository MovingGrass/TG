using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonEnemyBullet : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject SteamBullet;

    [SerializeField] private Transform Shotpoint;
    public void ShootBullet()
    {
        GameObject shoot = Instantiate(SteamBullet, Shotpoint.position, Shotpoint.rotation);
        Destroy(shoot, 3);
    }
}
