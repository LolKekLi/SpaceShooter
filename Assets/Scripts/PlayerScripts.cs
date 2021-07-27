using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerScripts : MonoBehaviour
{
    
    [SerializeField]
    private float _speed = 0;
    [SerializeField]
    private float _speedMultiple = 2;

    [SerializeField]
    private GameObject _bullet;
    private GameObject _bulletFolder;

    [SerializeField]
    private GameObject _trippleShotBullet;
    private GameObject _trippleShotBulletFolder;
    private bool _isTripleShot = false;

    [SerializeField]
    private GameObject _bulletSpawnPos;

    private GameObject[] _poolBullet;
    private GameObject[] _trippleShotPoolBullet;
    private int _poolID = 0;
    private int _tripleShotPoolID = 0;

    [SerializeField]
    private float _fireRate = 0.3f;
    private int _poolBulletSize = 10;

    private int _lives = 3;
    private float _nextFire = -1.0f;

    private Quaternion _startRotation;
    private Quaternion _rightRotation;
    private Quaternion _leftRotation;
    private float _time = 0;

    [SerializeField]
    private GameObject _shield;
    private bool _shildIsActive = false;

    public static Action die = delegate { };
    public static Action damage = delegate { };

    private void Start()
    {
        _bulletFolder = new GameObject("Bullet");
       
        _trippleShotBulletFolder = new GameObject("TrippleShotBullet");
       
        _poolBullet = new GameObject[_poolBulletSize];
        _trippleShotPoolBullet = new GameObject[_poolBulletSize];

        for (int i = 0; i < _poolBulletSize; i++)
        {
            _poolBullet[i] = Instantiate(_bullet, transform.position, Quaternion.identity,_bulletFolder.transform);
            _trippleShotPoolBullet[i] = Instantiate(_trippleShotBullet, transform.position, Quaternion.identity, _trippleShotBulletFolder.transform);
            _poolBullet[i].SetActive(false);
            _trippleShotPoolBullet[i].SetActive(false);
        }

        _startRotation = transform.rotation;
        _rightRotation = Quaternion.Euler(0, 25, 0);
        _leftRotation = Quaternion.Euler(0, -25, 0);

       
    }

    void Update()
    {
        CalculateMuvment();

        if (Input.GetKey(KeyCode.Space) && Time.time > _nextFire)
        {
            Fire();
        }

        Rotation();
    }

    private void Rotation()
    {
        _time = Time.deltaTime * 5;
        if (Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, _rightRotation, _time);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, _leftRotation, _time);
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, _startRotation, _time);
        }
    }
    private void Fire()
    {
        _nextFire = Time.time + _fireRate;
    
        if (!_isTripleShot)
        {
            var poolBullet = _poolBullet[_poolID];
            poolBullet.transform.position = _bulletSpawnPos.transform.position;
            poolBullet.SetActive(true);

            _poolID++;

            if (_poolID >= _poolBulletSize)
            {
                _poolID = 0;
            }
        }
        else
        {
            var trippleShotPoolBullet = _trippleShotPoolBullet[_tripleShotPoolID];
            trippleShotPoolBullet.transform.position = _bulletSpawnPos.transform.position; 
            trippleShotPoolBullet.SetActive(true);

            for (int i = 0; i < trippleShotPoolBullet.transform.childCount; i++)
            {
                var child = trippleShotPoolBullet.transform.GetChild(i);
                child.gameObject.SetActive(true);
            }
            
            _tripleShotPoolID++;

            if (_tripleShotPoolID >= _poolBulletSize)
            {
                _tripleShotPoolID = 0;
            }
        }
    }
    void CalculateMuvment()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * (_speed * Time.deltaTime));

        var position = transform.position;
       
        transform.position = new Vector3(position.x, Mathf.Clamp(position.y, -4f, 6f), 0);

        if (position.x >= 11.2)
        {
            transform.position = new Vector3(-11.2f, position.y, 0);
        }
        else if (transform.position.x <= -11.2)
        {
            transform.position = new Vector3(11.2f, position.y, 0);
        }
    }
    public void Damage()
    {
        if (!_shildIsActive)
        {
            damage();
            _lives--;
            if (_lives < 1)
            {
                die();
                Destroy(this.gameObject);
            }
        }
        else
        {
            _shield.SetActive(false);
            _shildIsActive = false;
        }
    }
    public void TrippleShotActive()
    {
        _isTripleShot = true;
        StartCoroutine(TrippleShotTime());
    }
    private IEnumerator TrippleShotTime()
    {
        yield return new WaitForSeconds(5f);
        _isTripleShot = false;
    }
    public void SpeedActive()
    {
        _speed *= _speedMultiple;
        StartCoroutine(SpeedTime());
    }
    private IEnumerator SpeedTime()
    {
        yield return new WaitForSeconds(5f);
        _speed /= _speedMultiple;
    }

    public void ShieldActive()
    {
        _shield.SetActive(true);
        _shildIsActive = true;
    }
}
