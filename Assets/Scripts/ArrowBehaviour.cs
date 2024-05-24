using System.Collections.Generic;
using UnityEngine;

public class ArrowBehaviour : MonoBehaviour
{
    private Transform m_transform;

    private Transform m_tipTransform;

    private float m_tipRotation;

    public float TipRotation => Mathf.Clamp((float)(1 - m_tipRotation), 0.05f, 1f);

    private readonly string _levelKey = "Level";

    private List<int> _arrowSpeed = new List<int>(3) { 100, 200, 300 };

    private float _rotationSpeed;
    
    [SerializeField]
    private float _minAngle = 60f;

    [SerializeField]
    private float _maxAngle = 120f;

    private void Start()
    {
        _rotationSpeed = _arrowSpeed[PlayerPrefs.GetInt(_levelKey)];

        m_transform = GetComponent<Transform>();
        m_tipTransform = gameObject.transform.GetChild(0).GetComponent<Transform>();
        m_tipRotation = m_tipTransform.rotation.z;
    }

    private void Update()
    {
        m_tipRotation = m_tipTransform.rotation.z;

        //Debug.Log("Tip rotation " + m_tipTransform.rotation.z);

        if (m_transform != null)
        {
            float newAngle = Mathf.PingPong(Time.time * _rotationSpeed, _maxAngle - _minAngle) + _minAngle;
            m_transform.localRotation = Quaternion.Euler(0f, 0f, newAngle);
        }
    }
}
