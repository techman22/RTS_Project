using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingInteraction : MonoBehaviour
{

    bool spawner;

    public void Update()
    {
        if (GameObject.Find("Manager").GetComponent<PlayerManager>().buildMenu)
        {
            spawner = true;
            GameObject.Find("Canvas").GetComponent<build_Script>().UnitSpawner(spawner);
        }
        else
        {
            spawner = false;
            GameObject.Find("Canvas").GetComponent<build_Script>().UnitSpawner(spawner);
        }
    }

    public void SetSelected(bool isSelected)
    {
        transform.Find("Highlight").gameObject.SetActive(isSelected);
    }
}