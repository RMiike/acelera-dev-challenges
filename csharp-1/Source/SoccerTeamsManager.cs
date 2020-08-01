using System;
using System.Collections.Generic;
using System.Linq;
using Codenation.Challenge.Exceptions;
using Source;

namespace Codenation.Challenge
{
    public class SoccerTeamsManager : IManageSoccerTeams
    {
        private List<Team> _teams;

        public SoccerTeamsManager()
        {
            _teams = new List<Team>();
        }

        public void AddTeam(long id, string name, DateTime createDate, string mainShirtColor, string secondaryShirtColor)
        {
            if (TeamExists(id) != null)
                throw new UniqueIdentifierException();

            _teams.Add(new Team(id, name, createDate, mainShirtColor, secondaryShirtColor));
        }

        public void AddPlayer(long id, long teamId, string name, DateTime birthDate, int skillLevel, decimal salary)
        {
            var team = TeamExists(teamId);
            if (team == null)
                throw new TeamNotFoundException();

            if (PlayerExists(id) != null)
                throw new UniqueIdentifierException();

            team.AddNewPlayer(new Player(id, teamId, name, birthDate, skillLevel, salary));
        }

        public void SetCaptain(long playerId)
        {
            if (PlayerExists(playerId) == null)
                throw new PlayerNotFoundException();

            PlayerExists(playerId).SetCaptain();
        }

        public long GetTeamCaptain(long teamId)
        {
            var playerTeam = TeamExists(teamId);
            if (playerTeam == null)
                throw new TeamNotFoundException();

            var capitain = playerTeam.Players.Find(player => player.IsCaptain);

            return capitain == null
                    ? throw new CaptainNotFoundException()
                    : capitain.Id;
        }

        public string GetPlayerName(long playerId)
          => PlayerExists(playerId) == null
            ? throw new PlayerNotFoundException()
            : PlayerExists(playerId).Name;

        public string GetTeamName(long teamId)
          => TeamExists(teamId) == null
            ? throw new TeamNotFoundException()
            : TeamExists(teamId).Name;

        public List<long> GetTeamPlayers(long teamId)
        {
            var team = TeamExists(teamId);

            if (team == null)
                throw new TeamNotFoundException();

            return team
                    .Players
                    .OrderBy(players => players.Id)
                    .Select(players => players.Id)
                    .ToList();
        }

        public long GetBestTeamPlayer(long teamId)
        {
            var team = TeamExists(teamId);

            if (team == null)
                throw new TeamNotFoundException();

            return team
                    .Players
                    .OrderByDescending(players => players.SkillLevel)
                    .ThenBy(players => players.Id)
                    .First()
                    .Id;
        }

        public long GetOlderTeamPlayer(long teamId)
        {
            var team = TeamExists(teamId);

            if (team == null)
                throw new TeamNotFoundException();

            return team
                    .Players
                    .OrderBy(players => players.BirthDate)
                    .ThenBy(players => players.Id)
                    .First()
                    .Id;
        }

        public List<long> GetTeams()
          => _teams
            .Select(teams => teams.Id)
            .OrderBy(teamsid => teamsid)
            .ToList();

        public long GetHigherSalaryPlayer(long teamId)
        {
            var team = TeamExists(teamId);
            if (team == null)
                throw new TeamNotFoundException();

            return team.
                     Players
                     .OrderByDescending(players => players.Salary)
                     .ThenBy(players => players.Id)
                     .First()
                     .Id;
        }

        public decimal GetPlayerSalary(long playerId)
          => PlayerExists(playerId) != null
            ? PlayerExists(playerId).Salary
            : throw new PlayerNotFoundException();

        public List<long> GetTopPlayers(int top)
          => _teams
            .SelectMany(teams => teams.Players)
            .OrderByDescending(player => player.SkillLevel)
            .ThenBy(players => players.Id)
            .Take(top).Select(players => players.Id)
            .ToList();

        public string GetVisitorShirtColor(long teamId, long visitorTeamId)
        {
            var homeTeam = TeamExists(teamId);
            var visitorTeam = TeamExists(visitorTeamId);
            if (homeTeam == null || visitorTeam == null)
                throw new TeamNotFoundException();

            return homeTeam.MainShirtColor == visitorTeam.MainShirtColor
              ? visitorTeam.SecondaryShirtColor
              : visitorTeam.MainShirtColor;
        }

        private Team TeamExists(long id)
          => _teams
            .Find(team => team.Id == id);

        private Player PlayerExists(long id)
          => _teams
            .SelectMany(teams => teams.Players)
            .FirstOrDefault(player => player.Id == id);

    }
}
