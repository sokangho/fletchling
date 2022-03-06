interface RequireAuth {
  jwt: string;
}

export interface CreateJwtRequest {
  twitterUserId: string;
  accessToken: string;
  refreshToken: string;
}

export interface SearchTwitterUserRequest extends RequireAuth {
  username: string;
}
