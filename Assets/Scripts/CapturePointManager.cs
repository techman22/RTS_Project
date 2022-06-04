using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapturePointManager : MonoBehaviour
{
    private bool pCapture;
    private bool npcCapture;
    [SerializeField]
    private GameObject pCart;
    [SerializeField]
    private GameObject npcCart;
    [SerializeField]
    private GameObject spawnPos;
    private Transform Spawn;
    private GameObject clone;
    // Start is called before the first frame update
    void Start()
    {
        pCapture = false;
        npcCapture = false;
        Spawn = spawnPos.transform;
    }

    // Update is called once per frame
    void Update()
    {
        //checks for which player to spawn a cart
        if(pCapture && transform.childCount == 1)
        {
            clone = Instantiate(pCart, Spawn.position, Spawn.rotation);
            clone.transform.parent = transform;
        }
        if (npcCapture && transform.childCount == 1)
        {
            clone = Instantiate(npcCart, Spawn.position, Spawn.rotation);
            clone.transform.parent = transform;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PlayerUnit")
        {
            npcCapture = false;
            GetComponent<Renderer>().material.color = Color.blue;
            if(pCapture == false)
            {
                clone = Instantiate(pCart, Spawn.position, Spawn.rotation);
                clone.transform.parent = transform;
            }
            pCapture = true;

        }
        else if (other.gameObject.tag == "EnemyUnit")
        {
            pCapture = false;
            GetComponent<Renderer>().material.color = Color.red;
            if (npcCapture == false)
            {
                clone = Instantiate(npcCart, Spawn.position, Spawn.rotation);
                clone.transform.parent = transform;
            }
            npcCapture = true;
        }
    }
}