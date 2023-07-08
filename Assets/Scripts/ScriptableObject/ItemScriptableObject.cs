using UnityEngine;

namespace ScriptableObject
{
    [CreateAssetMenu(fileName = "Item", menuName = "Items/Item", order = 10)]
    public class ItemScriptableObject : UnityEngine.ScriptableObject
    {
        [SerializeField] public string itemID;
        [SerializeField] public float itemPotency;
        [SerializeField] public float itemDuration;
        [SerializeField] public float itemDropChance;
    }
}