using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MagnetWorld : MonoBehaviour
{
  public float Permeability = 0.05f;
  public float MaxForce = 10000.0f;

  public bool UseScaleForDebugDraw;

  void Start()
  {

  }

  Vector3 CalculateGilbertForce(Magnet magnet1, Magnet magnet2)
  {
    var m1 = magnet1.transform.position;
    var m2 = magnet2.transform.position;
    var r = m2 - m1;
    var dist = r.magnitude;
    var part0 = Permeability * magnet1.MagnetForce * magnet2.MagnetForce;
    var part1 = 4 * Mathf.PI * dist;

    var f = (part0 / part1);

    if (magnet1.MagneticPole == magnet2.MagneticPole)
      f = -f;

    return f * r.normalized;
  }

  void FixedUpdate()
  {
    var magnets = FindObjectsOfType<Magnet>();
    var magCount = magnets.Length;

    for (int i = 0; i < magCount; i++)
    {
      var m1 = magnets[i];
      if (m1.playerRigidbody == null)
        continue;

      var rb1 = m1.playerRigidbody;
      var accF1 = Vector3.zero;
      var accF2 = Vector3.zero;
      for (int j = 0; j < magCount; j++)
      {
        if (i == j)
          continue;

        var m2 = magnets[j];

        if (m2.MagnetForce < 5.0f)
          continue;

        if (m1.transform.parent == m2.transform.parent)
          continue;
        
        var f = CalculateGilbertForce(m1, m2);
        var magnetForce = m1.MagnetForce * m2.MagnetForce;

        accF1 += f * magnetForce;
      }

      if (accF1.magnitude > MaxForce)
      {
        accF1 = accF1.normalized * MaxForce;
      }
      rb1.AddForceAtPosition(accF1, m1.transform.position);
    }
  }
  
}
