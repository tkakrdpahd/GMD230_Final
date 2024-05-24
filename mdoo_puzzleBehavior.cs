using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mdoo_puzzleBehavior : MonoBehaviour
{
    private GameManager _gameManager;
    public int puzzleType;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();

        if (_gameManager == null)
        {
            Debug.LogError("GameManager not found in the scene!");
        }
    }

    // Check puzzle type on collision
    void OnTriggerEnter2D(Collider2D collider)
    {
        // Check if the collided object has the 'puzzleGoal' tag
        if (collider.gameObject.tag == "puzzleGoal")
        {
            // Attempt to retrieve the mdoo_puzzleGoalBehavior component from the collided game object
            mdoo_puzzleGoalBehavior goalBehavior = collider.gameObject.GetComponent<mdoo_puzzleGoalBehavior>();

            // Check if the component exists and if the goalType matches the puzzleType
            if (goalBehavior != null && puzzleType == goalBehavior.puzzleGoalType)
            {
                // Log when the puzzle type matches
                Debug.Log("Puzzle goal type matches!");

                // Call the turnOn method to change the appearance of the puzzle goal
                goalBehavior.turnOn();

                // Assuming setHasBox changes a status related to the player or game, it should be checked if correctly implemented
                if (_gameManager != null)
                {
                    _gameManager.setHasBox();
                    // Call reducePuzzleCount method to decrease the puzzle count
                    _gameManager.reducePuzzleCount();
                }
                else
                {
                    Debug.LogError("GameManager is not initialized.");
                }

                // Destroy this game object
                Destroy(gameObject);
            }
            else
            {
                // Log when no matching puzzle goal type is found or component is missing
                Debug.Log("No matching puzzle goal type or mdoo_puzzleGoalBehavior component missing.");
            }
        }
    }
}