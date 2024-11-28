using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LavelManager : MonoBehaviour
{
    public static LavelManager instance;

    public float waitToRespawn;

    public int gemsCollected;

    public float timeInLevel;

    public string levelToLoad;


    private void Awake()    
    {
        instance = this;
    }

    void Start()
    {
        timeInLevel = 0f;

        // Asegúrate de que el primer nivel esté siempre desbloqueado
        if (!PlayerPrefs.HasKey("Test2_unlocked"))
        {
            PlayerPrefs.SetInt("Test2_unlocked", 1);
            PlayerPrefs.Save();
        }

    }

    // Update is called once per frame
    void Update()
    {
        timeInLevel += Time.deltaTime;
    }


    public void RespawnPlayer()
    {
        StartCoroutine(RespawnCo());
    }

    IEnumerator RespawnCo()
    {
        PlayerController.instance.gameObject.SetActive(false);

        yield return new WaitForSeconds(waitToRespawn - (1f / UIController.instance.fadeSpeed));

        UIController.instance.FadeToBlack();

        yield return new WaitForSeconds((1f / UIController.instance.fadeSpeed) + .2f);

        UIController.instance.FadeFromBlack();

        PlayerController.instance.gameObject.SetActive(true);

        PlayerController.instance.transform.position = CheckpointController.instance.spawnPoint;

        PlayerHealthController.instance.currentHealth = PlayerHealthController.instance.maxHealth;

        UIController.instance.UpdateHealthDisplay();
    }


    public void EndLevel()
    {
        StartCoroutine(EndLevelCo());
    }


    IEnumerator EndLevelCo()
{
    AudioManager.instance.PlayLevelVictory();
    PlayerController.instance.stopInput = true;
    CameraController.instance.stopFollow = true;
    UIController.instance.levelCompleteText.SetActive(true);

    yield return new WaitForSeconds(1.5f);
    UIController.instance.FadeToBlack();

    yield return new WaitForSeconds((1f / UIController.instance.fadeSpeed) + .25f);

    // Lista de los nombres de las escenas (niveles en orden)
    string[] levels = { "Test2", "Test3", "Test4", "Test5" };

    // Obtén el nombre de la escena actual
    string currentScene = SceneManager.GetActiveScene().name;

    // Encuentra el índice de la escena actual
    int currentLevelIndex = System.Array.IndexOf(levels, currentScene);

    // Desbloquea el siguiente nivel, si existe
    if (currentLevelIndex >= 0 && currentLevelIndex < levels.Length - 1)
    {
        string nextLevel = levels[currentLevelIndex + 1];
        PlayerPrefs.SetInt(nextLevel + "_unlocked", 1); // Desbloquea el siguiente nivel
        Debug.Log("Desbloqueando el nivel: " + nextLevel);
    }

    // Marca el nivel actual como completado
    PlayerPrefs.SetInt(currentScene + "_unlocked", 1);

    // Guarda progreso de gemas y tiempo
    if (gemsCollected > PlayerPrefs.GetInt(currentScene + "_gems", 0))
    {
        PlayerPrefs.SetInt(currentScene + "_gems", gemsCollected);
    }
    if (timeInLevel < PlayerPrefs.GetFloat(currentScene + "_time", float.MaxValue))
    {
        PlayerPrefs.SetFloat(currentScene + "_time", timeInLevel);
    }

    PlayerPrefs.SetString("CurrentLevel", currentScene);
    PlayerPrefs.Save();

    // Carga la escena de selección de niveles
    SceneManager.LoadScene(levelToLoad);
}

}
