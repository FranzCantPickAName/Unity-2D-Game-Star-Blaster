using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifetime = 5f;
    [SerializeField] float fireRate = 0.2f;

    public bool isFiring;
    Coroutine FireCorountine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
            if (projectileRB != null)
            {
                projectileRB.linearVelocityY = projectileSpeed;

                Destroy(projectile, projectileLifetime);
            }

            yield return new WaitForSeconds(fireRate);
        }
    }
}
