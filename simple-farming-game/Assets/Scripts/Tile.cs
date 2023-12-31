using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    // Colors
    [SerializeField]
    private Color _baseColor,
        _offsetColor,
        _plantColor;

    private Color _tileColor;

    // Other Components
    [SerializeField]
    private SpriteRenderer _renderer;

    [SerializeField]
    private GameObject _highlight;

    // Lerp variables
    [SerializeField]
    private bool _isFarmActive = false;

    [SerializeField]
    private float _colorTransitionTime;
    private float _elapsedDuration;

    private int _scoreToAdd;

    public void Init(bool isOffset)
    {
        _tileColor = isOffset ? _offsetColor : _baseColor;
        _renderer.color = _tileColor;
    }

    void Update()
    {
        if (_isFarmActive)
        {
            _elapsedDuration += Time.deltaTime;
            float percentageComplete = _elapsedDuration / _colorTransitionTime;
            _renderer.color = Color.Lerp(_plantColor, _tileColor, percentageComplete);
            if (percentageComplete >= 1)
            {
                _isFarmActive = false;
                GameManager.PlayerScore += _scoreToAdd;
                print(GameManager.PlayerScore);
                _elapsedDuration = 0;
                percentageComplete = 0;
            }
        }
    }

    void OnMouseEnter()
    {
        _highlight.SetActive(true);
    }

    void OnMouseExit()
    {
        _highlight.SetActive(false);
    }

    void OnMouseDown()
    {
        switch (GameManager.SelectedSeed)
        {
            case 1:
                _plantColor = Color.blue;
                _colorTransitionTime = 1f;
                _scoreToAdd = 1;
                break;
            case 2:
                _plantColor = Color.black;
                _colorTransitionTime = 5f;
                _scoreToAdd = 5;
                break;
            case 3:
                _plantColor = Color.gray;
                _colorTransitionTime = 10f;
                _scoreToAdd = 10;
                break;
        }
        _isFarmActive = true;
    }
}
