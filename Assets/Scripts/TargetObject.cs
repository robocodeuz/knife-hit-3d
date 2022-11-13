using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetObject : MonoBehaviour
{
    #region Rotation Descriptions
    [System.Serializable] public class RotationDescription
    {
        public enum DescriptionType
        {
            linear,
            sinewaver
        }
        public DescriptionType type;
        public Vector3 Get(float dt)
        {
            switch(type)
            {
                case DescriptionType.linear : return direction * dt;
                
            }
            return Vector3.zero;
        }
        public Vector3 direction;
        public float duration;
    }
    #endregion

    public int currentBehaviorIndex;
    public float durationCompleted;
    public List<RotationDescription> behaviors;
    public RotationDescription Behavior
    {
        get => behaviors[currentBehaviorIndex];
    }


    private void Start()
    {
        
    }
    private void Update()
    {
        durationCompleted += Time.deltaTime;
        transform.Rotate(Behavior.Get(Time.deltaTime));
        if (durationCompleted >= Behavior.duration)
        {
            durationCompleted = 0;
            currentBehaviorIndex++;
            if (currentBehaviorIndex >= behaviors.Count)
                currentBehaviorIndex = 0;
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
