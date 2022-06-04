using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Controller : MonoBehaviour
{
    [SerializeField]
    private GameObject BuildButton;
    [SerializeField]
    private GameObject Building_BP;
    public Text ResourceText;

    private void spawn_building()
    {
        Instantiate(Building_BP);
    }

    public void UnitSpawner(bool unitbuild)
    {
        BuildButton.SetActive(unitbuild);
    }

    public void ResourceUpdate()
    {
        ResourceText.text = GlobalVariables.pResources.ToString();
    }
}