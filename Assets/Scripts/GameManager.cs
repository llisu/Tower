using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class CreateMonsterInfo
{
    public GameObject monsterPrefab;
    public float waitTime;
    public Transform bornPoint;//怪物有好几个出生点

    
}
public class GameManager : MonoBehaviour
{
    public Transform bornPoint;
    public List<CreateMonsterInfo> createMonsterInfoList = new List<CreateMonsterInfo>();
    private int monsterDeadCount;//怪物死亡次数

    public GameObject towerPrefab;
    public LayerMask layerMask;

    public float currentMoney;
    public Text moneyText;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CreateMonster());
    }

    // Updayate is called once per frame
    void Update()
    {
        moneyText.text = currentMoney.ToString();
        //建塔
        if (Input.GetMouseButtonDown(0))
        {
            Ray tempRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit tempRaycastHit;
            if(Physics.Raycast(tempRay,out tempRaycastHit,100000, layerMask))
            {
                Platform tempPlatform = tempRaycastHit.collider.GetComponentInParent<Platform>();
                if((tempPlatform != null)&&!tempPlatform.hasTower)
                {
                    float tempTowerCostMoney = towerPrefab.GetComponent<Tower>().costMoney;
                    if (currentMoney>= tempTowerCostMoney)
                    {
                        GameObject tempTower = GameObject.Instantiate(towerPrefab);
                        tempTower.transform.parent = null;
                        tempTower.transform.position = tempPlatform.transform.position;
                        tempTower.transform.rotation = tempPlatform.transform.rotation;
                        tempPlatform.hasTower = true;
                        currentMoney -= tempTowerCostMoney;
                    }
                    
                }
                
            }
        }
        
    }
    public IEnumerator CreateMonster()
    {
        foreach(var item in createMonsterInfoList)
        {
            GameObject tempMonster = GameObject.Instantiate(item.monsterPrefab);
            tempMonster.transform.parent = null;
            tempMonster.transform.position = item.bornPoint.transform.position;
            tempMonster.transform.rotation = item.bornPoint.transform.rotation;
            tempMonster.GetComponent<Monster>().onDeadAction += OnMonsterDead;
            yield return new WaitForSeconds(item.waitTime);
        }

    }
    public void OnMonsterDead(Monster pMonster)
    {
        currentMoney += pMonster.earnMoney;
        monsterDeadCount++;
        if(monsterDeadCount==createMonsterInfoList.Count)
        {
            SceneManager.LoadScene("SucceedScene");
        }
    }
}
