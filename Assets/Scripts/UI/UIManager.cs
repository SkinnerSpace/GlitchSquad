using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private List<GameObject> healthBarList = new List<GameObject>();  
    [SerializeField] private GameObject healthBarPrefab;              
    public List<Organ> organList = new List<Organ>();                

    private void Start()
    {
        GameObject[] organObjects = GameObject.FindGameObjectsWithTag("Organ");

        organList.Clear();

        foreach (GameObject organObject in organObjects)
        {
            Organ organComponent = organObject.GetComponent<Organ>();
            if (organComponent != null)
            {
                organList.Add(organComponent);  
            }
        }

        UpdateUI(organList);
    }

    private void UpdateUI(List<Organ> organs)
    {
        foreach (GameObject healthBar in healthBarList)
        {
            Destroy(healthBar);
        }
        healthBarList.Clear();

        foreach (Organ organ in organs)
        {
            GameObject newHealthBar = Instantiate(healthBarPrefab, this.transform);
            OrganHealthUI healthUI = newHealthBar.GetComponent<OrganHealthUI>();
            if (healthUI != null)
            {
                healthUI.SetOrgan(organ);  
            }
            healthBarList.Add(newHealthBar); 
        }
    }
}
