using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelSpawner : MonoBehaviour
{
    [SerializeField] private ObjectCache barrelCache;
    [SerializeField] private Vector2 minMaxSpawnRate;
    [SerializeField] private Transform[] spawnPoints;

    [SerializeField] private Upgrade[] rewardItem;

    Cronometer _spawnRate;
    int _lastSpawnIndex;

    List<int> _spawnIndexes = new List<int>();

    void Start()
    {
        _spawnRate = new Cronometer();
        _spawnRate.time = Random.Range(minMaxSpawnRate.x, minMaxSpawnRate.y);

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            _spawnIndexes.Add(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(_spawnIndexes.Count <= 0)
        {
            for (int i = 0; i < spawnPoints.Length; i++)
            {
                _spawnIndexes.Add(i);
            }
        }

        if (_spawnRate.Ended)
        {
            var _spawnIndex = Random.Range(0, _spawnIndexes.Count);
            _lastSpawnIndex = _spawnIndexes[_spawnIndex];
            _spawnIndexes.RemoveAt(_spawnIndex);

            var sp = spawnPoints[_lastSpawnIndex];

            var newBarrel = barrelCache.GetObject<Barrel>();
            newBarrel.transform.position = sp.position;
            newBarrel.gameObject.SetActive(true);

            _spawnRate.time = Random.Range(minMaxSpawnRate.x, minMaxSpawnRate.y);
            _spawnRate.Tick();

            if (Random.value > 0.4)
            {
                var newReward = Instantiate(rewardItem[Random.Range(0, rewardItem.Length)], sp.position + Vector3.up, Quaternion.identity);
                newBarrel.upgradeReward = newReward;
            }
        }
    }
}
