using System;
using UnityEngine;
using DG.Tweening;
using System.Collections;

public class KeyCollectAnimation : MonoBehaviour 
{
    [SerializeField] private RectTransform blueKeyPrefab, redKeyPrefab;
    [SerializeField] private Transform destination;
    [SerializeField] private Vector3 startingPosition;

    private void Start()
    {
        Inventory.instance.onCollectKey += StartAnimation;
    }

    private void OnDisable() 
    {
        Inventory.instance.onCollectKey -= StartAnimation;
    }

    private void StartAnimation(EKey keyColor)
    {
        if(keyColor == EKey.Blue)
        {
            var blueKeyInstance = Instantiate(blueKeyPrefab, startingPosition, Quaternion.identity, transform);
            StartCoroutine(SendToUI(blueKeyInstance));
            
        }
        if(keyColor == EKey.Red)
        {
            var redKeyInstance = Instantiate(redKeyPrefab, startingPosition, Quaternion.identity, transform);
            StartCoroutine(SendToUI(redKeyInstance));
        }
    }

    private IEnumerator SendToUI(RectTransform key)
    {
        while(true)
        {
            key.transform.localPosition = startingPosition;
            key.transform.DOMove(destination.position, 1f).SetEase(Ease.Linear).OnComplete(() => 
            {
                key.gameObject.SetActive(false);
            });
            yield break;
        }
        
    }
}
