using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class CreateMonsterInfo
{
    public GameObject monsterPrefab;
    public float waitTime;
}
public class GameManager : MonoBehaviour
{
    public Transform bornPoint;
    public List<CreateMonsterInfo> createMonsterInfoList = new List<CreateMonsterInfo>();
    private int monsterDeadCount;//怪物死亡次数
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CreateMonster());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator CreateMonster()
    {
        foreach(var item in createMonsterInfoList)
        {
            GameObject tempMonster = GameObject.Instantiate(item.monsterPrefab);
            tempMonster.transform.parent = null;
            tempMonster.transform.position = bornPoint.transform.position;
            tempMonster.transform.rotation = bornPoint.transform.rotation;
            tempMonster.GetComponent<Monster>().onDeadAction += OnMonsterDead;
            yield return new WaitForSeconds(item.waitTime);
        }

    }
    public void OnMonsterDead()
    {
        monsterDeadCount++;
        if(monsterDeadCount==createMonsterInfoList.Count)
        {
            SceneManager.LoadScene("SucceedScene");
        }
    }
}
