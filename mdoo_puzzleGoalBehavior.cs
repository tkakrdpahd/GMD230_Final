using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mdoo_puzzleGoalBehavior : MonoBehaviour
{
    public int puzzleGoalType;
    private SpriteRenderer _spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void turnOn()
    {
        _spriteRenderer.color = Color.white; // Set the color to white
    }
}
