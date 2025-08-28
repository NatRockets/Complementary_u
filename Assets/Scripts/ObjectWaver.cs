using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectWaver : MonoBehaviour
{
    private Transform tTransform;
    private Sequence seq;
    private float initScaleY;
    
    private void Awake()
    {
        float waveTime = Random.Range(0.4f, 0.6f);
        
        tTransform = transform;
        initScaleY = tTransform.localScale.y;
        seq = DOTween.Sequence()
            .Append(transform.DOScaleY(initScaleY * 1.1f, waveTime))
            .Append(transform.DOScale(initScaleY, waveTime))
            .SetLoops(-1);
    }

    private void OnEnable()
    {
        seq.Play();
    }

    private void OnDisable()
    {
        seq.Rewind();
    }
}
