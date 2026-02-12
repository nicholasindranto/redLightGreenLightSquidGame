using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // reference ke character controllernya, komponen buat nggerakin karakternya
    private CharacterController characterController;

    // kecepatan gerakannya
    public float movementSpeed;

    // reference ke animatornya
    public Animator animator;

    // nama parameter move animatornya
    public readonly string moveAnimParam = "Move";

    // akses si script doll controllernya supaya tau lagi red atau green light
    public DollController dollControllerScript;

    // apakah udah mati
    public bool isDead = false;

    // reference ke player body sama ragdollnya
    public GameObject playerBody, playerRagdoll, playerRagdollHips;

    // biar pas mati cameranya tetep ngikutin si ragdollnya
    public CameraFollow camFollowScript;

    // untuk particle system
    public ParticleSystem bloodParticle;

    // audio ketika kena tembak
    private AudioSource audioSource;
    public AudioClip gotShotSFX;

    // apakah sudah menang?
    public bool hasBeenWon = false;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        // kalo udah mati gak mungkin gerak
        if (isDead) return;

        // ambil dulu arah gerakannya
        // kanan kiri (A atau D, makanya horizontal)
        float moveX = Input.GetAxis("Horizontal"); // bakalan dapat angka dari 0 sampai 1
        // depan belakang (W atau S, makanya vertical)
        float moveZ = Input.GetAxis("Vertical");

        // set ke vector 3 nya
        Vector3 move = new Vector3(moveX, 0, moveZ); // ini dapat arahnya

        // pakai charactercontroller buat nggerakinnya
        // kali movementSpeed biar makin cepet larinya
        // kali Time.deltaTime biar konstan kecepatan larinya nggak tergantung sama FPS
        characterController.Move(move * movementSpeed * Time.deltaTime);

        // kalau masih belum menang maka masih bisa ditembak
        if (!hasBeenWon)
        {
            // pas gerak cek dulu, kalau lagi gerak dan lagi red light, maka mati
            if (moveX != 0 || moveZ != 0)
            {
                if (!dollControllerScript.isGreenLight) // lagi merah
                {
                    // dollnya nembak
                    dollControllerScript.ShootPlayer(transform);
                    Debug.Log("you dead, red light!");
                }
            }
        }

        // set angka perubahan di blend tree nya
        // kenapa magnitude? biar dia bisa nggabungin moveX sama moveZ jadi 1 garis
        float moveAnim = new Vector2(moveX, moveZ).magnitude; // magnitude = panjang vector

        // set parameter animatornya
        animator.SetFloat(moveAnimParam, moveAnim);

        // kalau movex sama movez nya 0 alias diem gak gerak, maka return
        // biar dia gak auto ngadep depan lagi pas diem
        if (moveX == 0 && moveZ == 0) return;

        // arahnya kemana?
        // ambil derajatnya dulu pakai tangen, hasilnya dalam bentuk radian
        float direction = Mathf.Atan2(moveX, moveZ);

        // set rotasinya pakai quaternion euler
        // yang diputer cuma y nya doang
        // karna hasilnya radian maka perlu diubah lagi ke degree
        transform.rotation = Quaternion.Euler(0, direction * Mathf.Rad2Deg, 0);
    }

    public void Dead()
    {
        isDead = true;

        audioSource.PlayOneShot(gotShotSFX);

        // play particle systemnya
        bloodParticle.Play();

        // bikin biar ngikutin ragdoll
        camFollowScript.playerTarget = playerRagdollHips.transform;
        // bikin biar jadi ragdoll
        playerBody.SetActive(false);
        playerRagdoll.SetActive(true);
        Debug.Log("player mati");

        StartCoroutine(RestartGame());
    }

    // pas dead maka restart alias masuk ke scene ini lagi
    private IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(3);

        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
}
