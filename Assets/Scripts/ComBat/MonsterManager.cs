using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterManager : MonoBehaviour
{
    public static MonsterManager Inst { get; private set; }

    void Awake() => Inst = this;

    [SerializeField] MsSO msSO;
    [SerializeField] GameObject monsterPrefab;
    [SerializeField] Transform[] spawnPoints;

    [SerializeField] Text msHpText;
    [SerializeField] Text msGpText;
    [SerializeField] Text msAtkText;

    List<Monster> activeMonsters = new List<Monster>();

    private void Start()
    {
        if (!MapSceneController.isBoss)
            SpawnMonster(msSO.monsters[0], spawnPoints[0]);
        else
            SpawnMonster(msSO.monsters[1], spawnPoints[0]);
    }

    public void SpawnMonster(MonsterInfo monsterInfo, Transform spawnPoint)
    {
        var monsterObject = Instantiate(monsterPrefab, spawnPoint.position, Utils.QI);
        var monster = monsterObject.GetComponent<Monster>();
        monster.Setup(monsterInfo);
        activeMonsters.Add(monster);
        msHpText.text = "HP  " + monster.msHealth.ToString();
        msGpText.text = "GP  " + monster.msGuard.ToString();
        msAtkText.text = "Atk  " + monster.attackDamage.ToString();
    }

    public void RemoveMonster(Monster monster)
    {
        activeMonsters.Remove(monster);
        Destroy(monster.gameObject);
    }

    public List<Monster> GetActiveMonsters()
    {
        return new List<Monster>(activeMonsters);
    }
}
