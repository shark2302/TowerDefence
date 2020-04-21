using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;
namespace Objects
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField]
        private SpawnerConfig _spawnerConfig;

        private List<GameObject> _enemyList;
        private Coroutine _spawnRoutine;
        private int _counter = 0;
        private int _destroyedObjects = 0;
        private void OnEnable()
        {
            _spawnRoutine = StartCoroutine(SpawnRoutine);
            _enemyList = new List<GameObject>();
        }

        private void OnDisable()
        {
            if (_spawnRoutine != null)
                StopCoroutine(_spawnRoutine);
            _spawnRoutine = null;
        }

        private IEnumerator SpawnRoutine
        {
            get
            {
                var element = _spawnerConfig.GetElement(_counter);
                if (element == null)
                {
                    enabled = false;
                    yield break;
                }

                yield return new WaitForSeconds(element.Delay);
                while (_counter <=  _spawnerConfig.SpawnData.Length - 1)
                {
                    var posZ = Random.Range(transform.position.z - 40, transform.position.z + 40)*.5f;
                    var obj = Instantiate(element.SpawnedPrefab, new Vector3(transform.position.x, transform.position.y, posZ), transform.rotation);
                    _enemyList.Add(obj);
                    element = _spawnerConfig.GetElement(++_counter);
                    if (element == null)
                    {
                        StartCoroutine(CountDestroyedObjects());
                        yield break;
                    }
                    yield return new WaitForSeconds(element.Delay); 
                }
               
            }
        }

        private IEnumerator CountDestroyedObjects()
        {
            Debug.Log("Courotine started");
            yield return new WaitForSeconds(2);
            while (_destroyedObjects < _counter)
            {
                foreach (var enemy in _enemyList)
                {
                    if (enemy == null)
                        _destroyedObjects += 1;
                }
                yield return new WaitForSeconds(2);
                
            }
        }

        public void DestroyAllSpawnedObjects()
        {
            foreach (var enemy in _enemyList)
            {
                Destroy(enemy);
            }
        }

        public bool AllSpawnedObjectsDestroyed()
        {
            return _destroyedObjects == _spawnerConfig.SpawnData.Length;
        }
        
        public List<GameObject> GetEnemyList()
        {
            return _enemyList;
        }
    }
    
    
}