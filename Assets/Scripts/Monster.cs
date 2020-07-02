﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Monster : MonoBehaviour
{
    public float currentBlood;
    public float hurtValue;
    public UnityAction<Monster> onDeadAction;
    public Transform endPoint;
    private NavMeshAgent navMeshAgent;

    public GameObject deadEffectPrefab;

    public float earnMoney;
    
    // Start is called before the first frame update
    void Start()
    {
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
        GameObject tempEffect= GameObject.Instantiate(deadEffectPrefab);
        tempEffect.transform.parent = null;
        tempEffect.transform.position = transform.position;
        tempEffect.transform.rotation = transform.rotation;

        onDeadAction?.Invoke(this);
        Destroy(gameObject);
        
    }
}
