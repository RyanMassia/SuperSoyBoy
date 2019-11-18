using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(Level))]
public class LevelEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Save level"))
        {

        }
        // 1 Grabs a handle on the Level component and sets the Level GameObject’s position and rotation to 0, 
        Level level = (Level)target;
        level.transform.position = Vector3.zero;
        level.transform.rotation = Quaternion.identity;
        // 2 levelRoot as a reference to the Level GameObject, which is the parent of all level items. 
        var levelRoot = GameObject.Find("Level");
        // 3  new instances of LevelDataRepresentation and LevelItemRepresentation
        var ldr = new LevelDataRepresentation();
        var levelItems = new List<LevelItemRepresentation>();
        foreach (Transform t in levelRoot.transform)
        {
            // 4    Loops through every child Transform object 
            var sr = t.GetComponent<SpriteRenderer>();
            var li = new LevelItemRepresentation()
            {
                position = t.position,
                rotation = t.rotation.eulerAngles,
                scale = t.localScale};
            // 5  
            if (t.name.Contains(" "))
            {
                li.prefabName = t.name.Substring(0, t.name.IndexOf(" "));
            }
            else
            {
                li.prefabName = t.name;
            }
            // 6   
            if (sr != null)
            {
                li.spriteLayer = sr.sortingLayerName;
                li.spriteColor = sr.color;
                li.spriteOrder = sr.sortingOrder;
            }   // 7  
            levelItems.Add(li);
        }
        // 8 
        ldr.levelItems = levelItems.ToArray();
        ldr.playerStartPosition = GameObject.Find("SoyBoy").transform.position;
        // 9 
        var currentCamSettings = FindObjectOfType<CameraLerpToTransform>();

        if (currentCamSettings != null)
        {
            ldr.cameraSettings = new CameraSettingsRepresentation()
            {
                cameraTrackTarget = currentCamSettings.camTarget.name,
                cameraZDepth = currentCamSettings.cameraZDepth,
                minX = currentCamSettings.minX,
                minY = currentCamSettings.minY,
                maxX = currentCamSettings.maxX,
                maxY = currentCamSettings.maxY,
                trackingSpeed = currentCamSettings.trackingSpeed };
        }
        var levelDataToJson = JsonUtility.ToJson(ldr);
        var savePath = System.IO.Path.Combine(Application.dataPath, level.levelName + ".json");
        System.IO.File.WriteAllText(savePath, levelDataToJson);
        Debug.Log("Level saved to " + savePath);
    }
}
