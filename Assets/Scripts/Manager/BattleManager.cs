using System.Collections;
using System.Collections.Generic;
using RievelGame;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static System.Action OnBattleStart = delegate { };
    public Transform playerTransform;
    public Transform enemyTransform;

    public float lerpRange = 5f;
    public float lerpSpeed = 5f;

    private void Awake()
    {
        StartCoroutine(LerpStartPositionCoroutine(playerTransform, 0, true));
        StartCoroutine(LerpStartPositionCoroutine(enemyTransform, 1, false));
    }

    IEnumerator LerpStartPositionCoroutine(Transform characterTransform, int arg2, bool arg3)
    {
        Vector3 startPosition = characterTransform.position;
        characterTransform.position += Vector3.right * lerpRange * (arg2 == 0 ? -1f : 1f);

        float _distance = Vector3.Distance(startPosition, characterTransform.position);

        while (_distance > 0.1f)
        {
            characterTransform.position = Vector3.Lerp(characterTransform.position, startPosition, lerpSpeed * Time.deltaTime);

            yield return null;
            _distance = Vector3.Distance(startPosition, characterTransform.position);
        }

        characterTransform.position = startPosition;

        if (arg3)
        {
            Debug.Log("run start fighting callback");
            OnBattleStart?.Invoke();
            UIWindowManager.Instance.OpenWindow("Gameplay");
        }
    }
}
