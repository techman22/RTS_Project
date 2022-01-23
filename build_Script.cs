using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class build_Script : MonoBehaviour
{
    //[SerializeField]
    //private GameObject building_blueprint;
    [SerializeField]
    private GameObject BuildButton;

    public void OnGui()
    {

    }

    public void Update()
    {
        if (GameObject.Find("Manager").GetComponent<PlayerManager>().buildMenu)
        {
            BuildButton.SetActive(true);
        }
    }

    //public void spawn_building()
    //{
    //    Instantiate(building_blueprint);
    //}
}
