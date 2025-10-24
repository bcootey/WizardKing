using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EvilWizardBaseState : MonoBehaviour
{
    protected EvilWizardStateManager stateManager;
    public EvilWizardBaseState(EvilWizardStateManager stateManager)
    {
        this.stateManager = stateManager;
    }
    public abstract void EnterState();
    public abstract void ExitState();
    public abstract void UpdateState();
}
