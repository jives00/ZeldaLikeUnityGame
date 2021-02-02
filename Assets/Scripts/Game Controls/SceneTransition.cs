using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour {

    [Header("Scene Variables")]
    public string           sceneToLoad;
    public Vector2          playerPosition;
    public VectorValue      playerStorage;
    public Vector2          cameraNewMax;
    public Vector2          cameraNewMin;
    public VectorValue      cameraMin;
    public VectorValue      cameraMax;

    [Header("Tranisition Variables")]
    public GameObject       fadeInPanel;
    public GameObject       fadeOutPanel;
    public float            fadeWait;

    private void Awake() {
        if (fadeInPanel != null) {
            GameObject panel = Instantiate(fadeInPanel, Vector3.zero, Quaternion.identity) as GameObject;
            Destroy(panel, 1);}
    }

    public void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player") && !other.isTrigger) {
            playerStorage.initialValue = playerPosition;
            StartCoroutine(FadeCo());}
    }

    public IEnumerator FadeCo() {
        if (fadeOutPanel != null) {
            Instantiate(fadeOutPanel, Vector3.zero, Quaternion.identity);}
        yield return new WaitForSeconds(fadeWait);
        resetCameraBounds();
        AsyncOperation aSyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);
        while (!aSyncOperation.isDone) {
            yield return null;}
    }

    public void resetCameraBounds() {
        cameraMax.initialValue = cameraNewMax;
        cameraMin.initialValue = cameraNewMin;
    }


    
}
