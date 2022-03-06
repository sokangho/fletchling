import TwitterUser from './TwitterUser';

export interface CreateJwtResponse {
  jwt: string;
}

export interface SearchTwitterUserResponse {
  users: TwitterUser[];
}
