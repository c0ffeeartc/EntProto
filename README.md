# EntProto
Entitas-CSharp "blueprints", depends on OdinInspector

---

### Depends on and tested with
  - Entitas 0.47.9 [releases](https://github.com/sschmid/Entitas-CSharp/releases)
  - Sirenix.Odin 1.0.6.0 [asset store](https://assetstore.unity.com/packages/tools/utilities/odin-inspector-and-serializer-89041)

### Installation
For use in your own project:
1. Ensure Entitas and other dependencies are installed
1. Copy EntProto folder into Assets
2. Remove EntProto/Examples folder
3. Done

For testing examples:
1. Create new Unity project
1. Install dependencies
1. Copy Preferences.properties into root of project
1. Copy EntProto folder into Assets
1. Open and run example scene

If you don't have Odin plugin it's possible to use other plugin that serializes List\<IComponent\> by changing few lines of code. [FullInspector](https://github.com/jacobdufault/fullinspector) has Free license GPLv3 for example.

### Usage
1. Inherit EnityProto
1. Inherit BaseProtoHolder, expose [SerializeField] with your entity prototypes, fill them in editor, pass them into Prototypes in Awake
1. Place inherited ProtoHolder script on top of Unity's Edit->ProjectSettings->ScriptExecutionOrder
1. Call BaseProtoHolder Clone and ApplyTo methods at runtime

Code part of these steps can be seen in example scripts [GameProtoHolder.cs](/Assets/EntProto/Examples/Scripts/GameProtoHolder.cs), [GameController.cs](/Assets/EntProto/Examples/Scripts/GameController.cs)

### Main concepts (theory)
  - List\<IComponent\> allows creating entity
  - It's possible to change entity completely by removing some components and adding other components, that's why there are Remove, Self and Shared fields
  - If Remove, Self and Shared fields have duplicates only top ones are processed other are skipped.

### Reasons to not use prototypes aka blueprints
  - __Serialization__. Plugins improve Unity's serialization, but even if all works well at current platform, no guarantee for others. It's like drugs, first feels great, then gives problems, later it's painful and hard to get off
  - __Refactoring__. After renaming component class, "blueprints" will lose these comoponents silently
  - __Version Control__. Creating entities through code usually is better version controlled than "blueprint" assets
  - __Cloning__. Unlike simple create functions, this method clones fields, which requires implementing IAfterCopy for any object that is non plain old data

Good usage of blueprints might be experimenting and prototyping with further transition into code.
