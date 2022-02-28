import NextAuth from 'next-auth';
import Providers from 'next-auth/providers';

import TwitterUser from '@/interfaces/TwitterUser';

export default NextAuth({
  providers: [
    Providers.Twitter({
      clientId: process.env.TWITTER_CLIENT_ID,
      clientSecret: process.env.TWITTER_CLIENT_SECRET
    })
  ],
  session: {
    jwt: true
  },
  jwt: {
    secret: process.env.NEXTAUTH_JWT_SECRET
  },
  callbacks: {
    // Put Twitter's access token in jwt
    async jwt(token, user, account, profile, isNewUser) {
      if (account?.accessToken) {
        token.accessToken = account.accessToken;
      }

      if (account?.refreshToken) {
        token.refreshToken = account.refreshToken;
      }

      if (profile) {
        const user: TwitterUser = {
          id: profile['id_str'] as string,
          username: profile['screen_name'] as string,
          displayName: profile['name'] as string,
          verified: profile.verified as boolean,
          profileUrl: '',
          profileImageUrl: profile['profile_image_url_https'] as string
        };
        token.user = user;
      }

      // console.log('token', JSON.stringify(token, null, 2));
      // console.log('user', JSON.stringify(user, null, 2));
      // console.log('account', JSON.stringify(account, null, 2));
      // console.log('profile', JSON.stringify(profile, null, 2));
      return token;
    },
    async session(session, token) {
      session.twitterUser = token.user;
      console.log(session);
      return session;
    }
  }
});
