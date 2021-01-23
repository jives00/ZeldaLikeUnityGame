using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour {

    private Animator anim;

    void Start() {
        anim = GetComponent<Animator>();
    }

    public void Smash() {
        anim.SetBool("smash", true);
        StartCoroutine(breakCo());
    }

    IEnumerator breakCo() {
        yield return new WaitForSeconds(.3f);
        this.gameObject.SetActive(false);
    }
}
