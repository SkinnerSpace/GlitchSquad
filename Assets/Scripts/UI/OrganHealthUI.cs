using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrganHealthUI : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;    
    private Organ organ;                             

    public void SetOrgan(Organ organToTrack)
    {
        organ = organToTrack;
        healthSlider.maxValue = organToTrack.maxHealth;
        healthSlider.value = organToTrack.currentHealth;
    }

    private void Update()
    {
        if (organ != null)
        {
            healthSlider.value = organ.currentHealth;
        }
    }
}
