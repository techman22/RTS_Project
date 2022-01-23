using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSGameManager : MonoBehaviour
{
    public static void UnitTakeDamage(UnitController attackingController, UnitController attackedController)
    {
        var damage = attackedController.unitStats.damage;

        attackedController.TakeDamage(attackedController, damage);
    }
}
