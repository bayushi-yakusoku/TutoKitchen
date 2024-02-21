using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader {

    public enum EnumScene {
        MainMenuScene,
        GameScene,
        LoadingScene
    }

    private static EnumScene targetScene;

    public static void LoadScene(EnumScene targetScene) {
        Loader.targetScene = targetScene;

        SceneManager.LoadScene(EnumScene.LoadingScene.ToString());
    }

    public static void DisplayScene() {
        SceneManager.LoadScene(targetScene.ToString());
    }

}
