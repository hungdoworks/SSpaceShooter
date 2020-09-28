using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject bullet = null;
    private GameObject mainCamera;
    private GameObject shield;
    private float speed = 5;
    private bool isAlive = true;

    private float autoFireDelay = 0.1f;
    private float autoFireRate = 0.1f;
    private float bulletOffset = 1;

    private bool canActivateShield = true;
    private int shiledCooldownTime = 10;
    private float shieldActiveTime = 5.0f;

    private Vector2 touchPrevPos;

    private GameManager gameManager;

    private AudioSource audioPlayer;
    public AudioClip sfxActivateShield;

    private float curSpeedX = 0.0f;
    private float curSpeedY = 0.0f;
    private Vector3 prevPos;
    private Vector3 tempPos;
    private bool isFlyingForward = true;

    float controlSensitive = 15.0f;
    float directionSensitive = 40.0f;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.Find("Main Camera");
        shield = transform.Find("Shield").gameObject;

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        audioPlayer = GetComponent<AudioSource>();

        InvokeRepeating("AutoFire", autoFireDelay, autoFireRate);
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive && gameManager.IsGamePaused() == false)
        {
            if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                UpdateInputMouse();
            }
            else if (Application.platform == RuntimePlatform.Android)
            {
                UpdateInputAndroid();
            }

            if (curSpeedX != 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 20 * -curSpeedX);
            }
            else if (curSpeedY != 0)
            {
                transform.rotation = Quaternion.Euler(30 * -curSpeedY, 0, 0);
            }

            Vector3 moveHorizontal = new Vector3(Time.deltaTime * speed * curSpeedX, 0, 0);
            Vector3 moveVertical = new Vector3(0, Time.deltaTime * speed * curSpeedY, 0);

            transform.position += moveHorizontal;
            mainCamera.transform.position += moveHorizontal;

            transform.position += moveVertical;
            mainCamera.transform.position += moveVertical;

            // Reset rotation of player if does have any input
            if (isFlyingForward)
            {
                if (curSpeedX != 0)
                    curSpeedX += curSpeedX > 0 ? -0.05f : 0.05f;

                if (curSpeedY != 0)
                    curSpeedY += curSpeedY > 0 ? -0.05f : 0.05f;
            }
        }
    }

    private void UpdateInputMouse()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            prevPos = Input.mousePosition;
            isFlyingForward = false;
        }
        else if (Input.GetButton("Fire1"))
        {
            Vector3 curPos = Input.mousePosition;

            float x = curPos.x - prevPos.x;
            float y = curPos.y - prevPos.y;

            if (tempPos.x == curPos.x && tempPos.y == curPos.y)
            {
                // Do nothing

                prevPos = curPos;
            }
            else
            {
                float absX = Mathf.Abs(x);
                float absY = Mathf.Abs(y);

                // Set absX greater than n to avoid control too sensitive
                if (absX > controlSensitive)
                {
                    if ((curSpeedX > 0 && x < 0) ||
                        curSpeedX < 0 && x > 0)
                    {
                        curSpeedX = 0;
                    }

                    if (curSpeedX <= 1.0f && curSpeedX >= -1.0f)
                        curSpeedX += x > 0 ? 0.05f : -0.05f;

                    // Want to sudden change direction of player
                    if (absX > directionSensitive && absY < directionSensitive)
                    {
                        curSpeedY = 0;
                    }
                }

                // Set absY greater than n to avoid control too sensitive
                if (absY > controlSensitive)
                {
                    if ((curSpeedY > 0 && y < 0) ||
                        curSpeedY < 0 && y > 0)
                    {
                        curSpeedY = 0;
                    }

                    if (curSpeedY < 1.0f && curSpeedY >= -1.0f)
                        curSpeedY += y > 0 ? 0.05f : -0.05f;

                    // Want to sudden change direction of player
                    if (absY > directionSensitive && absX < directionSensitive)
                    {
                        curSpeedX = 0;
                    }
                }
            }

            tempPos = curPos;
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            prevPos.Set(0, 0, 0);
            isFlyingForward = true;
        }
    }

    private void UpdateInputAndroid()
    {
        if (Input.touchCount > 0)
        {
            Touch currentTouch = Input.GetTouch(0);

            if (currentTouch.phase == TouchPhase.Began)
            {
                touchPrevPos = currentTouch.position;
                isFlyingForward = false;
            }
            else if (currentTouch.phase == TouchPhase.Moved)
            {
                Vector2 touchCurPos = currentTouch.position;

                float x = touchCurPos.x - touchPrevPos.x;
                float y = touchCurPos.y - touchPrevPos.y;

                float absX = Mathf.Abs(x);
                float absY = Mathf.Abs(y);

                // Set absX greater than n to avoid control too sensitive
                if (absX > controlSensitive)
                {
                    if ((curSpeedX > 0 && x < 0) ||
                        curSpeedX < 0 && x > 0)
                    {
                        curSpeedX = 0;
                    }

                    if (curSpeedX <= 1.0f && curSpeedX >= -1.0f)
                        curSpeedX += x > 0 ? 0.05f : -0.05f;

                    // Want to sudden change direction of player
                    if (absX > directionSensitive && absY < directionSensitive)
                    {
                        curSpeedY = 0;
                    }
                }

                // Set absY greater than n to avoid control too sensitive
                if (absY > controlSensitive)
                {
                    if ((curSpeedY > 0 && y < 0) ||
                        curSpeedY < 0 && y > 0)
                    {
                        curSpeedY = 0;
                    }

                    if (curSpeedY < 1.0f && curSpeedY >= -1.0f)
                        curSpeedY += y > 0 ? 0.05f : -0.05f;

                    // Want to sudden change direction of player
                    if (absY > directionSensitive && absX < directionSensitive)
                    {
                        curSpeedX = 0;
                    }
                }

            }
            else if (currentTouch.phase == TouchPhase.Stationary)
            {
                Vector2 touchCurPos = currentTouch.position;

                touchPrevPos = touchCurPos;
            }
            else if (currentTouch.phase == TouchPhase.Ended)
            {
                touchPrevPos.Set(0, 0);
                isFlyingForward = true;
            }
        }
    }

    private void AutoFire()
    {
        Instantiate(bullet, transform.position + new Vector3(0, 0, bulletOffset), bullet.transform.rotation);
    }

    public void ActivateShield()
    {
        if (CanActivateShield())
        {
            shield.SetActive(true);

            StartCoroutine("DeactivateShield");

            audioPlayer.PlayOneShot(sfxActivateShield);
        }
    }

    public IEnumerator DeactivateShield()
    {
        yield return new WaitForSeconds(shieldActiveTime);

        shield.SetActive(false);
        canActivateShield = false;

        StartCoroutine("ShieldCooldown");
    }

    public IEnumerator ShieldCooldown()
    {
        yield return new WaitForSeconds(shiledCooldownTime);

        canActivateShield = true;
    }

    public bool CanActivateShield()
    {
        return isAlive && shield.activeInHierarchy == false && canActivateShield;
    }
}
