using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Lorsque le projectile entre en collision avec un objet
    void OnCollisionEnter(Collision collision)
    {
        // Vérifier si l'objet avec lequel il entre en collision est un ennemi
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Détruire l'ennemi (ou désactiver si nécessaire)
            Destroy(collision.gameObject);  // Détruire l'ennemi

            // Détruire le projectile
            Destroy(gameObject);  // Détruire le projectile
        }
    }

    // Si le projectile utilise un collider avec l'option "Is Trigger" activée
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Détruire l'ennemi
            Destroy(other.gameObject);

            // Détruire le projectile
            Destroy(gameObject);
        }
    }
}