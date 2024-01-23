namespace Alumni.Infrastructure
{
    public class CurrentUserState
    {
        public int UserId { get; private set; }
        public string UserName { get; private set; }

        public void SetState(int userId, string userName)
        {
            UserId = userId;
            UserName = userName;
        }
    }
}
