using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoShooter : MonoBehaviour
{
    public GameObject projectilePrefab;  // Le prefab du projectile
    public Transform shootPoint;         // Le point d'où le projectile va être tiré
    public float fireRate = 1f;          // Intervalle entre chaque tir (en secondes)
    public float projectileSpeed = 10f;  // La vitesse du projectile

    private float timeSinceLastShot = 0f;

    void Update()
    {
        // On augmente le temps écoulé depuis le dernier tir
        timeSinceLastShot += Time.deltaTime;

        // Si le temps écoulé est supérieur à l'intervalle de tir, on tire un projectile
        if (timeSinceLastShot >= fireRate)
        {
            FireProjectile();
            timeSinceLastShot = 0f;  // Réinitialiser le timer
        }
    }

    // Fonction qui tire un projectile
    void FireProjectile()
    {
        if (projectilePrefab && shootPoint)
        {
            // Crée un nouveau projectile à la position de tir
            GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);

            // Donne au projectile une vitesse de déplacement
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = shootPoint.forward * projectileSpeed;
            }
        }
    }
}
