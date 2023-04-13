namespace Sat.Recruitment.Api
{
    public static class Constants
    {

        #region Información
        public const string normalType = "Normal";
        public const string superType = "SuperUser";
        public const string Premium = "Premium";

        public const string userCreated = "User Created";
        #endregion

        #region Errors
        public const string userDuplicated = "The user is duplicated.";
        public const string nameRequired = "The name is required.";
        public const string emailRequired = "The email is required.";
        public const string adressRequired = "The address is required.";
        public const string phoneRequired = "The phone is required.";
        #endregion

        #region Paths
        public const string pathUsersFile = "./../Sat.Recruitment.Api/Files//Users.txt";
        public const string pathFileNotExist = "The file doesnt exist.";
        #endregion
    }
}
