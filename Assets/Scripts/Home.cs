using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Home : MonoBehaviour
{
    public float currentBlood;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Hurt(float pHurtValue)
    {
        currentBlood -= pHurtValue;
        if(currentBlood <= 0)
        {
            currentBlood = 0;
            OnDead();
        }
    }
    void OnDead()
    {
        Destroy(gameObject);
        SceneManager.LoadScene("FailScene");
    }
}
