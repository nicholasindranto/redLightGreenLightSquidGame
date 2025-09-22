using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        // ambil dulu arah gerakannya
        // kanan kiri (A atau D, makanya horizontal)
        float moveX = Input.GetAxis("Horizontal");
        // depan belakang (W atau S, makanya vertical)
        float moveZ = Input.GetAxis("Vertical");

        // set ke vector 3 nya
        Vector3 move = new Vector3(moveX, 0, moveZ);

        // pakai charactercontroller buat nggerakinnya
        characterController.Move(move * movementSpeed * Time.deltaTime);

        // set angka perubahan di blend tree nya
        // kenapa magnitude? biar dia bisa nggabungin moveX sama moveZ jadi 1 garis
        float moveAnim = new Vector2(moveX, moveZ).magnitude;

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
}
