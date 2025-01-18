using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class DialogDisplay : MonoBehaviour
{
    [SerializeField] private float meetDistance;
    [SerializeField] private float displayTime;
    [SerializeField] private SpriteRenderer dialogDisplay;
    [SerializeField] private Sprite fullSprite;
    [SerializeField] private Sprite[] firstMeetSprites;

    private Player _player;
    private Organ _organ;
    private bool _meetDisplayed;
    private float _lastDisplayTime;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
        _organ = GetComponent<Organ>();
        _organ.OnMaxHealthReached += DisplayFull;
    }

    private void DisplayFull(Organ organ)
    {
        _lastDisplayTime = Time.time;
        dialogDisplay.sprite = fullSprite;
        dialogDisplay.gameObject.SetActive(true);
    }

    private void DisplayMeet()
    {
        _lastDisplayTime = Time.time;
        dialogDisplay.sprite = firstMeetSprites[Random.Range(0, firstMeetSprites.Length - 1)];
        dialogDisplay.gameObject.SetActive(true);
        _meetDisplayed = true;

        if(firstMeetSprites.Length > 1)
        {
            StartCoroutine(ResetMeet());
            IEnumerator ResetMeet()
            {
                yield return new WaitForSeconds(15);
                _meetDisplayed = false;
            }
        }
    }

    private void Update()
    {
        if (Vector3.Distance(_player.transform.position, transform.position) < meetDistance && !_meetDisplayed)
        {
            DisplayMeet();
        }

        if (Time.time > _lastDisplayTime + displayTime)
        {
            dialogDisplay.gameObject.SetActive(false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, meetDistance);
    }
}
