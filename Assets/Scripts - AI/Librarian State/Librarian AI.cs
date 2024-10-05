using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LibrarianAI : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform player;
    State currentState;

    private void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        currentState = new Idle(this.gameObject, agent, player);
    }

    private void Update()
    {
        currentState = currentState.Process();
    }

    private void OnGUI()
    {
        string content = currentState != null ? currentState.name.ToString() : "(No Current State)";
        GUILayout.Label($"<color='black'><size=40>{currentState}</size></color>");
    }
}
