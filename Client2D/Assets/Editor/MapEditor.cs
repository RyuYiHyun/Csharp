using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif
public class MapEditor
{
#if UNITY_EDITOR

    //% ctrl, # shift, & alt
    [MenuItem("Tools/GenerateMap")]
    private static void GenerateMap()
    {
        if (EditorUtility.DisplayDialog("HelloWorld", "Create?", "Create", "Create!"))
        {
            GameObject[] gameObjects = Resources.LoadAll<GameObject>("Prefabs/Map");

            foreach (GameObject go in gameObjects)
            {
                Tilemap tm = Util.FindChild<Tilemap>(go, "Tilemap_Collision", true);

                if (tm == null)
                {
                    return;
                }

                using (var writer = File.CreateText($"Assets/Resources/Map/{go.name}.txt"))
                {
                    writer.WriteLine(tm.cellBounds.xMin);
                    writer.WriteLine(tm.cellBounds.xMax);
                    writer.WriteLine(tm.cellBounds.yMin);
                    writer.WriteLine(tm.cellBounds.yMax);

                    for (int y = tm.cellBounds.yMax; y >= tm.cellBounds.yMin; y--)
                    {
                        for (int x = tm.cellBounds.xMin; x <= tm.cellBounds.xMax; x++)
                        {
                            TileBase tile = tm.GetTile(new Vector3Int(x, y, 0));
                            if (tile != null)
                            {
                                writer.Write("1");
                            }
                            else
                            {
                                writer.Write("0");
                            }
                        }
                        writer.WriteLine();
                    }

                }
            }
        }
    }

#endif
}
