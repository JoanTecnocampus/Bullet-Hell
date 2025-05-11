using UnityEngine;

public class PlayerShield : MonoBehaviour
{
    public GameObject shieldVisual;
    private bool isShieldActive = false;

    public void ActivateShield(float duration = 20f)
    {
        if (isShieldActive) return;

        isShieldActive = true;
        shieldVisual.SetActive(true);

        Invoke(nameof(DeactivateShield), duration);
    }

    private void DeactivateShield()
    {
        isShieldActive = false;
        shieldVisual.SetActive(false);
    }

    public bool IsShieldActive() => isShieldActive;
}
