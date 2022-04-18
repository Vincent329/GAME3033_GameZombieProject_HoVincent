using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : HealthComponent
{
    [Header("Render Properties")]
    [SerializeField]
    private ParticleSystem explosionFX;
    [SerializeField]
    private MeshRenderer meshRender;

    [Header("Explosion Variables")]
    [SerializeField] private float explosionForce;
    [SerializeField] private float radius;
    [SerializeField] private float damageValue;
    [SerializeField] private Transform explosionOrigin;
    [SerializeField] private BoxCollider box;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        explosionFX = GetComponentInChildren<ParticleSystem>();
        meshRender = GetComponentInChildren<MeshRenderer>();
        box = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Destroy()
    {
        explosionFX.Play();
        meshRender.enabled = false;
        box.enabled = false;
        GetComponent<Rigidbody>().useGravity = false;
        Knockback();
        StartCoroutine(DestroyDelay());
    }

    IEnumerator DestroyDelay()
    {
        yield return new WaitForSeconds(explosionFX.main.duration);
        Destroy(gameObject);
    }

    private void Knockback()
    {
        Collider[] colliders = Physics.OverlapSphere(explosionOrigin.position, radius);
        foreach (Collider hits in colliders)
        {
            Rigidbody rb = hits.GetComponent<Rigidbody>();
            IDamageable damageable = hits.GetComponent<IDamageable>();
            damageable?.TakeDamage(damageValue);
            if (rb != null)
            {
                Debug.Log(rb.name);
                if (hits.GetComponent<ZombieComponent>() != null)
                {
                    hits.GetComponent<ZombieComponent>().StunEnemy();
                }
                rb.AddExplosionForce(explosionForce, explosionOrigin.position, radius);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(explosionOrigin.position, radius);

    }
}
