using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollController : MonoBehaviour
{
    // tiap berapa detik gantinya
    public float minSec, maxSec;

    // apakah lagi ijo?
    public bool isGreenLight = true;

    // reference ke animatornya
    public Animator animator;

    // nama parameter animatornya
    public readonly string greenLightAnim = "GreenLight";

    // bullet yang mau di spawn
    public Transform bulletPrefab;

    // posisi bulletnya di spawn
    public Transform shootPoint;

    // biar nembaknya cuma sekali
    private bool isHasBeenShoot = false;

    // untuk sfx nya
    public AudioSource audioSource;
    public AudioClip redLightSFX, greenLightSFX, shootSFX;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChangeLight());
    }

    private IEnumerator ChangeLight()
    {
        yield return new WaitForSeconds(Random.Range(minSec, maxSec + 1));

        if (isGreenLight) // kalo lagi ijo
        {
            // jalanin animasi ke merah
            animator.SetBool(greenLightAnim, false);

            audioSource.PlayOneShot(redLightSFX);

            yield return new WaitForSeconds(0.7f); // beri waktu sepersekian detik untuk player diem
            // maka ganti ke merah
            isGreenLight = false;
            Debug.Log("lampu merah, player gak boleh jalan");
        }
        else
        {
            // kalo nggak, maka ganti ke ijo lagi
            isGreenLight = true;
            Debug.Log("lampu hijau, player boleh jalan");

            audioSource.PlayOneShot(greenLightSFX);

            // jalanin animasi ke hijau
            animator.SetBool(greenLightAnim, true);
        }

        StartCoroutine(ChangeLight());
    }

    public void ShootPlayer(Transform playerTarget)
    {
        // kalau udah nembak maka skip
        if (isHasBeenShoot) return;

        audioSource.PlayOneShot(shootSFX);

        // munculin bulletnya
        Transform bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
        bullet.GetComponent<BulletController>().playerTarget = playerTarget;
        // udah nembak
        isHasBeenShoot = true;
    }
}
