using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftHandUltimateState : LeftHandBaseState
{
    public LeftHandUltimateState(LeftHandStateManager manager) : base(manager) { }
    public override void EnterState()
    {
        Debug.Log("in ultimate state");
        stateManager.ultimateChargeBar.gameObject.SetActive(true);
        //resets charging bar to zero and sets its max value to whatever the spell is set to.
        stateManager.ultimateChargeBar.value = 0;
        stateManager.ultimateChargeBar.maxValue = stateManager.currentUltimateSpell.chargingTime;
        stateManager.casting = true;
        //stateManager.FPScontroller.canMove = false;
        //testing
        //stateManager.FPScontroller.gravity = 1;

    }
    public override void UpdateState()
    {
        stateManager.ultimateChargeBar.value += Time.deltaTime * 1;
        //if the charge bars value is greater than or equal to the ultimate spells charging time
        if (stateManager.ultimateChargeBar.value >= stateManager.currentUltimateSpell.chargingTime)
        {
            //starts the ultimate animation and spell casting
            stateManager.leftHandAnim.SetTrigger("Ultimate");
            stateManager.ultimateChargeBar.gameObject.SetActive(false);
            stateManager.ultimateChargeBar.value = 0;
        }
        //moves reticle
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
                        stateManager.currentUltimateSpell.spellReticle,
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
        stateManager.ultimateChargeBar.gameObject.SetActive(false);
        stateManager.ultimateChargeBar.value = 0;
        Destroy(stateManager.reticleInstance);
        stateManager.casting = false;
        //stateManager.FPScontroller.canMove = true;
        //stateManager.FPScontroller.gravity = 10;
    }
}
