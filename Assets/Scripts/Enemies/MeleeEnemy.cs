using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Log {

    public override void CheckDistance() {
    if (Vector3.Distance(target.position, transform.position) <= chaseRadius && Vector3.Distance(target.position, transform.position) > attackRadius) {
        if (currentState == EnemyState.idle || currentState == EnemyState.walk && currentState != EnemyState.stagger) {
            Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            ChangeAnim(temp - transform.position);
            myRigidBody.MovePosition(temp);
            ChangeState(EnemyState.walk);}
        } 
        else if (Vector3.Distance(target.position, transform.position) <= chaseRadius && Vector3.Distance(target.position, transform.position) <= attackRadius) {
            if (currentState == EnemyState.walk && currentState != EnemyState.stagger) {
                StartCoroutine(AttackCo());
            }
        }

    }

    public IEnumerator AttackCo() {
        currentState = EnemyState.attack;
        anim.SetBool("attack", true);
        yield return new WaitForSeconds(1f);
        currentState = EnemyState.walk;
        anim.SetBool("attack", false);
    } 


}
