using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class DSpawner : MonoBehaviour
{
    #region Variables
    [SerializeField] private GameObject ddongPrefabs;

    private float spawnStartDelay;
    private float spawnInterval;

    public int poolingCount;
    public float spawnHeight;

    private List<GameObject> pooledObjects = new List<GameObject>();

    #endregion Variables

    #region Unity Methods
    void Start()
    {
        spawnStartDelay = 2f;
        spawnInterval = 0.1f;

        InitPooledList();
        StartCoroutine(Spawn());
    }

    #endregion Unity Methods

    #region Helper Methods
    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(spawnStartDelay);

        while (true)
        {
            GameObject pooledObject = GetPooledObject();
            pooledObject.transform.position = new Vector3(Random.Range(-10, 10), spawnHeight, Random.Range(-10, 10));
            pooledObject.SetActive(true);

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    #endregion Helper Methods
}
