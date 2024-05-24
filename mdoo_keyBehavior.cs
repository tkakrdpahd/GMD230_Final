using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mdoo_keyBehavior : MonoBehaviour
{
    private GameManager _gameManager;


    // Start is called before the first frame update
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            // if plyer collied with the key
            Destroy(gameObject);
            _gameManager.collectedGameObject = _gameManager.collectedGameObject -= 1;
        }
    }
}
