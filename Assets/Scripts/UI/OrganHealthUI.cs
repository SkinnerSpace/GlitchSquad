using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrganHealthUI : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Image organImg;
    [SerializeField] private List<Sprite> organSpriteList;
    private Organ organ;

    public void SetOrgan(Organ organToTrack)
    {
        organ = organToTrack;
        healthSlider.maxValue = organToTrack.maxHealth;
        healthSlider.value = organToTrack.currentHealth;
        switch (organToTrack.organType)
        {
            case OrganType.Heart:
                organImg.sprite = organSpriteList[0];
                break;
            case OrganType.Liver:
                organImg.sprite = organSpriteList[1];
                break;
            case OrganType.Stomach:
                organImg.sprite = organSpriteList[2];
                break;
        }
    }

    private void Update()
    {
        if (organ != null)
        {
            healthSlider.value = organ.currentHealth;
        }
    }
}
