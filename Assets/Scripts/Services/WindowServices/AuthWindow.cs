public class AuthWindow : CustomWindow
{
    public void Auth()
    {
        var authService = ServiceLocator.Instance.GetService<AuthService>();
        if(authService == null)
        {
            return;
        }

        authService.Auth();
    }
}
