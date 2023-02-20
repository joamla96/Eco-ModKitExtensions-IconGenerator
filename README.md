How to install:

Download Zip of this project, and extract the Assets folder into your Mods folder with the EcoModKit.

Open "Assets\EcoModKit\Scripts\Editor\Extensions\GenerateIconsTool.cs" with any text editor and IDE of choice

Edit the strings at the top, to fit your project.
- `_sceneName`: should match the name of the scene will contain your icons *NOTE: The script will clear the scene for existing things in this scene. Make sure your icons are in a unique scene*
- `_sceneRootParent`: name should be left as default
- `_prefabTemplatePath`: points to the prefab which Icons will be based on. A default is included
- `_iconsPath`: path to the folder where your icon files are
- `_iconsFiletype`: file extension of your images
- `_iconPrefix`: prefix your icons with some text to make them unique to your project
- `_iconSuffix`: suffix your icons with some text, should allways end with Image.

Once installed, the tool is accessable via the menu inside the editor under
`ModKit > Tools > Generate Icons`
