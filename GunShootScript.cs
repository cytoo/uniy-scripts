using UnityEngine;

public class GunShootScript : MonoBehaviour
{
    public AudioSource GunSound;
    public Camera fpsCam;
    public float damge = 5f;
    public float range = 75f;
    public ParticleSystem muzzleFlash;
    public float fireRate = 15f;
    public float nTimeFire;
    void Update()
    {
        if(Input.GetButton("Fire1") && Time.time >= nTimeFire)
        {
            nTimeFire = Time.time + 1f/fireRate;
            shoot();
        }
        
    }
    void shoot()
    {
        muzzleFlash.Play();
        GunSound.Play();
        RaycastHit rayinfo;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out rayinfo, range))
        {
            TargetsScripts target = rayinfo.transform.GetComponent<TargetsScripts>();
            if(target != null)
            {
                target.takeDamage(damge);
            }
        }
    }
}
