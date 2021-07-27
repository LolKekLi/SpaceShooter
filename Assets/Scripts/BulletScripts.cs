using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScripts : MonoBehaviour
{
    [SerializeField] 
    private float _speed = 0;
    void Update()
    {
        transform.Translate(Vector3.up * (_speed * Time.deltaTime));
        BulletBounts();
    }
    private void BulletBounts()
    {
        if (transform.position.y >= 7)
        {
           gameObject.SetActive(false);
        }
    }
}
