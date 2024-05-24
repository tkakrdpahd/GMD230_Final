using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mdoo_rotatingWall : MonoBehaviour
{
    private GameObject _gameObjact;
    public Material _matRotatingWallOn;
    public Material _matRotatingWallOff;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        rotatingParents();  // Call this function every frame to check for mouse input
    }

    void OnMouseOver()
    {
        // Assigns the current GameObject to _gameObjact
        _gameObjact = gameObject;
        _spriteRenderer.material = _matRotatingWallOn;//assign the _mat to _gameObjact
    }

    void OnMouseExit()
    {
        _spriteRenderer.material = _matRotatingWallOff;//assign the _mat to _gameObjact

        _gameObjact = null;
    }

    void rotatingParents()
    {
        // Rotate the parent object on the Z-axis based on mouse button input
        if (_gameObjact != null && Input.GetMouseButtonDown(0))
        {
            _gameObjact.transform.parent.Rotate(0, 0, 5);
        }
        else if (_gameObjact != null && Input.GetMouseButtonDown(1))
        {
            _gameObjact.transform.parent.Rotate(0, 0, -5);
        }
    }
}
