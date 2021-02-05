﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// inherits from Enemy
public class Log : Enemy {

    public Rigidbody2D myRigidBody;
    
    [Header("Target Variables")]
    public Transform    target;
    public float        chaseRadius;
    public float        attackRadius;
    
    [Header("Animator")]
    public Animator     anim;

    void Start() {
        target          = GameObject.FindWithTag("Player").transform;
        myRigidBody     = GetComponent<Rigidbody2D>();
        currentState    = EnemyState.idle;
        anim            = GetComponent<Animator>();
        anim.SetBool("wakeUp", true);
    }

    void FixedUpdate() {
        CheckDistance();        
    }

    // Distance from log to target
    // Only chase if within chaseRadius but not within attackRadius
    public virtual void CheckDistance() {
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius && Vector3.Distance(target.position, transform.position) > attackRadius) {
            if (currentState == EnemyState.idle || currentState == EnemyState.walk && currentState != EnemyState.stagger) {
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                ChangeAnim(temp - transform.position);
                myRigidBody.MovePosition(temp);
                ChangeState(EnemyState.walk);}
                anim.SetBool("wakeUp", true);} 
        else if (Vector3.Distance(target.position, transform.position) > chaseRadius) {
            anim.SetBool("wakeUp", false);}
    }

    public void SetAnimFloat(Vector2 setVector) {
        anim.SetFloat("moveX", setVector.x);
        anim.SetFloat("moveY", setVector.y);
    }

    public void ChangeAnim(Vector2 direction) {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y)) {
            if (direction.x > 0) {
                SetAnimFloat(Vector2.right);}
            else if (direction.x < 0) {
                SetAnimFloat(Vector2.left);}}
        else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y)) {
            if (direction.y > 0) {
                SetAnimFloat(Vector2.up);}
            else if (direction.y < 0) {
                SetAnimFloat(Vector2.down);}}
    }

    public void ChangeState(EnemyState newState) {
        if (currentState != newState) {
            currentState = newState;}
    }

}
