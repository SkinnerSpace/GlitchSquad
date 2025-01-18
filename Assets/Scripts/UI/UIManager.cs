using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private List<GameObject> healthBarList = new List<GameObject>();
    [SerializeField] private GameObject healthBarPrefab;
    public List<Organ> organList = new List<Organ>();
    public int organFinishCount;
    public GameObject winGame;

    private void Start()
    {
        organFinishCount = 0;
        GameObject[] organObjects = GameObject.FindGameObjectsWithTag("Organ");

        organList.Clear();

        foreach (GameObject organObject in organObjects)
        {
            Organ organComponent = organObject.GetComponent<Organ>();
            if (organComponent != null)
            {
                organList.Add(organComponent);
                organComponent.OnMaxHealthReached += HandleOrganMaxHealthReached;
            }
        }

        UpdateUI(organList);
    }

    private void HandleOrganMaxHealthReached(Organ organ)
    {
        organFinishCount++;
        if (organFinishCount >= organList.Count)
        {
            StartCoroutine(Finish());

            IEnumerator Finish()
            {
                yield return new WaitForSeconds(2f);
                winGame.SetActive(true);
            }
        }
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

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }
}
