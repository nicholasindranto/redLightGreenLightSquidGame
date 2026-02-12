using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    // target si playernya
    public Transform playerTarget;

    // kecepatan pelurunya
    public float bulletSpeed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // movetowards tu kecepatannya konstan nggak kaya lerp yang dari cepet ke pelan
        transform.position = Vector3.MoveTowards(transform.position, playerTarget.position, bulletSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // cek kalau kenak player
        if (other.GetComponent<PlayerController>())
        {
            // maka player mati
            other.GetComponent<PlayerController>().Dead();

            // destroy bulletnya
            // kenapa gak pakai obj pooling? karna ni game bukan bullet hell / heaven
            // jadi nggak begitu banyak bullet yang mau di reuse... jarang jarang aja
            Destroy(gameObject);
        }
    }
}
