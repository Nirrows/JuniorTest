using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DoorSystem : MonoBehaviour
{
    [SerializeField] private Sprite _topSectionOpen, _lowSectionOpen;
    [SerializeField] private Sprite _topSectionClose, _lowSectionClose;

    [SerializeField] private Image[] _leftDoor, _rigthDoor, _centerDoor;

    [SerializeField, Range(1, 10)] private float _time;

    private void Start()
    {
        StartCoroutine(CR_FadeOut());
    }

    private IEnumerator CR_FadeOut()
    {
        float elapsedTime = 0f;
        while (elapsedTime < _time)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _centerDoor[0].sprite = _lowSectionOpen;

        elapsedTime = 0f;
        while (elapsedTime < _time)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _centerDoor[1].sprite = _topSectionOpen;
    }
}
