using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamDuvar : Environment
{
    private float time;
    protected override void EtkilesimeGir(Collider player)
    {
        time = enValueSO.wallTime;
        StartCoroutine(KýrýlanCam(time));
    }

    private IEnumerator KýrýlanCam(float time)
    {
        //CamDuvar Kodlarý Eklenebilir.
        //Animasyon, Ses ve VFX efektleri eklenecektir.
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
