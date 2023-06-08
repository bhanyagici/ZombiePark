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
        //GetAxis(): -1 ile 1 aras�nda bir de�erdir.
        //Horizontal: X pozisyonu idir.
        //Vertical: Z pozisyonu idir.
        //Time.deltaTime: �ki frame aras�nda ge�en s�reyi verir. Smooth bi ge�i� sa�lar.
        moveVector = new Vector3(Input.GetAxis("Horizontal")*speed*Time.deltaTime, 0, Input.GetAxis("Vertical")*speed*Time.deltaTime);
        moveVector= transform.TransformDirection(moveVector);//Player'�n bakt��� y�n� kabul et.
        if (!characterController.isGrounded)//E�er karakterim yerde de�il ise
        {
            moveVector.y -= Time.deltaTime * gravity;//KArakterime zemine kadar yava� yava� yer�ekimini uygula.
        }
        characterController.Move(moveVector);//O y�nde ilerle.
    }
}
