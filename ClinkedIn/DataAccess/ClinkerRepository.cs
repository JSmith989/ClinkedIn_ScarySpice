using ClinkedIn.Models;
using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.Data.SqlClient;
using Dapper;

namespace ClinkedIn.DataAccess
{
    public class ClinkerRepository
    {

        const string ConnectionString = "Server=localhost;Database=ClinkedIn;Trusted_Connection=True;";

        static List<Clinker> _clinkers = new List<Clinker>
        {
            new Clinker {Id = 1, Name = "Joe Exotic",  DaysRemaining = 1 },
            
        };

        public List<Clinker> GetAll()
        {
            var clinkers = new List<Clinker>();
            var sql = @"SELECT *
                        FROM Clinkers";
            using var db = new SqlConnection(ConnectionString);
            var results = db.Query<Clinker>(sql).ToList();
            
            return results;
        }

        public Clinker Get(int id)
        {
            var sql = @"SELECT *
                        FROM Clinkers
                        WHERE Id = @id";
            using var db = new SqlConnection(ConnectionString);
            var clinker = db.QueryFirstOrDefault<Clinker>(sql, new { id = id });

            return clinker;
        }

        public void Add(Clinker clinker)
        {
            var sql = @"INSERT INTO [dbo].[Clinkers] ([Name], [DaysRemaining])
			                   OUTPUT inserted.Id, inserted.Name, inserted.DaysRemaining
			                   VALUES(@Name, @DaysRemaining)";
            using var db = new SqlConnection(ConnectionString);
            var id = db.ExecuteScalar<int>(sql, clinker);
            clinker.Id = id;
        }

        /*

                

                public Dictionary<string, List<string>> GetAllInterests()
                {
                    var myReturnList = new Dictionary<string, List<string>>();
                    foreach (var clinker in _clinkers)
                    {
                        if (clinker.Interests != null) myReturnList.Add(clinker.Name, clinker.Interests);
                    }
                    return myReturnList;
                }

                public void removeInterest(int serialNumber, string interest)
                {
                    var clinker = Get(serialNumber);
                    clinker.Interests.RemoveAt(clinker.Interests.FindIndex(n => n.Equals(interest, StringComparison.OrdinalIgnoreCase)));
                }

                public void removeService(int serialNumber, string service)
                {
                    var clinker = Get(serialNumber);
                    clinker.Services.RemoveAt(clinker.Services.FindIndex(n => n.Equals(service, StringComparison.OrdinalIgnoreCase)));
                }

                public Dictionary<string, List<string>> GetAllServices()
                {
                    var allServices = new Dictionary<string, List<string>>();
                    foreach (var clinker in _clinkers)
                    {
                        if (clinker.Services != null) allServices.Add(clinker.Name, clinker.Services);
                    }
                    return allServices;
                }

                // Crew: Friends of Friends
                public Dictionary<Clinker, List<Clinker>> GetFriendsOfFriends(Clinker clinker)
                {
                    var friendsOfFriends = new Dictionary<Clinker, List<Clinker>>();
                    var clinkersFriends = clinker.Friends;
                    foreach (var clinkerfriend in clinkersFriends)
                    {
                        friendsOfFriends.Add(clinkerfriend, clinkerfriend.Friends);
                    }

                    return friendsOfFriends;
                }
                public void AddFriend(int serialNumber, int friendId)
                {
                    var clinker = Get(serialNumber);
                    var friend = Get(friendId);

                    if (!clinker.Friends.Contains(friend))
                    {
                        clinker.Friends.Add(friend);
                    }
                    if (!friend.Friends.Contains(clinker))
                    {
                        friend.Friends.Add(clinker);
                    }
                }

                public void AddEnemy(int serialNumber, int enemyId)
                {
                    var clinker = Get(serialNumber);
                    var enemy = Get(enemyId);

                    if (!clinker.Enemies.Contains(enemy))
                    {
                        clinker.Enemies.Add(enemy);
                    }
                    if (!enemy.Enemies.Contains(clinker))
                    {
                        enemy.Enemies.Add(clinker);
                    }
                }

                public void RemoveClinker(int serialNumber)
                {
                    var clinker = Get(serialNumber);

                    _clinkers.Remove(clinker);
                }

                public void RemoveFriend(int serialNumber, int friendSerial)
                {
                    var clinker = Get(serialNumber);
                    var friend = Get(friendSerial);

                    if (clinker.Friends.Contains(friend))
                    {
                        clinker.Friends.Remove(friend);
                    }
                    if (friend.Friends.Contains(clinker))
                    {
                        friend.Friends.Remove(clinker);
                    }
                }

                public void RemoveEnemy(int serialNumber, int enemySerial)
                {
                    var clinker = Get(serialNumber);
                    var enemy = Get(enemySerial);

                    if (clinker.Enemies.Contains(enemy))
                    {
                        clinker.Enemies.Remove(enemy);
                    }
                    if (enemy.Enemies.Contains(clinker))
                    {
                        enemy.Enemies.Remove(clinker);
                    }
                }

                public void RemoveAll(int serialNumber)
                {
                    for (var i = 0; i < _clinkers.Count; i++)
                    {
                        RemoveFriend(serialNumber, _clinkers[i].Id);
                        RemoveEnemy(serialNumber, _clinkers[i].Id);
                    }
                }

                public int DaysLeft(int serialNumber)
                {
                    var clinker = Get(serialNumber);
                    return clinker.DaysRemaining;
                }*/
    }

}
