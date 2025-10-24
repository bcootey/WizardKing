using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class PlayerDash : MonoBehaviour
{
    public Transform orientation;

    [Header("Dash Settings")]
    public float dashForce = 20f;
    public float dashUpwardForce = 0f;
    public float dashTime = 0.2f;
    [Header("Dash Effects")]
    public ParticleSystem dashEffect;
    [Header("Dash Charges")]
    public int maxDashCharges = 3;
    public float rechargeDuration = 3f; // Time it takes to fill 1 charge

    public int currentDashCharges;
    public float[] dashChargeTimers; // Individual timers per charge
    private Rigidbody rb;
    public bool isDashing = false;
    
    [Header("Hud Elements")]
    public Material[] chargeFillMaterials;

    public GameObject dashTest;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentDashCharges = maxDashCharges;

        dashChargeTimers = new float[maxDashCharges];
        for (int i = 0; i < maxDashCharges; i++)
            dashChargeTimers[i] = rechargeDuration; // Full = available
    }

    void Update()
    {
        // Dash Input
        if (Input.GetKeyDown(KeyCode.LeftShift) && currentDashCharges > 0)
        {
            Vector3 dashDir = GetDashDirection();
            StartCoroutine(Dash(dashDir));
        }

        if (isDashing)
        {
            dashTest.SetActive(true);
        }
        else
        {
            dashTest.SetActive(false);
        }
            
        HandleDashRecharge();
        UpdateDashChargeHud();
    }

    Vector3 GetDashDirection()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 inputDirection = orientation.forward * z + orientation.right * x;
        return inputDirection.sqrMagnitude > 0.01f ? inputDirection.normalized : orientation.forward;
    }

    IEnumerator Dash(Vector3 dashDirection)
    {
        isDashing = true;

        UseDashCharge(); // Update charges + timers
        PlayDashEffect();
        Vector3 dashVelocity = dashDirection * dashForce + Vector3.up * dashUpwardForce;
        float startTime = Time.time;

        while (Time.time < startTime + dashTime)
        {
            rb.linearVelocity = dashVelocity;
            yield return null;
        }

        isDashing = false;
        rb.linearVelocity = Vector3.zero;
    }

    void UseDashCharge()
    {
        // Find the last available charge (fully filled)
        for (int i = maxDashCharges - 1; i >= 0; i--)
        {
            if (dashChargeTimers[i] >= rechargeDuration)
            {
                dashChargeTimers[i] = 0f; // Used — now starts recharging
                currentDashCharges--;
                ShiftTimersAfterUse(i);
                break;
            }
        }
    }

    void ShiftTimersAfterUse(int usedIndex)
    {
        // Look for a timer that was recharging (i.e., partially filled)
        for (int i = usedIndex + 1; i < maxDashCharges; i++)
        {
            if (dashChargeTimers[i] > 0f && dashChargeTimers[i] < rechargeDuration)
            {
                // Transfer progress backward
                dashChargeTimers[usedIndex] = dashChargeTimers[i];
                dashChargeTimers[i] = 0f;
                break;
            }
        }
    }

    void HandleDashRecharge()
    {
        for (int i = 0; i < maxDashCharges; i++)
        {
            if (dashChargeTimers[i] < rechargeDuration)
            {
                dashChargeTimers[i] += Time.deltaTime;

                if (dashChargeTimers[i] >= rechargeDuration)
                {
                    dashChargeTimers[i] = rechargeDuration;
                    currentDashCharges = Mathf.Min(currentDashCharges + 1, maxDashCharges);
                }

                // Only recharge one at a time
                break;
            }
        }
    }
    
    void UpdateDashChargeHud()
    {
        for (int i = 0; i < chargeFillMaterials.Length; i++)
        {
            float normalized = dashChargeTimers[i] >= rechargeDuration
                ? 1f // fully charged
                : NormalizeValue(0f, rechargeDuration, dashChargeTimers[i]);

            chargeFillMaterials[i].SetFloat("_Health", normalized);
        }
    }

    private float NormalizeValue(float min, float max, float number)
    {
        return (number - min) / (max - min);
    }

    private void PlayDashEffect()
    {
        dashEffect.Play();
    }

}
