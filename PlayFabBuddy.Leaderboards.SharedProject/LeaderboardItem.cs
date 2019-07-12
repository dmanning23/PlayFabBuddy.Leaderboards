using System;
using System.Collections.Generic;
using System.Text;

namespace PlayFabBuddy.Leaderboards
{
	/// <summary>
	/// A single item in the leaderboard.
	/// </summary>
	public class LeaderboardItem
	{
		public string DisplayName { get; set; }

		public int Position { get; set; }

		public int Score { get; set; }

		public LeaderboardItem(string name, int position, int score)
		{
			DisplayName = name;
			Position = position;
			Score = score;
		}
	}
}
