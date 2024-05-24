using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mdoo_goalBehavior : MonoBehaviour
{
    private GameManager _gameManager;

    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        if (_gameManager == null)
        {
            print("GameManager is not found in the scene!");
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            if (_gameManager != null)
            {
                _gameManager.LoadNextScene();
            }
            else
            {
                print("GameManager reference is not set or missing!");
            }
        }
    }

}
