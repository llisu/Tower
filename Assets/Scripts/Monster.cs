using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Monster : MonoBehaviour
{
    public float currentBlood;
    public float hurtValue;
    public UnityAction onDeadAction;
    private Transform endPoint;
    private NavMeshAgent navMeshAgent;
    
    // Start is called before the first frame update
    void Start()
    {
        endPoint = GameObject.Find("EndPoint").transform;

        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.SetDestination(endPoint.position);//设置目标点
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        Home tempHome = collision.collider.GetComponentInParent<Home>();
        if(tempHome!=null)
        {
            tempHome.Hurt(hurtValue);
            Destroy(gameObject);
        }
    }
    public void Hurt(float pHurtValue)
    {
        currentBlood -= pHurtValue;
        if (currentBlood <= 0)
        {
            currentBlood = 0;
            OnDead();
        }
    }
    void OnDead()
    {
        onDeadAction?.Invoke();
        Destroy(gameObject);
        
    }
}
