using UnityEngine;

public class TargetsScripts : MonoBehaviour
{
    public float health = 50f;
    public  AudioSource ExplodeSound;
    public ParticleSystem ExplodeEffect;
    public void explode()
    {
        ExplodeSound.Play();
        ExplodeEffect.Play();
    }
    public void takeDamage(float amount)
    {
        health -= amount;
        if(health <= 0)
        {
            die();
        }
    }
    void die()
    {
        explode();
        Destroy(gameObject);
    }
}
