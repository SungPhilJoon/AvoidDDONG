using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    #region Variables
    public float moveSpeed;

    [SerializeField] private Transform standardTarget;

    private Vector3 inputDirection;

    public bool isDead = false;

    #endregion Variables

    #region Unity Methods
    void Start()
    {
        inputDirection = Vector3.zero;
    }

    void Update()
    {
        transform.Translate(inputDirection * moveSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DDONG") || other.CompareTag("DeadZone"))
        {
            this.gameObject.SetActive(false);
        }
    }

    void OnDisable()
    {
        OnDead();
        GameManager.Instance.LoadData();
        GameManager.Instance.GameOver();
    }

    #endregion Unity Methods

    #region Helper Methods
    public void Move(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 inputVector = context.ReadValue<Vector2>();
            Vector3 standardForward = ((Vector3.forward * standardTarget.forward.z) + (Vector3.right * standardTarget.forward.x)).normalized;
            Vector3 standardRight = standardTarget.right.normalized;
            inputDirection = (standardForward * inputVector.y) + (standardRight * inputVector.x);
            inputDirection.Normalize();
        }
        else if (context.canceled)
        {
            inputDirection = Vector3.zero;
        }
    }

    public void OnDead()
    {
        isDead = true;
    }

    #endregion Helper Methods
}
