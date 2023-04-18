using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class JumperAgent : Agent
{
    GameObject _object;
    ObjectMovement _objectScript;
    public bool _isOnGround;
    private float jumpSpeed = 0.3f;
    public LayerMask whatIsGround;
    [SerializeField]
    private float _gravity = 0.7f;
    private Rigidbody _body;
    private float yVelocity;
    public override void OnEpisodeBegin()
    {
        _body = GetComponent<Rigidbody>();

        _body.freezeRotation = true;
        transform.position = new Vector3(0, 0.5f, -3f);
        _object = GameObject.FindGameObjectWithTag("Object");
        _objectScript = _object.GetComponent<ObjectMovement>();
        _object.transform.localPosition = _objectScript.Spawn;
        _objectScript.Hit = false;
        _rewardGiven = false;
        base.OnEpisodeBegin();
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(_isOnGround);
        base.CollectObservations(sensor);
    }
    private bool _rewardGiven;
    public override void OnActionReceived(ActionBuffers actions)
    {
        _isOnGround = Physics.Raycast(transform.position, Vector3.down, transform.lossyScale.y * 0.5f + 0.2f, whatIsGround);
        if (_isOnGround)
        {
            if (actions.DiscreteActions[0] == 1)
            {
                yVelocity = jumpSpeed;//set velocity to jump
                
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + yVelocity, transform.localPosition.z); //ellevate
            }
            if (_objectScript.Respawn)
                _rewardGiven = false;
        }
        else
        {
            yVelocity -= _gravity * Time.deltaTime; //reduce velocity
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + yVelocity, transform.localPosition.z); //update position
            
            if(_object.transform.localPosition.z <= transform.localPosition.z && !_rewardGiven)
            {
                AddReward(0.3f);
                _rewardGiven = true;
            }
        }
        
        if (_objectScript.Hit)
        {
            if (_rewardGiven)
            {
                AddReward(-0.03f);
            }
            EndEpisode();
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
    

}
