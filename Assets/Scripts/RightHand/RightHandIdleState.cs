using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHandIdleState : RightHandBaseState
{
    public RightHandIdleState(RightHandStateManager manager) : base(manager) { }
    public override void EnterState()
    {
        //makes sure it doesnt swing 2-3 times when 1 input is given next time sword is swung
        stateManager.rightHandAnim.ResetTrigger("Swing");
        stateManager.rightHandAnim.ResetTrigger("Swing2");
        stateManager.rightHandAnim.ResetTrigger("Swing3");
    }
    public override void UpdateState()
    {
        if (Input.GetMouseButtonDown(0))
        {
            stateManager.SetNextState(new RightHandSwing1(stateManager));
        }
    }
    public override void ExitState()
    {

    }
}
