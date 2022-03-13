using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class DSpawner : MonoBehaviour
{
    #region Helper Methods
    public void InitPooledList()
    {
        for (int i = 0; i < poolingCount; i++)
        {
            GameObject newDDONG = Instantiate(ddongPrefabs, transform);
            newDDONG.SetActive(false);
            pooledObjects.Add(newDDONG);
        }
    }

    public GameObject GetPooledObject()
    {
        if (pooledObjects.Count > 0)
        {
            for (int i = 0; i < pooledObjects.Count; i++)
            {
                if (!pooledObjects[i].activeSelf)
                {
                    return pooledObjects[i];
                }
            }

            int beforeCreateCount = pooledObjects.Count;
            InitPooledList();
            return pooledObjects[beforeCreateCount];
        }
        else
        {
            return null;
        }
    }

    #endregion Helper Methods
}
