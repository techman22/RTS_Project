using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitController : MonoBehaviour
{

    private NavMeshAgent navAgent;
    private Transform curTarget;
    private float attackTimer;

    public UnitStats unitStats;
    private float health;

    private void Start()
    {
        health = unitStats.health;
        navAgent = GetComponent<NavMeshAgent>();
        attackTimer = unitStats.attackSpeed;
    }

    private void Update()
    {
        attackTimer += Time.deltaTime;
        if (curTarget != null)
        {
            navAgent.destination = curTarget.position;

            var distance = (transform.position - curTarget.position).magnitude;
            if(distance <= unitStats.attackRange)
            {
                Attack();
            }
        }
    }

    public void MoveUnit(Vector3 dest)
    {
        curTarget = null;
        navAgent.destination = dest;
    }

    public void SetSelected(bool isSelected)
    {
        transform.Find("Highlight").gameObject.SetActive(isSelected);
    }

    public void SetNewTarget(Transform enemy)
    {
        curTarget = enemy;
    }

    public void Attack()
    {
        if (attackTimer >= unitStats.attackSpeed)
        {
            RTSGameManager.UnitTakeDamage(this, curTarget.GetComponent<UnitController>());
            attackTimer = 0;
        }
    }

    public void TakeDamage(UnitController enemy, float damage)
    {
        StartCoroutine(Flasher(GetComponent<Renderer>().material.color));
        health -= 10;
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

    private void OnTriggerEnter(Collider other)
    {
        if(gameObject.tag == "PlayerUnit")
        {
            if (other.tag == "EnemyUnit")
            {
                SetNewTarget(other.transform);
            }
           
        }
        else if (gameObject.tag == "EnemyUnit")
        {
            if (other.tag == "PlayerUnit")
            {
                SetNewTarget(other.transform);
            }
        }
    }
}
