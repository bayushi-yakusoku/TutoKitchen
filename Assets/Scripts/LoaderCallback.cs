using UnityEngine;

public class LoaderCallback : MonoBehaviour {

    bool firstFrame = true;

    void Update() {
        if (firstFrame) {
            firstFrame = false;
            Loader.DisplayScene();
        }        
    }
}
