using System.Collections;
using System.Collections.Generic;
using UnityEngine;


 /* Uses the Framework as outlined in Ryan Hipple's 2017 Unite talk.
 *  https://www.youtube.com/watch?v=raQ3iHhE_Kk
 *  https://github.com/roboryantron/Unite2017
 */


[CreateAssetMenu]
public class BugRuntimeSet : RuntimeSet<Bug>
{
    [Tooltip("Event to raise when the list is empty.")]
    [SerializeField] private GameEvent EmptyEvent;

    public override void Remove(Bug thing)
    {
        base.Remove(thing);

        if(Items.Count == 0)
        {
            EmptyEvent.Raise();
        }
    }
}
