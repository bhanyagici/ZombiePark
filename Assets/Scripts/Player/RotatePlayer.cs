using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlayer : MonoBehaviour
{
    public float sesivityX = 3;
    public float sensivityY = 3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RotatePlayerBody();
    }
    private float rotateY = 0;
    private float rotateX = 0;
    private void RotatePlayerBody()
    {
        rotateY += sensivityY * Input.GetAxis("Mouse Y")*-1;//Mouse'un Y eksenini al ve onu -1 ile �arp.
        //Mouse d�nd�k�e girilen veriyi �zerine ekle.
        rotateX += sesivityX * Input.GetAxis("Mouse X");//Mouse'un X  eksenini al.
        rotateY = Math.Clamp(rotateY, -80, 80);//Kelep�e Y�ntemi: Belirlenen de�er aras�nda rotasyon s�n�r� sa�l�yor.
        transform.eulerAngles = new Vector3(rotateY, rotateX, 0);//Transformumun rotasyonunu mousedan al�nan verilerle birle�tir.
    }
}
