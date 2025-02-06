using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptObjLibrary : MonoBehaviour
{
    [SerializeField] private SessionData _sessionData;
    public SessionData SessionInfo { get { return _sessionData; } }
}
