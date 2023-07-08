using UnityEngine;

namespace ScriptableObject
{
    [CreateAssetMenu(fileName = "Enemy", menuName = "Enemies/Enemy", order = 1)]
    public class EnemyScriptableObject : UnityEngine.ScriptableObject
    {
        [SerializeField] public int health;

        [SerializeField] public float speed;
        [SerializeField] public int pointWhenDeath;
        [SerializeField] public int damage;
    }
}