using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnviromentValues", menuName = "Enviroment/Value")]
public class EnviromentValuesSO : ScriptableObject
{
    public string objectName;
    public float jumpForce;
    public float wallTime;

    //Buradaki verilen bir örnektir. Herbir çevresel etkileþim için ayrý SO oluþturulabilir.
    //Deðerler çoðaltýlabilir.
}

