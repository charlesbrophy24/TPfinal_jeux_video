using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoShooterVise : MonoBehaviour
{
public GameObject projectilePrefab;      // Le prefab du projectile
public Transform shootPoint;             // Le point d'où le projectile va être tiré
public float fireRate = 1f;              // Intervalle entre chaque tir (en secondes)
public float projectileSpeed = 10f;      // La vitesse du projectile
public float seekRadius = 50f;           // Rayon dans lequel le projectile va chercher un ennemi
public LayerMask enemyLayer;             // Layer des ennemis (à définir dans l'éditeur)

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

void FireProjectile()
{
    if (projectilePrefab && shootPoint)
    {
        // Trouver tous les ennemis avec le tag "Enemy"
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // Trouver l'ennemi le plus proche
        GameObject closestEnemy = GetClosestEnemy(enemies);

        if (closestEnemy != null)
        {
            // Orienter la tour (shootPoint) vers l'ennemi le plus proche
            Vector3 directionToTarget = (closestEnemy.transform.position - shootPoint.position).normalized;
            shootPoint.rotation = Quaternion.LookRotation(directionToTarget);

            // Créer le projectile à la position de tir
            GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);

            // Calculer la direction vers l'ennemi et donner une vitesse au projectile
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Appliquer la direction et la vitesse au projectile
                rb.velocity = directionToTarget * projectileSpeed;
            }
        }
    }
}

// Fonction pour obtenir l'ennemi le plus proche
GameObject GetClosestEnemy(GameObject[] enemies)
{
    GameObject closestEnemy = null;
    float minDistance = Mathf.Infinity;

    foreach (GameObject enemy in enemies)
    {
        // Calculer la distance à l'ennemi
        float distanceToEnemy = Vector3.Distance(shootPoint.position, enemy.transform.position);

        // Vérifier si cet ennemi est le plus proche
        if (distanceToEnemy < minDistance)
        {
            minDistance = distanceToEnemy;
            closestEnemy = enemy;
        }
    }

    return closestEnemy;
}


}