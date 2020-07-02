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
    public Transform endPoint;//怪物有好几个终点

    
}
public class GameManager : MonoBehaviour
{
    public Transform bornPoint;
    public List<CreateMonsterInfo> createMonsterInfoList = new List<CreateMonsterInfo>();
    private int monsterDeadCount;//怪物死亡次数

    public GameObject plaecTowerPrefab;
    public List<GameObject> towerPrefab = new List<GameObject>();//存放预制体
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
                if((tempPlatform != null)&&!tempPlatform.hasTower&& plaecTowerPrefab!=null)
                {
                    float tempTowerCostMoney = plaecTowerPrefab.GetComponent<Tower>().costMoney;
                    if (currentMoney>= tempTowerCostMoney)
                    {
                        GameObject tempTowerGo = GameObject.Instantiate(plaecTowerPrefab);
                        tempTowerGo.transform.parent = null;
                        tempTowerGo.transform.position = tempPlatform.transform.position;
                        tempTowerGo.transform.rotation = tempPlatform.transform.rotation;
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
            GameObject tempMonsterGo = GameObject.Instantiate(item.monsterPrefab);
            tempMonsterGo.transform.parent = null;
            tempMonsterGo.transform.position = item.bornPoint.transform.position;
            tempMonsterGo.transform.rotation = item.bornPoint.transform.rotation;
            Monster tempMonster = tempMonsterGo.GetComponent<Monster>();
            tempMonster.endPoint = item.endPoint;
            tempMonster.onDeadAction += OnMonsterDead;
            yield return new WaitForSeconds(item.waitTime);
        }

    }
    public void OnMonsterDead(Monster pMonster)
    {
        currentMoney += pMonster.earnMoney;
        monsterDeadCount++;
        if(monsterDeadCount==createMonsterInfoList.Count)
        {
          
            Invoke("LoadSucceedScene", 3);
        }
    }

    public void OnTowerClickButtonDown()
    {
        plaecTowerPrefab = towerPrefab[0];
    }
    public void OnTower1ClickButtonDown()
    {
        plaecTowerPrefab = towerPrefab[1];
    }
    void LoadSucceedScene()
    {
        SceneManager.LoadScene("SucceedScene");
    }
}
