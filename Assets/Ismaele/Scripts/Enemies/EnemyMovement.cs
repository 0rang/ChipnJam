﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : PlayerMovement
{
    // Other properties belonging to the enemy
    private NavMeshAgent enemyAgent;
    public EnemySensor sensor;

    private bool shouldJump;

    protected override void Start()
    {
        base.Start();

        enemyAgent = GetComponent<NavMeshAgent>();

        enemyAgent.updatePosition = false;
        enemyAgent.updateRotation = false;
        enemyAgent.autoTraverseOffMeshLink = false;
    }

    protected override void Update()
    {
        enemyAgent.nextPosition = transform.position;
        PlayerInput input = ResolveInput();

        Move(input);
        RotateModel();

        if (shouldJump)
            enemyAgent.CompleteOffMeshLink();
    }

    private PlayerInput ResolveInput()
    {
        PlayerInput enemyInput = new PlayerInput();

        if (!sensor.SensesPlayer)
        {
            enemyInput.moveInput = Vector2.zero;
            enemyInput.jump = true;
            enemyInput.attack = false;
            enemyInput.ability = false;
        } else
        {
            // Moving towards the player
            enemyAgent.SetDestination(sensor.Player.position);

            enemyInput.moveInput = CondenseVector3(enemyAgent.desiredVelocity);

            Debug.Log(enemyAgent.isOnOffMeshLink);

            if (enemyAgent.isOnOffMeshLink && Controller.isGrounded)
            {
                enemyInput.jump = true;
                shouldJump = true;
            } else
            {
                enemyInput.jump = false;
                shouldJump = false;
            }

            enemyInput.attack = false;
            enemyInput.ability = false;
        }

        return enemyInput;
    }
}