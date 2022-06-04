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
    public Transform target;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
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
            Vector3 LookAtPos = curTarget.position;
            LookAtPos.y = transform.position.y;
            transform.LookAt(LookAtPos);
            var distance = (transform.position - curTarget.position).magnitude;
            if(distance <= unitStats.attackRange)
            {
                anim.SetBool("Walking", false);
                Attack();
            }
            else
            {
                anim.SetBool("Walking", true);
            }
        }
        if (transform.position == navAgent.destination)
        {
            anim.SetBool("Walking", false);
        }
    }

    public void MoveUnit(Vector3 dest)
    {
        anim.SetBool("Walking", true);
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
        anim.SetBool("Attacking", true);
        if (attackTimer >= unitStats.attackSpeed)
        {
            if (curTarget.tag == "PlayerUnit" || curTarget.tag == "EnemyUnit")
            {
                RTSGameManager.UnitTakeDamage(this, curTarget.GetComponent<UnitController>());
                if(curTarget.GetComponent<UnitController>().health <= 0)
                {
                    curTarget = null;
                    anim.SetBool("Attacking", false);
                }
            }
            else if (curTarget.tag == "Building" || curTarget.tag == "EnemyBuilding")
            {
                RTSGameManager.BuildingTakeDamage(this, curTarget.GetComponent<BuildingInteraction>());
            }
            attackTimer = 0;
        }
    }

    public void TakeDamage(UnitController enemy, float damage)
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

    private void OnTriggerEnter(Collider other)
    {
        if(gameObject.tag == "PlayerUnit")
        {
            if (other.tag == "EnemyUnit" && curTarget == null)
            {
                SetNewTarget(other.transform);
            }
           
        }
        else if (gameObject.tag == "EnemyUnit")
        {
            if (other.tag == "PlayerUnit" && curTarget == null)
            {
                SetNewTarget(other.transform);
            }
        }
    }
}