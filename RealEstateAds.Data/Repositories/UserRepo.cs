using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RealEstateAds.Data.Contracts;
using RealEstateAdsEntities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;

namespace RealEstateAds.Data.Repositories
{
    public class UserRepo : IUserRepo
    {
        private readonly RealEstateAdsContext context;
        public UserRepo(RealEstateAdsContext context)
        {
            this.context = context;
        }

        #region User
        public User AddUser(User user)
        {
            if (user == null) return null;
            context.Users.Add(user);
            return user;
        }
        public User GetUser(string userName)
        {
            var user = context.Users.FirstOrDefault(a => a.Name == userName);
            return  user == null ? null : user;         
        }
        public User GetUser(int userId)
        {
            var user = context.Users.Where(u => u.Id == userId).FirstOrDefault();
            return user == null ? null : user;
        }
        public bool ValidUser(string name, string password) =>
            context.Users.Where(a => a.Name == name && a.Password == password).FirstOrDefault() == null ? false : true;
        
        public void DeleteUser(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            context.Users.Remove(user);
        }
        public bool UserExists(int userId) => context.Users.Any(a => a.Id == userId);
        public bool RateUser(int userId, string value)
        {
            var user = GetUser(userId);
            if (user == null) return false;

            Rating rate = new Rating
            {
                UserId = user.Id,
                Rate=int.Parse(value)
            };
            context.Ratings.Add(rate);
            Save();
            user.AverageRating = CalcAvgRate(user);
            return true;
        }
        public IEnumerable<User> GetUsers() => context.Users.ToList<User>();
        public string AuthenticateUser(string name, string password)
        {
            var user = context.Users.Where(a => a.Name == name && a.Password == password).FirstOrDefault();
            if (user == null) return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes("this is my custom Secret key for authnetication");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, name)
                }),
                IssuedAt=DateTime.Now,
                Expires = DateTime.Now.AddMinutes(15),                
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature),                
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        #endregion

        #region RealEstate
        public RealEstate AddRealEstate(int userId, RealEstate realEstate)
        {
            var user = GetUser(userId);
            if (realEstate == null) return null;
            realEstate.UserId = userId;
            realEstate.CreatedOn = DateTime.Now;
            if (realEstate.RentingPrice > 0) realEstate.CanBeRented = true;
            if (realEstate.PurchasePrice > 0) realEstate.CanBeBought = true;

            context.RealEstates.Add(realEstate);
            user.RealEstates.Add(realEstate);
            return realEstate;
        }
        public IEnumerable<RealEstate> GetAllRealEstate(int userId)
        {
            return context.RealEstates.Where(c => c.UserId == userId).ToList();
        }
        public RealEstate GetRealEstateFromUser(int userId, int realEstateId)
        {
            return context.RealEstates.Where(c => c.UserId == userId && c.Id == realEstateId).FirstOrDefault();
        }
        public RealEstate GetRealEstate(int realEstateId) =>
                            context.RealEstates.Include("Comments").Where(c => c.Id == realEstateId).FirstOrDefault();
        public RealEstate GetRealEstate(int userId, int realEstateId) =>
                        context.RealEstates.Where(c => c.Id == realEstateId && c.UserId == userId).FirstOrDefault();

        public bool DeleteRealEstateFromUser(int userId, int realEstateId)
        {
            var realEstate = GetRealEstate(userId, realEstateId);
            if (realEstate == null) return false;
            context.RealEstates.Remove(realEstate);
            return true;
        }
        public IEnumerable<RealEstate> GetRealEstates(string skip, string take)
        {
            if (skip == "") skip = "0";
            if (take == "") take = "10";
            return context.RealEstates.ToList<RealEstate>()
                .OrderByDescending(o => o.CreatedOn)
                .Skip(int.Parse(skip))
                .Take(int.Parse(take));
        }
        #endregion

        #region Comment
        public Comment AddComment(string username, Comment comment)
        {
            var user = GetUser(username);
            if (user == null) return null;
            comment.UserId = user.Id;
            comment.CreatedOn = DateTime.Now;
            context.Comments.Add(comment);
            user.Comments.Add(comment);

            var realestate = GetRealEstate(comment.RealEstateId);
            realestate.Comments.Add(comment);

            return comment;
        }
        public IEnumerable<Comment> CommentsByUser(int userid) => context.Comments.Where(a => a.UserId == userid).ToList();      
        public IEnumerable<Comment> GetCommentsRealEstate(int realEstateId, string skip, string take)
        {            
            if (skip == "") skip = "0";
            if (take == "") take = "10";
            return context.Comments.Include("Creator").Where(r => r.RealEstateId == realEstateId)
                .OrderByDescending(o => o.CreatedOn)
                .Skip(int.Parse(skip))
                .Take(int.Parse(take)).ToList();
        }
        public IEnumerable<Comment> GetCommentsFromUser(string userName, string skip, string take)
        {
            var user = GetUser(userName);
            if (user == null) return null;
            if (skip == "") skip = "0";
            if (take == "") take = "10";
            return context.Comments.ToList<Comment>()
                .Where(u => u.UserId == user.Id)
                .OrderByDescending(o => o.CreatedOn)
                .Skip(int.Parse(skip))
                .Take(int.Parse(take));
        }
        #endregion

        private double? CalcAvgRate(User user)
        {
            var ratingList = GetRatings(user.Id);
            if (ratingList.Count() == 0) return null;

            double x = 0;
            foreach (var rate in ratingList)
            {
                x += rate.Rate;
            }
            return x / ratingList.Count();
        }
        public IEnumerable<Rating> GetRatings(int userId) => context.Ratings.Where(r => r.User.Id == userId).ToList();
        public bool Save() => (context.SaveChanges() >= 0);
    }
}
