using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float hurtValue;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
    private void OnCollisionEnter(Collision collision)
    {
        Monster tempMonster = collision.collider.GetComponentInParent<Monster>();
        if (tempMonster != null)
        {
            tempMonster.Hurt(hurtValue);
            Destroy(gameObject);
        }
    }
}
