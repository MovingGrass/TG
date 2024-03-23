using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBoss : MonoBehaviour
{
    // Start is called before the first frame update
    public void Win()
    {
    

    // Memanggil fungsi EndGame dari GameManager
        GameManager.Instance.Win();
    }
}
