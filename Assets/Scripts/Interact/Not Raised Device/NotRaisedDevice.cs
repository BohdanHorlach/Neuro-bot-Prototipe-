using UnityEngine;

public class NotRaisedDevice : MonoBehaviour
{
    [Header("Base Settings")]
    [SerializeField] private Rigidbody2D thisRigidBody;
    [SerializeField] private Collider2D thisCollider;
    [SerializeField] private SpriteRenderer displaedSprite;
    [SerializeField] private FinderObjectFromSpace checkGround;
    [SerializeField] private LevitationObject levitation;


    [Header("Source Prefab")]
    [SerializeField] private Device devicePrefab;


    private bool hasGround = false;


    private void OnEnable()
    {
        checkGround.OnChangeSpace += UpdateHasGround;
    }


    private void OnDisable()
    {
        checkGround.OnChangeSpace -= UpdateHasGround;
    }


    private void Start()
    {
        displaedSprite.sprite = devicePrefab.GetComponentInChildren<SpriteRenderer>().sprite;

        levitation.isLevitation = false;
    }


    private void Update()
    {
        TurnOffSimulation();
    }


    private void UpdateHasGround(bool value)
    {
        hasGround = value;
    }


    private void TurnOffSimulation()
    {
        if (hasGround == true)
        {
            Destroy(thisRigidBody);
            thisCollider.isTrigger = true;
            levitation.isLevitation = true;
        }
    }


    public Device GetDevice()
    {
        return devicePrefab;
    }
}