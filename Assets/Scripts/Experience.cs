using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Experience : MonoBehaviour
{
    public float experienceToLevelUp = 400;
    [Header("HUD elements")]
    public Slider experienceSlider;
    public TextMeshProUGUI experienceText;

    void Start()
    {
        experienceSlider.value = PlayerStats.instance.experience;
        experienceSlider.minValue = 0;
        experienceSlider.maxValue = experienceToLevelUp;
        UpdateExperienceHUD();
    }
    public void TryToLevelUp() //attempts to level up the player if experience is over threshold
    {
        if (PlayerStats.instance.experience >= experienceToLevelUp)
        {
            LevelUp();
        }
    }

    private void UpdateExperienceHUD()
    {
        experienceSlider.value = PlayerStats.instance.experience;
        experienceText.text = "Experience: " + PlayerStats.instance.experience + "/" + experienceToLevelUp;
    }
    private void LevelUp()
    {
        PlayerStats.instance.experience -= experienceToLevelUp;
        PlayerStats.instance.skillPoints += 1;
        UpdateExperienceThreshold();
        UpdateExperienceHUD();
    }

    private void UpdateExperienceThreshold() // used to increase the amount of experience required to level up every level
    {
        experienceToLevelUp = experienceToLevelUp * 1.3f;
        experienceSlider.maxValue = experienceToLevelUp;
    }

    public void GainExperience(float amount)
    {
        PlayerStats.instance.experience += amount;
        UpdateExperienceHUD();
    }
    
    
}
