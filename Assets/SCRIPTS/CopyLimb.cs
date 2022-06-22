using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyLimb : MonoBehaviour
{
    [SerializeField] private Transform targetLimb;
    [SerializeField] private ConfigurableJoint m_ConfigurableJoint;

    Quaternion targetInitialRotation;
    void Start()
    {
        m_ConfigurableJoint = GetComponent<ConfigurableJoint>();
        targetInitialRotation = targetLimb.transform.localRotation;
    }

    private void FixedUpdate() {
        m_ConfigurableJoint.targetRotation = copyRotation();
    }

    private Quaternion copyRotation() {
        return Quaternion.Inverse(targetLimb.localRotation) * targetInitialRotation;
    }
}
