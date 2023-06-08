using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public int speed = 3;
    const float gravity = 9.8f;
    CharacterController characterController;
    // Start is called before the first frame update
    void Start()
    {
       characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveCharacter();
    }
    Vector3 moveVector;
    private void MoveCharacter()
    {
        //GetAxis(): -1 ile 1 arasýnda bir deðerdir.
        //Horizontal: X pozisyonu idir.
        //Vertical: Z pozisyonu idir.
        //Time.deltaTime: Ýki frame arasýnda geçen süreyi verir. Smooth bi geçiþ saðlar.
        moveVector = new Vector3(Input.GetAxis("Horizontal")*speed*Time.deltaTime, 0, Input.GetAxis("Vertical")*speed*Time.deltaTime);
        moveVector= transform.TransformDirection(moveVector);//Player'ýn baktýðý yönü kabul et.
        if (!characterController.isGrounded)//Eðer karakterim yerde deðil ise
        {
            moveVector.y -= Time.deltaTime * gravity;//KArakterime zemine kadar yavaþ yavaþ yerçekimini uygula.
        }
        characterController.Move(moveVector);//O yönde ilerle.
    }
}
