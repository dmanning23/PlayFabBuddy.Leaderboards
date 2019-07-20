using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlayFabBuddyLib.Leaderboards
{
	public interface IPlayFabLeaderboardService
	{
		/// <summary>
		/// Get the player's current high score
		/// </summary>
		/// <param name="highScoreList">The name of the list to check</param>
		/// <returns>The player's current high score.</returns>
		Task<int> GetHighScore(string highScoreList);

		/// <summary>
		/// Check if the player got a high score 
		/// </summary>
		/// <param name="highScoreList">The name of the list to check</param>
		/// <param name="points">The number of points the player got this round</param>
		/// <returns>True if greater than the player's current high score.</returns>
		Task<bool> IsHighScore(string highScoreList, int points);

		/// <summary>
		/// Update the player's score if they got a highscore.
		/// </summary>
		/// <param name="highScoreList">The name of the list to check</param>
		/// <param name="points">The number of points the player got this round</param>
		/// <returns>True if the player's score was updated. False if the number of points is less than their current high score.</returns>
		Task<bool> AddHighScore(string highScoreList, int points);

		/// <summary>
		/// Get a leaderboard.
		/// </summary>
		/// <param name="highScoreList">The leaderboard to get</param>
		/// <param name="num">The number of entries to get. Defaults to 10, max 100</param>
		/// <returns></returns>
		Task<IEnumerable<LeaderboardItem>> GetLeaderboard(string highScoreList, int num = 10);

		/// <summary>
		/// Get a leaderboard around the player
		/// </summary>
		/// <param name="highScoreList">The leaderboard to get</param>
		/// <param name="num">The number of entries to get. Defaults to 10, max 100</param>
		/// <returns></returns>
		Task<IEnumerable<LeaderboardItem>> GetLeaderboardAroundPlayer(string highScoreList, int num = 10);

		/// <summary>
		/// Get a leaderboard with only the player's friends.
		/// </summary>
		/// <param name="highScoreList">The leaderboard to get</param>
		/// <param name="num">The number of entries to get. Defaults to 10, max 100</param>
		/// <returns></returns>
		Task<IEnumerable<LeaderboardItem>> GetFriendLeaderboard(string highScoreList, int num = 10);

		/// <summary>
		/// Get a leaderboard with only the player's friends, around the player
		/// </summary>
		/// <param name="highScoreList">The leaderboard to get</param>
		/// <param name="num">The number of entries to get. Defaults to 10, max 100</param>
		/// <returns></returns>
		Task<IEnumerable<LeaderboardItem>> GetFriendLeaderboardAroundPlayer(string highScoreList, int num = 10);
	}
}
