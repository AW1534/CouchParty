using UnityEngine;
using UnityEditor;
public class colorizer : EditorWindow
{
  Color color;
    [MenuItem("Window/colorizer")]
    public static void ShowWindow ()
{
    GetWindow<colorizer>("colorizer");
}
   void OnGUI ()
   {
      GUILayout.Label("Change the color of the selected objects", EditorStyles.boldLabel);
       
      color = EditorGUILayout.ColorField("pick yer color matey", color);

       if (GUILayout.Button("Accept"))
       {
          colorize();
       }
   }

   void colorize ()
   {
       Debug.Log("your wish do be my command doe");
           foreach (GameObject obj in Selection.gameObjects)
           {
               Renderer renderer = obj.GetComponent<Renderer>();
               if (renderer != null)
               {
                   renderer.sharedMaterial.color = color;
               }
           }
   }
}