using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreMultiplier : Item
{
    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            base.OnTriggerEnter2D(other);
        }
    }

    public override void OnActive(PlayerController player)
    {
        GameController.Instance.pointMultiplier *= itemData.itemPotency;
        base.OnActive(player);
    }

    public override void OnDeactivate(PlayerController player)
    {
        GameController.Instance.pointMultiplier *= 1 / itemData.itemPotency;
        base.OnDeactivate(player);
    }
}