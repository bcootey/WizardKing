using UnityEngine;

public class PlayerParry : MonoBehaviour
{
    public Animator parryLegAnim;
    public GameObject parryEffect;
    public Transform parryEffectSpawnPoint;
    private bool parriedThisAttempt;
    [Header("Parry Gains")]
    public int parryHealthGain;
    public int parryManaGain;
    void Update()
    {
        if (!GameStateManager.CanAcceptGameplayInput)
            return;
        
        if (Input.GetKeyDown(KeyCode.F))
        {
            parryLegAnim.SetTrigger("Parry");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IParryable>(out var parryable))
        {
            if (parryable.IsParryable)
            {
                parryable.IsParried = true;
                parryable.SetIsNotParryable();
                parriedThisAttempt = true;
            }

        }
        //if the parry happened
        if (parriedThisAttempt)
        {
            Instantiate(parryEffect, parryEffectSpawnPoint.position, Quaternion.identity);
            OnParry();
            parriedThisAttempt = false;
        }
            
    }

    private void OnParry()
    {
        HitStop.instance.Stop(.1f); 
        Health.instance.IncreaseHealth(parryHealthGain);
        Mana.instance.IncreaseMana(parryManaGain);
    }
    
}
