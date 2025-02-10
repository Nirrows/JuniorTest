using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doublsb.Dialog;

public abstract class SceneScript : MonoBehaviour
{
    [SerializeField] protected DialogManager DialogManager;
    public abstract void StartScene();
}
