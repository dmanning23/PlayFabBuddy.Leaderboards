﻿using PlayFab.ClientModels;
using PlayFabBuddyLib.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayFabBuddyLib.Leaderboards
{
	public class PlayFabLeaderboardService : IPlayFabLeaderboardService
	{
		#region Properties

		IPlayFabClient _playfab;
		IPlayFabAuthService _auth;

		#endregion //Properties

		#region Methods

		public PlayFabLeaderboardService(IPlayFabClient playfab, IPlayFabAuthService auth)
		{
			_playfab = playfab;
			_auth = auth;
		}

		public async Task<int> GetHighScore(string highScoreList)
		{
			try
			{
				var result = await _playfab.GetPlayerStatisticsAsync(new GetPlayerStatisticsRequest()
				{
					StatisticNames = new List<string> { highScoreList },
				});

				if (result.Error != null)
				{
					//An error occurred
					return 0;
				}

				var stat = result.Result.Statistics.FirstOrDefault(x => x.StatisticName == highScoreList);
				return (stat?.Value ?? 0);
			}
			catch (Exception)
			{
			}
			return 0;
		}

		public async Task<bool> IsHighScore(string highScoreList, int points)
		{
			var result = await _playfab.GetPlayerStatisticsAsync(new GetPlayerStatisticsRequest()
			{
				StatisticNames = new List<string> { highScoreList }
			});

			if (result.Error != null)
			{
				//An error occurred
				return false;
			}

			var stat = result.Result.Statistics.FirstOrDefault(x => x.StatisticName == highScoreList);
			return (null != stat && stat.Value < points);
		}

		public async Task<bool> SetStat(string highScoreList, int points)
		{
			var result = await _playfab.UpdatePlayerStatisticsAsync(new UpdatePlayerStatisticsRequest()
			{
				Statistics = new List<StatisticUpdate> {
					new StatisticUpdate() {
						StatisticName = highScoreList,
						Value = points,
					}
				}
			});

			return (result.Error == null);
		}

		public async Task<IEnumerable<LeaderboardItem>> GetLeaderboard(string highScoreList, int num = 10)
		{
			var result = await _playfab.GetLeaderboardAsync(new GetLeaderboardRequest()
			{
				MaxResultsCount = num,
				StatisticName = highScoreList,
			});

			if (null != result.Error)
			{
				return PopulateLeaderboardItems(result.Result.Leaderboard);
			}
			else
			{
				return new List<LeaderboardItem>();
			}
		}

		public async Task<IEnumerable<LeaderboardItem>> GetLeaderboardAroundPlayer(string highScoreList, int num = 10)
		{
			var result = await _playfab.GetLeaderboardAroundPlayerAsync(new GetLeaderboardAroundPlayerRequest()
			{
				MaxResultsCount = num,
				StatisticName = highScoreList,
			});

			if (null == result.Error)
			{
				return PopulateLeaderboardItems(result.Result.Leaderboard);
			}
			else
			{
				return new List<LeaderboardItem>();
			}
		}

		public async Task<IEnumerable<LeaderboardItem>> GetFriendLeaderboard(string highScoreList, int num = 10)
		{
			var result = await _playfab.GetFriendLeaderboardAsync(new GetFriendLeaderboardRequest()
			{
				MaxResultsCount = num,
				StatisticName = highScoreList,
			});

			if (null == result.Error)
			{
				return PopulateLeaderboardItems(result.Result.Leaderboard);
			}
			else
			{
				return new List<LeaderboardItem>();
			}
		}

		public async Task<IEnumerable<LeaderboardItem>> GetFriendLeaderboardAroundPlayer(string highScoreList, int num = 10)
		{
			var result = await _playfab.GetFriendLeaderboardAroundPlayerAsync(new GetFriendLeaderboardAroundPlayerRequest()
			{
				MaxResultsCount = num,
				StatisticName = highScoreList,
			});

			if (null != result.Error)
			{
				return PopulateLeaderboardItems(result.Result.Leaderboard);
			}
			else
			{
				return new List<LeaderboardItem>();
			}
		}

		private List<LeaderboardItem> PopulateLeaderboardItems(List<PlayerLeaderboardEntry> entries)
		{
			return entries.Select(x => new LeaderboardItem(x.DisplayName, x.Position, x.StatValue)).ToList();
		}

		#endregion //Methods
	}
}
