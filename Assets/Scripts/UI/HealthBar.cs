using UnityEngine;

public class HealthBar : MonoBehaviour 
{
    Health healthComponent;
    [SerializeField] RectTransform foreground;
    private void Awake() 
    {
        healthComponent = GetComponentInParent<Health>();
    }
    private void Start() 
    {
        UpdateUI();   
    }

    private void OnEnable() 
    {
        healthComponent.onTakeDamage += UpdateUI;
    }

    private void OnDisable() 
    {
        healthComponent.onTakeDamage -= UpdateUI;
    }

    private void UpdateUI()
    {
        foreground.localScale = new Vector3(healthComponent.GetFraction(), 1, 1);
    }
}