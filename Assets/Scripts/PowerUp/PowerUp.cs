using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3;
    [SerializeField]
    private PowerUpID _powerUpID;

    void Update()
    {
        transform.Translate(Vector3.down * (_speed * Time.deltaTime));
        if (transform.position.y <= -6.5f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            other.gameObject.SetActive(false);
        }
        else if(other.gameObject.CompareTag("Player"))
        {
            var playerScripts = other.gameObject.GetComponent<PlayerScripts>();
            if (playerScripts != null)
            {
                switch (_powerUpID)
                {
                    case PowerUpID.Shield:
                        playerScripts.ShieldActive();
                        break;
                    case PowerUpID.TripleShot:
                        playerScripts.TrippleShotActive();
                        break;
                    case PowerUpID.Speed:
                        playerScripts.SpeedActive();
                        break;
                }

            }
        }
        Destroy(this.gameObject);
    }
}
