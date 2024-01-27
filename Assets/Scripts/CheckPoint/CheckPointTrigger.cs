using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointTrigger : MonoBehaviour
{
    [Header("Check Point")]
    [SerializeField] private int _checkPointID;
    [SerializeField] Transform _checkPointTransform;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameProgressManager.Instance.SetCheckPoint(_checkPointID, _checkPointTransform.position);
            print($"Check Point {_checkPointID}");
            gameObject.SetActive(false);
        }
    }
}
