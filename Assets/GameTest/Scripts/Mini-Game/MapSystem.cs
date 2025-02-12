using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class MapSystem : MonoBehaviour
{
    [SerializeField] Text _points;

    [SerializeField] Image[] _mapSlots;
    [SerializeField] Sprite[] _mapSave;
    [SerializeField] Sprite _char;
    [SerializeField] Image[] _lifes;
    private int _lifePoints;

    public bool isAlive { get { return _lifePoints > 0; } }

    private void Awake()
    {
        _mapSave = new Sprite[_mapSlots.Length];

        for (int i = 0; i < _mapSlots.Length; i++)
        {
            _mapSave[i] = _mapSlots[i].sprite;
        }
    }

    public void Moving(DoorPosition position)
    {
        for (int i = 0; i < _mapSlots.Length; i++)
        {
            _mapSlots[i].sprite = _mapSave[i];
        }

        switch (position)
        {
            case DoorPosition.center:
                _mapSlots[2].sprite = _char;
                break;
            case DoorPosition.left:
                _mapSlots[1].sprite = _char;
                break;
            case DoorPosition.right:
                _mapSlots[3].sprite = _char;
                break;
            case DoorPosition.stone:
                _mapSlots[0].sprite = _char;
                break;
        }
    }

    public void ModifyPoints(float points)
    {
        _points.text = points.ToString();
    }
    public void RemoveLife()
    {
        _lifePoints -= 1;

        _lifes[_lifePoints].color = Color.gray;

        if(_lifePoints < 0)
            SceneManager.LoadScene(3);
    }
    public void SetLifePoints(int max)
    {
        _lifePoints = max;

        for (int i = 0; i < _lifePoints; i++)
        {
            _lifes[i].color = Color.white;
        }
    }
}
