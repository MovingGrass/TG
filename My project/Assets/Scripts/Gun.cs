using System.Collections;
using UnityEngine;
using TMPro;

public class Gun : MonoBehaviour
{
    public Animator anim;
    [SerializeField] private Transform Shotpoint;
    public GameObject Bullet;
    public TMP_Text hext;

    private bool can_shoot = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && can_shoot)
        {
            if (anim != null)
                anim.SetTrigger("shoot");
            StartCoroutine(DelayedFunctionExecution());
        }
    }

    IEnumerator DelayedFunctionExecution()
    {
        can_shoot = false;
        // Wait for 0.2 seconds (your current delay)
        yield return new WaitForSeconds(0.2f);

        if (Bullet != null && Shotpoint != null)
            Shoot();
        else
            Debug.LogError("Bullet or Shotpoint is not assigned!");

        // Wait for additional 4.8 seconds (total of 5 seconds)
        int A = 5;
        hext.text = A.ToString();

        while (A > 0)
        {
            // Check if the script is still attached to a GameObject
            if (this == null)
                yield break;

            yield return new WaitForSeconds(1f);
            A--;
            hext.text = A.ToString();
        }

        can_shoot = true;
    }

    void Shoot()
    {
        GameObject nembak = Instantiate(Bullet, Shotpoint.position, Shotpoint.rotation);
        Destroy(nembak, 3);
    }
}
