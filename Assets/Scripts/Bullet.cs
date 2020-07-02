using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float hurtValue;
    public float speed;
    public GameObject deadEffectPrefab;
    public Rigidbody myRigidbody;
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position += transform.forward * speed * Time.deltaTime;
        myRigidbody.velocity = transform.forward * speed;
    }
    private void OnCollisionEnter(Collision collision)
    {
        Monster tempMonster = collision.collider.GetComponentInParent<Monster>();
        if (tempMonster != null)
        {
            tempMonster.Hurt(hurtValue);
            GameObject tempEffect = GameObject.Instantiate(deadEffectPrefab);
            tempEffect.transform.parent = null;
            tempEffect.transform.position = collision.contacts[0].point;//子弹碰到的第一个对象的点                                                                                     
            Destroy(gameObject);
        }
    }
}
