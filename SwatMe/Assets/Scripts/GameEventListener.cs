using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Uses the Framework as outlined in Ryan Hipple's 2017 Unite talk.
*  https://www.youtube.com/watch?v=raQ3iHhE_Kk
*  https://github.com/roboryantron/Unite2017
*/
public class GameEventListener : MonoBehaviour
{
    [Tooltip("Event to register with.")]
    public GameEvent Event;

    [Tooltip("Response to invoke when Event is raised.")]
    public UnityEngine.Events.UnityEvent Response;

    private void OnEnable()
    {
        Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        Event.UnregisterListener(this);
    }

    public void OnEventRaised()
    {
        Response.Invoke();
    }
}
