using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingInteraction : MonoBehaviour
{

    bool spawner;
    private float health = 100;

    public void Update()
    {
        if (GameObject.Find("Manager").GetComponent<PlayerManager>().buildMenu)
        {
            spawner = true;
            GameObject.Find("Canvas").GetComponent<UI_Controller>().UnitSpawner(spawner);
        }
        else
        {
            spawner = false;
            GameObject.Find("Canvas").GetComponent<UI_Controller>().UnitSpawner(spawner);
        }
    }

    public void SetSelected(bool isSelected)
    {
        transform.Find("Highlight").gameObject.SetActive(isSelected);
    }

    public void TakeBuildingDamage(float damage)
    {
        StartCoroutine(Flasher(GetComponent<Renderer>().material.color));
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator Flasher(Color defaultColor)
    {
        for (int i = 0; i < 2; i++)
        {
            GetComponent<Renderer>().material.color = Color.gray;
            yield return new WaitForSeconds(.05f);
            GetComponent<Renderer>().material.color = defaultColor;
            yield return new WaitForSeconds(.05f);
        }
    }
}