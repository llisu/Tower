using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float attackInterval;//子弹发射的时间间隔
    public float attackDistance;
    private float preAttackTime;
    private Transform firePoint;

    public float costMoney;                                                                                                                      
    // Start is called before the first frame update
    void Start()
    {
        firePoint = transform.Find("Point").Find("FirePoint");
        preAttackTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Monster tempMonster = GameObject.FindObjectOfType<Monster>();
        if(tempMonster!=null)
        {
            if(Vector3.Distance(transform.position,tempMonster.transform.position)<=attackDistance)
            {
                Aim(tempMonster.transform);
                if((Time.time-preAttackTime)> attackInterval)
                {
                    Shoot();
                    preAttackTime = Time.time;
                }
            }
        }
    }
    public void Aim(Transform pTarget)
    {
        Quaternion tempQuaternion = Quaternion.LookRotation(pTarget.position - transform.position);
        transform.eulerAngles=new Vector3(0, tempQuaternion.eulerAngles.y, 0);
    }
    public void  Shoot()
    {
        GameObject tempBullet = GameObject.Instantiate(bulletPrefab);
        tempBullet.transform.parent = null;
        tempBullet.transform.rotation = firePoint.rotation;
        tempBullet.transform.position = firePoint.position;
    }
}
