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
    private float _colorTransitionTime = 3f;
    private float _elapsedDuration;

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
                _elapsedDuration = 0;
                percentageComplete = 0;
                _isFarmActive = false;
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
        _isFarmActive = true;
    }
}
