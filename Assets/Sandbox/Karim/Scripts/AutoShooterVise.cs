using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoShooterVise : MonoBehaviour
{
    public GameObject projectilePrefab;      // Le prefab du projectile
    public Transform shootPoint;             // Le point d'où le projectile va être tiré
    public float fireRate = 1f;              // Intervalle entre chaque tir
    public float projectileSpeed = 10f;      // Vitesse du projectile
    public float seekRadius = 50f;           // Rayon de recherche des ennemis
    public LayerMask enemyLayer;             // Layer des ennemis
    public GameObject muzzleFlashPrefab;     // Prefab du Muzzle Flash (effet de particules)
    public float muzzleFlashDuration = 0.2f; // Durée du Muzzle Flash
    public Color radiusColor = new Color(0, 0, 1, 0.3f); // Couleur du rayon visible dans l'éditeur

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
            // Trouver tous les ennemis dans le rayon de détection avec le Layer spécifié
            Collider[] enemiesInRange = Physics.OverlapSphere(shootPoint.position, seekRadius, enemyLayer);

            // Filtrer pour ne garder que ceux avec le tag "Enemy"
            List<GameObject> validEnemies = new List<GameObject>();
            foreach (Collider col in enemiesInRange)
            {
                if (col.CompareTag("Enemy"))  // Vérifie que l'ennemi a le tag "Enemy"
                {
                    validEnemies.Add(col.gameObject);
                }
            }

            // Si des ennemis valides existent
            if (validEnemies.Count > 0)
            {
                // Trouver l'ennemi le plus proche
                GameObject closestEnemy = GetClosestEnemy(validEnemies.ToArray());

                if (closestEnemy != null)
                {
                    // Calculer la direction vers l'ennemi
                    Vector3 directionToTarget = (closestEnemy.transform.position - shootPoint.position).normalized;

                    // Tourner instantanément vers l'ennemi
                    shootPoint.rotation = Quaternion.LookRotation(directionToTarget);

                    // Jouer l'effet Muzzle Flash si disponible
                    if (muzzleFlashPrefab != null)
                    {
                        GameObject muzzleFlash = Instantiate(muzzleFlashPrefab, shootPoint.position, shootPoint.rotation);
                        Destroy(muzzleFlash, muzzleFlashDuration); // Détruire après la durée spécifiée
                    }

                    // Créer le projectile à la position de tir
                    GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);

                    // Donner une vitesse au projectile dans la direction de l'ennemi
                    Rigidbody rb = projectile.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.velocity = shootPoint.forward * projectileSpeed;
                    }
                }
            }
        }
    }

    // Fonction pour trouver l'ennemi le plus proche
    GameObject GetClosestEnemy(GameObject[] enemies)
    {
        GameObject closestEnemy = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(shootPoint.position, enemy.transform.position);
            if (distanceToEnemy < minDistance)
            {
                minDistance = distanceToEnemy;
                closestEnemy = enemy;
            }
        }

        return closestEnemy;
    }

    // Fonction pour dessiner la zone de recherche dans l'éditeur
    void OnDrawGizmos()
    {
        if (shootPoint != null)
        {
            Gizmos.color = radiusColor;
            Gizmos.DrawSphere(shootPoint.position, seekRadius);
        }
    }
}
