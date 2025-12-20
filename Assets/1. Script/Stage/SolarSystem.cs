using System.Collections.Generic;
using UnityEngine;

public class SolarSystem : MonoBehaviour
{
    private Sun sun;
    private List<Planet> planets = new List<Planet>();

    private void Awake()
    {
        sun = GetComponentInChildren<Sun>();
    }

    public void AddPlanet(Planet pl)
    {
        if(!pl) return;
        planets.Add(pl);
    }

    private void Update()
    {
        for ( int i = 0; i<planets.Count; i++ )
        {
            Planet pl = planets[i];
            GameObject planet_object = pl.gameObject;
            GameObject sun_object = sun.gameObject;
            
            Vector3 sun_pos = sun_object.transform.position;

            planet_object.transform.RotateAround(sun_pos, pl.revDir.normalized, pl.revSpeed * Time.deltaTime);
        }
    }
}
