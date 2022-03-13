using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCamera : MonoBehaviour
{
    #region Variables
    public float height = 5f;
    public float distance = 10f;
    public float angle = 45f;
    public float lookAtHeight = 2f;
    public float smoothSpeed = 0.5f;

    [SerializeField] private Transform target;

    private Vector3 calcVector;

    #endregion Variables

    #region Properties
    public Transform Target => target;

    #endregion Properties

    #region Unity Methods
    void Start()
    {
        HandleCamera();
    }

    void LateUpdate()
    {
        HandleCamera();
    }

    #endregion Unity Methods

    #region Helper Methods
    public void HandleCamera()
    {
        if (target == null)
        {
            return;
        }

        Vector3 worldPosition = (Vector3.forward * -distance) + (Vector3.up * height);
        Vector3 rotatePosition = Quaternion.AngleAxis(angle, Vector3.up) * worldPosition;

        Vector3 flatTargetPosition = target.position;
        flatTargetPosition.y += lookAtHeight;

        Vector3 finalPosition = flatTargetPosition + rotatePosition;

        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, finalPosition, ref calcVector, smoothSpeed);
        transform.position = smoothedPosition;

        transform.LookAt(flatTargetPosition);
    }

    #endregion Helper Methods
}
