namespace BusinessLogic
{
    public static class Constants
    {
        #region Información
        public const string normalType = "Normal";
        public const string superType = "SuperUser";
        public const string premiumType = "Premium";

        public const string userCreated = "User Created";

        public const string GetAllUsersSuccesfully = "The users were loaded succesfully.";
        public const string userInfoValidated = "The user info was validated successfully.";
        #endregion

        #region Errors
        public const string userDuplicated = "The user is duplicated.";
        public const string nameRequired = "The name is required.";
        public const string emailRequired = "The email is required.";
        public const string adressRequired = "The address is required.";
        public const string phoneRequired = "The phone is required.";

        public const string exceptionEmail = "An error ocurred when we try to normalize the user email: ";
        public const string exceptionGetUsersFile = "An error ocurred when we try to get users file.txt: ";
        public const string exceptionRegisterNewUser = "An error ocurred when we try to register a new user on file.txt";
        public const string exceptionWhenGetUserList = "An error ocurred when we try to get users list";

        public const string userIsDuplicated = "The user info has already exists with another user.";
        #endregion

        #region Paths
        public const string pathUsersFile = "./../Sat.Recruitment.Api/Files//Users.txt";
        public const string pathFileNotExist = "The file doesnt exist.";
        #endregion
    }
}
