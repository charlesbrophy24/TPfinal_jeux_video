using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoShooterVise : MonoBehaviour
{
    public GameObject projectilePrefab;      // Le prefab du projectile
    public Transform shootPoint;             // Le point d'où le projectile va être tiré (représente la tour)
    public float fireRate = 1f;              // Intervalle entre chaque tir (en secondes)
    public float projectileSpeed = 10f;      // La vitesse du projectile
    public float seekRadius = 50f;           // Rayon dans lequel la tour cherche un ennemi
    public LayerMask enemyLayer;             // Layer des ennemis (à définir dans l'éditeur)
    public float rotationSpeed = 5f;         // Vitesse de rotation de la tour vers l'ennemi
    public Color radiusColor = new Color(0, 0, 1, 0.3f); // Couleur du rayon (par défaut bleu avec transparence)

    public ParticleSystem shootParticles;    // Système de particules pour le tir
    public float particleDuration = 1f;      // Durée des particules en secondes

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
            // Jouer les particules si elles sont assignées
            if (shootParticles != null)
            {
                shootParticles.Play();
                StartCoroutine(StopParticlesAfterDelay());
            }

            // Trouver tous les ennemis dans le rayon de détection
            Collider[] enemiesInRange = Physics.OverlapSphere(shootPoint.position, seekRadius, enemyLayer);

            // Filtrer pour ne garder que les ennemis avec le tag "Enemy"
            List<GameObject> validEnemies = new List<GameObject>();
            foreach (Collider col in enemiesInRange)
            {
                if (col.CompareTag("Enemy"))
                {
                    validEnemies.Add(col.gameObject);
                }
            }

            // Si des ennemis valides sont dans le rayon
            if (validEnemies.Count > 0)
            {
                // Trouver l'ennemi le plus proche
                GameObject closestEnemy = GetClosestEnemy(validEnemies.ToArray());

                if (closestEnemy != null)
                {
                    // Calculer la direction vers l'ennemi
                    Vector3 directionToTarget = (closestEnemy.transform.position - shootPoint.position).normalized;

                    // Faire tourner la tour progressivement vers l'ennemi
                    Vector3 newDirection = Vector3.RotateTowards(shootPoint.forward, directionToTarget, rotationSpeed * Time.deltaTime, 0f);
                    shootPoint.rotation = Quaternion.LookRotation(newDirection);

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
    }

    IEnumerator StopParticlesAfterDelay()
    {
        // Attendre la durée spécifiée
        yield return new WaitForSeconds(particleDuration);
        if (shootParticles != null)
        {
            shootParticles.Stop();
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

    // Fonction pour dessiner la zone de recherche dans l'éditeur
    void OnDrawGizmos()
    {
        // Vérifier si la position du shootPoint est définie et visible
        if (shootPoint != null)
        {
            // Configurer la couleur du rayon (modifiable dans l'éditeur)
            Gizmos.color = radiusColor;

            // Dessiner une sphère pour représenter le rayon de recherche
            Gizmos.DrawSphere(shootPoint.position, seekRadius);
        }
    }
}
