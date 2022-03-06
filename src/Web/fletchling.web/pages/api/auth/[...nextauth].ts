import NextAuth from 'next-auth';
import Providers from 'next-auth/providers';
import apiService from 'services/apiService';

import { CreateJwtRequest } from '@/interfaces/ApiRequests';
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
    async signIn(user, account, profile) {
      if (account.provider === 'twitter') {
        console.log(user);
        const authenticatedUser: TwitterUser = {
          id: profile['id_str'] as string,
          username: profile['screen_name'] as string,
          displayName: profile['name'] as string,
          verified: profile.verified as boolean,
          profileUrl: '',
          profileImageUrl: profile['profile_image_url_https'] as string
        };
        user.details = authenticatedUser;

        const createJwtRequest: CreateJwtRequest = {
          twitterUserId: authenticatedUser.id,
          accessToken: account.accessToken,
          refreshToken: account.refreshToken as string
        };

        // Get jwt from backend api
        const jwt = await apiService.createJwt(createJwtRequest);
        user.jwt = jwt;

        return true;
      }

      return false;
    },

    async jwt(token, user, account, profile, isNewUser) {
      // user is only available on sign in
      // Get user details and jwt from sign in callback
      if (user) {
        token.authenticatedUser = user.details;
        token.jwt = user.jwt;
      }

      return token;
    },

    async session(session, token) {
      // Store authenticated user's details in session
      session.twitterUser = token.authenticatedUser;
      session.jwt = token.jwt;
      return session;
    }
  }
});
