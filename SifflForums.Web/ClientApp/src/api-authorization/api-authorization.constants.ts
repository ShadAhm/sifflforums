export const ReturnUrlType = 'returnUrl';

export const QueryParameterNames = {
  ReturnUrl: ReturnUrlType
};

export const LogoutActions = {
  Logout: 'logout'
};

export const LoginActions = {
  Login: 'login',
  Register: 'register'
};

// Auth-related API endpoints; login/register/refresh come from ASP.NET Core Identity (MapIdentityApi)
export const AuthApiPaths = {
  Login: 'api/auth/login',
  Register: 'api/auth/register',
  Refresh: 'api/auth/refresh',
  CurrentUser: 'api/users/me'
};

let applicationPaths: ApplicationPathsType = {
  DefaultLoginRedirectPath: '/',
  Login: `authentication/${LoginActions.Login}`,
  Register: `authentication/${LoginActions.Register}`,
  LogOut: `authentication/${LogoutActions.Logout}`,
  LoginPathComponents: [],
  RegisterPathComponents: [],
  LogOutPathComponents: []
};

applicationPaths = {
  ...applicationPaths,
  LoginPathComponents: applicationPaths.Login.split('/'),
  RegisterPathComponents: applicationPaths.Register.split('/'),
  LogOutPathComponents: applicationPaths.LogOut.split('/')
};

interface ApplicationPathsType {
  readonly DefaultLoginRedirectPath: string;
  readonly Login: string;
  readonly Register: string;
  readonly LogOut: string;
  readonly LoginPathComponents: string [];
  readonly RegisterPathComponents: string [];
  readonly LogOutPathComponents: string [];
}

export const ApplicationPaths: ApplicationPathsType = applicationPaths;
