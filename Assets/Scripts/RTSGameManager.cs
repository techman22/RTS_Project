using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSGameManager : MonoBehaviour
{
    public static void UnitTakeDamage(UnitController attackingController, UnitController attackedController)
    {
        var damage = attackingController.unitStats.damage;

        attackedController.TakeDamage(attackedController, damage);
    }

    public static void BuildingTakeDamage(UnitController attackingController, BuildingInteraction attackedBuilding)
    {
        var damage = attackingController.unitStats.damage;

        attackedBuilding.TakeBuildingDamage(damage);
    }
}