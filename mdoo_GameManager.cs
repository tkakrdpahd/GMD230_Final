using System.Collections;
using TMPro;  // Importing the TextMeshPro namespace
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class GameManager : MonoBehaviour
{
    private GameObject _gameObject;
    public GameObject _playerControl;
    public TMP_Text messageUI;  // Component to display messages

    public int puzzleCount;
    public int collectedGameObject;
    public int RemainingTime = 60;
    public TMP_Text CountDownUI;

    private void Start()
    {
        StartCoroutine(CountDown());
    }

    private void Update()
    {
        VideoController();

        if (SceneManager.GetActiveScene().name == "BadScene" && Input.GetKeyDown(KeyCode.Space))
        {
            int currentSceneNumber = PlayerPrefs.GetInt("highestLevel");
            SceneManager.LoadScene("Level_" + currentSceneNumber.ToString("D2"));
        }
        else
        {
            GameObject instructions = GameObject.Find("Instructions");
            if (instructions != null && Input.GetKeyDown(KeyCode.Space))
            {
                Destroy(instructions);
            }
        }
    }

    public void VideoController()
    {
        if (SceneManager.GetActiveScene().name == "Level_00" && Input.GetKeyDown(KeyCode.Space))
        {
            foreach (VideoPlayer player in FindObjectsOfType<VideoPlayer>())
            {
                Destroy(player.gameObject);
            }
            LoadNextScene();
        }
    }

    public void reducePuzzleCount()
    {
        puzzleCount--;
        Debug.Log(puzzleCount);  // Changed from print to Debug.Log
        if (puzzleCount == 0)
        {
            PlayerPrefs.DeleteAll();
            SceneManager.LoadScene("END");
        }
    }

    public void setHasBox()
    {
        _playerControl.GetComponent<PlayerControl>().hasBox = false;
        _playerControl.GetComponent<PlayerControl>().collidingBox = null;
    }

    public void LoadNextScene()
    {
        if (collectedGameObject != 0)
        {
            StartCoroutine(DisplayMessage($"You need {collectedGameObject} more keys", 3));
            return;
        }

        StopAllCoroutines();
        string currentSceneName = SceneManager.GetActiveScene().name;
        string lastTwoChars = currentSceneName.Substring(currentSceneName.Length - 2);
        int currentSceneNumber;

        if (int.TryParse(lastTwoChars, out currentSceneNumber))
        {
            int newSceneNumber = currentSceneNumber + 1;
            SceneManager.LoadScene("Level_" + newSceneNumber.ToString("D2"));
            PlayerPrefs.SetInt("highestLevel", newSceneNumber);
        }
        else
        {
            Debug.LogError("Failed to parse scene number from the scene name: " + currentSceneName);
        }
    }

    public void LoadBadScene()
    {
        SceneManager.LoadScene("BadScene");
    }

    private IEnumerator CountDown()
    {
        while (RemainingTime > 0)
        {
            CountDownUI.text = RemainingTime.ToString();
            yield return new WaitForSeconds(1);
            RemainingTime--;
        }

        LoadBadScene();
    }

    private IEnumerator DisplayMessage(string message, float duration)
    {
        messageUI.text = message;
        messageUI.enabled = true;
        yield return new WaitForSeconds(duration);
        messageUI.enabled = false;
    }
}
