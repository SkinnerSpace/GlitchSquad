using Bubbles;
using UnityEngine;

public class StomachCapacityController : MonoBehaviour
{
    public int newMaxCount;

    private Organ _organ;
    private BubblesRoot _bubblesRoot;

    private void Start()
    {
        _organ = GetComponent<Organ>();
        _bubblesRoot = FindObjectOfType<BubblesRoot>();
        _organ.OnMaxHealthReached += UpgradeRoot;
    }

    private void UpgradeRoot(Organ organ)
    {
        _bubblesRoot.UpgradeMaxCount(newMaxCount);
    }
}
