import { AuthServiceConfig, GoogleLoginProvider } from 'angularx-social-login';

export function provideSocialLoginConfig() {
    const config = new AuthServiceConfig([
        {
          id: GoogleLoginProvider.PROVIDER_ID,
          provider: new GoogleLoginProvider('672744660702-i317c5qmsrs6l0stpub8fqoeuk4rhf8s.apps.googleusercontent.com')
        }
      ]);

    return config;
}