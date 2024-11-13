using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoShooter : MonoBehaviour
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
            // Trouver un ennemi dans la portée du tir
            Collider[] enemiesInRange = Physics.OverlapSphere(shootPoint.position, seekRadius, enemyLayer);

            // Si des ennemis sont trouvés dans la portée
            if (enemiesInRange.Length > 0)
            {
                // On cible l'ennemi le plus proche
                Transform target = GetClosestEnemy(enemiesInRange);

                // Créer le projectile à la position de tir
                GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);

                // Calculer la direction vers l'ennemi et donner une vitesse au projectile
                Rigidbody rb = projectile.GetComponent<Rigidbody>();
                if (rb != null && target != null)
                {
                    // Calculer la direction vers l'ennemi
                    Vector3 directionToTarget = (target.position - shootPoint.position).normalized;

                    // Appliquer la direction et la vitesse au projectile
                    rb.velocity = directionToTarget * projectileSpeed;
                }
            }
        }
    }

    // Fonction pour obtenir l'ennemi le plus proche
    Transform GetClosestEnemy(Collider[] enemies)
    {
        Transform closestEnemy = null;
        float minDistance = Mathf.Infinity;

        foreach (Collider enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(shootPoint.position, enemy.transform.position);
            if (distanceToEnemy < minDistance)
            {
                minDistance = distanceToEnemy;
                closestEnemy = enemy.transform;
            }
        }

        return closestEnemy;
    }
}
