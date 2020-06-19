using System.Collections.Generic;
using UnityEngine;

public class StickyBehavior : MonoBehaviour
{
    private GameObject _stickyBody;
    private List<GameObject> _stickedObjects;
    private Animator _animator;

    void Start()
    {
        _stickyBody = GameObject.Find("StickyBody");
        _stickedObjects = new List<GameObject> { };
        _animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("WoodThing"))
        {
            other.transform.SetParent(_stickyBody.transform);
            _stickedObjects.Add(other.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("ResetBush"))
        {
            RemoveStickedObjects();
        }
    }

    private void RemoveStickedObjects()
    {
        Debug.Log(_stickedObjects.Count);
        foreach (GameObject gameObject in _stickedObjects)
        {
            gameObject.transform.SetParent(null);
        }

        _stickedObjects.Clear();
    }
}