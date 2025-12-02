using System.Numerics;
using UnityEngine;

public class ControlManager : MonoSingleton<ControlManager>
{
    [SerializeField]
    private GameObject create_level_object;
    public void StartGame( LevelContainer container )
    {
        int count = container.level_objects.Count;
        for( int index = 0; index < count; index++ )
        {
            LevelObject level_object = container.level_objects[index];
            GameableObject gameable_object = level_object.gameable_object;
            GameObject game_object = Instantiate(gameable_object.prefab);
            game_object.transform.parent = create_level_object.transform;
            game_object.transform.position = level_object.pos;

            CameraAttachObject cam_obj = game_object.GetComponent<CameraAttachObject>();
            if (cam_obj)
            {
                CameraManager.Instance.addObject(cam_obj);
            }
        }
    }
}
