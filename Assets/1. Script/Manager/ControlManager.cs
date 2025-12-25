using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using NUnit.Framework.Constraints;
using UnityEngine;

public class ControlManager : MonoBehaviour
{
    [SerializeField]
    private string createObjectTagName;
    private GameObject createObject;

    [SerializeField]
    private string solarSystemObjectTagName;
    private GameObject solarSystemObject;

    private void Awake()
    {
        createObject = GameObject.FindGameObjectWithTag(createObjectTagName);
        solarSystemObject = GameObject.FindGameObjectWithTag(solarSystemObjectTagName);
        SettingGame(LevelSystem.Instance.CurContainer);

        EventManager.Instance.AddEventListner("Clear", (e,args) =>
        {
            ClearLevel();
        });

        EventManager.Instance.AddEventListner("Fail", (e,args) =>
        {
            Over();
        });

        EventManager.Instance.AddEventListner("Clean", (e,args) =>
        {
            Clear();
        });
    }

    public void SettingGame( LevelContainer container )
    {
        int count = container.level_objects.Count;
        for( int index = 0; index < count; index++ )
        {
            LevelObject level_object = container.level_objects[index];
            GameableObject gameable_object = level_object.gameable_object;
            GameObject game_object = Instantiate(gameable_object.prefab);

            game_object.transform.parent = createObject.transform;
            game_object.transform.position = level_object.pos;

            CameraAttachObject cam_obj = game_object.GetComponent<CameraAttachObject>();
            if (cam_obj)
            {
                CameraManager.Instance.addObject(cam_obj);
            }
            
            Planet planet = game_object.GetComponent<Planet>();
            if (planet)
            {
                if (!solarSystemObject) return;
                SolarSystem solarSystem = solarSystemObject.GetComponent<SolarSystem>();
                solarSystem.AddPlanet(planet);

                planet.SettingPlanet(gameable_object as PlaneObject);
            }

            Ship ship = game_object.GetComponent<Ship>();
            if (ship)
            {
                ship.Setting(gameable_object as ShipObject);
            }
        }
        for ( int index = 0; index < 4; index++ )
        {
            CameraManager.Instance.ActiveCamera(index);
        }
    }

    private void ClearLevel()
    {
        Time.timeScale = 0.3f;
        StartCoroutine(End());
    }

    private void Over()
    {
        Time.timeScale = 0.3f;
        StartCoroutine(End());
    }

    private void Clear()
    {
        foreach( Transform trans in createObject.transform )
        {
            Destroy(trans.gameObject);
        }
    }

    private IEnumerator End()
    {
        yield return new WaitForSeconds(5f);
        Time.timeScale = 1f;
    }
}
