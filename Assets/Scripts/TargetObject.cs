using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TargetObject : MonoBehaviour
{
    [System.Serializable] public class RotationDescription
    {
        public enum DescriptionType
        {
            linear,
            sinewaver
        }
        public DescriptionType type;
        public Vector3 Get(float dt, float done)
        {
            switch(type)
            {
                case DescriptionType.linear: return direction * dt;
                case DescriptionType.sinewaver: return Mathf.Sin(done / duration * Mathf.PI * 2) * direction;
            }
            return Vector3.zero;
        }
        public Vector3 direction;
        public float duration;
    }

    public int _currentBehaviorIndex;
    public float _durationCompleted;
    public List<RotationDescription> _behaviors;
    public RotationDescription Behavior => _behaviors[_currentBehaviorIndex];


    private void Start()
    {
        int c = Random.Range(2, 10);
        for (int i = 0; i < c; i++)
        {
            
        }
    }
    private void Update()
    {
        _durationCompleted += Time.deltaTime;
        transform.Rotate(Behavior.Get(Time.deltaTime, _durationCompleted));
        if (_durationCompleted >= Behavior.duration)
        {
            _durationCompleted = 0;
            _currentBehaviorIndex++;
            if (_currentBehaviorIndex >= _behaviors.Count)
                _currentBehaviorIndex = 0;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        StopAllCoroutines();
        StartCoroutine(HitAnim());
    }
    private IEnumerator HitAnim()
    {
        var targetPos = Vector3.up * .6f;
        while(transform.position.y < .5)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime);
            yield return null;
        }
        targetPos = Vector3.zero;
        while (transform.position.y > 0)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime);
            yield return null;
        }
    }
}
