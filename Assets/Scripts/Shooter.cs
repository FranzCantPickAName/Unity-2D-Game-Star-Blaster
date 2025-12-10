using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("Base Variables")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifetime = 5f;
    [SerializeField] float baseFireRate = 0.2f;

    [Header("AI Variables")]
    [SerializeField] bool useAI;
    [SerializeField] float minimumFireRate = 0.2f;
    [SerializeField] float fireRateVariance = 0f;

    [HideInInspector] public bool isFiring;
    Coroutine FireCorountine;
    AudioManager audioManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioManager = FindFirstObjectByType<AudioManager>();

        if (useAI)
        {
            isFiring = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
    }

    void Fire()
    {
        if (isFiring && FireCorountine == null)
        {
            FireCorountine = StartCoroutine(FireContinously());
        }
        else if (!isFiring && FireCorountine != null)
        {
            StopCoroutine(FireCorountine);
            FireCorountine = null;
        }
    }

    IEnumerator FireContinously()
    {
        while (true)
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            Rigidbody2D projectileRB = projectile.GetComponent<Rigidbody2D>();
            if (useAI)
            {
                projectileRB.linearVelocityY = -1 * projectileSpeed;

                Destroy(projectile, projectileLifetime);
            }
            else
            {
                projectileRB.linearVelocityY = projectileSpeed;

                audioManager.PlayShootingSFX();

                Destroy(projectile, projectileLifetime);
            }

                float waitTime = Random.Range(baseFireRate - fireRateVariance, baseFireRate + fireRateVariance);
            waitTime = Mathf.Clamp(waitTime, minimumFireRate, float.MaxValue);


            yield return new WaitForSeconds(waitTime);
        }
    }
}
