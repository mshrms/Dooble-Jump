using System;

namespace MyEvents
{
	public static class EventHolder
	{
		public static Action onGameStart;
		public static Action onGamePause;
		public static Action onReturnToMainMenu;
		//public static Action onControlsChange;

		public static Action onPlayerJump;
		public static Action onPlayerLand;

		public static Action onPlayerDeath;
	}
}
