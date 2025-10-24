using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftHandCastingState : LeftHandBaseState
{
    public LeftHandCastingState(LeftHandStateManager manager) : base(manager) { }
    public override void EnterState()
    {
        stateManager.leftHandAnim.SetTrigger("Cast");
        stateManager.casting = true;
        
    }
    public override void UpdateState()
    {
        if (stateManager.casting)
        {
            Ray ray = stateManager.mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, stateManager.wallLayer))
            {
                if (stateManager.reticleInstance == null)
                {
                    Debug.Log("reticle doesnt exist");
                    // Instantiate once at first valid hit point
                    stateManager.reticleInstance = GameObject.Instantiate(
                        stateManager.currentSpell.spellReticle,
                        hit.point,
                        Quaternion.identity
                    );
                }
                else
                {
                    // Follow hit point
                    stateManager.reticleInstance.transform.position = hit.point;
                    stateManager.spellSpawnPoint = hit.point;
                }
            }
        }
    }
    public override void ExitState()
    {
        Destroy(stateManager.reticleInstance);
        stateManager.casting = false;
    }
}
