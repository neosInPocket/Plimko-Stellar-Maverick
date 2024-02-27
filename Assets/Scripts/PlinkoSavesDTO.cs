using System;

[Serializable]
public class PlinkoSavesDTO : ICloneable
{
	public int currentPlinkoLevel;
	public int currentPinkoCoins;
	public bool upgrade1;
	public bool upgrade2;
	public bool volumeMusic;
	public bool volumeEffects;
	public bool train;
	public float timePlayed;
	public int clickedTime;
	public int deathTime;

	public object Clone()
	{
		var plinkoSaveData = new PlinkoSavesDTO();
		plinkoSaveData.currentPlinkoLevel = 1;
		plinkoSaveData.currentPinkoCoins = 100;
		plinkoSaveData.upgrade1 = false;
		plinkoSaveData.upgrade1 = false;
		plinkoSaveData.volumeMusic = true;
		plinkoSaveData.volumeEffects = true;
		plinkoSaveData.train = true;
		plinkoSaveData.timePlayed = 0;
		plinkoSaveData.clickedTime = 0;
		plinkoSaveData.deathTime = 0;
		return plinkoSaveData;
	}
}
