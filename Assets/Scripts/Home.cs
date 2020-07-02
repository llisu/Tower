using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Home : MonoBehaviour
{
    public float currentBlood;
    public GameObject deadEffectPrefab;
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
        
        GameObject tempEffect = GameObject.Instantiate(deadEffectPrefab);
        tempEffect.transform.parent = null;
        tempEffect.transform.position = transform.position;
        tempEffect.transform.rotation = transform.rotation;

        transform.GetChild(0).gameObject.SetActive(false);
        Invoke("LoadFailScene", 3);
    }
    void LoadFailScene()
    {
        SceneManager.LoadScene("FailScene");                                                                                                                                         
    }
}
