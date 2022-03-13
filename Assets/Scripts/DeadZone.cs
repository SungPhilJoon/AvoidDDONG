using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    #region Unity Methods
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("DDONG"))
        {
            collision.gameObject.SetActive(false);
        }
    }

    #endregion Unity Methods
}
