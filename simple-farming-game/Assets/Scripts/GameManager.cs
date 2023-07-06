using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int _width,
        _height;

    [SerializeField]
    private Tile _tilePrefab;

    [SerializeField]
    private Transform _camera;

    [SerializeField]
    private int[] _seeds = new int[3];

    private static int _selectedSeed;
    public static int SelectedSeed
    {
        get { return _selectedSeed; }
        set { _selectedSeed = value; }
    }

    private static int _playerScore;
    public static int PlayerScore
    {
        get { return _playerScore; }
        set { _playerScore = value; }
    }

    void Start()
    {
        GenerateGrid();
    }

    void Update()
    {
        GameObject.Find("PlayerScore").GetComponent<TextMeshProUGUI>().text =
            $"Score: {PlayerScore}";
    }

    void GenerateGrid()
    {
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                var spawnedTile = Instantiate(_tilePrefab, new Vector3(x, y), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";

                var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                spawnedTile.Init(isOffset);
            }
        }

        _camera.transform.position = new Vector3(
            (float)_width / 2 - 0.5f,
            (float)_height / 2 - 1f,
            -20
        );
    }

    public void SelectSeed(int seed)
    {
        switch (seed)
        {
            case 1:
                _selectedSeed = _seeds[0];
                break;
            case 2:
                _selectedSeed = _seeds[1];
                break;
            case 3:
                _selectedSeed = _seeds[2];
                break;
        }
        print($"Selected seed = {_selectedSeed}");
    }
}
