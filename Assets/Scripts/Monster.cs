using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    public float currentBlood;
    public float hurtValue;
    public UnityAction<Monster> onDeadAction;
    public Transform endPoint;
    private NavMeshAgent navMeshAgent;
    public Slider slider;
    private float totalBlood;
    public GameObject deadEffectPrefab;

    public float earnMoney;
    
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.SetDestination(endPoint.position);//设置目标点
        totalBlood = currentBlood;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = currentBlood / totalBlood;
        slider.transform.LookAt(Camera.main.transform);//3D血条始终看向摄像机，防止血条跟着怪物旋转
    }
    private void OnCollisionEnter(Collision collision)
    {
        Home tempHome = collision.collider.GetComponentInParent<Home>();
        if(tempHome!=null)
        {
            tempHome.Hurt(hurtValue);
            Hurt(currentBlood);
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
