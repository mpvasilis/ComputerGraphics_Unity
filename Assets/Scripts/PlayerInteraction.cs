using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerInteraction : MonoBehaviour
{


    private byte currentBlock = 1;

    public int coins = 50;
    public int lifes = 3;
    public int blocks = 0;

    public int level;
    public int falldistance = 0;


    public Text coinsTXT;
    public Text lifesTXT;
    public Text blocksTXT;
    public Text msg;

    public bool reachedthetop = false;
    public float DisstanceToTheGround;

    public CharacterController controller;


    public void updateTexts()
    {
        coinsTXT.text = coins.ToString();
        lifesTXT.text = lifes.ToString();
        blocksTXT.text = blocks.ToString();

        if (coins == 0)
        {
            coinsTXT.color = Color.red;
        }
        else
        {
            coinsTXT.color = Color.white;
        }

        if (blocks == 0)
        {
            blocksTXT.color = Color.red;
        }
        else
        {
            blocksTXT.color = Color.white;
        }


        if (lifes == 1)
        {
            lifesTXT.color = Color.red;
        }
        else
        {
            lifesTXT.color = Color.white;
        }
    }


    void Start()
    {
        //DisstanceToTheGround = GetComponent<Collider>().bounds.extents.y;
        updateTexts();
    }

    void Update()
    {
        bool isplayerjumping = GameObject.Find("FPSController").GetComponent<FirstPersonController>().m_Jumping;

        if (controller.isGrounded)
        {
            level = (int)transform.position.y - 1;

        }
        else if (!controller.isGrounded && !isplayerjumping)
        {

            falldistance = level - (int)transform.position.y;
            print(falldistance);


        }

        if (falldistance != 0 && controller.isGrounded && !isplayerjumping && transform.position.y > 0)
        {
            if (falldistance > 0)
            {
                if (coins >= falldistance * 5)
                {
                    coins = coins - falldistance * 5;
                    StartCoroutine(ShowShortMessage(2, "You lost " + falldistance * 5 + " coins for falling down!"));
                }
                else
                {

                    lifes = lifes - 1;
                    StartCoroutine(ShowShortMessage(2, "You lost a life for falling down!"));

                    if (lifes == 0)
                    {
                        SceneManager.LoadScene("Game Over");
                        SceneManager.UnloadSceneAsync("Main World");
                    }
                }



                falldistance = 0;
                updateTexts();
            }
            else
            {
                falldistance = 0;
            }

        }


        if (Input.GetKeyDown(KeyCode.B))
        {
            if (coins >= 5)
            {
                blocks++;
                coins -= 5;
                updateTexts();
                StartCoroutine(ShowShortMessage(2, "Bought cube!"));

            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {

            Ray ray = new Ray(transform.position + transform.forward / 2, transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1f))
            {
                coins += 5;
                updateTexts();
            }
        }


        if (Input.GetKeyDown(KeyCode.Alpha1)) currentBlock = 1;
        if (Input.GetKeyDown(KeyCode.Alpha2)) currentBlock = 2;
        if (Input.GetKeyDown(KeyCode.Alpha3)) currentBlock = 3;
        if (Input.GetKeyDown(KeyCode.Alpha4)) currentBlock = 4;

        if (Input.GetMouseButtonDown(1))
        {

            Ray ray = new Ray(transform.position + transform.forward / 2, transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 10f))
            {
                Vector3 position = hit.point - hit.normal / 2;
                Destroy(new Vector3(Mathf.Floor(position.x), Mathf.Floor(position.y), Mathf.Ceil(position.z)), hit.collider.gameObject);

            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (blocks >= 1)
            {
                Ray ray = new Ray(transform.position + transform.forward / 2, transform.forward);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 10f))
                {
                    Vector3 position = hit.point + hit.normal / 2;
                    Create(new Vector3(Mathf.Floor(position.x), Mathf.Floor(position.y), Mathf.Ceil(position.z)), hit.collider.gameObject);
                    blocks--;
                    coins += 5;
                    updateTexts();

                }

            }
            else
            {
                StartCoroutine(ShowShortMessage(2, "Not enough cubes. Press B to buy!"));

            }


        }

        int size = GameObject.Find("Piece").GetComponent<Piece>().size;
        if (reachedthetop == false && transform.position.y > size)
        {
            //bool IsGrounded = Physics.Raycast(transform.position, Vector3.down, DisstanceToTheGround + 0.01f);

            if (controller.isGrounded)
            {
                reachedthetop = true;
                lifes++;
                coins += 100;
                updateTexts();
                StartCoroutine(ShowShortMessage(2, "You reached the top! You got 1 extra life and 100 coins!"));

            }
        }


    }

    void Destroy(Vector3 position, GameObject piece)
    {
        Piece p = piece.GetComponent<Piece>();
        if (p.canDestroy(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y), Mathf.RoundToInt(position.z)))
        {
            p.SetBlock(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y), Mathf.RoundToInt(position.z), 0);

        }
        else
        {
            StartCoroutine(ShowShortMessage(2, "You can not destroy this!"));

        }
    }
    void Create(Vector3 position, GameObject piece)
    {
        Piece p = piece.GetComponent<Piece>();

        p.SetBlock(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y), Mathf.RoundToInt(position.z), currentBlock);
    }

    IEnumerator ShowShortMessage(int seconds, string text)
    {
        msg.text = text;
        msg.enabled = true;
        yield
        return new WaitForSeconds(2);
        msg.enabled = false;
    }
}