﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    public float         speed;
    public Rigidbody2D   myRigidBody;

    void Start() {}

    public void Setup(Vector2 velocity, Vector3 direction) {
        myRigidBody.velocity = velocity.normalized * speed;
        transform.rotation = Quaternion.Euler(direction);
    }

    public void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("enemy")) {
            Destroy(this.gameObject);}
    }

}
