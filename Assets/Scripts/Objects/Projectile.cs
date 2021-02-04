using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    [Header("Movement Variables")]
    public float            moveSpeed;
    public Vector2          directionToMove;
    
    [Header("Lifetime Variables")]
    public float            lifetime;
    
    private float           lifetimeSeconds;
    public Rigidbody2D     myRigidBody;

    void Start() {
        lifetimeSeconds = lifetime;
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    void Update() {
        lifetimeSeconds -= Time.deltaTime;
        if (lifetimeSeconds <= 0) {
            Destroy(this.gameObject);}
    }

    public void Launch(Vector2 initialVelocity) {
        myRigidBody.velocity = initialVelocity * moveSpeed;
    }

    public void OnTriggerEnter2D(Collider2D other) {
        Destroy(this.gameObject);
    }
}
