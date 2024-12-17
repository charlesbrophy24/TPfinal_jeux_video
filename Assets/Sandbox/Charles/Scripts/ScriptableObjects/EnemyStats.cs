using UnityEngine;

// Define a ScriptableObject to hold enemy stats
[CreateAssetMenu(fileName = "New Enemy Stats", menuName = "Game/Enemy Stats", order = 1)]
public class EnemyStats : ScriptableObject
{
    [Header("General Stats")]
    public string enemyName;
    public int health;

    [Header("Attack Stats")]
    public float damage;
    public float attackSpeed;
    public float attackRange;

    [Header("Movement Stats")]
    public float moveSpeed;

    [Header("Other Stats")]
    public int experienceValue; // XP given to the player upon defeat
    public GameObject enemyPrefab; // Reference to the prefab
}