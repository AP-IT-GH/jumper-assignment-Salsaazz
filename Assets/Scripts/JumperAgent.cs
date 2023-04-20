using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class JumperAgent : Agent
{
    public bool _isOnGround;
    private float jumpSpeed = 0.3f;
    public LayerMask whatIsGround;
    [SerializeField]
    private float _gravity = 0.7f;
    private Rigidbody _body;
    private float yVelocity;
    private int _count = 0;
    private float _energy;

    private int _reekscount;
    private int _average;
    private int _curretnMax;
    private int _allTimeMax;
    public override void OnEpisodeBegin()
    {
        _body = GetComponent<Rigidbody>();
        _energy = 5f;
        _body.freezeRotation = true;
        transform.position = new Vector3(0, 0.5f, -3f);
        if(_reekscount%20 == 0)
        {
            _reekscount = 0;
            _average /= 20;
            Debug.Log($"20 passed. Average: {_average}. Max: {_curretnMax}. All Time Max: {_allTimeMax}");
            _average = 0;
            _curretnMax = 0;
        }
        _reekscount++;
        _count = 0;
        base.OnEpisodeBegin();
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(_isOnGround);
        sensor.AddObservation(_energy);
        base.CollectObservations(sensor);
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        _isOnGround = Physics.Raycast(transform.position, Vector3.down, transform.lossyScale.y * 0.5f + 0.2f, whatIsGround);
        if (_isOnGround)
        {            
            if (actions.DiscreteActions[0] == 1 && _energy >0)
            {
                yVelocity = jumpSpeed;//set velocity to jump
                _energy--;
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + yVelocity, transform.localPosition.z); //ellevate
                AddReward(-0.2f);
            }
        }
        else
        {
            yVelocity -= _gravity * Time.deltaTime; //reduce velocity
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + yVelocity, transform.localPosition.z); //update position
        }
        base.OnActionReceived(actions);
    }
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.DiscreteActions;
        continuousActionsOut[0] = 0;
        if (Input.GetKey(KeyCode.UpArrow) && _isOnGround)
        {
            Debug.Log("JUMPED");
            continuousActionsOut[0] = 1;

        }
        else
        {
            Debug.Log("Cant jump");
            continuousActionsOut[0] = 0;

        }
        Debug.Log("UITGEVOERD");
    }

    public void GiveReward(float reward)
    {
        _count++;
        AddReward(reward* (float)Math.Ceiling(_count / 2f));        
    }
    public void SetHit()
    {
        if (_count != 0) { 
            
            if(_energy == 0)
            {
                Debug.Log($"Died of hunger. Obstacles avoided: {_count}");
            }
            else
            {
                Debug.Log($"Obstacles avoided: {_count}");
            }
        }
        _average += _count;
        if(_count> _curretnMax)
        {
            _curretnMax = _count;
            
        }
        if (_count > _allTimeMax)
        {
            _allTimeMax = _count;
        }
        AddReward(-0.2f);
        if (_energy == 0) AddReward(-0.5f);
        EndEpisode();
    }
    public void GetFood()
    {
        AddReward(0.5f);
        _energy +=2f;
    }
}
