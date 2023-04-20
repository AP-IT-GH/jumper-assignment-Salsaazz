using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 Spawn { get; private set; }
    GameObject _player;
    private const float _minSpeed = 5;
    private const float _maxSpeed = 10;
    private float _speed = _minSpeed;
    public bool Hit { get; set; }
    void Start()
    {
        Spawn = gameObject.transform.localPosition;
        _player = GameObject.FindGameObjectWithTag("Player");
        _speed = UnityEngine.Random.Range(_minSpeed, _maxSpeed);
    }

    // Update is called once per frame
    void Update()
    {
            var currentPos = gameObject.transform.localPosition;
            gameObject.transform.localPosition = new Vector3(currentPos.x, currentPos.y, currentPos.z - _speed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject == _player)
        {
           _player.GetComponent<JumperAgent>().GetFood();
        }
        gameObject.transform.localPosition = Spawn;
        _speed = UnityEngine.Random.Range(_minSpeed, _maxSpeed);
    }
}
