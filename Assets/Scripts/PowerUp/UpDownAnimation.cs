using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownAnimation : MonoBehaviour
{
    //private float _speed = 2;
    
    private Vector3 _endPos;
    private float _upDistance = 0.2f;
    private void Start()
    {
        _endPos = new Vector3(transform.position.x, transform.position.y + _upDistance, transform.position.z);
        StartCoroutine(UpAnimetion());
    }

    private IEnumerator UpAnimetion()
    {
        float time = 0;
        while (true)
        {
            time += Time.deltaTime;
            
            transform.position = Vector3.Lerp(transform.position, _endPos, time/0.5f);
            if (transform.position == _endPos)
            {
                var temp = transform.position;
                transform.position = _endPos;
                _endPos = temp;
                time = 0;
            }
            yield return 0;
        }
    }
}
