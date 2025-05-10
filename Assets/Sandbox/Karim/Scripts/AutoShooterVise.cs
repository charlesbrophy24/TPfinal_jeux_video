using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoShooterVise : MonoBehaviour
{
    public GameObject projectilePrefab;      // Le prefab du projectile
    public Transform shootPoint;             // Le point d'où le projectile va être tiré
    public Transform turretTransform;        // La partie de la tour qui va pivoter pour viser l'ennemi
    public float fireRate = 1f;              // Intervalle entre chaque tir
    public float projectileSpeed = 10f;      // Vitesse du projectile
    public float seekRadius = 50f;           // Rayon de recherche des ennemis
    public LayerMask enemyLayer;             // Layer des ennemis
    public GameObject muzzleFlashPrefab;     // Prefab du Muzzle Flash (effet de particules)
    public float muzzleFlashDuration = 0.2f; // Durée du Muzzle Flash
    public float rotationSpeed = 5f;         // Vitesse de rotation de la tour
    public Color radiusColor = new Color(0, 0, 1, 0.3f); // Couleur du rayon visible dans l'éditeur

    private float timeSinceLastShot = 0f;

    void Update()
    {
        // On augmente le temps écoulé depuis le dernier tir
        timeSinceLastShot += Time.deltaTime;

        // Trouver l'ennemi le plus proche
        GameObject targetEnemy = GetClosestEnemyInRange();

        if (targetEnemy != null)
        {
            RotateTurretTowards(targetEnemy); // Tourner la tour vers l'ennemi

            // Tirer si le timer le permet
            if (timeSinceLastShot >= fireRate)
            {
                FireProjectile(targetEnemy);
                timeSinceLastShot = 0f;  // Réinitialiser le timer
            }
        }
    }

    void FireProjectile(GameObject targetEnemy)
    {
        if (projectilePrefab && shootPoint)
        {
            // Calculer la direction vers l'ennemi
            Vector3 directionToTarget = (targetEnemy.transform.position - shootPoint.position).normalized;

            // Faire jouer l'effet Muzzle Flash si présent
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
                rb.velocity = directionToTarget * projectileSpeed;
            }
        }
    }

    void RotateTurretTowards(GameObject targetEnemy)
    {
        // Calculer la direction vers l'ennemi
        Vector3 directionToTarget = (targetEnemy.transform.position - turretTransform.position).normalized;

        // Calculer la rotation souhaitée
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToTarget.x, 0, directionToTarget.z));

        // Appliquer une rotation douce à la tour
        turretTransform.rotation = Quaternion.Slerp(turretTransform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
    }

    GameObject GetClosestEnemyInRange()
    {
        // Trouver tous les ennemis dans le rayon de détection
        Collider[] enemiesInRange = Physics.OverlapSphere(shootPoint.position, seekRadius, enemyLayer);

        List<GameObject> validEnemies = new List<GameObject>();
        foreach (Collider col in enemiesInRange)
        {
            if (col.CompareTag("Enemy")) // Vérifier le tag "Enemy"
            {
                validEnemies.Add(col.gameObject);
            }
        }

        // Retourner l'ennemi le plus proche
        return GetClosestEnemy(validEnemies.ToArray());
    }

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

    void OnDrawGizmos()
    {
        if (shootPoint != null)
        {
            Gizmos.color = radiusColor;
            Gizmos.DrawSphere(shootPoint.position, seekRadius);
        }
    }
}
