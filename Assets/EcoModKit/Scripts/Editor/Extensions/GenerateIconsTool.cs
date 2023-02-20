using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

using UnityEditor;
using UnityEditor.SceneManagement;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Eco.Client
{
    public class GenerateIconsTool
    {
        private static readonly string _sceneName = "EcoRoadSignIcons";
        private static readonly string _sceneRootParentName = "Items";
        private static readonly string _prefabTemplatePath = "Assets/IconPrefab.prefab";

        private static readonly string _iconsPath = "Assets/Images";
        private static readonly string _iconsFiletype = ".png";

        private static readonly string _iconPrefix = "RoadSign";
        private static readonly string _iconSuffix = "Image";

        [MenuItem("ModKit/Tools/Generate Icons")]
        private static void GenerateIcons()
        {
            var icons = ImportIcons();
            var scene = PrepareScene();

            var prefabTemplate = AssetDatabase.LoadAssetAtPath(_prefabTemplatePath, typeof(GameObject));

            CreateIcons(scene, icons, prefabTemplate);
            
        }

        private static void CreateIcons(Scene scene, List<TextureImporter> icons, Object prefabTemplate)
        {
            var rootObjects = scene.GetRootGameObjects();
            var itemsObjects = rootObjects.First(x => x.GetScenePath() == _sceneRootParentName);

            foreach (var icon in icons)
            {
                var iconGO = PrefabUtility.InstantiatePrefab(prefabTemplate, scene) as GameObject;
                PrefabUtility.UnpackPrefabInstance(iconGO, PrefabUnpackMode.Completely, InteractionMode.AutomatedAction);

                iconGO.transform.SetParent(itemsObjects.transform, false);
                iconGO.name = _iconPrefix + GetAssetName(icon.assetPath) + _iconSuffix;

                var imageComponent = iconGO.GetComponent<UnityEngine.UI.Image>();
                imageComponent.sprite = (Sprite)AssetDatabase.LoadAssetAtPath(icon.assetPath, typeof(Sprite));
            }
        }

        private static string GetAssetName(string AssetPath)
        {
            return AssetPath.Split('/').Last().Replace(_iconsFiletype, "");
        }

        private static Scene PrepareScene()
        {
            var scene = SceneManager.GetSceneByName(_sceneName);

            var rootObjects = scene.GetRootGameObjects();
            var itemsObjects = rootObjects.First(x => x.GetScenePath() == _sceneRootParentName);

            var itemChildren = itemsObjects.GetChildren();

            foreach (var item in itemChildren)
            {
                Object.DestroyImmediate(item);
            }

            return scene;
        }

        private static List<TextureImporter> ImportIcons()
        {
            AssetDatabase.Refresh();

            var iconsToGenerate = Directory.GetFiles(_iconsPath).Where(x => x.EndsWith(_iconsFiletype)).ToList();

            var assets = new List<TextureImporter>();
            foreach (var icon in iconsToGenerate)
            {
                var asset = AssetImporter.GetAtPath(icon) as TextureImporter;
                assets.Add(asset);

                asset.textureShape = TextureImporterShape.Texture2D;
                asset.textureType = TextureImporterType.Sprite;
                asset.alphaIsTransparency = true;

                asset.SaveAndReimport();
            }

            return assets;
        }
    }
}
