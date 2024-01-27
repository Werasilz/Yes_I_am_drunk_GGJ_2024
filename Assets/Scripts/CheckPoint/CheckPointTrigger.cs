using UnityEngine;

public class CheckPointTrigger : MonoBehaviour
{
    [Header("Check Point")]
    [SerializeField] private int _checkPointID;
    [SerializeField] private bool _isAllowTrigger = true;
    [SerializeField] private Transform _checkPointTransform;
    public Transform CheckPointTransform => _checkPointTransform;

    private void OnTriggerEnter(Collider other)
    {
        if (_isAllowTrigger == false)
        {
            return;
        }

        if (other.gameObject.CompareTag("Player"))
        {
            GameProgressManager.Instance.SetCheckPoint(_checkPointID, _checkPointTransform.position);
            print($"Check Point {_checkPointID}");
            gameObject.SetActive(false);
        }
    }
}
