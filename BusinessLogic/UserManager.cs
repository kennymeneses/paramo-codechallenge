using Microsoft.Extensions.Logging;
using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class UserManager : IUserManager
    {
        ILogger<UserManager> _logger;
        public List<User> Users;
        StreamReader _reader;
        
        public UserManager(ILogger<UserManager> logger)
        {
            _logger = logger;   
        }

        public async Task<Result> CreateUser(User user)
        {
            try
            {
                _logger.LogInformation("A request for create user begins.");

                var result = new Result();

                user.Money = GetUserMoney(user);
                user.Email = NormalizeEmail(user.Email);
                var userList = await GetUsersFromFile();
                ValidateUserInfo(user, userList);
                await AddUserToFile(user);

                result.IsSuccess = true;
                result.Errors = null;

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("An error ocurred: " + ex.Message);
                throw;
            }
        }

        public async Task AddUserToFile(User user)
        {
            try
            {
                var path = Directory.GetCurrentDirectory() + Constants.pathUsersFile;

                using (StreamWriter sw = File.AppendText(path))
                {
                    await sw.WriteLineAsync($"{user.Name},{user.Email},{user.Phone},{user.Address},{user.UserType},{user.Money}");
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private decimal GetUserMoney(User user) 
        {
            switch(user.UserType)
            {
                case Constants.normalType:
                    {
                        if (user.Money > 100)
                        {
                            var percentage = Convert.ToDecimal(0.12);
                            //If new user is normal and has more than USD100
                            var gif = user.Money * percentage;
                            user.Money += gif;

                        }
                        else if (user.Money < 100 && user.Money > 10)
                        {
                            var percentage = Convert.ToDecimal(0.8);
                            var gif = user.Money * percentage;
                            user.Money += gif;
                        }

                        break;
                    }

                case Constants.superType: 
                    {
                        if (user.Money > 100)
                        {
                            var percentage = Convert.ToDecimal(0.20);
                            var gif = user.Money * percentage;
                            user.Money += gif;
                        }

                        break;
                    }

                case Constants.premiumType:
                    {
                        if (user.Money > 100)
                        {
                            var gif = user.Money * 2;
                            user.Money += gif;
                        }

                        break;
                    }
            }
            return user.Money;
        }

        private string NormalizeEmail(string email)
        {
            try
            {
                string response = string.Empty;

                var aux = email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);
                var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);
                aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);
                response = string.Join("@", new string[] { aux[0], aux[1] });

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(Constants.exceptionEmail + ex.Message);
                throw;
            }            
        }

        private async Task<List<User>> GetUsersFromFile()
        {
            try
            {
                var path = Directory.GetCurrentDirectory() + Constants.pathUsersFile;
                var users = new List<User>();

                if (!File.Exists(path))
                {
                    throw new FileNotFoundException(Constants.pathFileNotExist);
                }

                using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true))
                using (var reader = new StreamReader(fileStream))
                {
                    while (reader.Peek() >= 0)
                    {
                        var line = await reader.ReadLineAsync();
                        var _user = new User
                        {
                            Name = line.Split(',')[0].ToString(),
                            Email = line.Split(',')[1].ToString(),
                            Phone = line.Split(',')[2].ToString(),
                            Address = line.Split(',')[3].ToString(),
                            UserType = line.Split(',')[4].ToString(),
                            Money = decimal.Parse(line.Split(',')[5].ToString()),
                        };
                        users.Add(_user);
                    }
                    reader.Close();
                }
                return users;
            }
            catch (Exception ex)
            {
                _logger.LogError(Constants.exceptionGetUsersFile + ex.Message);
                throw;
            }
        }

        private void ValidateUserInfo(User user, List<User> users)
        {
            try
            {
                var isDuplicated = false;
                foreach (var usuario in users)
                {
                    if (usuario.Email == user.Email || usuario.Phone == user.Phone || usuario.Address == user.Address)
                    {
                        isDuplicated = true;

                        _logger.LogError(Constants.userDuplicated);
                        throw new Exception(Constants.userDuplicated);
                    }
                }

                //if(!isDuplicated)
                //{
                //    response.IsSuccess = true;
                //    response.Errors = string.Empty;
                //}

                _logger.LogInformation(Constants.userInfoValidated);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error: " + ex.Message);
                throw;
            }
        }
    }
}
