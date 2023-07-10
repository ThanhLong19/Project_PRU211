using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyPot : Item
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
        if (player == null) return;
        player.health.ChangeHealth((int)itemData.itemPotency, 1);
        base.OnActive(player);
    }
}