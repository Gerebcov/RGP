using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [SerializeField]
    Dictionary<int, State> states = new Dictionary<int, State>();

    State currentSatate;
    protected int currentStateIndex;

    protected void InitState(int id, State state)
    {
        states.Add(id, state);
    }
    
    public void SetState(int id)
    {
        if(currentSatate != null)
        {
            currentSatate.End?.Invoke();
        }
        currentSatate = states[id];
        currentSatate.Start?.Invoke();

        currentStateIndex = id;
    }

    private void Update()
    {
        if (currentSatate != null)
            currentSatate.Update?.Invoke();
    }
}
