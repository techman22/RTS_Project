using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class build_Script : MonoBehaviour
{
    [SerializeField]
    private GameObject BuildButton;
    [SerializeField]
    private GameObject Building_BP;

    private void spawn_building()
    {
        Instantiate(Building_BP);
    }

    public void UnitSpawner(bool unitbuild)
    {
        BuildButton.SetActive(unitbuild);
    }
}
