﻿using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UserServices.Helpers;
using UserServices.Models;
using UserServices.Reponsitories.DbContext;
using UserServices.Reponsitories.Interfaces;

namespace UserServices.Reponsitories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _users = null;
        private readonly IFollowRepository _followRepository = null;

        public UserRepository(IOptions<AppSettings> settings)
        {
            var dbContext = new MongoDbContext(settings);
            _users = dbContext.Users;
            _followRepository = new FollowRepository(settings);
        }

        public User Add(User user)
        {
            _users.InsertOne(user);
            return user;
        }

        public User Delete(User document)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            List<User> users = _users.Find(x => true).ToList();
            return users;
        }

        public User GetById(string id)
        {
            var user = _users.Find(x => x.Id.Equals(id)).FirstOrDefault();

            if (user == null)
            {
                return user;
            }

            var followerCount = _followRepository.GetAllFollower(id).Count();
            var followingCount = _followRepository.GetAllFollowing(id).Count();
            user.FollowerCount = followerCount;
            user.FollowingCount = followingCount;
            return user;
        }

        public object GetUserStatistics(StatisticsFilter filter)
        {
            DateTimeFormatInfo format = new DateTimeFormatInfo();
            format.ShortDatePattern = "dd-MM-yyy";
            format.DateSeparator = "-";           

            Expression<Func<User, bool>> dateFilter =
                x => filter.From <= x.CreatedDate && x.CreatedDate < filter.To;

            var users = _users.AsQueryable()
                .Where(dateFilter.Compile())
                .OrderBy(x => x.CreatedDate)
                .Select(x => x)
                .ToList();

            var data = users.GroupBy(x => x.CreatedDate.ToString("dd-MM-yyy"))
                    .Select(x => new
                    {
                        name = x.Key,
                        value = x.Count()
                    }).ToList();

            var dummyData = Enumerable.Range(0, (filter.To - filter.From).Days)
                .Select(i => new
                {
                    name = filter.From.AddDays(i).ToString("dd-MM-yyy"),
                    value = 0
                }).ToList();

            var exceptData = data.Select(x => new
            {
                name = x.name,
                value = 0
            }).ToList();

            var result = data.Union(
                    dummyData.Except(exceptData)
                )
                .OrderBy(x => Convert.ToDateTime(x.name,format))
                .Select(x =>x);
            
            return new List<object>
            {
                new {
                    name = "User",
                    series = result
                }
            };
        }

        public IEnumerable<User> GetUsers(string search, int page)
        {
            // Search filter
            if (search == null || search.Trim() == "")
            {
                search = "";
            }
            Expression<Func<User, bool>> searchFilter;
            searchFilter = u => u.UserName.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0
                                || u.DisplayName.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0;

            var users = _users.AsQueryable()
                        .Where(searchFilter.Compile())
                        .Skip(12 * (page - 1))
                        .Take(12)
                        .ToList();
            return users;
        }

        public void IncreaseContributionPoint(string userId, int point)
        {
            var result = _users.FindOneAndUpdateAsync(
                u => u.Id == userId,
                Builders<User>.Update.Inc("contribution_point", point));
        }

        public User Update(User user)
        {
            var result = _users.ReplaceOne(u => u.Id.Equals(user.Id), user);
            if (!result.IsAcknowledged)
            {
                return null;
            }
            return user;
        }

        // Return false if username is exist
        public bool CheckUsername(string userId, string username)
        {
            var user = _users.Find(u => u.UserName == username && !u.Id.Equals(userId)).FirstOrDefault();         
            return user == null;
        }

        public bool BanAnUser(string userId)
        {
            _users.FindOneAndUpdate(
                Builders<User>.Filter.Eq(x => x.Id, userId),
                Builders<User>.Update.Set(x => x.Active, false));
            return true;
        }

        public bool UnBanAnUser(string userId)
        {
            _users.FindOneAndUpdate(
                Builders<User>.Filter.Eq(x => x.Id, userId),
                Builders<User>.Update.Set(x => x.Active, true));
            return true;
        }

        public object GetUserContributionPoint(string userId)
        {
            var user = _users.Find(x => x.Id.Equals(userId)).FirstOrDefault();
            return new
            {
                userId = user.Id,
                contributionPoint = user.ContributionPoint
            };
        }
    }
}
