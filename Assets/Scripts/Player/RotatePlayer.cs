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
        rotateY += sensivityY * Input.GetAxis("Mouse Y")*-1;//Mouse'un Y eksenini al ve onu -1 ile çarp.
        //Mouse döndükçe girilen veriyi üzerine ekle.
        rotateX += sesivityX * Input.GetAxis("Mouse X");//Mouse'un X  eksenini al.
        rotateY = Math.Clamp(rotateY, -80, 80);//Kelepçe Yöntemi: Belirlenen deðer arasýnda rotasyon sýnýrý saðlýyor.
        transform.eulerAngles = new Vector3(rotateY, rotateX, 0);//Transformumun rotasyonunu mousedan alýnan verilerle birleþtir.
    }
}
