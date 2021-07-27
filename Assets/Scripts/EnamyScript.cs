using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnamyScript : MonoBehaviour
{
    [SerializeField]
    private float _speed = 0;

    public static Action destroyEnamy = delegate { };
   void Update()
   {
       Move();
   }

   private void Move()
   {
       transform.Translate(Vector3.down*_speed*Time.deltaTime);
       if (transform.position.y <= -6)
       {
           var randomX = Random.Range(-9f, 9f);
           transform.position = new Vector3(randomX, 6, 0);
       }
   }

   private void OnTriggerEnter(Collider other)
   {
       if (other.tag == "Player")
       {
           var plaeyr  = other.gameObject.GetComponent<PlayerScripts>();
           if (plaeyr!=null)
           {
               plaeyr.Damage(); 
           }
           Destroy(this.gameObject);
       }
       else if (other.tag == "Bullet")
       {
           other.gameObject.SetActive(false);
           destroyEnamy();
           Destroy(this.gameObject);

       }
   }
}
