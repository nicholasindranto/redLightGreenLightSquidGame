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

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChangeLight());
    }

    IEnumerator ChangeLight()
    {
        yield return new WaitForSeconds(Random.Range(minSec, maxSec + 1));

        if (isGreenLight) // kalo lagi ijo
        {
            // maka ganti ke merah
            isGreenLight = false;
            Debug.Log("lampu merah, player gak boleh jalan");

            // jalanin animasi ke merah
            animator.SetBool(greenLightAnim, false);
        }
        else
        {
            // kalo nggak, maka ganti ke ijo lagi
            isGreenLight = true;
            Debug.Log("lampu hijau, player boleh jalan");

            // jalanin animasi ke hijau
            animator.SetBool(greenLightAnim, true);
        }

        StartCoroutine(ChangeLight());
    }
}
