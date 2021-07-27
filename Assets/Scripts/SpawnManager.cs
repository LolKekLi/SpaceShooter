using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] 
    private GameObject _enamy;
    private GameObject _enamyContainer;
    [SerializeField]
    private GameObject[] _powerUps;
   

    private GameObject _powerUpContainer;

    [SerializeField]
    private float _whaitTime = 1f;

    private bool _stopSpawning = false;
    
    private void OnEnable()
    {
        PlayerScripts.die += PlayerScripts_Die;
    }

    private void OnDisable()
    {
        PlayerScripts.die -= PlayerScripts_Die;
    }

    
    void Start()
    {
        _enamyContainer = new GameObject("EnamyFolder");
        _enamyContainer.transform.parent = this.transform;
        _powerUpContainer = new GameObject("PowerUpFolder");
        _powerUpContainer.transform.parent = this.transform;
        StartCoroutine(SpawnEnamy(_enamyContainer));
        StartCoroutine(SpawnPowerUp(_powerUpContainer));
    }
    private void PlayerScripts_Die()
    {
        _stopSpawning = true;
    }
    private IEnumerator SpawnPowerUp(GameObject powerUpContainer)
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(3f, 7f));
            var spawnPos = new Vector3(Random.Range(-4f, 6f), 8, 0);
            var range = Random.Range(0,_powerUps.Length);
            Instantiate(_powerUps[range], spawnPos, Quaternion.identity, powerUpContainer.transform);
        }
    }

    private IEnumerator SpawnEnamy(GameObject container)
    {

        while (!_stopSpawning)
        {
            var spawnPos = new Vector3(Random.Range(-4f, 6f), 8, 0);
            Instantiate(_enamy, spawnPos,Quaternion.identity,container.transform);
            yield return new WaitForSeconds(_whaitTime);
        }
    }
}
