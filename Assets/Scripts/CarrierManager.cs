using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrierManager : MonoBehaviour
{
    private Transform endPos;
    private GameObject target;
    public float Speed;

    // Start is called before the first frame update
    void Start()
    {
        if(gameObject.tag == "PlayerCar")
        {
            target = GameObject.FindWithTag("Building");
            endPos = target.transform;
        }
        else if (gameObject.tag == "EnCart")
        {
            target = GameObject.Find("EnemyBuilding");
            endPos = target.transform;
        }
        if(target == null)
        {
            Debug.Log("No target");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookAtPosition = endPos.position;
        lookAtPosition.y = transform.position.y;
        transform.LookAt(lookAtPosition);
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPos.position, Speed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Building")
        {
            GlobalVariables.pResources += 100;
            Destroy(gameObject);
        }
        else if (other.tag == "EnemyBuilding")
        {
            Destroy(gameObject);
        }
    }
}