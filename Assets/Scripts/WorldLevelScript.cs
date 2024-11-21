using UnityEngine;

public class WorldLevelScript : MonoBehaviour {

	public int packIndex;
	public int worldIndex;
	public int levelIndex;

	public void WorldSelected()
	{
		LevelsParser.selectedPack = packIndex;
		LevelsParser.selectedWorld = worldIndex;
		LevelsParser.CurrentPack = packIndex;
		
		/*LevelsParser.CurrentPack = packIndex;
		LevelsParser.CurrentWorld = worldIndex;*/
		
		/*LevelsParser.CurrentPack = packIndex;
			LevelsParser*/

		if (packIndex < LevelsParser.levelParser.lastUnlockedPack)
		{
			LevelsParser.levelParser.WorldSelected();
		}
		else if (packIndex == LevelsParser.levelParser.lastUnlockedPack && worldIndex <= LevelsParser.levelParser.lastUnlockedWorld)
		{
			LevelsParser.levelParser.WorldSelected();
		}
	}

	public void LevelSelected()
	{
		LevelsParser.selectedLevel = levelIndex;

		if (LevelsParser.selectedPack < LevelsParser.levelParser.lastUnlockedPack)
		{
			LevelsParser.levelParser.LevelSelected();
		}
		else if (LevelsParser.selectedPack == LevelsParser.levelParser.lastUnlockedPack && LevelsParser.selectedWorld < LevelsParser.levelParser.lastUnlockedWorld)
		{
			LevelsParser.levelParser.LevelSelected();
		}
		else if (LevelsParser.selectedPack == LevelsParser.levelParser.lastUnlockedPack && LevelsParser.selectedWorld == LevelsParser.levelParser.lastUnlockedWorld && levelIndex <= LevelsParser.levelParser.lastUnlockedLevel)
		{
			LevelsParser.levelParser.LevelSelected();
		}
	}
}
